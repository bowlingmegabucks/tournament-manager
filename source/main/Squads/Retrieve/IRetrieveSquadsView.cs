
namespace NewEnglandClassic.Squads.Retrieve;
internal interface IView
{
    Guid TournamentId { get; }
    
    void BindSquads(IEnumerable<IViewModel> squads);
    
    void Disable();
    
    void DisplayError(string message);

    SquadId? AddSquad(Guid tournamentId);

    void RefreshSquads();
}
