using BowlingMegabucks.TournamentManager.Database.Entities;
using BowlingMegabucks.TournamentManager.IntegrationTests.Squads;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Sweepers;

internal static class SweeperEntityFactory
{
    public static IEnumerable<SweeperSquad> Bogus(int count, TournamentId tournamentId)
        => new SweeperEntityFaker(tournamentId).Generate(count);
}

internal sealed class SweeperEntityFaker
    : SquadEntityFaker<SweeperSquad>
{
    public SweeperEntityFaker(TournamentId tournamentId)
        : this(DateTime.UtcNow.GetHashCode(), tournamentId)
    { }

    public SweeperEntityFaker(int seed, TournamentId tournamentId)
        : base(seed, tournamentId)
    {
        RuleFor(squad => squad.CashRatio, faker => faker.Random.Decimal(0.8m, 1.2m));

        RuleFor(squad => squad.EntryFee, faker => faker.Finance.Amount(min: 50, max: 150, decimals: 0));

        RuleFor(squad => squad.Games, faker => faker.Random.Short(min: 1, max: 10));
    }
}