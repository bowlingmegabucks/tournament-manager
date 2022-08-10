namespace NewEnglandClassic.Squads.Retrieve;
internal class Presenter
{
    private readonly IView _view;

    private readonly IAdapter _getSquadsAdapter;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;
        _getSquadsAdapter = new Adapter(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockGetSquadsAdapter"></param>
    internal Presenter(IView mockView, IAdapter mockGetSquadsAdapter)
    {
        _view = mockView;
        _getSquadsAdapter = mockGetSquadsAdapter;
    }

    public void Execute()
    {
        var squads = _getSquadsAdapter.Execute(_view.TournamentId);

        if (_getSquadsAdapter.Error != null)
        {
            _view.Disable();
            _view.DisplayError(_getSquadsAdapter.Error.Message);
        }
        else
        {
            _view.BindSquads(squads.OrderBy(squad => squad.Date));
        }
    }

    internal void AddSquad()
    {
        var squadId = _view.AddSquad(_view.TournamentId);
        
        if (squadId != null)
        {
            _view.RefreshSquads();
        }
    }
}
