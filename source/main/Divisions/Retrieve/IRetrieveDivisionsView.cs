namespace NewEnglandClassic.Divisions.Retrieve;

internal interface IView
{
    TournamentId TournamentId { get; }

    void DisplayError(string message);
    
    void Disable();

    void BindDivisions(IEnumerable<IViewModel> divisions);

    NewEnglandClassic.Divisions.Id? AddDivision(TournamentId tournamentId);

    void RefreshDivisions();
}
