
namespace NortheastMegabuck.Tournaments.Results;

internal class BusinessLogic : IBusinessLogic
{
    public Models.ErrorDetail? Error { get; private set; }

    private readonly ICalculator _calculator;
    private readonly Squads.Results.IBusinessLogic _retrieveSquadResults;
    private readonly Tournaments.Retrieve.IBusinessLogic _retrieveTournament;

    public BusinessLogic(IConfiguration config)
    {
        _calculator = new Calculator();
        _retrieveSquadResults = new Squads.Results.BusinessLogic(config);
        _retrieveTournament = new Tournaments.Retrieve.BusinessLogic(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockCalculator"></param>
    /// <param name="mockRetrieveSquadResults"></param>
    /// <param name="mockRetrieveTournament"></param>
    internal BusinessLogic(ICalculator mockCalculator, Squads.Results.IBusinessLogic mockRetrieveSquadResults, Tournaments.Retrieve.IBusinessLogic mockRetrieveTournament)
    {
        _calculator = mockCalculator;
        _retrieveSquadResults = mockRetrieveSquadResults;
        _retrieveTournament = mockRetrieveTournament;
    }

    public IEnumerable<Models.TournamentResults> Execute(TournamentId id)
    {
        var tournament = _retrieveTournament.Execute(id);

        if (_retrieveTournament.Error != null)
        {
            Error = _retrieveTournament.Error;

            return Enumerable.Empty<Models.TournamentResults>();
        }

        var squadResults = _retrieveSquadResults.Execute(id);

        if (_retrieveSquadResults.Error != null)
        {
            Error = _retrieveSquadResults.Error;

            return Enumerable.Empty<Models.TournamentResults>();
        }

        return squadResults.Select(squadResult => new Models.TournamentResults(squadResult.Key, squadResult, _calculator.Execute(squadResult.Key.Id, squadResult, tournament!.FinalsRatio))).ToList();
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    IEnumerable<Models.TournamentResults> Execute(TournamentId tournamentId);
}