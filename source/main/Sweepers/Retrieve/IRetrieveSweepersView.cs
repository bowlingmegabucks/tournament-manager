
namespace NewEnglandClassic.Sweepers.Retrieve;
internal interface IView
{
    Guid TournamentId { get; }
    
    void BindSweepers(IEnumerable<IViewModel> squads);
    
    void Disable();
    
    void DisplayError(string message);

    Guid? AddSweeper(Guid tournamentId);

    void RefreshSweepers();
}
