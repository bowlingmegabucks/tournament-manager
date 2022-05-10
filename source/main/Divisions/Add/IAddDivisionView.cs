namespace NewEnglandClassic.Divisions.Add;
internal interface IView 
{ 
    bool IsValid();

    IViewModel Division { get; }

    void KeepOpen();

    void DisplayErrors(IEnumerable<string> errors);

    void DisplayMessage(string message);

    void Close();
}
