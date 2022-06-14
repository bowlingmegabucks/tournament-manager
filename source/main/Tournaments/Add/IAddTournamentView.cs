namespace NewEnglandClassic.Tournaments.Add;
internal interface IView : NewEnglandClassic.IView
{
    void DisplayErrors(IEnumerable<string> errors);
    
    IViewModel Tournament { get; }
}
