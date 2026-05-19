using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Squads.Results;

/// <summary>
/// Handles the presentation logic for displaying squad results, including error handling and result binding.
/// </summary>
public class Presenter
{
    private readonly IView _view;
    private readonly IAdapter _adapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class.
    /// </summary>
    /// <param name="view">The view interface for displaying squad results.</param>
    /// <param name="services">The service provider for dependency injection.</param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;
        _adapter = services.GetRequiredService<IAdapter>();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView">The mock view for testing.</param>
    /// <param name="mockAdapter">The mock adapter for testing.</param>
    internal Presenter(IView mockView, IAdapter mockAdapter)
    {
        _view = mockView;
        _adapter = mockAdapter;
    }

    /// <summary>
    /// Executes the process of retrieving and displaying squad results asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves results by division, handles errors, and binds the results to the view.
    /// </remarks>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var scoresByDivision = (await _adapter.ExecuteAsync(_view.SquadId, cancellationToken).ConfigureAwait(true)).OrderBy(score => score.Key);

        if (_adapter.Error != null)
        {
            _view.DisplayError(_adapter.Error.Message);
            return;
        }

        foreach (var divisionScores in scoresByDivision)
        {
            _view.BindResults(divisionScores.Key, divisionScores.Any(score => score.Handicap > 0), [.. divisionScores]);
        }
    }
}
