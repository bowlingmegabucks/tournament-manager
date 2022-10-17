
namespace NortheastMegabuck.Scores.Update;
internal interface IView
{
    IEnumerable<IViewModel> Scores { get; }

    void DisplayError(string message);

    void DisplayMessage(string message);

    void KeepOpen();
}
