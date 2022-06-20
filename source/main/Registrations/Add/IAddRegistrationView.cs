
namespace NewEnglandClassic.Registrations.Add;
internal interface IView
{
    Guid TournamentId { get; }
    
    void DisplayError(string message);
    
    void Disable();
    
    void BindDivisions(IEnumerable<Divisions.IViewModel> divisions);
    
    void BindSquads(IEnumerable<Squads.IViewModel> squads);
    
    void BindSweepers(IEnumerable<Sweepers.IViewModel> sweepers);
}
