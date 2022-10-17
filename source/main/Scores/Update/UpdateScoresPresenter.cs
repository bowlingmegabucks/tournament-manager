
namespace NortheastMegabuck.Scores.Update;

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
        _adapter.Execute(_view.Scores);

        if (_adapter.Errors.Any())
        {
            _view.DisplayError(string.Join(Environment.NewLine, _adapter.Errors.Select(error => error.Message)));
            _view.KeepOpen();
        }
        else
        {
            _view.DisplayMessage("Scores updated");
        }
    }
}
