namespace NewEnglandClassic.Models;

internal class Tournament
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateOnly Start { get; set; }

    public DateOnly End { get; set; }

    public decimal EntryFee { get; set; }

    public short Games { get; set; }

    public decimal FinalsRatio { get; set; }

    public decimal CashRatio { get; set; }

    public string BowlingCenter { get; set; } = string.Empty;

    public bool Completed { get; set; }

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
        BowlingCenter = entity.BowlingCenter;
        Completed = entity.Completed;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal Tournament()
    {

    }
}
