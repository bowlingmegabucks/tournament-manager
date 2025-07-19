using Ardalis.SmartEnum;

namespace BowlingMegabucks.TournamentManager.Models;

/// <summary>
/// 
/// </summary>
public sealed class Gender
    : SmartEnum<Gender>
{
    /// <summary>
    /// 
    /// </summary>
    public static readonly Gender Male = new(0, nameof(Male));

    /// <summary>
    /// 
    /// </summary>
    public static readonly Gender Female = new(1, nameof(Female));

    private Gender(int value, string name)
        : base(name, value)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IDictionary<int, string> ToDictionary()
        => List.ToDictionary(gender => gender.Value, gender => gender.Name);
}
