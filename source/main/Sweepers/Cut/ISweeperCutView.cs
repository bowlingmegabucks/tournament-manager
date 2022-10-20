
namespace NortheastMegabuck.Sweepers.Cut;
internal interface IView
{
    void DisplayError(string message);

    void BindResults(IEnumerable<IViewModel> results);
}
