
namespace NortheastMegabuck.Models;

/// <summary>
/// 
/// </summary>
public sealed class Division : IEquatable<Division>
{
    /// <summary>
    /// 
    /// </summary>
    public DivisionId Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public short Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public TournamentId TournamentId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public short? MinimumAge { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public short? MaximumAge { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? MinimumAverage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? MaximumAverage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal? HandicapPercentage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? HandicapBase { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? MaximumHandicapPerGame { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Division()
    {
        Name = string.Empty;
        Id = DivisionId.New();
    }

    internal Division(Database.Entities.Division entity)
    {
        Id = entity.Id;
        Number = entity.Number;
        Name = entity.Name;
        TournamentId = entity.TournamentId;
        MinimumAge = entity.MinimumAge;
        MaximumAge = entity.MaximumAge;
        MinimumAverage = entity.MinimumAverage;
        MaximumAverage = entity.MaximumAverage;
        HandicapPercentage = entity.HandicapPercentage;
        HandicapBase = entity.HandicapBase;
        MaximumHandicapPerGame = entity.MaximumHandicapPerGame;
        Gender = entity.Gender;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Division? other)
        => other != null && Id == other.Id;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
        => Equals(obj as Division);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
        => Id.GetHashCode();
}
