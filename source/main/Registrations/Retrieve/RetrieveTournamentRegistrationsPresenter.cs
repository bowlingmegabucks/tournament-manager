using System.Globalization;

namespace NortheastMegabuck.Registrations.Retrieve;
internal class TournamentRegistrationsPresenter
{
    private readonly ITournamentRegistrationsView _view;

    private readonly IAdapter _registrationsAdapter;
    private readonly Squads.Retrieve.IAdapter _squadsAdapter;
    private readonly Sweepers.Retrieve.IAdapter _sweepersAdapter;

    private readonly Lazy<Delete.IAdapter> _deleteAdapter;
    private Delete.IAdapter DeleteAdapter => _deleteAdapter.Value;

    public TournamentRegistrationsPresenter(ITournamentRegistrationsView view, IConfiguration config)
    {
        _view = view;

        _registrationsAdapter = new Adapter(config);
        _squadsAdapter = new Squads.Retrieve.Adapter(config);
        _sweepersAdapter = new Sweepers.Retrieve.Adapter(config);

        _deleteAdapter = new Lazy<Delete.IAdapter>(() => new Delete.Adapter(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRgistrationsAdapter"></param>
    /// <param name="mockSquadsAdapter"></param>
    /// <param name="mockSweepersAdapter"></param>
    /// <param name="mockDeleteAdapter"></param>
    internal TournamentRegistrationsPresenter(ITournamentRegistrationsView mockView, IAdapter mockRgistrationsAdapter, Squads.Retrieve.IAdapter mockSquadsAdapter, Sweepers.Retrieve.IAdapter mockSweepersAdapter, Delete.IAdapter mockDeleteAdapter)
    {
        _view = mockView;
        _registrationsAdapter = mockRgistrationsAdapter;
        _squadsAdapter = mockSquadsAdapter;
        _sweepersAdapter = mockSweepersAdapter;
        _deleteAdapter = new Lazy<Delete.IAdapter>(()=> mockDeleteAdapter);
    }

    public void Execute(CancellationToken cancellationToken)
    {
        var registrationsTask = _registrationsAdapter.ExecuteAsync(_view.TournamentId, cancellationToken);
        var squadsTask = Task.Run(() => _squadsAdapter.Execute(_view.TournamentId));
        var sweepersTask = Task.Run(() => _sweepersAdapter.Execute(_view.TournamentId));

        var tasks = new List<Task> { registrationsTask, squadsTask, sweepersTask };

        Task.WaitAll(tasks.ToArray(), cancellationToken);

        var errors = new[] { _registrationsAdapter.Error, _squadsAdapter.Error, _sweepersAdapter.Error }.Where(error => error != null).ToList();

        if (errors.Count > 0)
        {
            _view.DisplayError(string.Join(Environment.NewLine, errors.Select(error => error!.Message).Distinct()));

            return;
        }

        var squads = squadsTask.Result.ToDictionary(squad => squad.Id, squad => squad.Date.ToString("MM/dd/yy htt", CultureInfo.CurrentCulture));
        var squadEntries = squads.ToDictionary(squad => squad.Value, squad => registrationsTask.Result.Count(registration => registration.SquadsEntered.Contains(squad.Key)));
        _view.SetSquadEntries(squadEntries);
        _view.BindSquadDates(squads);

        var sweepers = sweepersTask.Result.ToDictionary(sweeper => sweeper.Id, sweeper => sweeper.Date.ToString("MM/dd/yy htt", CultureInfo.CurrentCulture));
        var sweeperEntries = sweepers.ToDictionary(sweeper => sweeper.Value, sweeper => registrationsTask.Result.Count(registration => registration.SweepersEntered.Contains(sweeper.Key)));
        sweeperEntries.Add("Super Sweeper", registrationsTask.Result.Count(registration => registration.SuperSweeperEntered));
        _view.SetSweeperEntries(sweeperEntries);
        _view.BindSquadDates(sweepers);

        _view.BindRegistrations(registrationsTask.Result.OrderBy(registration => registration.LastName).ThenBy(registration => registration.FirstName));

        var divisionEntries = registrationsTask.Result.GroupBy(registration => registration.DivisionName).ToDictionary(g => g.Key, g => g.Sum(r=> r.SquadsEnteredCount));
        _view.SetDivisionEntries(divisionEntries);
    }

    public async Task DeleteAsync(RegistrationId id, CancellationToken cancellationToken)
    {
        if (!_view.Confirm("Are you sure you want to delete this bowler's entire registration?"))
        {
            return;
        }

        await DeleteAdapter.ExecuteAsync(id, cancellationToken).ConfigureAwait(true);

        if (DeleteAdapter.Error != null)
        {
            _view.DisplayError(DeleteAdapter.Error.Message);
        }
        else
        {
            _view.RemoveRegistration(id);
        }
    }

    public void UpdateBowlerName(BowlerId id)
    {
        var updatedName = _view.UpdateBowlerName(id);

        if (updatedName == null)
        {
            return;
        }

        _view.UpdateBowlerName(updatedName!);
    }
}
