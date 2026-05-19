using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Divisions.Add;

/// <summary>
/// Handles the presentation logic for adding a new division, coordinating between the view and the data adapters.
/// </summary>
public class Presenter
{
    private readonly Lazy<Retrieve.IAdapter> _retrieveDivisionsAdapter;
    private Retrieve.IAdapter RetrieveDivisionsAdapter => _retrieveDivisionsAdapter.Value;

    private readonly Lazy<IAdapter> _addDivisionAdapter;
    private IAdapter AddDivisionAdapter => _addDivisionAdapter.Value;

    private readonly IView _view;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class with the specified view and service provider.
    /// </summary>
    /// <param name="view">The view interface for displaying and adding division information.</param>
    /// <param name="services">The service provider used to resolve dependencies.</param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;

        _retrieveDivisionsAdapter = new Lazy<Retrieve.IAdapter>(services.GetRequiredService<Retrieve.IAdapter>);
        _addDivisionAdapter = new Lazy<IAdapter>(services.GetRequiredService<IAdapter>);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class for unit testing with mock dependencies.
    /// </summary>
    /// <param name="mockView">A mock view for testing.</param>
    /// <param name="mockRetrieveDivisionsAdapter">A mock adapter for retrieving division data.</param>
    /// <param name="mockAddDivisionAdapter">A mock adapter for adding division data.</param>
    internal Presenter(IView mockView, Retrieve.IAdapter mockRetrieveDivisionsAdapter, IAdapter mockAddDivisionAdapter)
    {
        _view = mockView;
        _retrieveDivisionsAdapter = new Lazy<Retrieve.IAdapter>(() => mockRetrieveDivisionsAdapter);
        _addDivisionAdapter = new Lazy<IAdapter>(() => mockAddDivisionAdapter);
    }

    /// <summary>
    /// Retrieves the next available division number asynchronously and updates the view.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves the list of existing divisions and sets the next division number in the view.
    /// If an error occurs, the error message is displayed to the user.
    /// </remarks>
    public async Task GetNextDivisionNumberAsync(CancellationToken cancellationToken)
    {
        var divisions = await RetrieveDivisionsAdapter.ExecuteAsync(_view.Division.TournamentId, cancellationToken).ConfigureAwait(true);

        if (RetrieveDivisionsAdapter.Error != null)
        {
            _view.DisplayErrors([RetrieveDivisionsAdapter.Error.Message]);
        }
        else
        {
            _view.Division.Number = (short)(divisions.Count() + 1);
        }
    }

    /// <summary>
    /// Executes the operation to add a division asynchronously, handling validation, errors, and user feedback.
    /// </summary>
    /// <remarks>
    /// This method validates the division data before attempting to add it. If validation fails, the
    /// view remains open, and no further action is taken. If the operation encounters errors, the errors are displayed
    /// to the user, and the view remains open. On success, a confirmation message is displayed, the division's ID is
    /// updated, and the view is closed.
    /// </remarks>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();
            return;
        }

        var id = await AddDivisionAdapter.ExecuteAsync(_view.Division, cancellationToken).ConfigureAwait(true);

        if (AddDivisionAdapter.Errors.Any())
        {
            _view.KeepOpen();
            _view.DisplayErrors(AddDivisionAdapter.Errors.Select(e => e.Message));
        }
        else
        {
            _view.DisplayMessage($"{_view.Division.DivisionName} division added.");
            _view.Division.Id = id!.Value;
            _view.Close();
        }
    }
}
