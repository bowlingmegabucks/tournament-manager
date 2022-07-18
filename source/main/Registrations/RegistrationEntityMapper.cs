
namespace NewEnglandClassic.Registrations;
internal class EntityMapper : IEntityMapper
{
    public Database.Entities.Registration Execute(Models.Registration registration)
        => new()
        {
            Id = registration.Id,
            BowlerId = registration.Bowler.Id,
            DivisionId = registration.Division.Id,
            Average = registration.Average,
            Squads = registration.Squads.Union(registration.Sweepers).Select(
                id => new Database.Entities.SquadRegistration
                {
                    RegistrationId = registration.Id,
                    SquadId = id
                }).ToList()
        };
}

internal interface IEntityMapper
{
    Database.Entities.Registration Execute(Models.Registration registration);
}