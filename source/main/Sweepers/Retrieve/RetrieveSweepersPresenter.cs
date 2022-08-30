namespace NortheastMegabuck.Sweepers.Retrieve;
internal class Presenter
{
    private readonly IView _view;

    private readonly IAdapter _getSweepersAdapter;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;
        _getSweepersAdapter = new Adapter(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockGetSweepersAdapter"></param>
    internal Presenter(IView mockView, IAdapter mockGetSweepersAdapter)
    {
        _view = mockView;
        _getSweepersAdapter = mockGetSweepersAdapter;
    }

    public void Execute()
    {
        var sweepers = _getSweepersAdapter.Execute(_view.TournamentId);

        if (_getSweepersAdapter.Error != null)
        {
            _view.Disable();
            _view.DisplayError(_getSweepersAdapter.Error.Message);
        }
        else
        {
            _view.BindSweepers(sweepers.OrderBy(sweeper => sweeper.Date));
        }
    }

    internal void AddSweeper()
    {
        var sweeperId = _view.AddSweeper(_view.TournamentId);
        
        if (sweeperId != null)
        {
            _view.RefreshSweepers();
        }
    }
}
