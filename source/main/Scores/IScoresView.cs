
namespace BowlingMegabucks.TournamentManager.Scores;
internal interface IView
{
    SquadId SquadId { get; }

    void DisplayError(string message);

    void Disable();

    void BindLaneAssignments(IEnumerable<LaneAssignments.IViewModel> laneAssignments);

    void BindSquadScores(IEnumerable<IViewModel> squadScores);
}
