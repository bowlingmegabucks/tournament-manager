using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Bowlers.Update;

/// <summary>
/// Handles the presentation logic f    or updating a bowler's information, coordinating between the view and the data adapters.
/// </summary>
public class Presenter
{
    private readonly IView _view;

    private readonly Retrieve.IAdapter _retrieveBowlerAdapter;

    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class with the specified view and service provider.
    /// </summary>
    /// <param name="view">The view interface for displaying and updating bowler information.</param>
    /// <param name="services">The service provider used to resolve dependencies.</param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;

        _retrieveBowlerAdapter = services.GetRequiredService<Retrieve.IAdapter>();
        _adapter = new Lazy<IAdapter>(services.GetRequiredService<IAdapter>);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class for unit testing with mock dependencies.
    /// </summary>
    /// <param name="mockView">A mock view for testing.</param>
    /// <param name="mockRetrieveBowlerAdapter">A mock adapter for retrieving bowler data.</param>
    /// <param name="mockUpdateBowlerAdapter">A mock adapter for updating bowler data.</param>
    internal Presenter(IView mockView, Retrieve.IAdapter mockRetrieveBowlerAdapter, IAdapter mockUpdateBowlerAdapter)
    {
        _view = mockView;
        _retrieveBowlerAdapter = mockRetrieveBowlerAdapter;
        _adapter = new Lazy<IAdapter>(() => mockUpdateBowlerAdapter);
    }

    /// <summary>
    /// Loads the bowler's data asynchronously and binds it to the view.
    /// </summary>  
    /// <param name="id">The unique identifier of the bowler to load.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves the bowler's information using the retrieve adapter. If an error occurs, it displays the error and disables the view.
    /// Otherwise, it binds the retrieved bowler data to the view.
    /// </remarks>
    public async Task LoadAsync(BowlerId id, CancellationToken cancellationToken)
    {
        var bowler = await _retrieveBowlerAdapter.ExecuteAsync(id, cancellationToken).ConfigureAwait(true);

        if (_retrieveBowlerAdapter.Error != null)
        {
            _view.DisplayError(_retrieveBowlerAdapter.Error.Message);
            _view.Disable();

            return;
        }

        _view.Bind(bowler!);
    }

    /// <summary>
    /// Executes the update operation for the bowler's information asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method validates the view input, attempts to update the bowler's information, and handles any errors by displaying them to the user.
    /// If the update is successful, it displays a confirmation message and closes the view.
    /// </remarks>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();

            return;
        }

        await Adapter.ExecuteAsync(_view.Bowler, cancellationToken).ConfigureAwait(true);

        if (Adapter.Errors.Any())
        {
            _view.DisplayErrors(Adapter.Errors.Select(error => error.Message));
            _view.KeepOpen();

            return;
        }

        _view.DisplayMessage($"Bowler updated");
        _view.OkToClose();
    }
}
