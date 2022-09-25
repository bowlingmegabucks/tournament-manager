
namespace NortheastMegabuck.LaneAssignments.Update;
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

    void IDataLayer.Execute(SquadId squadId, BowlerId bowlerId, string position)
        => _repository.Update(squadId, bowlerId, position);
}

internal interface IDataLayer
{
    void Execute(SquadId squadId, BowlerId bowlerId, string position);
}