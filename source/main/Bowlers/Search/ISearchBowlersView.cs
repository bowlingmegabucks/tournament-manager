
namespace NortheastMegabuck.Bowlers.Search;
internal interface IView
{
    void BindResults(IEnumerable<IViewModel> bowlers);

    void DisplayError(string message);

    void DisplayMessage(string message);
    
    Models.BowlerSearchCriteria SearchCriteria { get; }
}
