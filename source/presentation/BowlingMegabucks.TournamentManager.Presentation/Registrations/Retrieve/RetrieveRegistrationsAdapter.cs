﻿
namespace BowlingMegabucks.TournamentManager.Registrations.Retrieve;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error
        => _businessLogic.ErrorDetail;

    private readonly IBusinessLogic _businessLogic;

    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    async Task<IEnumerable<ITournamentRegistrationViewModel>> IAdapter.ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var registrations = await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        return registrations.Select(registration => new TournamentRegistrationViewModel(registration));
    }

    async Task<ITournamentRegistrationViewModel?> IAdapter.ExecuteAsync(RegistrationId id, CancellationToken cancellationToken)
    {
        var registration = await _businessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        return registration is not null ? new TournamentRegistrationViewModel(registration) : null;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<ITournamentRegistrationViewModel>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Task<ITournamentRegistrationViewModel?> ExecuteAsync(RegistrationId id, CancellationToken cancellationToken);
}