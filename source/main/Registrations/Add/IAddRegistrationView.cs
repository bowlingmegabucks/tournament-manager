
namespace NortheastMegabuck.Registrations.Add;
internal interface IView
{    
    void DisplayError(string message);
    
    void Disable();
    
    void BindDivisions(IEnumerable<Divisions.IViewModel> divisions);
    
    void BindSquads(IEnumerable<Squads.IViewModel> squads);
    
    void BindSweepers(IEnumerable<Sweepers.IViewModel> sweepers);

    void BindBowler(Bowlers.Retrieve.IViewModel bowler);

    BowlerId? SelectBowler();

    void Close();

    Bowlers.Add.IViewModel Bowler { get; }

    NortheastMegabuck.DivisionId DivisionId { get; }

    int? Average { get; }

    IEnumerable<SquadId> Squads { get; }

    IEnumerable<SquadId> Sweepers { get; }

    bool SuperSweeper { get; }

    bool IsValid();

    void KeepOpen();

    void DisplayMessage(string message);
}
