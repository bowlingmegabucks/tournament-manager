
namespace NortheastMegabuck.Sweepers.Complete;
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

    public void Execute(SquadId id)
        => _repository.Complete(id);
}

internal interface IDataLayer
{
    void Execute(SquadId id);
}