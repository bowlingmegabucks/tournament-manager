
namespace NortheastMegabuck.Sweepers.Results;
internal interface IView
{
    void DisplayError(string message);

    void BindResults(IEnumerable<IViewModel> results);
}
