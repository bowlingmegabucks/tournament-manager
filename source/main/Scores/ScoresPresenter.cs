
namespace NortheastMegabuck.Scores;
internal class Presenter
{
    private readonly IView _view;

    private readonly LaneAssignments.Retrieve.IAdapter _retrieveLaneAssignmentsAdapter;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _retrieveLaneAssignmentsAdapter = new LaneAssignments.Retrieve.Adapter(config);
    }

    internal Presenter(IView mockView, LaneAssignments.Retrieve.IAdapter mockRetrieveLaneAssignmentsAdapter)
    {
        _view = mockView;
        _retrieveLaneAssignmentsAdapter = mockRetrieveLaneAssignmentsAdapter;
    }

    public void LoadLaneAssignments()
    {
        var laneAssignments = _retrieveLaneAssignmentsAdapter.Execute(_view.SquadId).Where(assignment=> !string.IsNullOrWhiteSpace(assignment.LaneAssignment)).ToList();

        if (_retrieveLaneAssignmentsAdapter.Error != null)
        {
            _view.DisplayError(_retrieveLaneAssignmentsAdapter.Error.Message);
            _view.Disable();
        }
        else
        {
            laneAssignments.Sort();

            _view.BindLaneAssignments(laneAssignments);
        }
    }
}
