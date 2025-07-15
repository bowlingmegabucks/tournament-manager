using NortheastMegabuck.Bowlers;

namespace NortheastMegabuck.Registrations.Add;

internal class Adapter : IAdapter
{
    private readonly Lazy<IBusinessLogic> _businessLogic;
    private IBusinessLogic BusinessLogic => _businessLogic.Value;

    public IEnumerable<Models.ErrorDetail> Errors
        => BusinessLogic.Errors;

    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => businessLogic);
    }

    public async Task<RegistrationId?> ExecuteAsync(Bowlers.IViewModel bowler, DivisionId divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average, CancellationToken cancellationToken)
        => await ExecuteAsync(new Models.Registration(bowler.ToModel(), divisionId, squads, sweepers, superSweeper, average), cancellationToken).ConfigureAwait(false);

    private async Task<RegistrationId?> ExecuteAsync(Models.Registration registration, CancellationToken cancellationToken)
        => await BusinessLogic.ExecuteAsync(registration, cancellationToken).ConfigureAwait(false);

    public async Task<LaneAssignments.IViewModel?> ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
    {
        var registration = await BusinessLogic.ExecuteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);

        return registration is not null ? new LaneAssignments.ViewModel(registration) : null;
    }
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task<RegistrationId?> ExecuteAsync(Bowlers.IViewModel bowler, DivisionId divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average, CancellationToken cancellationToken);

    Task<LaneAssignments.IViewModel?> ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);
}