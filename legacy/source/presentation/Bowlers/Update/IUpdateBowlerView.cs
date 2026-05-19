namespace BowlingMegabucks.TournamentManager.Bowlers.Update;

/// <summary>
/// Represents the view for updating a bowler's information, providing methods for displaying errors, binding data, and validating input.
/// </summary>
public interface IView
{
    /// <summary>
    /// Displays a single error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    void DisplayError(string message);

    /// <summary>
    /// Displays multiple error messages to the user.
    /// </summary>
    /// <param name="messages">A collection of error messages to display.</param>
    void DisplayErrors(IEnumerable<string> messages);

    /// <summary>
    /// Binds the specified bowler view model to the view.
    /// </summary>
    /// <param name="viewModel">The bowler view model to bind.</param>
    void Bind(Retrieve.IViewModel viewModel);

    /// <summary>
    /// Disables the view, preventing further user interaction.
    /// </summary>
    void Disable();

    /// <summary>
    /// Closes the view, indicating the operation was successful.
    /// </summary>
    void OkToClose();

    /// <summary>
    /// Gets the view model containing the bowler's information.
    /// </summary>
    IViewModel Bowler { get; }

    /// <summary>
    /// Determines whether the current input in the view is valid.
    /// </summary>
    /// <returns>True if the input is valid; otherwise, false.</returns>
    bool IsValid();

    /// <summary>
    /// Keeps the view open, preventing it from closing.
    /// </summary>
    void KeepOpen();

    /// <summary>
    /// Displays an informational message to the user.
    /// </summary>
    /// <param name="message">The message to display.</param>
    void DisplayMessage(string message);
}
