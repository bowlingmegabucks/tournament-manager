namespace NortheastMegabuck.LaneAssignments;
internal class Presenter
{
    private readonly IView _view;
    private readonly ILaneAvailability _laneAvailability;

    private readonly Lazy<Retrieve.IAdapter> _retrieveAdapter;
    private Retrieve.IAdapter RetrieveAdapter => _retrieveAdapter.Value;
    public Presenter(IConfiguration config, IView view)
    {
        _view = view;
        _laneAvailability = new LaneAvailability();

        _retrieveAdapter = new Lazy<Retrieve.IAdapter>(() => new Retrieve.Adapter(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockLaneAvailability"></param>
    /// <param name="mockRetrieveAdapter"></param>
    internal Presenter(IView mockView, ILaneAvailability mockLaneAvailability, Retrieve.IAdapter mockRetrieveAdapter)
    {
        _view = mockView;
        _laneAvailability = mockLaneAvailability;
        _retrieveAdapter = new Lazy<Retrieve.IAdapter>(() => mockRetrieveAdapter);
    }

    public void Load()
    {
        try
        {
            var lanes = _laneAvailability.Generate(_view.StartingLane, _view.NumberOfLanes, _view.MaxPerPair);

            _view.BuildLanes(lanes);
        }
        catch (Exception ex)
        {
            _view.DisplayError(ex.Message);
            _view.Disable();

            return;
        }    

        var assignments = RetrieveAdapter.Execute(_view.SquadId);

        if (RetrieveAdapter.Error != null)
        {
            _view.DisplayError(RetrieveAdapter.Error.Message);
            _view.Disable();

            return;
        }

        _view.BindRegistrations(assignments.Where(assignment=> string.IsNullOrWhiteSpace(assignment.LaneAssignment)));
        _view.BindLaneAssignments(assignments.Where(assignment => !string.IsNullOrWhiteSpace(assignment.LaneAssignment)));
    }

    //public void Update(SquadId squadId, BowlerId bowlerId, string laneAssignment)
    //{ }
}
