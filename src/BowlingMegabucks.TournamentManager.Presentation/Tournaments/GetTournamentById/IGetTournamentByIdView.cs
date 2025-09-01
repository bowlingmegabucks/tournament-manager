using ErrorOr;

/// <summary>
/// Represents the view interface for retrieving and displaying a single tournament's details.
/// </summary>
namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournamentById;

public interface IGetTournamentByIdView
{
    /// <summary>
    /// Shows a processing message to the user, typically to indicate that a background operation is in progress.
    /// </summary>
    /// <param name="message">The message to display to the user.</param>
    /// <param name="cancellationTokenSource">The <see cref="CancellationTokenSource"/> that can be used to cancel the processing operation.</param>
    void ShowProcessingMessage(string message, CancellationTokenSource cancellationTokenSource);

    /// <summary>
    /// Hides the currently displayed processing message from the user.
    /// </summary>
    void HideProcessingMessage();

    /// <summary>
    /// Binds the tournament details to the view.
    /// </summary>
    /// <param name="tournament">The tournament details to display.</param>
    void BindTournament(TournamentDetailViewModel tournament);

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="errors">The errors to display.</param>
    void DisplayErrorMessage(IEnumerable<Error> errors);
}
