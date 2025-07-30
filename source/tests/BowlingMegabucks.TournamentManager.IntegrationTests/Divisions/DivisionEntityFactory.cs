using Bogus;
using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Divisions;

internal static class DivisionEntityFactory
{
    public static IEnumerable<Database.Entities.Division> Bogus(int count, TournamentId tournamentId)
        => new DivisionEntityFaker(tournamentId).Generate(count);
}

internal sealed class DivisionEntityFaker
    : Faker<Database.Entities.Division>
{
    public DivisionEntityFaker(TournamentId tournamentId)
        : this(DateTime.UtcNow.GetHashCode(), tournamentId)
    { }

    public DivisionEntityFaker(int seed, TournamentId tournamentId)
    {
        UseSeed(seed);

        RuleFor(division => division.Id, _ => DivisionId.New());
        RuleFor(division => division.TournamentId, _ => tournamentId);
        RuleFor(division => division.Number, faker => faker.Random.Short(1, 10));

        RuleFor(division => division.Name, faker => $"{faker.Company.Random.Word()} Division");

        RuleFor(division => division.MinimumAge, faker => faker.Random.Short(18, 60).OrNull(faker, 0.5f));
        RuleFor(division => division.MaximumAge, faker => faker.Random.Short(18, 60).OrNull(faker, 0.5f));

        RuleFor(division => division.Gender, faker => faker.PickRandom(Gender.Male, Gender.Female).OrNull(faker, 0.5f));

        RuleFor(division => division.HandicapPercentage, faker => faker.Random.Decimal(0.8m, 1.2m).OrNull(faker, 0.5f));
        RuleFor(division => division.HandicapBase, faker => faker.Random.Int(200, 240).OrNull(faker, 0.5f));
        RuleFor(division => division.MaximumHandicapPerGame, faker => faker.Random.Int(0,30).OrNull(faker, 0.5f));
    }
}