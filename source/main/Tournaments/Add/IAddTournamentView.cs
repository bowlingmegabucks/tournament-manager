namespace NewEnglandClassic.Tournaments.Add;
internal interface IView
{
    bool IsValid();

    IViewModel Tournament { get; }

    void KeepOpen();

    void DisplayErrors(IEnumerable<string> errorMessages);
    
    void DisplayMessage(string message);

    void Close();
}
