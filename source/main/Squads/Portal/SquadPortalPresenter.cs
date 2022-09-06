namespace NortheastMegabuck.Squads.Portal;
internal class Presenter
{
    private readonly IView _view;
    private readonly Retrieve.IAdapter _retrieveSquadAdapter;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _retrieveSquadAdapter = new Retrieve.Adapter(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRetrieveSquadAdapter"></param>
    internal Presenter(IView mockView, Retrieve.IAdapter mockRetrieveSquadAdapter)
    {
        _view = mockView;
        _retrieveSquadAdapter = mockRetrieveSquadAdapter;
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
    }
}
