namespace NortheastMegabuck.Tournaments.Results;

/// <summary>
/// 
/// </summary>
public class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public Models.ErrorDetail? ErrorDetail { get; private set; }

    private readonly ICalculator _calculator;
    private readonly Squads.Results.IBusinessLogic _retrieveSquadResults;
    private readonly Retrieve.IBusinessLogic _retrieveTournament;

    internal BusinessLogic(ICalculator calculator, Squads.Results.IBusinessLogic retrieveSquadResults, Retrieve.IBusinessLogic retrieveTournament)
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
        var tournament = await _retrieveTournament.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        if (_retrieveTournament.ErrorDetail != null)
        {
            ErrorDetail = _retrieveTournament.ErrorDetail;

            return [];
        }

        var squadResults = await _retrieveSquadResults.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        if (_retrieveSquadResults.ErrorDetail != null)
        {
            ErrorDetail = _retrieveSquadResults.ErrorDetail;

            return [];
        }

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