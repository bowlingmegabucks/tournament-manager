namespace BowlingMegabucks.TournamentManager.Divisions.Retrieve;

/// <summary>
/// Represents the view for retrieving and displaying divisions, providing methods for error handling, data binding, and division management.
/// </summary>
public interface IView
{
    /// <summary>
    /// Gets the unique identifier of the tournament for which divisions are being retrieved.
    /// </summary>
    TournamentId TournamentId { get; }

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <remarks>
    /// This method is used to present errors that occur during division retrieval or management.
    /// </remarks>
    void DisplayError(string message);

    /// <summary>
    /// Disables the view, preventing further user interaction.
    /// </summary>
    /// <remarks>
    /// This method is typically called when a critical error occurs or when the view should not accept further input.
    /// </remarks>
    void Disable();

    /// <summary>
    /// Binds a collection of division view models to the view for display.
    /// </summary>
    /// <param name="divisions">A collection of division view models to display.</param>
    /// <remarks>
    /// This method is used to present the list of divisions to the user.
    /// </remarks>
    void BindDivisions(IEnumerable<IViewModel> divisions);

    /// <summary>
    /// Initiates the process to add a new division to the specified tournament.
    /// </summary>
    /// <param name="tournamentId">The unique identifier of the tournament to which the division will be added.</param>
    /// <returns>The unique identifier of the newly added division, or null if the operation was cancelled or failed.</returns>
    /// <remarks>
    /// This method is used to prompt the user to add a new division and returns the result of the operation.
    /// </remarks>
    DivisionId? AddDivision(TournamentId tournamentId);

    /// <summary>
    /// Refreshes the list of divisions asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method reloads the division data, typically after an add, update, or delete operation.
    /// </remarks>
    Task RefreshDivisionsAsync(CancellationToken cancellationToken);
}
