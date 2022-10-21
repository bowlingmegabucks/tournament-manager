namespace NortheastMegabuck.Sweepers.Portal;
internal class Presenter
{
    private readonly IView _view;
    private readonly Retrieve.IAdapter _retrieveSquadAdapter;

    private readonly Lazy<Complete.IAdapter> _completeSweeperAdapter;
    private Complete.IAdapter CompleteSweeperAdapter => _completeSweeperAdapter.Value;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _retrieveSquadAdapter = new Retrieve.Adapter(config);
        _completeSweeperAdapter = new Lazy<Complete.IAdapter>(() => new Complete.Adapter(config));
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

    public void Load()
    {
        var squad = _retrieveSquadAdapter.Execute(_view.Id);

        if (_retrieveSquadAdapter.Error != null)
        {
            _view.DisplayError(_retrieveSquadAdapter.Error.Message);

            _view.Close();

            return;
        }

        _view.SetPortalTitle($"{squad!.Date:MM/dd/yyyy hh:mmtt}");

        _view.StartingLane = squad.StartingLane;
        _view.NumberOfLanes = squad.NumberOfLanes;
        _view.MaxPerPair = squad.MaxPerPair;
        _view.Complete = squad.Complete;
    }

    internal void Complete()
    {
        if (!_view.Confirm("Are you sure you want to complete this sweeper?"))
        {
            return;
        }

        CompleteSweeperAdapter.Execute(_view.Id);

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
