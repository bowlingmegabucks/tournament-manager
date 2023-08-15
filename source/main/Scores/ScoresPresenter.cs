
namespace NortheastMegabuck.Scores;
internal class Presenter
{
    private readonly IView _view;

    private readonly LaneAssignments.Retrieve.IAdapter _retrieveLaneAssignmentsAdapter;
    private readonly Retrieve.IAdapter _retrieveSquadScoresAdapter;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _retrieveLaneAssignmentsAdapter = new LaneAssignments.Retrieve.Adapter(config);
        _retrieveSquadScoresAdapter = new Retrieve.Adapter(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRetrieveLaneAssignmentsAdapter"></param>
    /// <param name="mockRetrieveSquadScoresAdapter"></param>
    internal Presenter(IView mockView, LaneAssignments.Retrieve.IAdapter mockRetrieveLaneAssignmentsAdapter, Retrieve.IAdapter mockRetrieveSquadScoresAdapter)
    {
        _view = mockView;
        _retrieveLaneAssignmentsAdapter = mockRetrieveLaneAssignmentsAdapter;
        _retrieveSquadScoresAdapter = mockRetrieveSquadScoresAdapter;
    }

    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        var laneAssignments = (await _retrieveLaneAssignmentsAdapter.ExecuteAsync(_view.SquadId, cancellationToken).ConfigureAwait(false)).Where(assignment => !string.IsNullOrWhiteSpace(assignment.LaneAssignment)).Order().ToList();
        var squadScores = await _retrieveSquadScoresAdapter.ExecuteAsync(_view.SquadId, cancellationToken).ConfigureAwait(false);

        if (_retrieveLaneAssignmentsAdapter.Error != null)
        {
            _view.DisplayError(_retrieveLaneAssignmentsAdapter.Error.Message);
            _view.Disable();
        }
        else if (_retrieveSquadScoresAdapter.Error != null)
        {
            _view.DisplayError(_retrieveSquadScoresAdapter.Error.Message);
            _view.Disable();
        }
        else
        {            
            _view.BindLaneAssignments(laneAssignments);
            _view.BindSquadScores(squadScores);
        }
    }
}
