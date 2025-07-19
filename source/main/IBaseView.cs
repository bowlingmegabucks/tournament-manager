
namespace BowlingMegabucks.TournamentManager;

internal interface IView
{
    bool IsValid();

    void KeepOpen();

    void DisplayMessage(string message);

    void Close();
}
