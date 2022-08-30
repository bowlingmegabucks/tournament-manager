namespace NortheastMegabuck.Squads.Add;

internal interface IView : NortheastMegabuck.IView
{
    void SetTournamentFinalsRatio(string ratio);

    void SetTournamentCashRatio(string ratio);

    IViewModel Squad { get; }

    void DisplayError(string message);
}
