namespace NewEnglandClassic.Divisions.Retrieve;

internal interface IView
{
    Guid TournamentId { get; }

    void DisplayError(string message);
    
    void Disable();

    void BindDivisions(IEnumerable<IViewModel> divisions);

    DivisionId? AddDivision(Guid tournamentId);

    void RefreshDivisions();
}
