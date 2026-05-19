using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Scores.Update;

/// <summary>
/// Handles the presentation logic for updating scores in the tournament manager.
/// </summary>
public class Presenter
{
    private readonly IView _view;

    private readonly IAdapter _adapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class.
    /// </summary>
    /// <param name="view">The view interface for updating scores.</param>
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
    /// Executes the score update process asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method updates scores using the adapter, displays errors if any occur, and notifies the view of the result.
    /// </remarks>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await _adapter.ExecuteAsync(_view.Scores, cancellationToken).ConfigureAwait(true);

        if (_adapter.Errors.Any())
        {
            _view.DisplayError(string.Join(Environment.NewLine, _adapter.Errors.Select(error => error.Message)));
            _view.KeepOpen();
        }
        else
        {
            _view.DisplayMessage("Scores updated");
            _view.OkToClose();
        }
    }
}
