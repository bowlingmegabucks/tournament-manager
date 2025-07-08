
using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.Tournaments.Seeding;

internal class BusinessLogic : IBusinessLogic
{
    public Models.ErrorDetail? Error
        => _tournamentResults.Error;

    private readonly Results.IBusinessLogic _tournamentResults;
    private readonly ICalculator _calculator;

    public BusinessLogic(IConfiguration config)
    {
        _tournamentResults = new Results.BusinessLogic(config);
        _calculator = new Calculator();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockTournamentResults"></param>
    /// <param name="mockCalculator"></param>
    internal BusinessLogic(Results.IBusinessLogic mockTournamentResults, ICalculator mockCalculator)
    {
        _tournamentResults = mockTournamentResults;
        _calculator = mockCalculator;
    }

    public async Task<IEnumerable<Models.TournamentFinalsSeeding>> ExecuteAsync(TournamentId id, CancellationToken cancellationToken)
        => (await _tournamentResults.ExecuteAsync(id, cancellationToken).ConfigureAwait(false)).Select(_calculator.Execute).ToList();
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<Models.TournamentFinalsSeeding>> ExecuteAsync(TournamentId id, CancellationToken cancellationToken);
}

