using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Models;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;

namespace BowlingMegabucks.TournamentManager.Tournaments.Results;

/// <summary>
/// 
/// </summary>
internal class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public Models.ErrorDetail? ErrorDetail { get; private set; }

    private readonly ICalculator _calculator;
    private readonly Squads.Results.IBusinessLogic _retrieveSquadResults;
    private readonly IQueryHandler<GetRegistrationByIdQuery, Models.Tournament?> _retrieveTournament;

    public BusinessLogic(ICalculator calculator, Squads.Results.IBusinessLogic retrieveSquadResults, IQueryHandler<GetRegistrationByIdQuery, Models.Tournament?> retrieveTournament)
    {
        _calculator = calculator;
        _retrieveSquadResults = retrieveSquadResults;
        _retrieveTournament = retrieveTournament;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tournamentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Models.TournamentResults>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var tournamentResult = await _retrieveTournament.HandleAsync(new() { Id = tournamentId }, cancellationToken).ConfigureAwait(false);

        if (tournamentResult.IsError)
        {
            ErrorDetail = tournamentResult.FirstError.ToErrorDetail();

            return [];
        }

        var squadResults = await _retrieveSquadResults.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        if (_retrieveSquadResults.ErrorDetail != null)
        {
            ErrorDetail = _retrieveSquadResults.ErrorDetail;

            return [];
        }

        var tournament = tournamentResult.Value!;

        return squadResults.Select(squadResult => new Models.TournamentResults(squadResult.Key, squadResult, _calculator.Execute(squadResult.Key.Id, [.. squadResult], tournament!.FinalsRatio))).ToList();
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
    /// <param name="tournamentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Models.TournamentResults>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);
}