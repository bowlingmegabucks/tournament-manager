using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Bowlers.Update;

/// <summary>
/// Handles the presentation logic for updating a bowler's name, coordinating between the view and the data adapters.
/// </summary>
public class NamePresenter
{
    private readonly IBowlerNameView _view;

    private readonly Retrieve.IAdapter _retrieveBowlerAdapter;

    private readonly Lazy<IAdapter> _updateBowlerNameAdapter;
    private IAdapter UpdateBowlerNameAdapter => _updateBowlerNameAdapter.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="NamePresenter"/> class with the specified view and service provider.
    /// </summary>
    /// <param name="view">The view interface for displaying and updating bowler name information.</param>
    /// <param name="services">The service provider used to resolve dependencies.</param>
    /// <remarks>
    /// This constructor is used in production to inject the required view and adapters via dependency injection.
    /// </remarks>
    public NamePresenter(IBowlerNameView view, IServiceProvider services)
    {
        _view = view;

        _retrieveBowlerAdapter = services.GetRequiredService<Retrieve.IAdapter>();
        _updateBowlerNameAdapter = new Lazy<IAdapter>(services.GetRequiredService<IAdapter>);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NamePresenter"/> class for unit testing with mock dependencies.
    /// </summary>
    /// <param name="mockView">A mock view for testing.</param>
    /// <param name="mockRetrieveBowlerAdapter">A mock adapter for retrieving bowler data.</param>
    /// <param name="mockUpdateBowlerNameAdapter">A mock adapter for updating bowler name data.</param>
    internal NamePresenter(IBowlerNameView mockView, Retrieve.IAdapter mockRetrieveBowlerAdapter, IAdapter mockUpdateBowlerNameAdapter)
    {
        _view = mockView;
        _retrieveBowlerAdapter = mockRetrieveBowlerAdapter;
        _updateBowlerNameAdapter = new Lazy<IAdapter>(() => mockUpdateBowlerNameAdapter);
    }

    /// <summary>
    /// Loads the bowler's data asynchronously and binds it to the view.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves the bowler's information using the retrieve adapter. If an error occurs, it displays the error and disables the view.
    /// Otherwise, it binds the retrieved bowler data to the view.
    /// </remarks>
    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        var bowler = await _retrieveBowlerAdapter.ExecuteAsync(_view.Id, cancellationToken).ConfigureAwait(true);

        if (_retrieveBowlerAdapter.Error != null)
        {
            _view.DisplayError(_retrieveBowlerAdapter.Error.Message);
            _view.Disable();

            return;
        }

        _view.Bind(bowler!);
    }

    /// <summary>
    /// Executes the update operation for the bowler's name asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method validates the view input, attempts to update the bowler's name, and handles any errors by displaying them to the user.
    /// If the update is successful, it displays a confirmation message and closes the view.
    /// </remarks>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();

            return;
        }

        await UpdateBowlerNameAdapter.ExecuteAsync(_view.Id, _view.BowlerName, cancellationToken).ConfigureAwait(true);

        if (UpdateBowlerNameAdapter.Errors.Any())
        {
            _view.DisplayErrors(UpdateBowlerNameAdapter.Errors.Select(error => error.Message));
            _view.KeepOpen();

            return;
        }

        _view.DisplayMessage($"{_view.FullName}'s name updated");
        _view.OkToClose();
    }
}
