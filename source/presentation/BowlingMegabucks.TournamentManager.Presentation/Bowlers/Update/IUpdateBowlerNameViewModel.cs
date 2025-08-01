
namespace BowlingMegabucks.TournamentManager.Bowlers.Update;

/// <summary>
/// 
/// </summary>
public interface INameViewModel
{
    /// <summary>
    /// 
    /// </summary>
    string FirstName { get; }

    /// <summary>
    /// 
    /// </summary>
    string MiddleInitial { get; }

    /// <summary>
    /// 
    /// </summary>
    string LastName { get; }

    /// <summary>
    /// 
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