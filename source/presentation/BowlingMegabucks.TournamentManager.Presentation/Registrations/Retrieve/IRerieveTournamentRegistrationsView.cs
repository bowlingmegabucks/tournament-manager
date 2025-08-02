namespace BowlingMegabucks.TournamentManager.Registrations.Retrieve;

/// <summary>
/// Represents the view interface for displaying and managing tournament registrations.
/// </summary>
public interface ITournamentRegistrationsView
{
    /// <summary>
    /// Gets the tournament identifier.
    /// </summary>
    TournamentId TournamentId { get; }

    /// <summary>
    /// Binds the list of registrations to the view.
    /// </summary>
    /// <param name="registrations">A collection of tournament registration view models.</param>
    /// <remarks>
    /// Use this method to populate the registration list in the UI.
    /// </remarks>
    void BindRegistrations(IEnumerable<ITournamentRegistrationViewModel> registrations);

    /// <summary>
    /// Prompts the user to confirm an action with the specified message.
    /// </summary>
    /// <param name="message">The confirmation message.</param>
    /// <returns>True if the user confirms; otherwise, false.</returns>
    /// <remarks>
    /// Use this method to show a confirmation dialog to the user.
    /// </remarks>
    bool Confirm(string message);

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <remarks>
    /// Use this method to show error messages in the UI.
    /// </remarks>
    void DisplayError(string message);

    /// <summary>
    /// Displays a message to the user.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <remarks>
    /// Use this method to show informational messages in the UI.
    /// </remarks>
    void DisplayMessage(string message);

    /// <summary>
    /// Removes a registration with the specified identifier from the view.
    /// </summary>
    /// <param name="id">The registration identifier.</param>
    /// <remarks>
    /// Use this method to remove a registration from the UI after deletion.
    /// </remarks>
    void RemoveRegistration(RegistrationId id);

    /// <summary>
    /// Sets the number of entries per division in the view.
    /// </summary>
    /// <param name="divisionEntries">A dictionary mapping division names to entry counts.</param>
    /// <remarks>
    /// Use this method to display division entry counts in the UI.
    /// </remarks>
    void SetDivisionEntries(IDictionary<string, int> divisionEntries);

    /// <summary>
    /// Sets the number of entries per squad in the view.
    /// </summary>
    /// <param name="squadEntries">A dictionary mapping squad names to entry counts.</param>
    /// <remarks>
    /// Use this method to display squad entry counts in the UI.
    /// </remarks>
    void SetSquadEntries(IDictionary<string, int> squadEntries);

    /// <summary>
    /// Sets the number of entries per sweeper in the view.
    /// </summary>
    /// <param name="sweeperEntries">A dictionary mapping sweeper names to entry counts.</param>
    /// <remarks>
    /// Use this method to display sweeper entry counts in the UI.
    /// </remarks>
    void SetSweeperEntries(IDictionary<string, int> sweeperEntries);

    /// <summary>
    /// Binds the squad dates to the view.
    /// </summary>
    /// <param name="squadDates">A dictionary mapping squad IDs to date strings.</param>
    /// <remarks>
    /// Use this method to display squad dates in the UI.
    /// </remarks>
    void BindSquadDates(IDictionary<SquadId, string> squadDates);

    /// <summary>
    /// Prompts the user to update the bowler's name for the specified bowler ID.
    /// </summary>
    /// <param name="id">The bowler identifier.</param>
    /// <returns>The updated bowler name, or null if not updated.</returns>
    /// <remarks>
    /// Use this method to show a dialog for updating a bowler's name.
    /// </remarks>
    string? UpdateBowlerName(BowlerId id);

    /// <summary>
    /// Updates the bowler's name in the view.
    /// </summary>
    /// <param name="bowlerName">The new bowler name.</param>
    /// <remarks>
    /// Use this method to update the displayed bowler name in the UI.
    /// </remarks>
    void UpdateBowlerName(string bowlerName);

    /// <summary>
    /// Updates the super sweeper status for the specified registration.
    /// </summary>
    /// <param name="id">The registration identifier.</param>
    /// <remarks>
    /// Use this method to update the super sweeper status in the UI.
    /// </remarks>
    void UpdateBowlerSuperSweeper(RegistrationId id);
}
