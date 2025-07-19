namespace BowlingMegabucks.TournamentManager.Tournaments.Add;
internal interface IView : BowlingMegabucks.TournamentManager.IView
{
    void DisplayErrors(IEnumerable<string> errorMessages);

    void OkToClose();

    IViewModel Tournament { get; }
}
