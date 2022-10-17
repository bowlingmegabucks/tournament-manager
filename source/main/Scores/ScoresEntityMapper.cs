
namespace NortheastMegabuck.Scores;

internal class EntityMapper : IEntityMapper
{
    public Database.Entities.SquadScore Execute(Models.SquadScore score)
        => new()
        {
            BowlerId = score.BowlerId,
            SquadId = score.SquadId,
            Game = score.GameNumber,
            Score = score.Score
        };
}

internal interface IEntityMapper
{
    Database.Entities.SquadScore Execute(Models.SquadScore score);
}