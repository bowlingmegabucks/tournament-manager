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

    public RegistrationId? Execute(Bowlers.Add.IViewModel bowler, DivisionId divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average)
        => Execute(new Models.Registration(new Models.Bowler(bowler), divisionId, squads, sweepers, superSweeper, average));

    private RegistrationId? Execute(Models.Registration registration)
    {
        var id = BusinessLogic.Execute(registration);

        return id;
    }

    public LaneAssignments.IViewModel? Execute(BowlerId bowlerId, SquadId squadId)
    {
        var registration = BusinessLogic.Execute(bowlerId, squadId);

        return registration is not null ? new LaneAssignments.ViewModel(registration) : null;
    }
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    RegistrationId? Execute(Bowlers.Add.IViewModel bowler, DivisionId divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average);

    LaneAssignments.IViewModel? Execute(BowlerId bowlerId, SquadId squadId);
}