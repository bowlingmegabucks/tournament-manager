namespace NortheastMegabuck.Tournaments.Add;
internal interface IView : NortheastMegabuck.IView
{
    void DisplayErrors(IEnumerable<string> errors);
    
    IViewModel Tournament { get; }
}
