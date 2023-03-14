namespace NortheastMegabuck.Models;
internal class Squad
{
    public SquadId Id { get; set; }

    public TournamentId TournamentId { get; set; }

    internal Tournament? Tournament { get; set; }

    public decimal? CashRatio { get; set; }

    public decimal? FinalsRatio { get; set; }

    public DateTime Date { get; set; }
    
    public short MaxPerPair { get; set; }

    public short StartingLane { get; set; }

    public short NumberOfLanes { get; set; }

    public bool Complete { get; set; }

    public Squad(Database.Entities.TournamentSquad squad)
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

    public Squad(Squads.IViewModel viewModel)
    {
        Id = viewModel.Id;
        TournamentId = viewModel.TournamentId;
        CashRatio = viewModel.CashRatio;
        FinalsRatio = viewModel.FinalsRatio;
        Date = viewModel.Date;
        MaxPerPair = viewModel.MaxPerPair;
        StartingLane = viewModel.StartingLane;
        NumberOfLanes = viewModel.NumberOfLanes;
        Complete = viewModel.Complete;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal Squad()
    {
        Tournament = new Tournament();
        Id = SquadId.New();
    }
}
