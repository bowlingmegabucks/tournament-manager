
namespace NewEnglandClassic.Sweepers.Add;
internal interface IView
{
    IViewModel Sweeper { get; }

    void DisplayError(string message);

    void DisplayMessage(string message);

    void Disable();

    void KeepOpen();

    void BindDivisions(IEnumerable<Divisions.IViewModel> divisions);

    Guid TournamentId { get; }

    bool IsValid();

    void Close();
}
