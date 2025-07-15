namespace NortheastMegabuck.Registrations.Add;
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

    internal Presenter(IView view, Divisions.Retrieve.IAdapter divisionAdapter, Squads.Retrieve.IAdapter squadAdapter, Sweepers.Retrieve.IAdapter sweeperAdapter, Bowlers.Retrieve.IAdapter bowlerAdapter, IAdapter adapter)
    {
        _view = view;

        _retrieveDivisionsAdapter = divisionAdapter;
        _retrieveSquadsAdapter = squadAdapter;
        _retrieveSweepersAdapter = sweeperAdapter;
        _retrieveBowlerAdapter = new Lazy<Bowlers.Retrieve.IAdapter>(() => bowlerAdapter);
        _adapter = new Lazy<IAdapter>(() => adapter);
    }

    public async Task LoadAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var bowlerId = _view.SelectBowler();

        if (bowlerId == null)
        {
            _view.Close();

            return;
        }

        var divisions = await _retrieveDivisionsAdapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(true);
        var squads = await _retrieveSquadsAdapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(true);
        var sweepers = await _retrieveSweepersAdapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(true);

        Bowlers.Retrieve.IViewModel? bowler = null;

        if (bowlerId != BowlerId.Empty)
        {
            bowler = await RetrieveBowlerAdapter.ExecuteAsync(bowlerId.Value, cancellationToken).ConfigureAwait(true);
        }

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
            _view.BindDivisions(divisions.OrderBy(division => division.Number));

            _view.BindSquads(squads.Where(squad => !squad.Complete).OrderBy(squad => squad.Date));
            _view.BindSweepers(sweepers.Where(squad => !squad.Complete).OrderBy(sweeper => sweeper.Date));

            if (bowler is not null)
            {
                _view.BindBowler(bowler);
            }
        }
    }

    public async Task LoadAsync(TournamentId tournamentId, SquadId squadId, CancellationToken cancellationToken)
    {
        var bowlerId = _view.SelectBowler();

        if (bowlerId == null)
        {
            _view.Close();

            return;
        }

        var divisions = await _retrieveDivisionsAdapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(true);
        var squads = await _retrieveSquadsAdapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(true);
        var sweepers = await _retrieveSweepersAdapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(true);

        Bowlers.Retrieve.IViewModel? bowler = null;

        if (bowlerId != BowlerId.Empty)
        {
            bowler = await RetrieveBowlerAdapter.ExecuteAsync(bowlerId.Value, cancellationToken).ConfigureAwait(true);
        }

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
            _view.BindDivisions(divisions.OrderBy(division => division.Number));

            _view.BindSquads(squads.Where(squad => !squad.Complete).OrderBy(squad => squad.Date), squadId);
            _view.BindSweepers(sweepers.Where(sweeper => !sweeper.Complete).OrderBy(sweeper => sweeper.Date), squadId);

            if (bowler is not null)
            {
                _view.BindBowler(bowler);
            }
        }
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();
            return;
        }

        await Adapter.ExecuteAsync(_view.Bowler, _view.DivisionId, _view.Squads, _view.Sweepers, _view.SuperSweeper, _view.Average, cancellationToken).ConfigureAwait(true);

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
