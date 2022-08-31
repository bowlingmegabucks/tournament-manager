namespace NortheastMegabuck.Squads.Retrieve;
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

    public IEnumerable<Models.Squad> Execute(TournamentId tournamentId)
        => _repository.Retrieve(tournamentId).Select(squad=> new Models.Squad(squad));

    public Models.Squad Execute(SquadId id)
        => new (_repository.Retrieve(id));
}

internal interface IDataLayer
{
    IEnumerable<Models.Squad> Execute(TournamentId tournamentId);

    Models.Squad Execute(SquadId id);
}