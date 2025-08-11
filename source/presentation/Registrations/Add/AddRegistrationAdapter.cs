using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Bowlers;
using BowlingMegabucks.TournamentManager.Models;
using BowlingMegabucks.TournamentManager.Registrations.Create;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Registrations.Add;

internal class Adapter : IAdapter
{
    private readonly Lazy<IBusinessLogic> _businessLogic;
    private IBusinessLogic BusinessLogic => _businessLogic.Value;

    private readonly ICommandHandler<CreateRegistrationCommand, RegistrationId> _createRegistrationCommandHandler;

    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    public Adapter(ICommandHandler<CreateRegistrationCommand, RegistrationId> createRegistrationCommandHandler, IBusinessLogic businessLogic)
    {
        _createRegistrationCommandHandler = createRegistrationCommandHandler;
        _businessLogic = new Lazy<IBusinessLogic>(() => businessLogic);
    }

    public async Task<RegistrationId?> ExecuteAsync(IViewModel bowler, TournamentId tournamentId, DivisionId divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average, CancellationToken cancellationToken)
    {
        var command = new CreateRegistrationCommand
        {
            Bowler = bowler.ToModel(),
            TournamentId = tournamentId,
            DivisionId = divisionId,
            Squads = squads,
            Sweepers = sweepers,
            SuperSweeper = superSweeper,
            Average = average,
            Payment = new Models.Payment
            {
                Amount = 0,
                ConfirmationCode = $"Manual_{Guid.CreateVersion7()}"
            }
        };

        var result = await _createRegistrationCommandHandler.HandleAsync(command, cancellationToken);

        if (result.IsError)
        {
            Errors = result.Errors.ToErrorDetails();

            return null;
        }

        return result.Value;
    }

    public async Task<LaneAssignments.IViewModel?> ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
    {
        var registration = await BusinessLogic.ExecuteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);

        Errors = BusinessLogic.Errors;

        return registration is not null ? new LaneAssignments.ViewModel(registration) : null;
    }
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task<RegistrationId?> ExecuteAsync(Bowlers.IViewModel bowler, TournamentId tournamentId, DivisionId divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average, CancellationToken cancellationToken);

    Task<LaneAssignments.IViewModel?> ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);
}