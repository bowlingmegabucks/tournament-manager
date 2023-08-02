namespace NortheastMegabuck.Divisions.Add;
internal interface IView : NortheastMegabuck.IView
{
    void DisplayErrors(IEnumerable<string> errorMessages);
    
    IViewModel Division { get; }
}
