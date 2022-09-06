namespace NortheastMegabuck.Squads.Portal;
internal interface IView
{
    void SetPortalTitle(string title);

    void DisplayError(string message);

    void Close();

    SquadId Id { get; }
}
