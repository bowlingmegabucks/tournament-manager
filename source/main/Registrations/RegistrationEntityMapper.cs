
namespace NortheastMegabuck.Registrations;
internal class EntityMapper : IEntityMapper
{
    private readonly Bowlers.IEntityMapper _bowlerMapper;

    internal EntityMapper()
    {
        _bowlerMapper = new Bowlers.EntityMapper();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="bowlerMapper"></param>
    internal EntityMapper(Bowlers.IEntityMapper bowlerMapper)
    {
        _bowlerMapper = bowlerMapper;
    }

    public Database.Entities.Registration Execute(Models.Registration registration)
        => new()
        {
            Id = registration.Id,
            BowlerId = registration.Bowler.Id,
            Bowler = _bowlerMapper.Execute(registration.Bowler),
            DivisionId = registration.Division.Id,
            Average = registration.Average,
            Squads = registration.Squads.Select(squad=> squad.Id).Union(registration.Sweepers.Select(sweeper=> sweeper.Id)).Select(
                id => new Database.Entities.SquadRegistration
                {
                    RegistrationId = registration.Id,
                    SquadId = id
                }).ToList(),
            SuperSweeper = registration.SuperSweeper
        };
}

internal interface IEntityMapper
{
    Database.Entities.Registration Execute(Models.Registration registration);
}