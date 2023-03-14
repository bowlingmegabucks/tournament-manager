
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

    public IEnumerable<Models.TournamentFinalsSeeding> Execute(TournamentId id)
        => _tournamentResults.Execute(id).Select(_calculator.Execute).ToList();
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    IEnumerable<Models.TournamentFinalsSeeding> Execute(TournamentId id);
}

