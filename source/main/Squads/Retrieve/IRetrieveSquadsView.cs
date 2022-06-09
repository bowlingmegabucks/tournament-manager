
namespace NewEnglandClassic.Squads.Retrieve;
internal interface IView
{
    void BindSquads(IEnumerable<IViewModel> squads);
    
    void Disable();
    
    void DisplayError(string message);
}
