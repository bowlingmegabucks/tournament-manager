
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Models;
using BowlingMegabucks.TournamentManager.Registrations.DeleteRegistration;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Registrations.Delete;
internal class Adapter : IAdapter
{
    public ErrorDetail? Error { get; private set; }

    private readonly IBusinessLogic _businessLogic;
    private readonly ICommandHandler<DeleteRegistrationCommand, Deleted> _commandHandler;

    public Adapter(IBusinessLogic businessLogic, ICommandHandler<DeleteRegistrationCommand, Deleted> commandHandler)
    {
        _businessLogic = businessLogic;
        _commandHandler = commandHandler;
    }

    public async Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
    {
        await _businessLogic.ExecuteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);
        
        Error = _businessLogic.ErrorDetail;
    }

    public async Task ExecuteAsync(RegistrationId id, CancellationToken cancellationToken)
    {
        var result = await _commandHandler.HandleAsync(new DeleteRegistrationCommand { Id = id }, cancellationToken);

        if (result.IsError)
        {
            Error = result.FirstError.ToErrorDetail();
        }
    }
}

internal interface IAdapter
{
    ErrorDetail? Error { get; }

    Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);

    Task ExecuteAsync(RegistrationId id, CancellationToken cancellationToken);
}