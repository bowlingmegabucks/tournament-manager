namespace NortheastMegabuck.LaneAssignments;
internal interface IView
{
    SquadId SquadId { get; }

    int StartingLane { get; }

    int NumberOfLanes { get; }

    int MaxPerPair { get; }

    void BuildLanes(IEnumerable<string> lanes);

    void DisplayError(string message);

    void Disable();

    void BindRegistrations(IEnumerable<IViewModel> registrations);

    void BindLaneAssignments(IEnumerable<IViewModel> assignments);

    void RemoveLaneAssignment(IViewModel registration);

    void AssignToLane(IViewModel registration, string position);

    IViewModel? AddRegistration(SquadId squadId);
}
