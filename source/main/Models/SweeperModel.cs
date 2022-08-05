
namespace NewEnglandClassic.Models;
internal class Sweeper
{
    public Guid Id { get; set; }

    public Guid TournamentId { get; set; }

    internal Tournament? Tournament { get; set; }

    public decimal EntryFee { get; set; }

    public short Games { get; set; }

    public decimal CashRatio { get; set; }

    public DateTime Date { get; set; }

    public short MaxPerPair { get; set; }

    public short StartingLane { get; set; }

    public short NumberOfLanes { get;set; }

    public bool Complete { get; set; }

    public IDictionary<DivisionId, int?> Divisions { get; set; }

    public Sweeper(Sweepers.IViewModel viewModel)
    {
        Id = viewModel.Id;
        TournamentId = viewModel.TournamentId;
        EntryFee = viewModel.EntryFee;
        Games = viewModel.Games;
        CashRatio = viewModel.CashRatio;
        Date = viewModel.Date;
        MaxPerPair = viewModel.MaxPerPair;
        StartingLane = viewModel.StartingLane;
        NumberOfLanes = viewModel.NumberOfLanes;
        Complete = viewModel.Complete;
        Divisions = viewModel.Divisions;
    }

    public Sweeper(Database.Entities.SweeperSquad sweeper)
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
        Divisions = sweeper.Divisions.ToDictionary(division => division.DivisionId, division => division.BonusPinsPerGame);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal Sweeper()
    {
        Divisions = new Dictionary<DivisionId, int?>();
    }
}
