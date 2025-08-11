using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Tournaments.Seeding;
/// <summary>
/// Handles the presentation logic for displaying tournament seeding results.
/// </summary>
public class Presenter
{
    private readonly IView _view;
    private readonly IAdapter _adapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class.
    /// </summary>
    /// <param name="view">The view interface for the presenter.</param>
    /// <param name="services">The service provider for dependency injection.</param>
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

    /// <summary>
    /// Executes the workflow to retrieve and display seeding results asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token for the async operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method fetches seeding results, groups them by division, and updates the view. If an error occurs, it is displayed to the user.
    /// </remarks>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var scoresByDivision = (await _adapter.ExecuteAsync(_view.Id, cancellationToken).ConfigureAwait(true)).GroupBy(score => score.DivisionName);

        if (_adapter.Error != null)
        {
            _view.DisplayError(_adapter.Error.Message);
            return;
        }

        foreach (var divisionScores in scoresByDivision)
        {
            _view.BindResults(divisionScores.Key, [.. divisionScores]);
        }
    }
}
