
namespace NortheastMegabuck.Registrations.Delete;

internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    internal DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
        => await _repository.DeleteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);

    public async Task ExecuteAsync(RegistrationId id, CancellationToken cancellationToken)
        => await _repository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
}

internal interface IDataLayer
{
    Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);

    Task ExecuteAsync(RegistrationId id, CancellationToken cancellationToken);
}