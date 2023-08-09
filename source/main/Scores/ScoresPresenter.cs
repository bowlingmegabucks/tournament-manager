
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

    public void Load(CancellationToken cancellationToken)
    {
        var laneAssignmentTask = _retrieveLaneAssignmentsAdapter.ExecuteAsync(_view.SquadId, cancellationToken);
        var squadScoreTask = Task.Run(() => _retrieveSquadScoresAdapter.Execute(_view.SquadId).ToList());
        
        Task.WaitAll(new Task[] { laneAssignmentTask, squadScoreTask }, cancellationToken: cancellationToken);

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
            var laneAssignments = laneAssignmentTask.Result.Where(assignment => !string.IsNullOrWhiteSpace(assignment.LaneAssignment)).Order().ToList();
            
            _view.BindLaneAssignments(laneAssignments);

            var squadScores = squadScoreTask.Result;
            _view.BindSquadScores(squadScores);
        }
    }
}
