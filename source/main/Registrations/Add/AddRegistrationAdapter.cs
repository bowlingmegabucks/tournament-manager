namespace NortheastMegabuck.Registrations.Add;

internal class Adapter : IAdapter
{
    private readonly Lazy<IBusinessLogic> _businessLogic;
    private IBusinessLogic BusinessLogic => _businessLogic.Value;

    public IEnumerable<Models.ErrorDetail> Errors
        => BusinessLogic.Errors;

    internal Adapter(IConfiguration config)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => new BusinessLogic(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockBusinessLogic"></param>
    internal Adapter(IBusinessLogic mockBusinessLogic)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => mockBusinessLogic);
    }

    public async Task<RegistrationId?> ExecuteAsync(Bowlers.Add.IViewModel bowler, DivisionId divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average, CancellationToken cancellationToken)
        => await ExecuteAsync(new Models.Registration(new Models.Bowler(bowler), divisionId, squads, sweepers, superSweeper, average), cancellationToken).ConfigureAwait(false);

    private async Task<RegistrationId?> ExecuteAsync(Models.Registration registration, CancellationToken cancellationToken)
        => await BusinessLogic.ExecuteAsync(registration, cancellationToken).ConfigureAwait(false);

    public LaneAssignments.IViewModel? Execute(BowlerId bowlerId, SquadId squadId)
    {
        var registration = BusinessLogic.Execute(bowlerId, squadId);

        return registration is not null ? new LaneAssignments.ViewModel(registration) : null;
    }
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task<RegistrationId?> ExecuteAsync(Bowlers.Add.IViewModel bowler, DivisionId divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average, CancellationToken cancellationToken);

    LaneAssignments.IViewModel? Execute(BowlerId bowlerId, SquadId squadId);
}