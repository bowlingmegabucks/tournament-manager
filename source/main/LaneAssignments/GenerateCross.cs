
namespace NortheastMegabuck.LaneAssignments;
internal abstract class GenerateCross : IGenerate
{
    IEnumerable<string> IGenerate.Execute(short startingLane, string letter, short games, IList<short> lanesUsed, short defaultSkip)
    {
        var lanes = new List<short> { startingLane };

        for (short gameNumber = 2; gameNumber <= games; gameNumber++)
        {
            var previousLane = lanes[lanes.Count - 1];
            var previousIndex = lanesUsed.IndexOf(previousLane);
            var nextLane = NextLane(startingLane, lanesUsed, defaultSkip, previousIndex, gameNumber);

            lanes.Add(nextLane);
        }

        var adjustedLanes = new List<short>();

        for (var i = 0; i < lanes.Count; i++)
        {
            if (i % 2 == 0)
            {
                adjustedLanes.Add(lanes[i]);
            }
            else if (lanes[i] % 2 == 0) //even lane even game
            {
                adjustedLanes.Add(--lanes[i]);
            }
            else
            {
                adjustedLanes.Add(++lanes[i]);
            }
        }

        return adjustedLanes.Select(lane => $"{lane}{letter}").ToList();
    }

    protected abstract short NextLane(int startingLane, IList<short> lanesUsed, short defaultSkip, int previousIndex, short gameNumber);

    public short DetermineSkip(int lanes)
    {
        return lanes switch
        {
            var _ when lanes <= 16 => 0,
            var _ when lanes <= 24 => 1,
            var _ when lanes <= 32 => 2,
            var _ when lanes <= 40 => 3,
            var _ when lanes <= 48 => 4,
            var _ when lanes <= 56 => 5,
            _ => 6,
        };
    }
}
