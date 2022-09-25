
namespace NortheastMegabuck.Bowlers.Search;
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
        var bowlers = Adapter.Execute(_view.SearchCriteria).ToList();

        if (Adapter.Error != null)
        {
            _view.DisplayError(Adapter.Error.Message);
        }
        else if (!bowlers.Any())
        {
            _view.DisplayMessage("No Results");
        }
        else
        {
            _view.BindResults(bowlers.OrderBy(bowler => bowler.LastName).ThenBy(bowler => bowler.FirstName));
        } 
    }
}
