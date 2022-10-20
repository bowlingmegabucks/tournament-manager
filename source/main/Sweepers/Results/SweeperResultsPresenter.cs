
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

    public void Execute(SquadId squadId)
    {
        var results = _adapter.Execute(squadId);

        if (_adapter.Error != null)
        {
            _view.DisplayError(_adapter.Error.Message);
        }
        else
        {
            _view.BindResults(results);
        }
    }
}
