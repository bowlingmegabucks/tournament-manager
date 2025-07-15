using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Tournaments.Add;
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
        if (!_view.IsValid())
        {
            _view.KeepOpen();
            return;
        }

        var id = await _adapter.ExecuteAsync(_view.Tournament, cancellationToken).ConfigureAwait(true);

        if (_adapter.Errors.Any())
        {
            _view.KeepOpen();
            _view.DisplayErrors(_adapter.Errors.Select(e => e.Message).ToList());
        }
        else
        {
            _view.DisplayMessage($"{_view.Tournament.TournamentName} successfully added");
            _view.Tournament.Id = id!.Value;
            _view.OkToClose();
            _view.Close();
        }
    }
}
