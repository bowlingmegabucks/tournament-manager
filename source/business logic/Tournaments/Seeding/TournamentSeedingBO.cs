namespace NortheastMegabuck.Tournaments.Seeding;

/// <summary>
/// 
/// </summary>
public class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public Models.ErrorDetail? ErrorDetail
        => _tournamentResults.ErrorDetail;

    private readonly Results.IBusinessLogic _tournamentResults;
    private readonly ICalculator _calculator;

    internal BusinessLogic(Results.IBusinessLogic tournamentResults, ICalculator calculator)
    {
        _tournamentResults = tournamentResults;
        _calculator = calculator;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Models.TournamentFinalsSeeding>> ExecuteAsync(TournamentId id, CancellationToken cancellationToken)
        => (await _tournamentResults.ExecuteAsync(id, cancellationToken).ConfigureAwait(false)).Select(_calculator.Execute).ToList();
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
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Models.TournamentFinalsSeeding>> ExecuteAsync(TournamentId id, CancellationToken cancellationToken);
}

