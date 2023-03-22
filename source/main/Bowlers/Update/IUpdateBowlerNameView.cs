
namespace NortheastMegabuck.Bowlers.Update;
internal interface IBowlerNameView : IView
{
    BowlerId Id { get; }

    void DisplayError(string message);

    void DisplayErrors(IEnumerable<string> messages);

    void Bind(Retrieve.IViewModel viewModel);

    void Disable();

    INameViewModel BowlerName { get; }

    string FullName { get; }
}
