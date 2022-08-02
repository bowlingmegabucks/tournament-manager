
namespace NewEnglandClassic.Registrations.Add;
internal class Presenter
{
    private readonly IView _view;

    private readonly Divisions.Retrieve.IAdapter _retrieveDivisionsAdapter;

    private readonly Squads.Retrieve.IAdapter _retrieveSquadsAdapter;
    private readonly Sweepers.Retrieve.IAdapter _retrieveSweepersAdapter;

    private readonly Lazy<Bowlers.Retrieve.IAdapter> _retrieveBowlerAdapter;
    private Bowlers.Retrieve.IAdapter RetrieveBowlerAdapter => _retrieveBowlerAdapter.Value;

    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _retrieveDivisionsAdapter = new Divisions.Retrieve.Adapter(config);

        _retrieveSquadsAdapter = new Squads.Retrieve.Adapter(config);
        _retrieveSweepersAdapter = new Sweepers.Retrieve.Adapter(config);

        _retrieveBowlerAdapter = new Lazy<Bowlers.Retrieve.IAdapter>(() => new Bowlers.Retrieve.Adapter());

        _adapter = new Lazy<IAdapter>(() => new Adapter(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockDivisionAdapter"></param>
    /// <param name="mockSquadAdapter"></param>
    /// <param name="mockSweeperAdapter"></param>
    /// <param name="mockBowlerAdapter"></param>
    /// <param name="mockAdapter"></param>
    internal Presenter(IView mockView, Divisions.Retrieve.IAdapter mockDivisionAdapter, Squads.Retrieve.IAdapter mockSquadAdapter, Sweepers.Retrieve.IAdapter mockSweeperAdapter, Bowlers.Retrieve.IAdapter mockBowlerAdapter, IAdapter mockAdapter)
    {
        _view = mockView;

        _retrieveDivisionsAdapter = mockDivisionAdapter;
        _retrieveSquadsAdapter = mockSquadAdapter;
        _retrieveSweepersAdapter = mockSweeperAdapter;
        _retrieveBowlerAdapter = new Lazy<Bowlers.Retrieve.IAdapter>(() => mockBowlerAdapter);
        _adapter = new Lazy<IAdapter>(() => mockAdapter);
    }

    public void Load()
    {
        var bowlerId = _view.SelectBowler();

        if (bowlerId == null)
        {
            _view.Close();

            return;
        }

        var divisionsTask = Task.Run(() => _retrieveDivisionsAdapter.ForTournament(_view.TournamentId));
        var squadsTask = Task.Run(() => _retrieveSquadsAdapter.ForTournament(_view.TournamentId));
        var sweepersTask = Task.Run(() => _retrieveSweepersAdapter.ForTournament(_view.TournamentId));

        var tasks = new List<Task> { divisionsTask, squadsTask, sweepersTask };

        var bowlerTask = Task.Run(() => RetrieveBowlerAdapter.Execute(bowlerId.Value));

        if (bowlerId != Guid.Empty)
        {
            tasks.Add(bowlerTask);
        }

        Task.WaitAll(tasks.ToArray());

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
        else if (RetrieveBowlerAdapter.Error != null)
        {
            _view.DisplayError(RetrieveBowlerAdapter.Error.Message);
            _view.Disable();
        }
        else
        {
            _view.BindDivisions(divisionsTask.Result.OrderBy(division => division.Number));

            _view.BindSquads(squadsTask.Result.OrderBy(squad => squad.Date));
            _view.BindSweepers(sweepersTask.Result.OrderBy(sweeper => sweeper.Date));

            if (tasks.Contains(bowlerTask))
            {
                _view.BindBowler(bowlerTask.Result!);
            }   
        }
    }

    public void Execute()
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();
            return;
        }

        Adapter.Execute(_view.Bowler, _view.Division, _view.Squads, _view.Sweepers, _view.Average);

        if (Adapter.Errors.Any())
        {
            _view.KeepOpen();
            _view.DisplayError(string.Join(Environment.NewLine, Adapter.Errors.Select(error => error.Message)));
        }
        else
        {
            _view.DisplayMessage("Registration added");
            _view.Close();
        }
    }
}
