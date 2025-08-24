namespace BowlingMegabucks.TournamentManager.Sweepers.Retrieve;

/// <summary>
/// Represents a view for retrieving and managing sweepers, including binding, error display, and sweeper addition.
/// </summary>
public interface IView
{
    /// <summary>
    /// Gets the unique identifier for the tournament.
    /// </summary>
    TournamentId TournamentId { get; }

    /// <summary>
    /// Binds the collection of sweeper view models to the view.
    /// </summary>
    /// <param name="squads">The collection of sweeper view models.</param>
    /// <remarks>
    /// Use this method to display or update the list of sweepers in the view.
    /// </remarks>
    void BindSweepers(IEnumerable<IViewModel> squads);

    /// <summary>
    /// Disables the view, preventing further user interaction.
    /// </summary>
    /// <remarks>
    /// Call this method to make the view read-only or inactive, such as during processing or error states.
    /// </remarks>
    void Disable();

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <remarks>
    /// Use this method to inform the user of errors that occur during sweeper retrieval or management.
    /// </remarks>
    void DisplayError(string message);

    /// <summary>
    /// Prompts the user to add a new sweeper for the specified tournament.
    /// </summary>
    /// <param name="tournamentId">The tournament identifier for which to add a sweeper.</param>
    /// <returns>The identifier of the newly added sweeper, or null if the operation was cancelled.</returns>
    /// <remarks>
    /// Use this method to initiate the process of adding a new sweeper to a tournament.
    /// </remarks>
    SquadId? AddSweeper(TournamentId tournamentId);

    /// <summary>
    /// Refreshes the list of sweepers asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method reloads the sweepers from the data source and updates the view.
    /// </remarks>
    Task RefreshSweepersAsync(CancellationToken cancellationToken);
}
