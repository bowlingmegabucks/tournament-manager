namespace BowlingMegabucks.TournamentManager.Tournaments.Add;
/// <summary>
/// Represents the view interface for adding a tournament.
/// </summary>
internal interface IView 
    : TournamentManager.IView
{
    /// <summary>
    /// Displays error messages to the user.
    /// </summary>
    /// <param name="errorMessages">A collection of error messages to display.</param>
    /// <remarks>
    /// This method is called when validation or processing errors occur.
    /// </remarks>
    void DisplayErrors(IEnumerable<string> errorMessages);

    /// <summary>
    /// Signals that it is okay to close the view.
    /// </summary>
    /// <remarks>
    /// This method is called when the operation completes successfully and the view can be closed.
    /// </remarks>
    void OkToClose();

    /// <summary>
    /// Gets the tournament view model.
    /// </summary>
    IViewModel Tournament { get; }
}
