namespace BowlingMegabucks.TournamentManager.Squads.Results;

/// <summary>
/// Represents a view for displaying squad results, including error display and result binding.
/// </summary>
public interface IView
{
    /// <summary>
    /// Gets the unique identifier for the squad.
    /// </summary>
    SquadId SquadId { get; }

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <remarks>
    /// Use this method to inform the user of errors that occur while displaying squad results.
    /// </remarks>
    void DisplayError(string message);

    /// <summary>
    /// Binds the results to the view for a specific division.
    /// </summary>
    /// <param name="divisionName">The name of the division.</param>
    /// <param name="isHandicap">Indicates whether the results are for a handicap division.</param>
    /// <param name="scores">The collection of result view models to display.</param>
    /// <remarks>
    /// Use this method to display or update the results for a division in the view.
    /// </remarks>
    void BindResults(string divisionName, bool isHandicap, ICollection<IViewModel> scores);
}
