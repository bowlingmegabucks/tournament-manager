namespace BowlingMegabucks.TournamentManager.Sweepers.Portal;

/// <summary>
/// Represents a view for the sweeper portal, providing methods to display and update sweeper portal details.
/// </summary>
public interface IView
{
    /// <summary>
    /// Sets the portal title in the view.
    /// </summary>
    /// <param name="title">The title to display.</param>
    /// <remarks>
    /// Use this method to update the portal's title based on the current sweeper or context.
    /// </remarks>
    void SetPortalTitle(string title);

    /// <summary>
    /// Sets the starting lane in the view.
    /// </summary>
    /// <param name="startingLane">The starting lane number.</param>
    /// <remarks>
    /// Use this method to display the starting lane for the sweeper.
    /// </remarks>
    void SetStartingLane(int startingLane);

    /// <summary>
    /// Sets the number of lanes in the view.
    /// </summary>
    /// <param name="numberOfLanes">The number of lanes.</param>
    /// <remarks>
    /// Use this method to display the total number of lanes for the sweeper.
    /// </remarks>
    void SetNumberOfLanes(int numberOfLanes);

    /// <summary>
    /// Sets the maximum number of bowlers per pair in the view.
    /// </summary>
    /// <param name="maxPerPair">The maximum number of bowlers per pair.</param>
    /// <remarks>
    /// Use this method to display the max per pair value for the sweeper.
    /// </remarks>
    void SetMaxPerPair(int maxPerPair);

    /// <summary>
    /// Sets the completion status in the view.
    /// </summary>
    /// <param name="complete">A value indicating whether the sweeper is complete.</param>
    /// <remarks>
    /// Use this method to display or update the completion status for the sweeper.
    /// </remarks>
    void SetComplete(bool complete);

    /// <summary>
    /// Displays a general message to the user.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <remarks>
    /// Use this method to provide feedback or information to the user.
    /// </remarks>
    void DisplayMessage(string message);

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <remarks>
    /// Use this method to inform the user of errors that occur in the portal.
    /// </remarks>
    void DisplayError(string message);

    /// <summary>
    /// Closes the portal view.
    /// </summary>
    /// <remarks>
    /// Call this method to close the portal when the operation is complete or cancelled.
    /// </remarks>
    void Close();

    /// <summary>
    /// Gets the unique identifier for the sweeper squad.
    /// </summary>
    SquadId Id { get; }

    /// <summary>
    /// Prompts the user for confirmation with a message.
    /// </summary>
    /// <param name="message">The confirmation message to display.</param>
    /// <returns><c>true</c> if the user confirms; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Use this method to confirm user actions in the portal.
    /// </remarks>
    bool Confirm(string message);
}
