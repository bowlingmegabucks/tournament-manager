namespace BowlingMegabucks.TournamentManager.Sweepers.Results;

/// <summary>
/// Represents a view for displaying sweeper results, including error display and result binding.
/// </summary>
public interface IView
{
    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <remarks>
    /// Use this method to inform the user of errors that occur while displaying sweeper results.
    /// </remarks>
    void DisplayError(string message);

    /// <summary>
    /// Binds the results to the view.
    /// </summary>
    /// <param name="results">The collection of result view models to display.</param>
    /// <remarks>
    /// Use this method to display or update the results in the view.
    /// </remarks>
    void BindResults(ICollection<IViewModel> results);
}
