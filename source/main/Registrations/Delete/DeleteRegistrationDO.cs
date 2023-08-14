
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

    public async Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
        => await _repository.DeleteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);

    public void Execute(RegistrationId id)
        => _repository.Delete(id);
}

internal interface IDataLayer
{
    Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);

    void Execute(RegistrationId id);
}