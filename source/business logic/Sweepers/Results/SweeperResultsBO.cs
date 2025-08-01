using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Models;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;

namespace BowlingMegabucks.TournamentManager.Sweepers.Results;

/// <summary>
/// 
/// </summary>
internal class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public Models.ErrorDetail? ErrorDetail { get; private set; }

    private readonly Lazy<Retrieve.IBusinessLogic> _retrieveSweeper;
    private Retrieve.IBusinessLogic RetrieveSweeper => _retrieveSweeper.Value;

    private readonly Lazy<IQueryHandler<GetTournamentByIdQuery, Models.Tournament?>> _retrieveTournament;
    private IQueryHandler<GetTournamentByIdQuery, Models.Tournament?> RetrieveTournament => _retrieveTournament.Value;

    private readonly Scores.Retrieve.IBusinessLogic _retrieveScores;

    public BusinessLogic(Retrieve.IBusinessLogic retrieveSweeper, IQueryHandler<GetTournamentByIdQuery, Models.Tournament?> retrieveTournament, Scores.Retrieve.IBusinessLogic retrieveScores)
    {
        _retrieveSweeper = new Lazy<Retrieve.IBusinessLogic>(() => retrieveSweeper);
        _retrieveTournament = new Lazy<IQueryHandler<GetTournamentByIdQuery, Models.Tournament?>>(() => retrieveTournament);
        _retrieveScores = retrieveScores;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="squadId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Models.SweeperResult?> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
    {
        var sweeper = await RetrieveSweeper.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        if (RetrieveSweeper.ErrorDetail != null)
        {
            ErrorDetail = RetrieveSweeper.ErrorDetail;

            return null;
        }

        var scores = await _retrieveScores.ExecuteAsync([squadId], cancellationToken).ConfigureAwait(false);

        return Execute(scores, sweeper!.CashRatio);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tournamentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Models.SweeperResult?> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var tournamentResult = await RetrieveTournament.HandleAsync(new() { Id = tournamentId }, cancellationToken).ConfigureAwait(false);

        if (tournamentResult.IsError)
        {
            ErrorDetail = tournamentResult.FirstError.ToErrorDetail();

            return null;
        }

        var superSweeperBowlers = await RetrieveSweeper.SuperSweeperBowlersAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        if (RetrieveSweeper.ErrorDetail != null)
        {
            ErrorDetail = RetrieveSweeper.ErrorDetail;

            return null;
        }

        var tournament = tournamentResult.Value!;

        var scores = (await _retrieveScores.ExecuteAsync(tournament.Sweepers.Select(sweeper => sweeper.Id), cancellationToken).ConfigureAwait(false)).Where(score => superSweeperBowlers.Contains(score.Bowler.Id));

        return Execute(scores, tournament!.SuperSweeperCashRatio);
    }

    private Models.SweeperResult? Execute(IEnumerable<Models.SquadScore> scores, decimal cashRatio)
    {
        if (_retrieveScores.ErrorDetail != null)
        {
            ErrorDetail = _retrieveScores.ErrorDetail;

            return null;
        }

        if (!scores.Any())
        {
            ErrorDetail = new Models.ErrorDetail("No scores entered for sweeper");

            return null;
        }

        var bowlerScores = scores.GroupBy(score => score.Bowler).Select(bowlerScore => new Models.BowlerSquadScore(bowlerScore)).Order().ToList();

        var casherCount = Math.Max(Convert.ToInt16(Math.Floor(bowlerScores.Count / cashRatio)), (short)1);

        return new Models.SweeperResult
        {
            Scores = bowlerScores,
            CasherCount = casherCount,
            CutScore = bowlerScores[casherCount - 1].Score
        };
    }
}

/// <summary>
/// 
/// </summary>
public interface IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    Models.ErrorDetail? ErrorDetail { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="squadId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Models.SweeperResult?> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tournamentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Models.SweeperResult?> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);
}