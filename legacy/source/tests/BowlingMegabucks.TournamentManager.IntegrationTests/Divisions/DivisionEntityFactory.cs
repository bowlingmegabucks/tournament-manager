using Bogus;
using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Divisions;

internal static class DivisionEntityFactory
{
    internal static short NextDivisionNumber = 1;

    public static IEnumerable<Database.Entities.Division> Bogus(int count, TournamentId tournamentId)
        => new DivisionEntityFaker(tournamentId).Generate(count);

    public static Database.Entities.Division Create(
        TournamentId tournamentId,
        string? name = null,
        short? number = null,
        short? minimumAge = null,
        short? maximumAge = null,
        int? minimumAverage = null,
        int? maximumAverage = null,
        decimal? handicapPercentage = null,
        int? handicapBase = null,
        int? maximumHandicapPerGame = null,
        Models.Gender? gender = null)
            => new()
            {
                Id = DivisionId.New(),
                Name = name ?? "Default Division",
                TournamentId = tournamentId,
                Number = number ?? NextDivisionNumber++,

                MinimumAge = minimumAge,
                MaximumAge = maximumAge,

                MinimumAverage = minimumAverage,
                MaximumAverage = maximumAverage,

                HandicapPercentage = handicapPercentage,
                HandicapBase = handicapBase,
                MaximumHandicapPerGame = maximumHandicapPerGame,

                Gender = gender
            };
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
        RuleFor(division => division.Number, _ => DivisionEntityFactory.NextDivisionNumber++);

        RuleFor(division => division.Name, faker => $"{faker.Company.Random.Word()} Division");

        RuleFor(division => division.MinimumAge, faker => faker.Random.Short(18, 60).OrNull(faker, 0.5f));
        RuleFor(division => division.MaximumAge, faker => faker.Random.Short(18, 60).OrNull(faker, 0.5f));

        RuleFor(division => division.Gender, faker => faker.PickRandom(Gender.Male, Gender.Female).OrNull(faker, 0.5f));

        RuleFor(division => division.HandicapPercentage, faker => faker.Random.Decimal(0.8m, 1.2m).OrNull(faker, 0.5f));
        RuleFor(division => division.HandicapBase, faker => faker.Random.Int(200, 240).OrNull(faker, 0.5f));
        RuleFor(division => division.MaximumHandicapPerGame, faker => faker.Random.Int(0,30).OrNull(faker, 0.5f));
    }
}