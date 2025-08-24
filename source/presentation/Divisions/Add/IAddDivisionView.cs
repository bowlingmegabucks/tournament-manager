namespace BowlingMegabucks.TournamentManager.Divisions.Add;

/// <summary>
/// Represents the view for adding a division, providing methods for displaying errors and accessing division data.
/// </summary>
public interface IView 
    : TournamentManager.IView
{
    /// <summary>
    /// Displays a collection of error messages to the user.
    /// </summary>
    /// <param name="errorMessages">A collection of error messages to display.</param>
    /// <remarks>
    /// This method is used to present validation or operation errors to the user when adding a division.
    /// </remarks>
    void DisplayErrors(IEnumerable<string> errorMessages);

    /// <summary>
    /// Gets the view model containing the division's data being added.
    /// </summary>
    IViewModel Division { get; }
}
