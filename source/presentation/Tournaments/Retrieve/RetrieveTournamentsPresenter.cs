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
