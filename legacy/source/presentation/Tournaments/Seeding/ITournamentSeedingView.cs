namespace BowlingMegabucks.TournamentManager.Tournaments.Seeding;
/// <summary>
/// Represents the view interface for displaying tournament seeding results.
/// </summary>
public interface IView
{
    /// <summary>
    /// Gets the tournament identifier.
    /// </summary>
    TournamentId Id { get; }

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <remarks>
    /// This method is called when an error occurs that needs to be shown to the user.
    /// </remarks>
    void DisplayError(string message);

    /// <summary>
    /// Binds the seeding results to the view for a specific division.
    /// </summary>
    /// <param name="divisionName">The name of the division.</param>
    /// <param name="scores">The collection of view models representing the scores.</param>
    /// <remarks>
    /// This method is called to update the view with the latest seeding results for a division.
    /// </remarks>
    void BindResults(string divisionName, ICollection<IViewModel> scores);
}
