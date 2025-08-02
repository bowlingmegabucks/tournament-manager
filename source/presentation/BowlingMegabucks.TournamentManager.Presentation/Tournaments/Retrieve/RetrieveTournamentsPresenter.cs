using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Tournaments.Retrieve;
/// <summary>
/// Handles the presentation logic for retrieving and managing tournaments.
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
    /// Executes the workflow to retrieve tournaments asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token for the async operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method fetches tournaments, handles errors, and updates the view accordingly.
    /// </remarks>
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

    /// <summary>
    /// Initiates the creation of a new tournament.
    /// </summary>
    /// <remarks>
    /// This method prompts the view to create a new tournament and opens it if successful.
    /// </remarks>
    public void NewTournament()
    {
        var (id, name, gamesPerSquad) = _view.CreateNewTournament();

        if (id != null)
        {
            _view.OpenTournament(id.Value, name, gamesPerSquad);
        }
    }
}
