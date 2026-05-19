namespace BowlingMegabucks.TournamentManager.Squads.Add;

/// <summary>
/// Represents a view for adding a squad, providing methods to display and update tournament details.
/// </summary>
public interface IView 
    : TournamentManager.IView
{
    /// <summary>
    /// Sets the tournament finals ratio in the view.
    /// </summary>
    /// <param name="ratio">The formatted finals ratio string.</param>
    /// <remarks>
    /// Use this method to display the finals ratio for the selected tournament.
    /// </remarks>
    void SetTournamentFinalsRatio(string ratio);

    /// <summary>
    /// Sets the tournament cash ratio in the view.
    /// </summary>
    /// <param name="ratio">The formatted cash ratio string.</param>
    /// <remarks>
    /// Use this method to display the cash ratio for the selected tournament.
    /// </remarks>
    void SetTournamentCashRatio(string ratio);

    /// <summary>
    /// Gets the squad view model containing squad details.
    /// </summary>
    IViewModel Squad { get; }

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <remarks>
    /// Use this method to inform the user of errors that occur during squad addition.
    /// </remarks>
    void DisplayError(string message);

    /// <summary>
    /// Sets the tournament entry fee in the view.
    /// </summary>
    /// <param name="entryFee">The formatted entry fee string.</param>
    /// <remarks>
    /// Use this method to display the entry fee for the selected tournament.
    /// </remarks>
    void SetTournamentEntryFee(string entryFee);
}
