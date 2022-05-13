namespace NewEnglandClassic.Divisions;
internal class EntityMapper : IEntityMapper
{
    Database.Entities.Division IEntityMapper.Execute(Models.Division division)
        => new()
        {
            Id = division.Id,
            Number = division.Number,
            Name = division.Name,
            TournamentId = division.TournamentId,
            MinimumAge = division.MinimumAge,
            MaximumAge = division.MaximumAge,
            MinimumAverage = division.MinimumAverage,
            MaximumAverage = division.MaximumAverage,
            HandicapPercentage = division.HandicapPercentage,
            HandicapBase = division.HandicapBase,
            MaximumHandicapPerGame = division.MaximumHandicapPerGame,
            Gender = division.Gender
        };
}

internal interface IEntityMapper
{
    Database.Entities.Division Execute(Models.Division division);
}