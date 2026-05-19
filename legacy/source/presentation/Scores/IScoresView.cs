namespace BowlingMegabucks.TournamentManager.Scores;

/// <summary>
/// Represents a view for displaying and interacting with squad scores and lane assignments.
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
    /// Use this method to inform the user of errors that occur during squad score operations.
    /// </remarks>
    void DisplayError(string message);

    /// <summary>
    /// Disables the view, preventing further user interaction.
    /// </summary>
    /// <remarks>
    /// Call this method to make the view read-only or inactive, such as during processing.
    /// </remarks>
    void Disable();

    /// <summary>
    /// Binds lane assignment data to the view.
    /// </summary>
    /// <param name="laneAssignments">The collection of lane assignment view models.</param>
    /// <remarks>
    /// Use this method to display or update lane assignments in the view.
    /// </remarks>
    void BindLaneAssignments(IEnumerable<LaneAssignments.IViewModel> laneAssignments);

    /// <summary>
    /// Binds squad score data to the view.
    /// </summary>
    /// <param name="squadScores">The collection of squad score view models.</param>
    /// <remarks>
    /// Use this method to display or update squad scores in the view.
    /// </remarks>
    void BindSquadScores(IEnumerable<IViewModel> squadScores);
}
