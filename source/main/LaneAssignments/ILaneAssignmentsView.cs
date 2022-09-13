namespace NortheastMegabuck.LaneAssignments;
internal interface IView
{
    TournamentId TournamentId { get; }
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

    BowlerId? SelectBowler(TournamentId tournamentId, SquadId squadId);

    bool NewRegistration(TournamentId tournamentId, SquadId squadId);

    void DisplayMessage(string message);

    void AddToUnassigned(IViewModel laneAssignment);

    void ClearLanes();

    void ClearUnassigned();
}
