
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Models;
using BowlingMegabucks.TournamentManager.Registrations.GetRegistrationById;

namespace BowlingMegabucks.TournamentManager.Registrations.Retrieve;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error { get; private set; }

    private readonly IBusinessLogic _businessLogic;

    private readonly IQueryHandler<GetRegistrationByIdQuery, Models.Registration?> _getRegistrationByIdQueryHandler;

    public Adapter(IBusinessLogic businessLogic, 
                   IQueryHandler<GetRegistrationByIdQuery, Models.Registration?> getRegistrationByIdQueryHandler)
    {
        _businessLogic = businessLogic;
        _getRegistrationByIdQueryHandler = getRegistrationByIdQueryHandler;
    }

    async Task<IEnumerable<ITournamentRegistrationViewModel>> IAdapter.ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var registrations = await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        Error = _businessLogic.ErrorDetail;

        return registrations.Select(registration => new TournamentRegistrationViewModel(registration));
    }

    async Task<ITournamentRegistrationViewModel?> IAdapter.ExecuteAsync(RegistrationId id, CancellationToken cancellationToken)
    {
        var registrationResult = await _getRegistrationByIdQueryHandler.HandleAsync(new() { Id = id }, cancellationToken);

        if (registrationResult.IsError)
        {
            Error = registrationResult.FirstError.ToErrorDetail();

            return null;
        }

        return new TournamentRegistrationViewModel(registrationResult.Value!);
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<ITournamentRegistrationViewModel>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Task<ITournamentRegistrationViewModel?> ExecuteAsync(RegistrationId id, CancellationToken cancellationToken);
}