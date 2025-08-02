using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Bowlers.Search;

/// <summary>
/// Handles the presentation logic for searching bowlers, coordinating between the view and the data adapter.
/// </summary>
public class Presenter
{
    private readonly IView _view;

    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class with the specified view and service provider.
    /// </summary>
    /// <param name="view">The view interface for displaying search results and messages.</param>
    /// <param name="services">The service provider used to resolve dependencies.</param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;
        _adapter = new Lazy<IAdapter>(services.GetRequiredService<IAdapter>);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class for unit testing with mock dependencies.
    /// </summary>
    /// <param name="mockView">A mock view for testing.</param>
    /// <param name="mockAdapter">A mock adapter for testing.</param>
    internal Presenter(IView mockView, IAdapter mockAdapter)
    {
        _view = mockView;
        _adapter = new Lazy<IAdapter>(() => mockAdapter);
    }

    /// <summary>
    /// Executes the search operation asynchronously and updates the view with the results.
    /// </summary>
    /// <remarks>
    /// This method retrieves a list of bowlers based on the search criteria defined in the view. If
    /// an error occurs during the operation, the error message is displayed in the view. If no results are found, a
    /// "No Results" message is displayed. Otherwise, the results are sorted by last name and first name, and then
    /// bound to the view.
    /// </remarks>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. Passing a canceled token will terminate the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var bowlers = (await Adapter.ExecuteAsync(_view.SearchCriteria, cancellationToken).ConfigureAwait(true)).ToList();

        if (Adapter.Error != null)
        {
            _view.DisplayError(Adapter.Error.Message);
        }
        else if (bowlers.Count == 0)
        {
            _view.DisplayMessage("No Results");
        }
        else
        {
            _view.BindResults(bowlers.OrderBy(bowler => bowler.LastName).ThenBy(bowler => bowler.FirstName));
        }
    }
}
