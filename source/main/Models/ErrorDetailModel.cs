
namespace NewEnglandClassic.Models;
internal class ErrorDetail
{
    public string Message { get; internal set; }

    public int ReturnCode { get; internal set; }

    public ErrorDetail(Exception ex) : this(ex.Message)
    {

    }

    public ErrorDetail(string message) : this(message, -1)
    {

    }

    public ErrorDetail(string message, int returnCode)
    {
        Message = message;
        ReturnCode = returnCode;
    }
}
