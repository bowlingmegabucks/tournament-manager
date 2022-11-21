
namespace NortheastMegabuck.Tournaments.Results;

internal interface IView
{
    TournamentId Id { get; }

    void DisplayError(string message);

    void BindResults(string divisionName, IEnumerable<IAtLargeViewModel> results);
}
