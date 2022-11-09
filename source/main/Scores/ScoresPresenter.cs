
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

    public void Load()
    {
        var laneAssignmentTask = Task.Run(() => _retrieveLaneAssignmentsAdapter.Execute(_view.SquadId).Where(assignment => !string.IsNullOrWhiteSpace(assignment.LaneAssignment)).ToList());
        var squadScoreTask = Task.Run(() => _retrieveSquadScoresAdapter.Execute(_view.SquadId).ToList());
        
        Task.WaitAll(laneAssignmentTask, squadScoreTask);

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
            var laneAssignments = laneAssignmentTask.Result.Order().ToList();
            
            _view.BindLaneAssignments(laneAssignments);

            var squadScores = squadScoreTask.Result;
            _view.BindSquadScores(squadScores);
        }
    }
}
