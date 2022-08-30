
namespace NortheastMegabuck.Sweepers.Add;
internal interface IView
{
    IViewModel Sweeper { get; }

    void DisplayError(string message);

    void DisplayMessage(string message);

    void Disable();

    void KeepOpen();

    void BindDivisions(IEnumerable<Divisions.IViewModel> divisions);

    TournamentId TournamentId { get; }

    bool IsValid();

    void Close();
}
