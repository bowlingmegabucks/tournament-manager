namespace NortheastMegabuck.Sweepers;
internal class EntityMapper : IEntityMapper
{
    public Database.Entities.SweeperSquad Execute(Models.Sweeper sweeper)
        => new()
        {
            Id = sweeper.Id,
            TournamentId = sweeper.TournamentId,
            CashRatio = sweeper.CashRatio,
            Date = sweeper.Date,
            MaxPerPair = sweeper.MaxPerPair,
            Complete = sweeper.Complete,
            EntryFee = sweeper.EntryFee,
            Games = sweeper.Games,
            Divisions = [.. sweeper.Divisions.Select(division => new Database.Entities.SweeperDivision
            {
                SweeperId = sweeper.Id,
                DivisionId = division.Key,
                BonusPinsPerGame = division.Value
            })],
            StartingLane = sweeper.StartingLane,
            NumberOfLanes = sweeper.NumberOfLanes
        };
}

internal interface IEntityMapper
{
    Database.Entities.SweeperSquad Execute(Models.Sweeper sweeper);
}