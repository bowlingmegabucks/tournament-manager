namespace NortheastMegabuck.Divisions.Retrieve;
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

    public IEnumerable<Models.Division> Execute(TournamentId tournamentId)
        => _repository.Retrieve(tournamentId).Select(division=> new Models.Division(division));

    public Models.Division? Execute(NortheastMegabuck.DivisionId id)
        => new(_repository.Retrieve(id));
}

internal interface IDataLayer
{
    IEnumerable<Models.Division> Execute(TournamentId tournamentId);

    Models.Division? Execute(NortheastMegabuck.DivisionId id);
}