
namespace BowlingMegabucks.TournamentManager.Tournaments.Seeding;
internal interface IView
{
    TournamentId Id { get; }

    void DisplayError(string message);

    void BindResults(string divisionName, ICollection<IViewModel> scores);
}
