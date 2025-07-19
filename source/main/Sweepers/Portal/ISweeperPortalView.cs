namespace BowlingMegabucks.TournamentManager.Sweepers.Portal;
internal interface IView
{
    void SetPortalTitle(string title);

    void SetStartingLane(int startingLane);

    void SetNumberOfLanes(int numberOfLanes);

    void SetMaxPerPair(int maxPerPair);

    void SetComplete(bool complete);

    void DisplayMessage(string message);

    void DisplayError(string message);

    void Close();

    SquadId Id { get; }

    bool Confirm(string message);
}
