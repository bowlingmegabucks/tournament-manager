
namespace NortheastMegabuck.Scores.Update;
internal class DataLayer : IDataLayer
{
    private readonly IEntityMapper _entityMapper;
    private readonly IRepository _repository;

    public DataLayer(IConfiguration config)
    {
        _entityMapper = new EntityMapper();
        _repository = new Repository(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockEntityMapper"></param>
    /// <param name="mockRepository"></param>
    internal DataLayer(IEntityMapper mockEntityMapper, IRepository mockRepository)
    {
        _entityMapper = mockEntityMapper;
        _repository = mockRepository;
    }

    public void Execute(IEnumerable<Models.SquadScore> scores)
        => _repository.Update(scores.Select(_entityMapper.Execute).ToList());
}

internal interface IDataLayer
{
    void Execute(IEnumerable<Models.SquadScore> scores);
}