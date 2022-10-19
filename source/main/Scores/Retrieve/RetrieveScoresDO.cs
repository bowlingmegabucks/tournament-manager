
namespace NortheastMegabuck.Scores.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    public DataLayer(IConfiguration config)
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

    public IEnumerable<Models.SquadScore> Execute(SquadId squadId)
        => _repository.Retrieve(squadId).Select(squadScore => new Models.SquadScore(squadScore));
}

internal interface IDataLayer
{
    IEnumerable<Models.SquadScore> Execute(SquadId squadId);
}