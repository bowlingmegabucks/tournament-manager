namespace NortheastMegabuck.Divisions.Retrieve;

internal interface IView
{
    TournamentId TournamentId { get; }

    void DisplayError(string message);
    
    void Disable();

    void BindDivisions(IEnumerable<IViewModel> divisions);

    NortheastMegabuck.Divisions.Id? AddDivision(TournamentId tournamentId);

    void RefreshDivisions();
}
