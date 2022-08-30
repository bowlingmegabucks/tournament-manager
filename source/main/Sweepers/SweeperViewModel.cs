
namespace NortheastMegabuck.Sweepers;
internal class ViewModel : IViewModel
{
    public SquadId Id { get; set; }

    public TournamentId TournamentId { get; set; }

    public decimal EntryFee { get; set; }

    public short Games { get; set; }

    public decimal CashRatio { get; set; }

    public DateTime Date { get; set; }

    public short MaxPerPair { get; set; }

    public short StartingLane { get; set; }

    public short NumberOfLanes { get; set; }

    public bool Complete { get; set; }

    public IDictionary<NortheastMegabuck.Divisions.Id, int?> Divisions { get; set; } = new Dictionary<NortheastMegabuck.Divisions.Id, int?>();

    public ViewModel(Models.Sweeper sweeper)
    {
        Id = sweeper.Id;
        TournamentId = sweeper.TournamentId;
        EntryFee = sweeper.EntryFee;
        Games = sweeper.Games;
        CashRatio = sweeper.CashRatio;
        Date = sweeper.Date;
        MaxPerPair = sweeper.MaxPerPair;
        Complete = sweeper.Complete;
        Divisions = sweeper.Divisions.ToDictionary(division=> division.Key, division=> division.Value);
    }
}

public interface IViewModel
{
    SquadId Id { get; set; }

    TournamentId TournamentId { get; set; }

    decimal EntryFee { get; set; }

    short Games { get; set; }

    decimal CashRatio { get; set; }

    DateTime Date { get; set; }

    short MaxPerPair { get; set; }

    short StartingLane { get; set; }

    short NumberOfLanes { get; set; }

    bool Complete { get; set; }

    IDictionary<NortheastMegabuck.Divisions.Id, int?> Divisions { get; }
}