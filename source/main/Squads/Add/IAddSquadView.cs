namespace BowlingMegabucks.TournamentManager.Squads.Add;

internal interface IView : BowlingMegabucks.TournamentManager.IView
{
    void SetTournamentFinalsRatio(string ratio);

    void SetTournamentCashRatio(string ratio);

    IViewModel Squad { get; }

    void DisplayError(string message);

    void SetTournamentEntryFee(string entryFee);
}
