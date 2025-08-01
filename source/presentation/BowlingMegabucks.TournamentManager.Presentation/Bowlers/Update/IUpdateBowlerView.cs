
namespace BowlingMegabucks.TournamentManager.Bowlers.Update;

/// <summary>
/// 
/// </summary>
public interface IView
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    void DisplayError(string message);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="messages"></param>
    void DisplayErrors(IEnumerable<string> messages);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="viewModel"></param>
    void Bind(Retrieve.IViewModel viewModel);

    /// <summary>
    /// 
    /// </summary>
    void Disable();

    /// <summary>
    /// 
    /// </summary>
    void OkToClose();

    /// <summary>
    /// 
    /// </summary>
    IViewModel Bowler { get; }

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
}
