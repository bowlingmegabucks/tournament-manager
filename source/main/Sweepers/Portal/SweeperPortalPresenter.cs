using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Sweepers.Portal;
internal class Presenter
{
    private readonly IView _view;
    private readonly Retrieve.IAdapter _retrieveSquadAdapter;

    private readonly Lazy<Complete.IAdapter> _completeSweeperAdapter;
    private Complete.IAdapter CompleteSweeperAdapter => _completeSweeperAdapter.Value;

    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;

        _retrieveSquadAdapter = services.GetRequiredService<Retrieve.IAdapter>();
        _completeSweeperAdapter = new Lazy<Complete.IAdapter>(services.GetRequiredService<Complete.IAdapter>);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRetrieveSquadAdapter"></param>
    /// <param name="mockCompleteSweeperAdapter"></param>
    internal Presenter(IView mockView, Retrieve.IAdapter mockRetrieveSquadAdapter, Complete.IAdapter mockCompleteSweeperAdapter)
    {
        _view = mockView;
        _retrieveSquadAdapter = mockRetrieveSquadAdapter;
        _completeSweeperAdapter = new Lazy<Complete.IAdapter>(() => mockCompleteSweeperAdapter);
    }

    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        var squad = await _retrieveSquadAdapter.ExecuteAsync(_view.Id, cancellationToken).ConfigureAwait(true);

        if (_retrieveSquadAdapter.Error != null)
        {
            _view.DisplayError(_retrieveSquadAdapter.Error.Message);

            _view.Close();

            return;
        }

        _view.SetPortalTitle($"{squad!.Date:MM/dd/yyyy hh:mmtt}");

        _view.SetStartingLane(squad.StartingLane);
        _view.SetNumberOfLanes(squad.NumberOfLanes);
        _view.SetMaxPerPair(squad.MaxPerPair);
        _view.SetComplete(squad.Complete);
    }

    internal async Task CompleteAsync(CancellationToken cancellationToken)
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
