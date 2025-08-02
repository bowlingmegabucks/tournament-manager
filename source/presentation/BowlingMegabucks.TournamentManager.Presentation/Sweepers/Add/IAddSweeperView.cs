namespace BowlingMegabucks.TournamentManager.Sweepers.Add;

/// <summary>
/// Represents a view for adding a sweeper, providing methods to display errors, bind divisions, and manage sweeper data.
/// </summary>
public interface IView
{
    /// <summary>
    /// Gets the sweeper view model containing sweeper details.
    /// </summary>
    IViewModel Sweeper { get; }

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <remarks>
    /// Use this method to inform the user of errors that occur during sweeper addition.
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
    /// Disables the view, preventing further user interaction.
    /// </summary>
    /// <remarks>
    /// Call this method to make the view read-only or inactive, such as during processing or error states.
    /// </remarks>
    void Disable();

    /// <summary>
    /// Keeps the view open, preventing it from closing.
    /// </summary>
    /// <remarks>
    /// Call this method when the view should remain open, such as after a validation error.
    /// </remarks>
    void KeepOpen();

    /// <summary>
    /// Binds division data to the view.
    /// </summary>
    /// <param name="divisions">The collection of division view models.</param>
    /// <remarks>
    /// Use this method to display or update divisions in the view.
    /// </remarks>
    void BindDivisions(IEnumerable<Divisions.IViewModel> divisions);

    /// <summary>
    /// Gets the unique identifier for the tournament.
    /// </summary>
    TournamentId TournamentId { get; }

    /// <summary>
    /// Determines whether the current view state is valid.
    /// </summary>
    /// <returns><c>true</c> if the view is valid; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Use this method to validate the view before attempting to add a sweeper.
    /// </remarks>
    bool IsValid();

    /// <summary>
    /// Closes the view.
    /// </summary>
    /// <remarks>
    /// Call this method to close the view when the operation is complete or cancelled.
    /// </remarks>
    void Close();
}
