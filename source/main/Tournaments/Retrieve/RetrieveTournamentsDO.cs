namespace NewEnglandClassic.Tournaments.Retrieve;

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

    IEnumerable<Models.Tournament> IDataLayer.Execute()
        => _repository.RetrieveAll().Select(tournament => new Models.Tournament(tournament));
}

internal interface IDataLayer
{
    IEnumerable<Models.Tournament> Execute();
}