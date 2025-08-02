using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Sweepers.Portal;

/// <summary>
/// Handles the presentation logic for the sweeper portal, including loading and completing sweepers.
/// </summary>
public class Presenter
{
    private readonly IView _view;
    private readonly Retrieve.IAdapter _retrieveSquadAdapter;

    private readonly Lazy<Complete.IAdapter> _completeSweeperAdapter;
    private Complete.IAdapter CompleteSweeperAdapter => _completeSweeperAdapter.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class.
    /// </summary>
    /// <param name="view">The view interface for the sweeper portal.</param>
    /// <param name="services">The service provider for dependency injection.</param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;
        _retrieveSquadAdapter = services.GetRequiredService<Retrieve.IAdapter>();
        _completeSweeperAdapter = new Lazy<Complete.IAdapter>(services.GetRequiredService<Complete.IAdapter>);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView">The mock view for testing.</param>
    /// <param name="mockRetrieveSquadAdapter">The mock retrieve squad adapter for testing.</param>
    /// <param name="mockCompleteSweeperAdapter">The mock complete sweeper adapter for testing.</param>
    internal Presenter(IView mockView, Retrieve.IAdapter mockRetrieveSquadAdapter, Complete.IAdapter mockCompleteSweeperAdapter)
    {
        _view = mockView;
        _retrieveSquadAdapter = mockRetrieveSquadAdapter;
        _completeSweeperAdapter = new Lazy<Complete.IAdapter>(() => mockCompleteSweeperAdapter);
    }

    /// <summary>
    /// Loads sweeper details asynchronously and updates the view.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method retrieves sweeper details, handles errors, and binds the results to the view.
    /// </remarks>
    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        var squad = await _retrieveSquadAdapter.ExecuteAsync(_view.Id, cancellationToken).ConfigureAwait(true);

        if (_retrieveSquadAdapter.Error != null)
        {
            _view.DisplayError(_retrieveSquadAdapter.Error.Message);
            _view.Close();
            return;
        }

        _view.SetPortalTitle($"{squad!.SweeperDate:MM/dd/yyyy hh:mmtt}");
        _view.SetStartingLane(squad.StartingLane);
        _view.SetNumberOfLanes(squad.NumberOfLanes);
        _view.SetMaxPerPair(squad.MaxPerPair);
        _view.SetComplete(squad.Complete);
    }

    /// <summary>
    /// Completes the sweeper asynchronously and updates the view.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method prompts for confirmation, completes the sweeper, handles errors, and updates the view accordingly.
    /// </remarks>
    public async Task CompleteAsync(CancellationToken cancellationToken)
    {
        if (!_view.Confirm("Are you sure you want to complete this sweeper?"))
        {
            return;
        }

        await CompleteSweeperAdapter.ExecuteAsync(_view.Id, cancellationToken).ConfigureAwait(true);

        if (CompleteSweeperAdapter.Error != null)
        {
            _view.DisplayError(CompleteSweeperAdapter.Error.Message);
        }
        else
        {
            _view.DisplayMessage("Sweeper successfully completed");
            _view.Close();
        }
    }
}
