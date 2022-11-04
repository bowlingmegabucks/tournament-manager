
namespace NortheastMegabuck.Registrations.Delete;

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

    public void Execute(BowlerId bowlerId, SquadId squadId)
        => _repository.Delete(bowlerId, squadId);

    public void Execute(RegistrationId id)
        => _repository.Delete(id);
}

internal interface IDataLayer
{
    void Execute(BowlerId bowlerId, SquadId squadId);

    void Execute(RegistrationId id);
}