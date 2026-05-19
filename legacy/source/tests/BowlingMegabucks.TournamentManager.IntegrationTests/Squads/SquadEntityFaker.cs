using Bogus;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Squads;

internal abstract class SquadEntityFaker<T>
    : Faker<T> where T : Database.Entities.Squad
{
    protected SquadEntityFaker(TournamentId tournamentId)
        : this(DateTime.UtcNow.GetHashCode(), tournamentId)
    { }

    protected SquadEntityFaker(int seed, TournamentId tournamentId)
    {
        UseSeed(seed);

        RuleFor(squad => squad.Id, _ => SquadId.New());
        RuleFor(squad => squad.TournamentId, _ => tournamentId);

        RuleFor(squad => squad.Date, faker => faker.Date.Future());

        RuleFor(squad => squad.MaxPerPair, faker => faker.Random.Short(2, 6));

        RuleFor(squad => squad.StartingLane, faker => faker.Random.Short(1, 10));
        RuleFor(squad => squad.NumberOfLanes, faker => faker.Random.Short(30, 50));
    }
}