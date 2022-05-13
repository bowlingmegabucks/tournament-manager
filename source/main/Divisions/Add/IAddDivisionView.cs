namespace NewEnglandClassic.Divisions.Add;
internal interface IView : NewEnglandClassic.IView
{
    void DisplayErrors(IEnumerable<string> errors);
    
    IViewModel Division { get; }
}
