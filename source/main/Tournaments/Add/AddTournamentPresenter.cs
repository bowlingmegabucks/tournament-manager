using Microsoft.Extensions.Configuration;

namespace NewEnglandClassic.Tournaments.Add;
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
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockAdapter"></param>
    internal Presenter(IView mockView, IAdapter mockAdapter)
    {
        _view = mockView;
        _adapter = mockAdapter;
    }

    public void Execute()
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();
            return;
        }

        var id = _adapter.Execute(_view.Tournament);

        if (_adapter.Errors.Any())
        {
            _view.KeepOpen();
            _view.DisplayErrors(_adapter.Errors.Select(e => e.Message).ToList());
        }
        else
        {
            _view.DisplayMessage($"{_view.Tournament.TournamentName} successfully added");
            _view.Tournament.Id = id!.Value;
            _view.Close();
        }
    }
}
