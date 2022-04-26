
namespace NewEnglandClassic.Tournaments;

internal class EntityMapper : IEntityMapper
{
    Database.Entities.Tournament IEntityMapper.Execute(Models.Tournament tournament)
        => new()
        {
            Id = tournament.Id,
            Name = tournament.Name,
            Start = tournament.Start,
            End = tournament.End,
            BowlingCenter = tournament.BowlingCenter,
            Games = tournament.Games,
            FinalsRatio = tournament.FinalsRatio,
            EntryFee = tournament.EntryFee,
            CashRatio = tournament.CashRatio,
            Completed = tournament.Completed
        };
}

internal interface IEntityMapper
{
    Database.Entities.Tournament Execute(Models.Tournament tournament);
}