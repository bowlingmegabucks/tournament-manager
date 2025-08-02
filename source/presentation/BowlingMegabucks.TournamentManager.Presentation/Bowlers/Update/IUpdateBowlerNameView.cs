namespace BowlingMegabucks.TournamentManager.Bowlers.Update;

/// <summary>
/// Represents the view for updating a bowler's name, providing methods for displaying errors, binding data, and accessing bowler name information.
/// </summary>
public interface IBowlerNameView
    : TournamentManager.IView
{
    /// <summary>
    /// Gets the unique identifier of the bowler.
    /// </summary>
    BowlerId Id { get; }

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
    /// Gets the view model containing the bowler's name details.
    /// </summary>
    INameViewModel BowlerName { get; }

    /// <summary>
    /// Gets the full name of the bowler as a formatted string.
    /// </summary>
    string FullName { get; }
}
