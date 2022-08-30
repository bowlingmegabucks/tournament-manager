namespace NortheastMegabuck.Tournaments.Retrieve;
internal class Presenter
{
    private readonly IView _view;

    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _adapter = new Lazy<IAdapter>(() => new Adapter(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockAdapter"></param>
    internal Presenter(IView mockView, IAdapter mockAdapter)
    {
        _view = mockView;
        _adapter = new Lazy<IAdapter>(() => mockAdapter);
    }

    public void Execute()
    {
        var tournaments = Adapter.Execute();

        if (Adapter.Error != null)
        {
            _view.DisplayErrorMessage(Adapter.Error.Message);
            _view.DisableOpenTournament();
        }
        else if (!tournaments.Any())
        {
            _view.DisableOpenTournament();
        }
        else
        {
            _view.BindTournaments(tournaments.ToList());
        }
    }

    public void NewTournament()
    {
        var tournament = _view.CreateNewTournament();

        if (tournament.id != null)
        {
            _view.OpenTournament(tournament.id.Value, tournament.name);
        }
    }
}
