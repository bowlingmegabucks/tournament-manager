namespace NewEnglandClassic.Divisions.Retrieve;
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

    public IEnumerable<Models.Division> ForTournament(Guid tournamentId)
        => _repository.ForTournament(tournamentId).Select(division=> new Models.Division(division));

    public Models.Division? Execute(Guid divisionId)
        => new(_repository.Retrieve(divisionId));
}

internal interface IDataLayer
{
    IEnumerable<Models.Division> ForTournament(Guid tournamentId);

    Models.Division? Execute(Guid divisionId);
}