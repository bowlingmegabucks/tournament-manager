namespace NortheastMegabuck.Sweepers.Portal;
internal interface IView
{
    void SetPortalTitle(string title);

    int StartingLane { set; }

    int NumberOfLanes { set; }

    int MaxPerPair { set; }

    bool Complete { set; }

    void DisplayMessage(string message);

    void DisplayError(string message);

    void Close();

    SquadId Id { get; }

    bool Confirm(string message);
}
