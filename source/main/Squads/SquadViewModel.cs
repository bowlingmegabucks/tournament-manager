namespace NortheastMegabuck.Squads;

internal class ViewModel(Models.Squad squad) : IViewModel
{
    public SquadId Id { get; set; } = squad.Id;

    public TournamentId TournamentId { get; set; } = squad.TournamentId;

    public decimal? EntryFee { get; set; } = squad.EntryFee;

    public decimal? CashRatio { get; set; } = squad.CashRatio;

    public decimal? FinalsRatio { get; set; } = squad.FinalsRatio;

    public DateTime Date { get; set; } = squad.Date;

    public short MaxPerPair { get; set; } = squad.MaxPerPair;

    public short StartingLane { get; set; } = squad.StartingLane;

    public short NumberOfLanes { get; set; } = squad.NumberOfLanes;

    public bool Complete { get; set; } = squad.Complete;

    public short NumberOfGames { get; set; }
}

internal interface IViewModel
{
    SquadId Id { get; set; }

    TournamentId TournamentId { get; set; }

    decimal? EntryFee { get; set; }

    decimal? CashRatio { get; set; }

    decimal? FinalsRatio { get; set; }

    DateTime Date { get; set; }

    short MaxPerPair { get; set; }

    short StartingLane { get; set; }

    short NumberOfLanes { get; set; }

    bool Complete { get; set; }
}
