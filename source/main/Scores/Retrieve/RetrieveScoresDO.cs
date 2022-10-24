
namespace NortheastMegabuck.Scores.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;
    private readonly Squads.IHandicapCalculator _handicapCalculator;

    public DataLayer(IConfiguration config)
    {
        _repository = new Repository(config);
        _handicapCalculator = new Squads.HandicapCalculator();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockRepository"></param>
    /// <param name="mockHandicapCalculator"></param>
    internal DataLayer(IRepository mockRepository, Squads.IHandicapCalculator mockHandicapCalculator)
    {
        _repository = mockRepository;
        _handicapCalculator = mockHandicapCalculator;
    }

    public IEnumerable<Models.SquadScore> Execute(SquadId squadId)
        => _repository.Retrieve(squadId).Select(squadScore => new Models.SquadScore(squadScore, _handicapCalculator));

    public IEnumerable<Models.SquadScore> Execute(IEnumerable<SquadId> squadIds)
        => _repository.Retrieve(squadIds).Select(squadScore=> new Models.SquadScore(squadScore, _handicapCalculator));

    public IEnumerable<Models.SquadScore> SuperSweeper(TournamentId tournamentId)
        => _repository.SuperSweeper(tournamentId).Select(squadScore => new Models.SquadScore(squadScore, _handicapCalculator));
}

internal interface IDataLayer
{
    IEnumerable<Models.SquadScore> Execute(SquadId squadId);

    IEnumerable<Models.SquadScore> Execute(IEnumerable<SquadId> squadIds);

    IEnumerable<Models.SquadScore> SuperSweeper(TournamentId tournamentId);
}