namespace NewEnglandClassic.Squads.Retrieve;
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

    public IEnumerable<Models.Squad> ForTournament(Guid tournamentId)
        => _repository.ForTournament(tournamentId).Select(division=> new Models.Squad(division));
}

internal interface IDataLayer
{
    IEnumerable<Models.Squad> ForTournament(Guid tournamentId);
}