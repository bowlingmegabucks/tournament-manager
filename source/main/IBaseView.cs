
namespace NewEnglandClassic;

internal interface IView
{
    bool IsValid();

    void KeepOpen();

    void DisplayErrors(IEnumerable<string> errors);

    void DisplayMessage(string message);

    void Close();
}
