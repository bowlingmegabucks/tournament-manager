
namespace NortheastMegabuck.Bowlers.Update;
internal interface IBowlerNameView
{
    BowlerId Id { get; }

    void DisplayError(string message);

    void Bind(Retrieve.IViewModel viewModel);

    void Disable();
}
