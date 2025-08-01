
namespace BowlingMegabucks.TournamentManager.Bowlers.Search;

/// <summary>
/// 
/// </summary>
public interface IView
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bowlers"></param>
    void BindResults(IEnumerable<IViewModel> bowlers);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    void DisplayError(string message);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    void DisplayMessage(string message);

    /// <summary>
    /// 
    /// </summary>
    Models.BowlerSearchCriteria SearchCriteria { get; }
}
