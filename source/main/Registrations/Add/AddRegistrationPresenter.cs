
namespace NewEnglandClassic.Registrations.Add;
internal class Presenter
{
    private readonly IView _view;

    private readonly Divisions.Retrieve.IAdapter _retrieveDivisionsAdapter;

    private readonly Squads.Retrieve.IAdapter _retrieveSquadsAdapter;
    private readonly Sweepers.Retrieve.IAdapter _retrieveSweepersAdapter;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _retrieveDivisionsAdapter = new Divisions.Retrieve.Adapter(config);

        _retrieveSquadsAdapter = new Squads.Retrieve.Adapter(config);
        _retrieveSweepersAdapter = new Sweepers.Retrieve.Adapter(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockDivisionAdapter"></param>
    /// <param name="mockSquadAdapter"></param>
    /// <param name="mockSweeperAdapter"></param>
    internal Presenter(IView mockView, Divisions.Retrieve.IAdapter mockDivisionAdapter, Squads.Retrieve.IAdapter mockSquadAdapter, Sweepers.Retrieve.IAdapter mockSweeperAdapter)
    {
        _view = mockView;

        _retrieveDivisionsAdapter = mockDivisionAdapter;
        _retrieveSquadsAdapter = mockSquadAdapter;
        _retrieveSweepersAdapter = mockSweeperAdapter;
    }

        public void Load()
    {
        var divisionsTask = Task.Run(() => _retrieveDivisionsAdapter.ForTournament(_view.TournamentId));
        var squadsTask = Task.Run(() => _retrieveSquadsAdapter.ForTournament(_view.TournamentId));
        var sweepersTask = Task.Run(() => _retrieveSweepersAdapter.ForTournament(_view.TournamentId));

        Task.WaitAll(divisionsTask, squadsTask, sweepersTask);

        if (_retrieveDivisionsAdapter.Error != null)
        {
            _view.DisplayError(_retrieveDivisionsAdapter.Error.Message);
            _view.Disable();
        }
        else if (_retrieveSquadsAdapter.Error != null)
        {
            _view.DisplayError(_retrieveSquadsAdapter.Error.Message);
            _view.Disable();
        }
        else if (_retrieveSweepersAdapter.Error != null)
        {
            _view.DisplayError(_retrieveSweepersAdapter.Error.Message);
            _view.Disable();
        }
        else
        {
            _view.BindDivisions(divisionsTask.Result.OrderBy(division=> division.Number));

            _view.BindSquads(squadsTask.Result.OrderBy(squad => squad.Date));
            _view.BindSweepers(sweepersTask.Result.OrderBy(sweeper => sweeper.Date));
        }
    }
}
