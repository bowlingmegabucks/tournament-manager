namespace BowlingMegabucks.TournamentManager.Divisions.Retrieve;

internal interface IView
{
    TournamentId TournamentId { get; }

    void DisplayError(string message);

    void Disable();

    void BindDivisions(IEnumerable<IViewModel> divisions);

    BowlingMegabucks.TournamentManager.DivisionId? AddDivision(TournamentId tournamentId);

    Task RefreshDivisionsAsync(CancellationToken cancellationToken);
}
