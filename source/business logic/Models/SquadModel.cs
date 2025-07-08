namespace NortheastMegabuck.Models;

/// <summary>
/// 
/// </summary>
public class Squad
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
    public decimal? CashRatio { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal? FinalsRatio { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal? EntryFee { get; set; }

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

    internal Squad(Database.Entities.TournamentSquad squad)
    {
        Id = squad.Id;
        TournamentId = squad.TournamentId;
        CashRatio = squad.CashRatio;
        FinalsRatio = squad.FinalsRatio;
        Date = squad.Date;
        MaxPerPair = squad.MaxPerPair;
        StartingLane = squad.StartingLane;
        NumberOfLanes = squad.NumberOfLanes;
        Complete = squad.Complete;
        EntryFee = squad.EntryFee;
    }

    /// <summary>
    /// 
    /// </summary>
    public Squad()
    {
        Tournament = new Tournament();
        Id = SquadId.New();
    }
}
