namespace BowlingMegabucks.TournamentManager.Scores.Update;

/// <summary>
/// Represents a view for updating scores in the tournament manager.
/// </summary>
public interface IView
{
    /// <summary>
    /// Gets the collection of score view models.
    /// </summary>
    IEnumerable<IViewModel> Scores { get; }

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <remarks>
    /// Use this method to inform the user of errors that occur during score updates.
    /// </remarks>
    void DisplayError(string message);

    /// <summary>
    /// Displays a general message to the user.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <remarks>
    /// Use this method to provide feedback or information to the user.
    /// </remarks>
    void DisplayMessage(string message);

    /// <summary>
    /// Keeps the view open, preventing it from closing.
    /// </summary>
    /// <remarks>
    /// Call this method when the view should remain open, such as after a validation error.
    /// </remarks>
    void KeepOpen();

    /// <summary>
    /// Allows the view to close.
    /// </summary>
    /// <remarks>
    /// Call this method when it is safe for the view to close, such as after a successful operation.
    /// </remarks>
    void OkToClose();
}
