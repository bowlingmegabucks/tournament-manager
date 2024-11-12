namespace NortheastMegabuck.Divisions.Retrieve;

internal interface IView
{
    TournamentId TournamentId { get; }

    void DisplayError(string message);

    void Disable();

    void BindDivisions(IEnumerable<IViewModel> divisions);

    NortheastMegabuck.DivisionId? AddDivision(TournamentId tournamentId);

    Task RefreshDivisionsAsync(CancellationToken cancellationToken);
}
