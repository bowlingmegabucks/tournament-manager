
namespace NortheastMegabuck.Bowlers.Search;
internal class Presenter
{
    private readonly IView _view;

    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    internal Presenter(IView view, IAdapter adapter)
    {
        _view = view;
        _adapter = new Lazy<IAdapter>(() => adapter);
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var bowlers = (await Adapter.ExecuteAsync(_view.SearchCriteria, cancellationToken).ConfigureAwait(true)).ToList();

        if (Adapter.Error != null)
        {
            _view.DisplayError(Adapter.Error.Message);
        }
        else if (bowlers.Count == 0)
        {
            _view.DisplayMessage("No Results");
        }
        else
        {
            _view.BindResults(bowlers.OrderBy(bowler => bowler.LastName).ThenBy(bowler => bowler.FirstName));
        }
    }
}
