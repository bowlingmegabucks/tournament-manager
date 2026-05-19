
namespace BowlingMegabucks.TournamentManager.LaneAssignments;
internal class StaggeredSkip : GenerateCross
{
    protected override short NextLane(int startingLane, IList<short> lanesUsed, short defaultSkip, int previousIndex, short gameNumber)
        => lanesUsed[(previousIndex + DetermineSkip(gameNumber, defaultSkip)) % lanesUsed.Count];

    private static int DetermineSkip(short gameNumber, short defaultSkip)
    {
        return ((gameNumber - 1) % 4) switch
        {
            0 or 1 => (defaultSkip + 1) * 2,
            2 => (defaultSkip + 2) * 2,
            _ => Math.Max(defaultSkip * 2, 2),//this is because when you do 0 skip game 4 becomes 9 and you don't skip any pairs
        };
    }
}
