
using System.Text;

namespace NortheastMegabuck.Models;
internal class PersonName
{
    public string First { get; set; } = string.Empty;

    public string MiddleInitial { get; set; } = string.Empty;

    public string Last { get; set; } = string.Empty;

    public string Suffix { get; set; } = string.Empty;

    public override string ToString()
    {
        var name = new StringBuilder($"{First} {Last}");

        if (!string.IsNullOrEmpty(Suffix))
        {
            name.Append($", {Suffix}");
        }

        return name.ToString();
    }
}
