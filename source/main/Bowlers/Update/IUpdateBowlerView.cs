
namespace BowlingMegabucks.TournamentManager.Bowlers.Update;
internal interface IView
{
    void DisplayError(string message);

    void DisplayErrors(IEnumerable<string> messages);

    void Bind(Retrieve.IViewModel viewModel);

    void Disable();

    void OkToClose();

    IViewModel Bowler { get; }

    bool IsValid();

    void KeepOpen();
    void DisplayMessage(string message);
}
