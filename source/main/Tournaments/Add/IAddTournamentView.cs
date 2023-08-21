namespace NortheastMegabuck.Tournaments.Add;
internal interface IView : NortheastMegabuck.IView
{
    void DisplayErrors(IEnumerable<string> errorMessages);

    void OkToClose();

    IViewModel Tournament { get; }
}
