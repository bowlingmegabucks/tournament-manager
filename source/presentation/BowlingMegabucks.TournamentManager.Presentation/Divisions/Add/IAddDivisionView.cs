namespace BowlingMegabucks.TournamentManager.Divisions.Add;

/// <summary>
/// 
/// </summary>
public interface IView 
    : TournamentManager.IView
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="errorMessages"></param>
    void DisplayErrors(IEnumerable<string> errorMessages);

    /// <summary>
    /// 
    /// </summary>
    IViewModel Division { get; }
}
