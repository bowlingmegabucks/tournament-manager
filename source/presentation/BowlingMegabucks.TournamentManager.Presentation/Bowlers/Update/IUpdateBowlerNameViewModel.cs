namespace BowlingMegabucks.TournamentManager.Bowlers.Update;

/// <summary>
/// Represents a view model containing the components of a bowler's name.
/// </summary>
public interface INameViewModel
{
    /// <summary>
    /// Gets the first name of the bowler.
    /// </summary>
    string FirstName { get; }

    /// <summary>
    /// Gets the middle initial of the bowler, if any.
    /// </summary>
    string MiddleInitial { get; }

    /// <summary>
    /// Gets the last name of the bowler.
    /// </summary>
    string LastName { get; }

    /// <summary>
    /// Gets the suffix of the bowler's name, if any (e.g., Jr., Sr., III).
    /// </summary>
    string Suffix { get; }
}

internal static class NameViewModelExtensions
{
    public static Models.PersonName ToPersonName(this INameViewModel viewModel)
    {
        return new Models.PersonName
        {
            First = viewModel.FirstName,
            MiddleInitial = viewModel.MiddleInitial,
            Last = viewModel.LastName,
            Suffix = viewModel.Suffix,
        };
    }
}