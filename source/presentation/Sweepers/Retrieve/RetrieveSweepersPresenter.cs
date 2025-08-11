using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Sweepers.Retrieve;

/// <summary>
/// Handles the presentation logic for retrieving and managing sweepers, including error handling and sweeper addition.
/// </summary>
public class Presenter
{
    private readonly IView _view;
    private readonly IAdapter _getSweepersAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class.
    /// </summary>
    /// <param name="view">The view interface for retrieving sweepers.</param>
    /// <param name="services">The service provider for dependency injection.</param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;
        _getSweepersAdapter = services.GetRequiredService<IAdapter>();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView">The mock view for testing.</param>
    /// <param name="mockGetSweepersAdapter">The mock adapter for testing.</param>
    internal Presenter(IView mockView, IAdapter mockGetSweepersAdapter)
    {
        _view = mockView;
        _getSweepersAdapter = mockGetSweepersAdapter;
    }

    /// <summary>
    /// Executes the process of retrieving and displaying sweepers asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves sweepers, handles errors, and binds the results to the view.
    /// </remarks>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var sweepers = await _getSweepersAdapter.ExecuteAsync(_view.TournamentId, cancellationToken).ConfigureAwait(true);

        if (_getSweepersAdapter.Error != null)
        {
            _view.Disable();
            _view.DisplayError(_getSweepersAdapter.Error.Message);
        }
        else
        {
            _view.BindSweepers(sweepers.OrderBy(sweeper => sweeper.SweeperDate));
        }
    }

    /// <summary>
    /// Prompts the user to add a new sweeper and refreshes the sweeper list if successful.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method initiates the process of adding a new sweeper and refreshes the sweeper list if a sweeper is added.
    /// </remarks>
    public async Task AddSweeperAsync(CancellationToken cancellationToken)
    {
        var sweeperId = _view.AddSweeper(_view.TournamentId);

        if (sweeperId != null)
        {
            await _view.RefreshSweepersAsync(cancellationToken).ConfigureAwait(true);
        }
    }
}
