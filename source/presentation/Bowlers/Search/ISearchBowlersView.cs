namespace BowlingMegabucks.TournamentManager.Bowlers.Search;

/// <summary>
/// Represents the view for searching bowlers, providing methods to display results and messages, and access search criteria.
/// </summary>
public interface IView
{
    /// <summary>
    /// Binds the search results to the view for display.
    /// </summary>
    /// <param name="bowlers">A collection of bowler view models representing the search results.</param>
    void BindResults(IEnumerable<IViewModel> bowlers);

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    void DisplayError(string message);

    /// <summary>
    /// Displays an informational message to the user.
    /// </summary>
    /// <param name="message">The message to display.</param>
    void DisplayMessage(string message);

    /// <summary>
    /// Gets the current search criteria entered by the user.
    /// </summary>
    Models.BowlerSearchCriteria SearchCriteria { get; }
}
