namespace NortheastMegabuck.Squads;

internal class ViewModel : IViewModel
{
    public SquadId Id { get; set; }

    public TournamentId TournamentId { get; set; }

    public decimal? CashRatio { get; set; }

    public decimal? FinalsRatio { get; set; }

    public DateTime Date { get; set; }

    public short MaxPerPair { get; set; }

    public short StartingLane { get; set; }

    public short NumberOfLanes { get; set; }

    public bool Complete { get; set; }

    public ViewModel(Models.Squad squad)
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
    }
}

public interface IViewModel
{
    SquadId Id { get; set; }

    TournamentId TournamentId { get; set; }

    decimal? CashRatio { get; set; }
    
    decimal? FinalsRatio { get; set; }

    DateTime Date { get; set; }

    short MaxPerPair { get; set; }

    short StartingLane { get; set; }

    short NumberOfLanes { get; set; }

    bool Complete { get; set; }
}
