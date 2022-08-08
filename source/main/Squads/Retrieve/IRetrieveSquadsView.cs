
namespace NewEnglandClassic.Squads.Retrieve;
internal interface IView
{
    TournamentId TournamentId { get; }
    
    void BindSquads(IEnumerable<IViewModel> squads);
    
    void Disable();
    
    void DisplayError(string message);

    SquadId? AddSquad(TournamentId tournamentId);

    void RefreshSquads();
}
