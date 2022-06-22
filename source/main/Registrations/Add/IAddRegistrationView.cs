
namespace NewEnglandClassic.Registrations.Add;
internal interface IView
{
    Guid TournamentId { get; }
    
    void DisplayError(string message);
    
    void Disable();
    
    void BindDivisions(IEnumerable<Divisions.IViewModel> divisions);
    
    void BindSquads(IEnumerable<Squads.IViewModel> squads);
    
    void BindSweepers(IEnumerable<Sweepers.IViewModel> sweepers);

    Guid? SelectBowler();

    void Close();

    Bowlers.Add.IViewModel Bowler { get; }

    Guid Division { get; }

    int? Average { get; }

    IEnumerable<Controls.ISelectedIds> Squads { get; }

    IEnumerable<Controls.ISelectedIds> Sweepers { get; }
}
