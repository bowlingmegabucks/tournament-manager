namespace NortheastMegabuck.Tournaments.Add;
internal interface IView : NortheastMegabuck.IView
{
    void DisplayErrors(IEnumerable<string> errorMessages);
    
    IViewModel Tournament { get; }
}
