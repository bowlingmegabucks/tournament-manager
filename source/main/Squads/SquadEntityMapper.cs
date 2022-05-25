namespace NewEnglandClassic.Squads;

internal class EntityMapper : IEntityMapper
{
    public Database.Entities.TournamentSquad Execute(Models.Squad model)
        => new()
        {
            Id = model.Id,
            TournamentId = model.TournamentId,
            CashRatio = model.CashRatio,
            FinalsRatio = model.FinalsRatio,
            Date = model.Date,
            MaxPerPair = model.MaxPerPair,
            Complete = model.Complete
        };
}

internal interface IEntityMapper
{
    Database.Entities.TournamentSquad Execute(Models.Squad model);
}