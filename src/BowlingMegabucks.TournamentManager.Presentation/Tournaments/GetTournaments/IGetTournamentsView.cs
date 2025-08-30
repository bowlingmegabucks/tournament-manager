namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;

/// <summary>
/// Represents the view interface for retrieving and managing tournaments.
/// </summary>
public interface IGetTournamentsView
{
    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <remarks>
    /// This method is called when an error occurs that needs to be shown to the user.
    /// </remarks>
    void DisplayErrorMessage(string message);

    /// <summary>
    /// Disables the option to open a tournament in the view.
    /// </summary>
    /// <remarks>
    /// This method is called when opening a tournament is not allowed or not possible.
    /// </remarks>
    void DisableOpenTournament();

    /// <summary>
    /// Binds the list of tournaments to the view.
    /// </summary>
    /// <param name="tournaments">The collection of tournament view models to display.</param>
    /// <remarks>
    /// This method is called to update the view with the available tournaments.
    /// </remarks>
    void BindTournaments(IReadOnlyCollection<TournamentSummaryViewModel> tournaments);

    /// <summary>
    /// Prompts the user to create a new tournament and returns the details.
    /// </summary>
    /// <returns>A tuple containing the tournament id, name, and games per squad.</returns>
    /// <remarks>
    /// This method is called when the user initiates the creation of a new tournament.
    /// </remarks>
    (Guid? id, string name, short gamesPerSquad) CreateNewTournament();

    /// <summary>
    /// Opens the specified tournament in the view.
    /// </summary>
    /// <param name="id">The tournament identifier.</param>
    /// <param name="tournamentName">The name of the tournament.</param>
    /// <param name="gamesPerSquad">The number of games per squad.</param>
    /// <remarks>
    /// This method is called to open and display the selected tournament.
    /// </remarks>
    void OpenTournament(Guid id, string tournamentName, short gamesPerSquad);
}
