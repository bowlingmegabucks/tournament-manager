
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

    public static string FullName(string firstName, string lastName, string suffix)
    {
        var name = new StringBuilder($"{firstName} {lastName}");

        if (!string.IsNullOrEmpty(suffix))
        {
            name.Append($", {suffix}");
        }

        return name.ToString();
    }

    public override string ToString()
        => FullName(First, Last, Suffix);
}
