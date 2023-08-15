
namespace NortheastMegabuck.Sweepers.Results;
internal class Presenter
{
    private readonly IView _view;
    private readonly IAdapter _adapter;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _adapter = new Adapter(config);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockAdapter"></param>
    internal Presenter(IView mockView, IAdapter mockAdapter)
    {
        _view = mockView;
        _adapter = mockAdapter;
    }

    public async Task ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
    {
        var results = await _adapter.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        if (_adapter.Error != null)
        {
            _view.DisplayError(_adapter.Error.Message);
        }
        else
        {
            _view.BindResults(results.ToList());
        }
    }

    public async Task ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var results = await _adapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(true);

        if (_adapter.Error != null)
        {
            _view.DisplayError(_adapter.Error.Message);
        }
        else
        {
            _view.BindResults(results.ToList());
        }
    }
}
