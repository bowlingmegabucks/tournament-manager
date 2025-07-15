using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Tournaments.Retrieve;
internal class Presenter
{
    private readonly IView _view;

    private readonly IAdapter _adapter;

    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;

        _adapter = services.GetRequiredService<IAdapter>();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockAdapter"></param>
    internal Presenter(IView mockView, IAdapter mockAdapter)
    {
        _view = mockView;
        _adapter = mockAdapter;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var tournaments = await _adapter.ExecuteAsync(cancellationToken).ConfigureAwait(true);

        if (_adapter.Error != null)
        {
            _view.DisplayErrorMessage(_adapter.Error.Message);
            _view.DisableOpenTournament();
        }
        else if (!tournaments.Any())
        {
            _view.DisableOpenTournament();
        }
        else
        {
            _view.BindTournaments([.. tournaments]);
        }
    }

    public void NewTournament()
    {
        var (id, name, gamesPerSquad) = _view.CreateNewTournament();

        if (id != null)
        {
            _view.OpenTournament(id.Value, name, gamesPerSquad);
        }
    }
}
