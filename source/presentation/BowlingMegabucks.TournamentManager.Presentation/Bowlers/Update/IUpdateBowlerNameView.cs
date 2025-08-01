
namespace BowlingMegabucks.TournamentManager.Bowlers.Update;

/// <summary>
/// 
/// </summary>
public interface IBowlerNameView
    : TournamentManager.IView
{
    /// <summary>
    /// 
    /// </summary>
    BowlerId Id { get; }

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
    INameViewModel BowlerName { get; }

    /// <summary>
    /// 
    /// </summary>
    string FullName { get; }
}
