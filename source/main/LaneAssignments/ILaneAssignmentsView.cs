namespace NortheastMegabuck.LaneAssignments;
internal interface IView
{
    SquadId SquadId { get; }

    int StartingLane { get; }

    int NumberOfLanes { get; }

    int MaxPerPair { get; }

    void BuildLanes(IEnumerable<string> lanes);
}
