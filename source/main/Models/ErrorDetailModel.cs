
namespace NortheastMegabuck.Models;
internal class ErrorDetail(string message, int returnCode)
{
    public string Message { get; internal set; } = message;

    public int ReturnCode { get; internal set; } = returnCode;

    public ErrorDetail(Exception ex) : this(ex.Message)
    {

    }

    public ErrorDetail(string message) : this(message, -1)
    {

    }
}