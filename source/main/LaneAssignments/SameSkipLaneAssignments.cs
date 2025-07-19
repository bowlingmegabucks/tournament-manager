
namespace BowlingMegabucks.TournamentManager.LaneAssignments;
internal class SameSkip : GenerateCross
{
    protected override short NextLane(int startingLane, IList<short> lanesUsed, short defaultSkip, int previousIndex, short gameNumber)
        => lanesUsed[(previousIndex + ((defaultSkip + 1) * 2)) % lanesUsed.Count];
}
