
namespace BowlingMegabucks.TournamentManager.Bowlers.Update;
internal interface IBowlerNameView
    : BowlingMegabucks.TournamentManager.IView
{
    BowlerId Id { get; }

    void DisplayError(string message);

    void DisplayErrors(IEnumerable<string> messages);

    void Bind(Retrieve.IViewModel viewModel);

    void Disable();

    void OkToClose();

    INameViewModel BowlerName { get; }

    string FullName { get; }
}
