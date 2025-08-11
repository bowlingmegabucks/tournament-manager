using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Registrations.Add;

/// <summary>
/// Handles presentation logic for adding a registration.
/// </summary>
public class Presenter
{
    private readonly IView _view;

    private readonly Divisions.Retrieve.IAdapter _retrieveDivisionsAdapter;

    private readonly Squads.Retrieve.IAdapter _retrieveSquadsAdapter;
    private readonly Sweepers.Retrieve.IAdapter _retrieveSweepersAdapter;

    private readonly Lazy<Bowlers.Retrieve.IAdapter> _retrieveBowlerAdapter;
    private Bowlers.Retrieve.IAdapter RetrieveBowlerAdapter => _retrieveBowlerAdapter.Value;

    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class.
    /// </summary>
    /// <param name="view">The view interface.</param>
    /// <param name="services">The service provider for dependency injection.</param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;
        _retrieveDivisionsAdapter = services.GetRequiredService<Divisions.Retrieve.IAdapter>();
        _retrieveSquadsAdapter = services.GetRequiredService<Squads.Retrieve.IAdapter>();
        _retrieveSweepersAdapter = services.GetRequiredService<Sweepers.Retrieve.IAdapter>();
        _retrieveBowlerAdapter = new Lazy<Bowlers.Retrieve.IAdapter>(services.GetRequiredService<Bowlers.Retrieve.IAdapter>);
        _adapter = new Lazy<IAdapter>(services.GetRequiredService<IAdapter>);
    }

    /// <summary>
    /// Unit Test Constructor.
    /// </summary>
    /// <param name="view">The view interface.</param>
    /// <param name="mockDivisionAdapter">Mock division adapter.</param>
    /// <param name="mockSquadAdapter">Mock squad adapter.</param>
    /// <param name="mockSweeperAdapter">Mock sweeper adapter.</param>
    /// <param name="mockBowlerAdapter">Mock bowler adapter.</param>
    /// <param name="mockAdapter">Mock registration adapter.</param>
    internal Presenter(IView view, Divisions.Retrieve.IAdapter mockDivisionAdapter, Squads.Retrieve.IAdapter mockSquadAdapter, Sweepers.Retrieve.IAdapter mockSweeperAdapter, Bowlers.Retrieve.IAdapter mockBowlerAdapter, IAdapter mockAdapter)
    {
        _view = view;

        _retrieveDivisionsAdapter = mockDivisionAdapter;
        _retrieveSquadsAdapter = mockSquadAdapter;
        _retrieveSweepersAdapter = mockSweeperAdapter;
        _retrieveBowlerAdapter = new Lazy<Bowlers.Retrieve.IAdapter>(() => mockBowlerAdapter);
        _adapter = new Lazy<IAdapter>(() => mockAdapter);
    }

    /// <summary>
    /// Loads registration data for the specified tournament and updates the view.
    /// </summary>
    /// <param name="tournamentId">The tournament identifier.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <remarks>
    /// This method retrieves divisions, squads, sweepers, and bowler information, handles errors, and updates the view accordingly.
    /// </remarks>
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

            _view.BindSquads(squads.Where(squad => !squad.Complete).OrderBy(squad => squad.SquadDate));
            _view.BindSweepers(sweepers.Where(squad => !squad.Complete).OrderBy(sweeper => sweeper.SweeperDate));

            if (bowler is not null)
            {
                _view.BindBowler(bowler);
            }
        }
    }

    /// <summary>
    /// Loads registration data for the specified tournament and squad, and updates the view.
    /// </summary>
    /// <param name="tournamentId">The tournament identifier.</param>
    /// <param name="squadId">The squad identifier.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <remarks>
    /// This method retrieves divisions, squads, sweepers, and bowler information for a specific squad, handles errors, and updates the view accordingly.
    /// </remarks>
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

            _view.BindSquads(squads.Where(squad => !squad.Complete).OrderBy(squad => squad.SquadDate), squadId);
            _view.BindSweepers(sweepers.Where(sweeper => !sweeper.Complete).OrderBy(sweeper => sweeper.SweeperDate), squadId);

            if (bowler is not null)
            {
                _view.BindBowler(bowler);
            }
        }
    }

    /// <summary>
    /// Executes the registration process and updates the view based on the result.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <remarks>
    /// This method validates the view, executes the registration, handles errors, and updates the view accordingly.
    /// </remarks>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();
            return;
        }

        await Adapter.ExecuteAsync(_view.Bowler, _view.TournamentId, _view.DivisionId, _view.Squads, _view.Sweepers, _view.SuperSweeper, _view.Average, cancellationToken).ConfigureAwait(true);

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
