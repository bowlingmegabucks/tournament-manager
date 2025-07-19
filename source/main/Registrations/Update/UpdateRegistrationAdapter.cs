using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.Registrations.Update;

internal sealed class Adapter : IAdapter
{
    private readonly IBusinessLogic _businessLogic;

    public IEnumerable<ErrorDetail> Errors
        => _businessLogic.Errors;
    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    async Task IAdapter.AddSuperSweeperAsync(RegistrationId id, CancellationToken cancellationToken)
        => await _businessLogic.AddSuperSweeperAsync(id, cancellationToken).ConfigureAwait(false);

    async Task IAdapter.ExecuteAsync(RegistrationId id, DivisionId divisionId, Gender? gender, int? average, string usbcId, DateOnly? dateOfBirth, CancellationToken cancellationToken)
        => await _businessLogic.ExecuteAsync(id, divisionId, gender, average, usbcId, dateOfBirth, cancellationToken).ConfigureAwait(false);

    async Task IAdapter.ExecuteAsync(RegistrationId id, int? average, CancellationToken cancellationToken)
        => await _businessLogic.ExecuteAsync(id, average, cancellationToken).ConfigureAwait(false);
}

internal interface IAdapter
{
    IEnumerable<ErrorDetail> Errors { get; }

    Task AddSuperSweeperAsync(RegistrationId id, CancellationToken cancellationToken);

    Task ExecuteAsync(RegistrationId id, DivisionId divisionId, Gender? gender, int? average, string usbcId, DateOnly? dateOfBirth, CancellationToken cancellationToken);

    Task ExecuteAsync(RegistrationId id, int? average, CancellationToken cancellationToken);
}