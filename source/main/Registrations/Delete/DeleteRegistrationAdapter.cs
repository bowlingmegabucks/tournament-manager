
namespace NortheastMegabuck.Registrations.Delete;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error
        => _businessLogic.ErrorDetail;

    private readonly IBusinessLogic _businessLogic;

    internal Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    public async Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
        => await _businessLogic.ExecuteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);

    public async Task ExecuteAsync(RegistrationId id, CancellationToken cancellationToken)
        => await _businessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);

    Task ExecuteAsync(RegistrationId id, CancellationToken cancellationToken);
}