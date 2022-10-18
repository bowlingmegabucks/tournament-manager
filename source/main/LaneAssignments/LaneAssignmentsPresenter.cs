using NortheastMegabuck.Scores;

namespace NortheastMegabuck.LaneAssignments;
internal class Presenter
{
    private readonly IView _view;
    private readonly ILaneAvailability _laneAvailability;

    private readonly Lazy<Retrieve.IAdapter> _retrieveAdapter;
    private Retrieve.IAdapter RetrieveAdapter => _retrieveAdapter.Value;

    private readonly Lazy<Update.IAdapter> _updateAdapter;
    private Update.IAdapter UpdateAdapter => _updateAdapter.Value;

    private readonly Lazy<Registrations.Add.IAdapter> _addRegistrationAdapter;
    private Registrations.Add.IAdapter AddRegistrationAdapter => _addRegistrationAdapter.Value;

    private readonly IGenerateCrossFactory _generateCrossFactory;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;
        _laneAvailability = new LaneAvailability();

        _retrieveAdapter = new Lazy<Retrieve.IAdapter>(() => new Retrieve.Adapter(config));
        _updateAdapter = new Lazy<Update.IAdapter>(() => new Update.Adapter(config));
        _addRegistrationAdapter = new Lazy<Registrations.Add.IAdapter>(()=> new Registrations.Add.Adapter(config));

        _generateCrossFactory = new GenerateCrossFactory();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockLaneAvailability"></param>
    /// <param name="mockRetrieveAdapter"></param>
    /// <param name="mockUpdateAdapter"></param>
    internal Presenter(IView mockView, ILaneAvailability mockLaneAvailability, Retrieve.IAdapter mockRetrieveAdapter, Update.IAdapter mockUpdateAdapter, Registrations.Add.IAdapter mockAddRegistrationAdapter, IGenerateCrossFactory mockGenerateCrossFactory)
    {
        _view = mockView;
        _laneAvailability = mockLaneAvailability;
        _retrieveAdapter = new Lazy<Retrieve.IAdapter>(() => mockRetrieveAdapter);
        _updateAdapter = new Lazy<Update.IAdapter>(() => mockUpdateAdapter);
        _addRegistrationAdapter = new Lazy<Registrations.Add.IAdapter>(() => mockAddRegistrationAdapter);
        _generateCrossFactory = mockGenerateCrossFactory;
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

    public void Update(SquadId squadId, IViewModel registration, string position)
    {
        UpdateAdapter.Execute(squadId, registration.BowlerId, position);

        if (UpdateAdapter.Error != null)
        {
            _view.DisplayError(UpdateAdapter.Error.Message);

            return;
        }

        if (string.IsNullOrEmpty(position))
        {
            _view.RemoveLaneAssignment(registration);
        }
        else
        {
            _view.AssignToLane(registration, position);
        }
    }

    public void AddToRegistration()
    {
        var bowlerId = _view.SelectBowler(_view.TournamentId, _view.SquadId);

        if (bowlerId == null)
        {
            _view.DisplayMessage("Add to Registration Canceled");

            return;
        }

        var laneAssignment = AddRegistrationAdapter.Execute(bowlerId.Value, _view.SquadId);

        if (AddRegistrationAdapter.Errors.Any())
        {
            _view.DisplayError(string.Join(Environment.NewLine, AddRegistrationAdapter.Errors.Select(error => error.Message)));

            return;
        }

        _view.DisplayMessage($"{laneAssignment!.BowlerName} is ready to be assigned to a lane");
        _view.AddToUnassigned(laneAssignment);
    }

    public void NewRegistration()
    {
        var added = _view.NewRegistration(_view.TournamentId, _view.SquadId);

        if (!added)
        {
            _view.DisplayMessage("New Registration Canceled");

            return;
        }

        _view.ClearLanes();
        _view.ClearUnassigned();

        Load();
    }

    internal void GenerateRecaps(IEnumerable<IViewModel> assignments)
    {
        var lanesUsed = assignments.Select(assignment => assignment.LaneNumber()).Distinct().OrderBy(laneNumber => laneNumber).ToList();

        //this is to add other lane if only one lane on pair is being used
        var evenLanesUsed = lanesUsed.Where(lane => lane % 2 == 0);
        var oddLanesUsed = lanesUsed.Where(lane => lane % 2 == 1);

        var allLanes = new List<short>();

        foreach (var evenLane in evenLanesUsed)
        {
            allLanes.Add(evenLane);
            allLanes.Add((short)(evenLane - 1));
        }

        foreach (var oddLane in oddLanesUsed)
        {
            allLanes.Add(oddLane);
            allLanes.Add((short)(oddLane + 1));
        }

        lanesUsed = allLanes.Distinct().OrderBy(lane => lane).ToList();

        var crossGenerator = _generateCrossFactory.Execute(_view.StaggeredSkipSelected);

        var skip = crossGenerator.DetermineSkip(lanesUsed.Count);

        var recaps = assignments.Select(assignment => new RecapSheetViewModel(assignment, crossGenerator.Execute(assignment.LaneNumber(), assignment.LaneLetter(), _view.Games, lanesUsed, skip))).ToList();

        _view.GenerateRecaps(recaps);
    }
}
