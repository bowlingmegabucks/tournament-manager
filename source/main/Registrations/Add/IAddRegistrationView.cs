
namespace NortheastMegabuck.Registrations.Add;
internal interface IView
{
    void DisplayError(string message);

    void Disable();

    void BindDivisions(IEnumerable<Divisions.IViewModel> divisions);

    void BindSquads(IEnumerable<Squads.IViewModel> squads);

    void BindSquads(IEnumerable<Squads.IViewModel> squads, SquadId squadId);

    void BindSweepers(IEnumerable<Sweepers.IViewModel> sweepers);

    void BindSweepers(IEnumerable<Sweepers.IViewModel> sweepers, SquadId squadId);

    void BindBowler(Bowlers.Retrieve.IViewModel bowler);

    BowlerId? SelectBowler();

    void Close();

    Bowlers.IViewModel Bowler { get; }

    DivisionId DivisionId { get; }

    int? Average { get; }

    IEnumerable<SquadId> Squads { get; }

    IEnumerable<SquadId> Sweepers { get; }

    bool SuperSweeper { get; }

    bool IsValid();

    void KeepOpen();

    void DisplayMessage(string message);
}
