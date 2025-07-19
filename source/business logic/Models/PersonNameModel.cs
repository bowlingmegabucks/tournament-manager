using System.Globalization;
using System.Text;

namespace BowlingMegabucks.TournamentManager.Models;

/// <summary>
/// 
/// </summary>
public class PersonName
{
    /// <summary>
    /// 
    /// </summary>
    public string First { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string MiddleInitial { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string Last { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string Suffix { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public PersonName()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="suffix"></param>
    /// <returns></returns>
    public static string FullName(string firstName, string lastName, string suffix)
    {
        var name = new StringBuilder($"{firstName} {lastName}");

        if (!string.IsNullOrEmpty(suffix))
        {
            name.Append(CultureInfo.InvariantCulture, $", {suffix}");
        }

        return name.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
        => FullName(First, Last, Suffix);
}
