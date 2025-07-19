using Microsoft.Extensions.DependencyInjection;
using BowlingMegabucks.TournamentManager.Scores;

namespace BowlingMegabucks.TournamentManager.LaneAssignments;
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

    private readonly Lazy<Registrations.Delete.IAdapter> _deleteAdapter;
    private Registrations.Delete.IAdapter DeleteAdapter => _deleteAdapter.Value;

    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;
        _laneAvailability = services.GetRequiredService<ILaneAvailability>();

        _retrieveAdapter = new Lazy<Retrieve.IAdapter>(services.GetRequiredService<Retrieve.IAdapter>);
        _updateAdapter = new Lazy<Update.IAdapter>(services.GetRequiredService<Update.IAdapter>);
        _addRegistrationAdapter = new Lazy<Registrations.Add.IAdapter>(services.GetRequiredService<Registrations.Add.IAdapter>);
        _deleteAdapter = new Lazy<Registrations.Delete.IAdapter>(services.GetRequiredService<Registrations.Delete.IAdapter>);

        _generateCrossFactory = services.GetRequiredService<IGenerateCrossFactory>();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockLaneAvailability"></param>
    /// <param name="mockRetrieveAdapter"></param>
    /// <param name="mockUpdateAdapter"></param>
    /// <param name="mockAddRegistrationAdapter"></param>
    /// <param name="mockGenerateCrossFactory"></param>
    /// <param name="mockDeleteAdapter"></param>
    internal Presenter(IView mockView, ILaneAvailability mockLaneAvailability, Retrieve.IAdapter mockRetrieveAdapter, Update.IAdapter mockUpdateAdapter, Registrations.Add.IAdapter mockAddRegistrationAdapter, IGenerateCrossFactory mockGenerateCrossFactory, Registrations.Delete.IAdapter mockDeleteAdapter)
    {
        _view = mockView;
        _laneAvailability = mockLaneAvailability;
        _retrieveAdapter = new Lazy<Retrieve.IAdapter>(() => mockRetrieveAdapter);
        _updateAdapter = new Lazy<Update.IAdapter>(() => mockUpdateAdapter);
        _addRegistrationAdapter = new Lazy<Registrations.Add.IAdapter>(() => mockAddRegistrationAdapter);
        _generateCrossFactory = mockGenerateCrossFactory;
        _deleteAdapter = new Lazy<Registrations.Delete.IAdapter>(() => mockDeleteAdapter);
    }

    public async Task LoadAsync(CancellationToken cancellationToken)
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

        var assignments = await RetrieveAdapter.ExecuteAsync(_view.SquadId, cancellationToken).ConfigureAwait(true);

        if (RetrieveAdapter.Error != null)
        {
            _view.DisplayError(RetrieveAdapter.Error.Message);
            _view.Disable();

            return;
        }

        _view.BindRegistrations(assignments.Where(assignment => string.IsNullOrWhiteSpace(assignment.LaneAssignment)));
        _view.BindLaneAssignments(assignments.Where(assignment => !string.IsNullOrWhiteSpace(assignment.LaneAssignment)));

        var entriesPerDivision = assignments.GroupBy(assignment => assignment.DivisionName).ToDictionary(group => group.Key, group => group.Count());
        _view.BindEntriesPerDivision(entriesPerDivision);
    }

    public async Task UpdateAsync(SquadId squadId, IViewModel registration, string updatedPosition, CancellationToken cancellationToken)
    {
        await UpdateAdapter.ExecuteAsync(squadId, registration.BowlerId, registration.LaneAssignment, updatedPosition, cancellationToken).ConfigureAwait(true);

        if (UpdateAdapter.Error != null)
        {
            _view.DisplayError(UpdateAdapter.Error.Message);
            _view.ClearHighlights();

            return;
        }

        if (string.IsNullOrEmpty(updatedPosition))
        {
            _view.RemoveLaneAssignment(registration);
        }
        else
        {
            _view.AssignToLane(registration, updatedPosition);
        }
    }

    public async Task AddToRegistrationAsync(CancellationToken cancellationToken)
    {
        var bowlerId = _view.SelectBowler(_view.TournamentId, _view.SquadId);

        if (bowlerId == null)
        {
            _view.DisplayMessage("Add to Registration Canceled");

            return;
        }

        var laneAssignment = await AddRegistrationAdapter.ExecuteAsync(bowlerId.Value, _view.SquadId, cancellationToken).ConfigureAwait(true);

        if (AddRegistrationAdapter.Errors.Any())
        {
            _view.DisplayError(string.Join(Environment.NewLine, AddRegistrationAdapter.Errors.Select(error => error.Message)));

            return;
        }

        _view.DisplayMessage($"{laneAssignment!.BowlerName} is ready to be assigned to a lane");
        _view.AddToUnassigned(laneAssignment);
    }

    public async Task NewRegistrationAsync(CancellationToken cancellationToken)
    {
        var added = _view.NewRegistration(_view.TournamentId, _view.SquadId);

        if (!added)
        {
            _view.DisplayMessage("New Registration Canceled");

            return;
        }

        _view.ClearLanes();
        _view.ClearUnassigned();

        await LoadAsync(cancellationToken).ConfigureAwait(true);
    }

    internal void GenerateRecaps(IEnumerable<IViewModel> assignments)
    {
        var lanesUsed = assignments.Select(assignment => assignment.LaneNumber()).Distinct().Order().ToList();

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

        lanesUsed = [.. allLanes.Distinct().Order()];

        var crossGenerator = _generateCrossFactory.Execute(_view.StaggeredSkipSelected);

        var skip = crossGenerator.DetermineSkip(lanesUsed.Count);

        var recaps = assignments.Select(assignment => new RecapSheetViewModel(assignment, crossGenerator.Execute(assignment.LaneNumber(), assignment.LaneLetter(), _view.Games, lanesUsed, skip))).ToList();

        _view.GenerateRecaps(recaps);
    }

    public async Task DeleteAsync(BowlerId bowlerId, CancellationToken cancellationToken)
    {
        if (!_view.Confirm("Are you sure you want remove bowler from this squad (Refund may be required)?"))
        {
            return;
        }

        await DeleteAdapter.ExecuteAsync(bowlerId, _view.SquadId, cancellationToken).ConfigureAwait(true);

        if (DeleteAdapter.Error != null)
        {
            _view.DisplayError(DeleteAdapter.Error.Message);
        }
        else
        {
            _view.DeleteRegistration(bowlerId);
        }
    }
}
