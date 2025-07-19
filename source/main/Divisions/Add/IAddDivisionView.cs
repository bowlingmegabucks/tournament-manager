namespace BowlingMegabucks.TournamentManager.Divisions.Add;
internal interface IView : BowlingMegabucks.TournamentManager.IView
{
    void DisplayErrors(IEnumerable<string> errorMessages);

    IViewModel Division { get; }
}
