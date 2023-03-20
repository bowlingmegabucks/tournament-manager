
using System.Text;

namespace NortheastMegabuck.Models;
internal class PersonName
{
    public string First { get; set; } = string.Empty;

    public string MiddleInitial { get; set; } = string.Empty;

    public string Last { get; set; } = string.Empty;

    public string Suffix { get; set; } = string.Empty;

    public PersonName()
    {

    }

    public PersonName(Bowlers.Update.INameViewModel viewModel)
    {
        First = viewModel.FirstName;
        MiddleInitial = viewModel.MiddleInitial;
        Last = viewModel.LastName;
        Suffix = viewModel.Suffix;
    }

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
