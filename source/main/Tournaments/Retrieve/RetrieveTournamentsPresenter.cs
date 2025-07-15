using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Tournaments.Retrieve;
internal class Presenter
{
    private readonly IView _view;

    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;

        _adapter = new Lazy<IAdapter>(services.GetRequiredService<IAdapter>);
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

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var tournaments = await Adapter.ExecuteAsync(cancellationToken).ConfigureAwait(true);

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
            _view.OpenTournament(tournament.id.Value, tournament.name, tournament.gamesPerSquad);
        }
    }
}
