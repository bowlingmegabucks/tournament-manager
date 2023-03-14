namespace NortheastMegabuck.Sweepers.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;
    
    internal DataLayer(IConfiguration config)
    {
        _repository = new Repository(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockRepository"></param>
    internal DataLayer(IRepository mockRepository)
    {
        _repository = mockRepository;
    }

    public IEnumerable<Models.Sweeper> Execute(TournamentId tournamentId)
        => _repository.Retrieve(tournamentId).Select(sweeper=> new Models.Sweeper(sweeper));

    public Models.Sweeper Execute(SquadId id)
        => new(_repository.Retrieve(id));

    //todo: unit tests
    public IEnumerable<BowlerId> SuperSweeperBowlers(TournamentId id)
        => _repository.SuperSweeperBowlers(id);
}

internal interface IDataLayer
{
    IEnumerable<Models.Sweeper> Execute(TournamentId tournamentId);

    IEnumerable<BowlerId> SuperSweeperBowlers(TournamentId tournamentId);

    Models.Sweeper Execute(SquadId id);
}