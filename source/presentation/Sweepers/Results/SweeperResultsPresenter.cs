using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Sweepers.Results;

/// <summary>
/// Handles the presentation logic for displaying sweeper results, including error handling and result binding.
/// </summary>
public class Presenter
{
    private readonly IView _view;
    private readonly IAdapter _adapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class.
    /// </summary>
    /// <param name="view">The view interface for displaying sweeper results.</param>
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
    /// Executes the process of retrieving and displaying sweeper results for a specific squad asynchronously.
    /// </summary>
    /// <param name="squadId">The unique identifier for the squad.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves results for a squad, handles errors, and binds the results to the view.
    /// </remarks>
    public async Task ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
    {
        var results = await _adapter.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(true);

        if (_adapter.Error != null)
        {
            _view.DisplayError(_adapter.Error.Message);
        }
        else
        {
            _view.BindResults([.. results]);
        }
    }

    /// <summary>
    /// Executes the process of retrieving and displaying sweeper results for a specific tournament asynchronously.
    /// </summary>
    /// <param name="tournamentId">The unique identifier for the tournament.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves results for a tournament, handles errors, and binds the results to the view.
    /// </remarks>
    public async Task ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var results = await _adapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(true);

        if (_adapter.Error != null)
        {
            _view.DisplayError(_adapter.Error.Message);
        }
        else
        {
            _view.BindResults([.. results]);
        }
    }
}
