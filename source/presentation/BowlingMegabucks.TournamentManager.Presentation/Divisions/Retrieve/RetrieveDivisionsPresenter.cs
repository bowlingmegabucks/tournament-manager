using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Divisions.Retrieve;

/// <summary>
/// Handles the presentation logic for retrieving and managing tournament divisions, coordinating between the view and the data adapter.
/// </summary>
public class Presenter
{
    private readonly IView _view;

    private readonly IAdapter _adapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class with the specified view and service provider.
    /// </summary>
    /// <param name="view">The view interface for displaying and managing divisions.</param>
    /// <param name="services">The service provider used to resolve dependencies.</param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;
        _adapter = services.GetRequiredService<IAdapter>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class for unit testing with mock dependencies.
    /// </summary>
    /// <param name="mockView">A mock view for testing.</param>
    /// <param name="mockAdapter">A mock adapter for testing.</param>
    internal Presenter(IView mockView, IAdapter mockAdapter)
    {
        _view = mockView;
        _adapter = mockAdapter;
    }

    /// <summary>
    /// Executes the operation to retrieve and bind tournament divisions, handling any errors that occur.
    /// </summary>
    /// <remarks>
    /// This method retrieves tournament divisions using the provided adapter and binds them to the view. If an error occurs during the operation, the error message is displayed, and the view is disabled.
    /// </remarks>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. Passing a canceled token will immediately terminate the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var divisions = await _adapter.ExecuteAsync(_view.TournamentId, cancellationToken).ConfigureAwait(true);

        if (_adapter.Error != null)
        {
            _view.DisplayError(_adapter.Error.Message);
            _view.Disable();
        }
        else
        {
            _view.BindDivisions(divisions);
        }
    }

    /// <summary>
    /// Adds a new division to the tournament and refreshes the list of divisions.
    /// </summary>
    /// <remarks>
    /// This method attempts to add a new division to the tournament using the current tournament ID. If the division is successfully added, the list of divisions is refreshed asynchronously.
    /// </remarks>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. If the operation is canceled, the method will terminate early.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task AddDivisionAsync(CancellationToken cancellationToken)
    {
        var divisionId = _view.AddDivision(_view.TournamentId);

        if (divisionId != null)
        {
            await _view.RefreshDivisionsAsync(cancellationToken).ConfigureAwait(true);
        }
    }
}
