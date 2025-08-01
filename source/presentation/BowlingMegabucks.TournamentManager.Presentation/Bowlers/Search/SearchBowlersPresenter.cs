
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Bowlers.Search;

/// <summary>
/// 
/// </summary>
public class Presenter
{
    private readonly IView _view;

    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="view"></param>
    /// <param name="services"></param>
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

    /// <summary>
    /// Executes the search operation asynchronously and updates the view with the results.
    /// </summary>
    /// <remarks>This method retrieves a list of bowlers based on the search criteria defined in the view.  If
    /// an error occurs during the operation, the error message is displayed in the view.  If no results are found, a
    /// "No Results" message is displayed. Otherwise, the results are sorted by last name and first name, and then
    /// bound to the view.</remarks>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. Passing a canceled token will terminate the operation.</param>
    /// <returns></returns>
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
