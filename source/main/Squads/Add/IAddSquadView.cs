namespace NewEnglandClassic.Squads.Add;

internal interface IView : NewEnglandClassic.IView
{
    void SetTournamentFinalsRatio(string ratio);

    void SetTournamentCashRatio(string ratio);

    IViewModel Squad { get; }

    void DisplayError(string message);
}
