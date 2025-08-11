using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Scores;

/// <summary>
/// Handles the presentation logic for loading and displaying squad scores and lane assignments.
/// </summary>
public class Presenter
{
    private readonly IView _view;

    private readonly LaneAssignments.Retrieve.IAdapter _retrieveLaneAssignmentsAdapter;
    private readonly Retrieve.IAdapter _retrieveSquadScoresAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class.
    /// </summary>
    /// <param name="view">The view interface for displaying scores.</param>
    /// <param name="services">The service provider for dependency injection.</param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;
        _retrieveLaneAssignmentsAdapter = services.GetRequiredService<LaneAssignments.Retrieve.IAdapter>();
        _retrieveSquadScoresAdapter = services.GetRequiredService<Retrieve.IAdapter>();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView">The mock view for testing.</param>
    /// <param name="mockRetrieveLaneAssignmentsAdapter">The mock lane assignments adapter for testing.</param>
    /// <param name="mockRetrieveSquadScoresAdapter">The mock squad scores adapter for testing.</param>
    internal Presenter(IView mockView, LaneAssignments.Retrieve.IAdapter mockRetrieveLaneAssignmentsAdapter, Retrieve.IAdapter mockRetrieveSquadScoresAdapter)
    {
        _view = mockView;
        _retrieveLaneAssignmentsAdapter = mockRetrieveLaneAssignmentsAdapter;
        _retrieveSquadScoresAdapter = mockRetrieveSquadScoresAdapter;
    }

    /// <summary>
    /// Loads lane assignments and squad scores asynchronously and updates the view.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves lane assignments and squad scores, handles errors, and binds the results to the view.
    /// </remarks>
    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        var laneAssignments = (await _retrieveLaneAssignmentsAdapter.ExecuteAsync(_view.SquadId, cancellationToken).ConfigureAwait(true)).Where(assignment => !string.IsNullOrWhiteSpace(assignment.LaneAssignment)).Order().ToList();
        var squadScores = await _retrieveSquadScoresAdapter.ExecuteAsync(_view.SquadId, cancellationToken).ConfigureAwait(true);

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
