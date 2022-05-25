namespace NewEnglandClassic.Models;
internal class Squad
{
    public Guid Id { get; set; }

    public Guid TournamentId { get; set; }

    internal Tournament Tournament { get; set; }

    public decimal? CashRatio { get; set; }

    public decimal? FinalsRatio { get; set; }

    public DateTime Date { get; set; }
    
    public int MaxPerPair { get; set; }

    public bool Complete { get; set; }

    public Squad(Database.Entities.TournamentSquad squad)
    {
        Id = squad.Id;
        TournamentId = squad.TournamentId;
        CashRatio = squad.CashRatio;
        FinalsRatio = squad.FinalsRatio;
        Date = squad.Date;
        MaxPerPair = squad.MaxPerPair;
        Complete = squad.Complete;

        Tournament = new Tournament(squad.Tournament);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal Squad()
    {
        Tournament = new Tournament();
    }
}
