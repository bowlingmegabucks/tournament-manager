
namespace NortheastMegabuck.Sweepers.Results;
internal class BusinessLogic : IBusinessLogic
{
    public Models.ErrorDetail? Error { get; private set; }

    private readonly Lazy<Retrieve.IBusinessLogic> _retrieveSweeper;
    private Retrieve.IBusinessLogic RetrieveSweeper => _retrieveSweeper.Value;

    private readonly Lazy<Tournaments.Retrieve.IBusinessLogic> _retrieveTournament;
    private Tournaments.Retrieve.IBusinessLogic RetrieveTournament => _retrieveTournament.Value;

    private readonly Scores.Retrieve.IBusinessLogic _retrieveScores;

    public BusinessLogic(IConfiguration config)
    {
        _retrieveSweeper = new Lazy<Retrieve.IBusinessLogic>(()=>  new Retrieve.BusinessLogic(config));
        _retrieveTournament = new Lazy<Tournaments.Retrieve.IBusinessLogic>(() => new Tournaments.Retrieve.BusinessLogic(config));
        _retrieveScores = new Scores.Retrieve.BusinessLogic(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockRetrieveSweeper"></param>
    /// <param name="mockRetrieveScores"></param>
    internal BusinessLogic(Retrieve.IBusinessLogic mockRetrieveSweeper, Tournaments.Retrieve.IBusinessLogic mockRetrieveTournament, Scores.Retrieve.IBusinessLogic mockRetrieveScores)
    {
        _retrieveSweeper = new Lazy<Retrieve.IBusinessLogic>(() => mockRetrieveSweeper);
        _retrieveTournament = new Lazy<Tournaments.Retrieve.IBusinessLogic>(() => mockRetrieveTournament);
        _retrieveScores = mockRetrieveScores;
    }

    public async Task<Models.SweeperResult?> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
    {
        var sweeper = await RetrieveSweeper.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        if (RetrieveSweeper.Error != null)
        {
            Error = RetrieveSweeper.Error;

            return null;
        }

        var scores = await _retrieveScores.ExecuteAsync(new[] { squadId }, cancellationToken).ConfigureAwait(false);

        return Execute(scores, sweeper!.CashRatio);
    }

    public async Task<Models.SweeperResult?> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var tournament = await RetrieveTournament.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        if (RetrieveTournament.Error != null)
        {
            Error = RetrieveTournament.Error;

            return null;
        }

        var superSweeperBowlers = await RetrieveSweeper.SuperSweeperBowlersAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        if (RetrieveSweeper.Error != null)
        {
            Error = RetrieveSweeper.Error;

            return null;
        }

        var scores = (await _retrieveScores.ExecuteAsync(tournament!.Sweepers.Select(sweeper=> sweeper.Id), cancellationToken).ConfigureAwait(false)).Where(score=> superSweeperBowlers.Contains(score.Bowler.Id));

        return Execute(scores, tournament!.SuperSweeperCashRatio);
    }

    private Models.SweeperResult? Execute(IEnumerable<Models.SquadScore> scores, decimal cashRatio)
    {
        if (_retrieveScores.Error != null)
        {
            Error = _retrieveScores.Error;

            return null;
        }

        if (!scores.Any())
        {
            Error = new Models.ErrorDetail("No scores entered for sweeper");

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

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    Task<Models.SweeperResult?> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);

    Task<Models.SweeperResult?> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);
}