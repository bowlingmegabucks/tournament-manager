
namespace BowlingMegabucks.TournamentManager;

/// <summary>
/// 
/// </summary>
public interface IView
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool IsValid();

    /// <summary>
    /// 
    /// </summary>
    void KeepOpen();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    void DisplayMessage(string message);

    /// <summary>
    /// 
    /// </summary>
    void Close();
}
