namespace BowlingMegabucks.TournamentManager;

/// <summary>
/// Represents a base interface for all views in the Tournament Manager application.
/// Provides methods for validation, displaying messages, and controlling view state.
/// </summary>
public interface IView
{
    /// <summary>
    /// Determines whether the current state of the view is valid.
    /// </summary>
    /// <returns><c>true</c> if the view is valid; otherwise, <c>false</c>.</returns>
    bool IsValid();

    /// <summary>
    /// Keeps the view open, preventing it from closing.
    /// </summary>
    void KeepOpen();

    /// <summary>
    /// Displays a message to the user in the view.
    /// </summary>
    /// <param name="message">The message to display to the user.</param>
    void DisplayMessage(string message);

    /// <summary>
    /// Closes the view.
    /// </summary>
    void Close();
}
