
namespace BowlingMegabucks.TournamentManager.Squads.Results;
internal interface IView
{
    SquadId SquadId { get; }

    void DisplayError(string message);

    void BindResults(string divisionName, bool isHandicap, ICollection<IViewModel> scores);
}
