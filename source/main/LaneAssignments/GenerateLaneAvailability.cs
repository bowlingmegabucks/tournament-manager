
namespace NortheastMegabuck.LaneAssignments;
internal class LaneAvailability : ILaneAvailability
{
    IEnumerable<string> ILaneAvailability.Generate(int startingLane, int numberOfLanes, int maxPerPair)
    {
        if (startingLane % 2 == 0)
        {
            throw new InvalidOperationException("Starting lane must be odd");
        }

        if (numberOfLanes % 2 == 1)
        {
            throw new InvalidOperationException("Number of lanes must be even");
        }

        var oddLetters = new List<string> { "A" };
        var evenLetters = new List<string>();

        switch (maxPerPair)
        {
            case 2:
                evenLetters.Add("B");
                break;
            case 3:
                evenLetters.Add("B");
                evenLetters.Add("C");
                break;
            case 4:
                oddLetters.Add("B");
                evenLetters.Add("C");
                evenLetters.Add("D");
                break;
            case 5:
                oddLetters.Add("B");
                evenLetters.Add("C");
                evenLetters.Add("D");
                evenLetters.Add("E");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(maxPerPair));
        }

        var laneAssignments = new List<string>();

        for (var i = startingLane; i <= startingLane + numberOfLanes - 1; i += 2)
        {
            laneAssignments.AddRange(oddLetters.Select(letter => $"{i}{letter}"));
            laneAssignments.AddRange(evenLetters.Select(letter => $"{i + 1}{letter}"));
        }

        return laneAssignments;
    }
}

internal interface ILaneAvailability
{
    IEnumerable<string> Generate(int startingLane, int numberOfLanes, int maxPerPair);
}
