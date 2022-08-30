
namespace NortheastMegabuck.Sweepers.Retrieve;
internal interface IView
{
    TournamentId TournamentId { get; }
    
    void BindSweepers(IEnumerable<IViewModel> squads);
    
    void Disable();
    
    void DisplayError(string message);

    SquadId? AddSweeper(TournamentId tournamentId);

    void RefreshSweepers();
}
