
using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.Registrations.Update;

internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    public DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(RegistrationId id, bool superSweeper, CancellationToken cancellationToken)
        => await _repository.UpdateAsync(id, superSweeper, cancellationToken).ConfigureAwait(false);

    public async Task ExecuteAsync(RegistrationId id, DivisionId divisionId, Gender? gender, int? average, string? usbcId, DateOnly? dateOfBirth, CancellationToken cancellationToken)
        => await _repository.UpdateAsync(id, divisionId, gender, average, usbcId, dateOfBirth, cancellationToken).ConfigureAwait(false);

    public async Task ExecuteAsync(RegistrationId id, int? average, CancellationToken cancellationToken)
        => await _repository.UpdateAsync(id, average, cancellationToken).ConfigureAwait(false);
}

internal interface IDataLayer
{
    Task ExecuteAsync(RegistrationId id, bool superSweeper, CancellationToken cancellationToken);

    Task ExecuteAsync(RegistrationId id, DivisionId divisionId, Gender? gender, int? average, string? usbcId, DateOnly? dateOfBirth, CancellationToken cancellationToken);

    Task ExecuteAsync(RegistrationId id, int? average, CancellationToken cancellationToken);
}