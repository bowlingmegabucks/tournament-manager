namespace NortheastMegabuck.Registrations.Retrieve;
internal class TournamentRegistrationsPresenter
{
    private readonly ITournamentRegistrationsView _view;

    private readonly IAdapter _registrationsAdapter;
    private readonly Squads.Retrieve.IAdapter _squadsAdapter;
    private readonly Sweepers.Retrieve.IAdapter _sweepersAdapter;

    public TournamentRegistrationsPresenter(ITournamentRegistrationsView view, IConfiguration config)
    {
        _view = view;

        _registrationsAdapter = new Adapter(config);
        _squadsAdapter = new Squads.Retrieve.Adapter(config);
        _sweepersAdapter = new Sweepers.Retrieve.Adapter(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRgistrationsAdapter"></param>
    /// <param name="mockSquadsAdapter"></param>
    /// <param name="mockSweepersAdapter"></param>
    internal TournamentRegistrationsPresenter(ITournamentRegistrationsView mockView, IAdapter mockRgistrationsAdapter, Squads.Retrieve.IAdapter mockSquadsAdapter, Sweepers.Retrieve.IAdapter mockSweepersAdapter)
    {
        _view = mockView;
        _registrationsAdapter = mockRgistrationsAdapter;
        _squadsAdapter = mockSquadsAdapter;
        _sweepersAdapter = mockSweepersAdapter;
    }

    public void Execute()
    {
        var registrationsTask = Task.Run(() => _registrationsAdapter.Execute(_view.TournamentId));
        var squadsTask = Task.Run(() => _squadsAdapter.Execute(_view.TournamentId));
        var sweepersTask = Task.Run(() => _sweepersAdapter.Execute(_view.TournamentId));

        var tasks = new List<Task> { registrationsTask, squadsTask, sweepersTask };

        Task.WaitAll(tasks.ToArray());

        var errors = new[] { _registrationsAdapter.Error, _squadsAdapter.Error, _sweepersAdapter.Error }.Where(error => error != null).ToList();

        if (errors.Any())
        {
            _view.DisplayError(string.Join(Environment.NewLine, errors.Select(error => error!.Message).Distinct()));

            return;
        }

        _view.BindRegistrations(registrationsTask.Result.OrderBy(registration => registration.LastName).ThenBy(registration => registration.FirstName));

        var divisionEntries = registrationsTask.Result.GroupBy(registration => registration.DivisionName).ToDictionary(g => g.Key, g => g.Count());
        _view.SetDivisionEntries(divisionEntries);

        var squads = squadsTask.Result.ToDictionary(squad => squad.Id, squad => squad.Date.ToString("MM/dd/yy htt"));
        var squadEntries = squads.ToDictionary(squad => squad.Value, squad => registrationsTask.Result.Count(registration => registration.SquadsEntered.Contains(squad.Key)));
        _view.SetSquadEntries(squadEntries);

        var sweepers = sweepersTask.Result.ToDictionary(sweeper => sweeper.Id, sweeper => sweeper.Date.ToString("MM/dd/yy htt"));
        var sweeperEntries = sweepers.ToDictionary(sweeper => sweeper.Value, sweeper => registrationsTask.Result.Count(registration => registration.SweepersEntered.Contains(sweeper.Key)));
        sweeperEntries.Add("Super Sweeper", registrationsTask.Result.Count(registration => registration.SuperSweeperEntered));
        _view.SetSweeperEntries(sweeperEntries);
    }
}
