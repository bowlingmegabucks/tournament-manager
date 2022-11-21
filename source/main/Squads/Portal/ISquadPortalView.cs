namespace NortheastMegabuck.Squads.Portal;
internal interface IView
{
    void SetPortalTitle(string title);

    int StartingLane { set; }

    int NumberOfLanes { set; }

    int MaxPerPair { set; }

    void DisplayError(string message);

    void Close();

    SquadId Id { get; }

    bool Confirm(string message);

    void DisplayMessage(string message);
}
