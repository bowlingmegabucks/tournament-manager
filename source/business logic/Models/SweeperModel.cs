
namespace BowlingMegabucks.TournamentManager.Models;

/// <summary>
/// 
/// </summary>
public class Sweeper
{
    /// <summary>
    /// 
    /// </summary>
    public SquadId Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public TournamentId TournamentId { get; set; }

    internal Tournament? Tournament { get; set; }

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
    public decimal CashRatio { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public short MaxPerPair { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public short StartingLane { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public short NumberOfLanes { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool Complete { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IDictionary<DivisionId, int?> Divisions { get; init; }

    internal Sweeper(Database.Entities.SweeperSquad sweeper)
    {
        Id = sweeper.Id;
        TournamentId = sweeper.TournamentId;
        EntryFee = sweeper.EntryFee;
        Games = sweeper.Games;
        CashRatio = sweeper.CashRatio!.Value;
        Date = sweeper.Date;
        MaxPerPair = sweeper.MaxPerPair;
        StartingLane = sweeper.StartingLane;
        NumberOfLanes = sweeper.NumberOfLanes;
        Complete = sweeper.Complete;
        Divisions = sweeper.Divisions?.ToDictionary(division => division.DivisionId, division => division.BonusPinsPerGame) ?? [];
    }

    /// <summary>
    /// 
    /// </summary>
    public Sweeper()
    {
        Divisions = new Dictionary<BowlingMegabucks.TournamentManager.DivisionId, int?>();
    }
}
