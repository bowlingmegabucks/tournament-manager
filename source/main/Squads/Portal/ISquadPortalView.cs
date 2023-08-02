namespace NortheastMegabuck.Squads.Portal;
internal interface IView
{
    void SetPortalTitle(string title);

    void SetStartingLane(int startingLane);

    void SetNumberOfLanes(int numberOfLanes);

    void SetMaxPerPair(int maxPerPair);

    void DisplayError(string message);

    void Close();

    SquadId Id { get; }

    bool Confirm(string message);

    void DisplayMessage(string message);
}
