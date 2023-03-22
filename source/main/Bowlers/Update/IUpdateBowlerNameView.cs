
namespace NortheastMegabuck.Bowlers.Update;
internal interface IBowlerNameView : IView
{
    BowlerId Id { get; }

    void DisplayError(string message);

    void Bind(Retrieve.IViewModel viewModel);

    void Disable();
}
