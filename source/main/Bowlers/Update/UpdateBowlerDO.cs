
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

    void IDataLayer.Execute(BowlerId id, string firstName, string middleInitial, string lastName, string suffix)
        => _repository.Update(id, firstName, middleInitial, lastName, suffix);
}

internal interface IDataLayer
{
    void Execute(BowlerId id, string firstName, string middleInitial, string lastName, string suffix);
}