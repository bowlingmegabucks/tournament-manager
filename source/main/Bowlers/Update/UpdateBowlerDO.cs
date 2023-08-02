
namespace NortheastMegabuck.Bowlers.Update;
internal sealed class DataLayer : IDataLayer
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

    void IDataLayer.Execute(BowlerId id, Models.PersonName name)
        => _repository.Update(id, name.First, name.MiddleInitial, name.Last, name.Suffix);
}

internal interface IDataLayer
{
    void Execute(BowlerId id, Models.PersonName name);
}