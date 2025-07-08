using NortheastMegabuck.Database.Entities;

namespace NortheastMegabuck.Models;

/// <summary>
/// 
/// </summary>
public class Tournament
{
    /// <summary>
    /// 
    /// </summary>
    public TournamentId Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public DateOnly Start { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateOnly End { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal EntryFee { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public short Games { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal FinalsRatio { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal CashRatio { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal SuperSweeperCashRatio { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string BowlingCenter { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public bool Completed { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<Squad> Squads { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<Sweeper> Sweepers { get; init; }

    internal Tournament(Database.Entities.Tournament entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Start = entity.Start;
        End = entity.End;
        EntryFee = entity.EntryFee;
        Games = entity.Games;
        FinalsRatio = entity.FinalsRatio;
        CashRatio = entity.CashRatio;
        SuperSweeperCashRatio = entity.SuperSweperCashRatio;
        BowlingCenter = entity.BowlingCenter;
        Completed = entity.Completed;

        Squads = entity.Squads?.Select(squad => new Squad(squad)).ToList() ?? Enumerable.Empty<Squad>();
        Sweepers = entity.Sweepers?.Select(sweeper => new Sweeper(sweeper)).ToList() ?? Enumerable.Empty<Sweeper>();
    }

    /// <summary>
    /// 
    /// </summary>
    public Tournament()
    {
        Squads = [];
        Sweepers = [];
    }
}