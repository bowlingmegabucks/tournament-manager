
namespace NortheastMegabuck.LaneAssignments;
internal class SameSkip : Generate
{
    protected override short NextLane(int startingLane, IList<short> lanesUsed, short defaultSkip, int previousIndex, short gameNumber)
        => lanesUsed[(previousIndex + (defaultSkip + 1) * 2) % lanesUsed.Count];
}
