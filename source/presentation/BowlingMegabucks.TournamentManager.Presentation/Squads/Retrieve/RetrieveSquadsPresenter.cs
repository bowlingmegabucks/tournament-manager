using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Squads.Retrieve;

/// <summary>
/// Handles the presentation logic for retrieving and managing squads, including error handling and squad addition.
/// </summary>
public class Presenter
{
    private readonly IView _view;
    private readonly IAdapter _getSquadsAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class.
    /// </summary>
    /// <param name="view">The view interface for retrieving squads.</param>
    /// <param name="services">The service provider for dependency injection.</param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;
        _getSquadsAdapter = services.GetRequiredService<IAdapter>();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView">The mock view for testing.</param>
    /// <param name="mockGetSquadsAdapter">The mock adapter for testing.</param>
    internal Presenter(IView mockView, IAdapter mockGetSquadsAdapter)
    {
        _view = mockView;
        _getSquadsAdapter = mockGetSquadsAdapter;
    }

    /// <summary>
    /// Executes the process of retrieving and displaying squads asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves squads, handles errors, and binds the results to the view.
    /// </remarks>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var squads = await _getSquadsAdapter.ExecuteAsync(_view.TournamentId, cancellationToken).ConfigureAwait(true);

        if (_getSquadsAdapter.Error != null)
        {
            _view.Disable();
            _view.DisplayError(_getSquadsAdapter.Error.Message);
        }
        else
        {
            _view.BindSquads(squads.OrderBy(squad => squad.Date));
        }
    }

    /// <summary>
    /// Prompts the user to add a new squad and refreshes the squad list if successful.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method initiates the process of adding a new squad and refreshes the squad list if a squad is added.
    /// </remarks>
    public async Task AddSquadAsync(CancellationToken cancellationToken)
    {
        var squadId = _view.AddSquad(_view.TournamentId);

        if (squadId != null)
        {
            await _view.RefreshSquadsAsync(cancellationToken).ConfigureAwait(true);
        }
    }
}
