namespace NewEnglandClassic.Sweepers.Retrieve;
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

    public IEnumerable<Models.Sweeper> ForTournament(Guid tournamentId)
        => _repository.ForTournament(tournamentId).Select(sweeper=> new Models.Sweeper(sweeper));
}

internal interface IDataLayer
{
    IEnumerable<Models.Sweeper> ForTournament(Guid tournamentId);
}