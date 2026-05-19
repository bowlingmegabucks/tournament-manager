using BowlingMegabucks.TournamentManager.Registrations.Retrieve;

namespace BowlingMegabucks.TournamentManager.Registrations.Update;

/// <summary>
/// Represents the view interface for updating a registration's average.
/// </summary>
public interface IAverageView
{
    /// <summary>
    /// Gets the registration identifier.
    /// </summary>
    RegistrationId RegistrationId { get; }

    /// <summary>
    /// Gets the average score.
    /// </summary>
    int? Average { get; }

    /// <summary>
    /// Binds the bowler view model to the view.
    /// </summary>
    /// <param name="viewModel">The bowler view model.</param>
    /// <remarks>
    /// Use this method to display bowler information in the UI.
    /// </remarks>
    void BindBowler(Bowlers.Retrieve.IViewModel viewModel);

    /// <summary>
    /// Binds the registration view model to the view.
    /// </summary>
    /// <param name="tournamentRegistrationViewModel">The tournament registration view model.</param>
    /// <remarks>
    /// Use this method to display registration information in the UI.
    /// </remarks>
    void BindRegistration(ITournamentRegistrationViewModel tournamentRegistrationViewModel);

    /// <summary>
    /// Disables the view, preventing further user interaction.
    /// </summary>
    /// <remarks>
    /// Use this method to disable the UI when an error occurs or processing is ongoing.
    /// </remarks>
    void Disable();

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
    /// Keeps the view open, preventing it from closing.
    /// </summary>
    /// <remarks>
    /// Use this method to keep the form open when validation fails or errors occur.
    /// </remarks>
    void KeepOpen();

    /// <summary>
    /// Closes the view, allowing the form to close.
    /// </summary>
    /// <remarks>
    /// Use this method to close the form after a successful update or when the user cancels.
    /// </remarks>
    void OkToClose();
}
