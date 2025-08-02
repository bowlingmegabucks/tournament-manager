using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Sweepers.Add;

/// <summary>
/// Handles the presentation logic for adding a new sweeper, including division retrieval and sweeper creation.
/// </summary>
public class Presenter
{
    private readonly IView _view;
    private readonly Divisions.Retrieve.IAdapter _retrieveDivisionsAdapter;
    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class.
    /// </summary>
    /// <param name="view">The view interface for adding a sweeper.</param>
    /// <param name="services">The service provider for dependency injection.</param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;
        _retrieveDivisionsAdapter = services.GetRequiredService<Divisions.Retrieve.IAdapter>();
        _adapter = new Lazy<IAdapter>(services.GetRequiredService<IAdapter>);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView">The mock view for testing.</param>
    /// <param name="mockRetrieveDivisionsAdapter">The mock divisions adapter for testing.</param>
    /// <param name="mockAdapter">The mock sweeper adapter for testing.</param>
    internal Presenter(IView mockView, Divisions.Retrieve.IAdapter mockRetrieveDivisionsAdapter, IAdapter mockAdapter)
    {
        _view = mockView;
        _retrieveDivisionsAdapter = mockRetrieveDivisionsAdapter;
        _adapter = new Lazy<IAdapter>(() => mockAdapter);
    }

    /// <summary>
    /// Retrieves divisions for the tournament and updates the view.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method fetches divisions and updates the view. Displays errors and disables the view if retrieval fails.
    /// </remarks>
    public async Task GetDivisionsAsync(CancellationToken cancellationToken)
    {
        var divisions = await _retrieveDivisionsAdapter.ExecuteAsync(_view.TournamentId, cancellationToken).ConfigureAwait(true);

        if (_retrieveDivisionsAdapter.Error != null)
        {
            _view.DisplayError(_retrieveDivisionsAdapter.Error.Message);
            _view.Disable();
        }
        else
        {
            _view.BindDivisions(divisions);
        }
    }

    /// <summary>
    /// Executes the process of adding a new sweeper asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method validates the view, attempts to add a sweeper, and updates the view with the result or errors.
    /// </remarks>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();
            return;
        }

        var id = await Adapter.ExecuteAsync(_view.Sweeper, cancellationToken).ConfigureAwait(true);

        if (Adapter.Errors.Any())
        {
            _view.KeepOpen();
            _view.DisplayError(string.Join(Environment.NewLine, Adapter.Errors.Select(error => error.Message)));
        }
        else
        {
            _view.Sweeper.Id = id!.Value;
            _view.DisplayMessage($"Sweeper added for {_view.Sweeper.Date:MM/dd/yyyy hh:mm tt}");
            _view.Close();
        }
    }
}
