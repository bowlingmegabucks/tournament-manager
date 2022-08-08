namespace NewEnglandClassic.Divisions.Retrieve;

internal interface IView
{
    TournamentId TournamentId { get; }

    void DisplayError(string message);
    
    void Disable();

    void BindDivisions(IEnumerable<IViewModel> divisions);

    DivisionId? AddDivision(TournamentId tournamentId);

    void RefreshDivisions();
}
