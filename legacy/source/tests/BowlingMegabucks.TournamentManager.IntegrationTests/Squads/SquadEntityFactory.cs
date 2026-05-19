using Bogus;

using BowlingMegabucks.TournamentManager.Database.Entities;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Squads;

internal static class SquadEntityFactory
{ 
    public static IEnumerable<TournamentSquad> Bogus(int count, TournamentId tournamentId)
        => new SquadEntityFaker(tournamentId).Generate(count);
}

internal sealed class SquadEntityFaker
    : SquadEntityFaker<TournamentSquad>
{
    public SquadEntityFaker(TournamentId tournamentId)
        : this(DateTime.UtcNow.GetHashCode(), tournamentId)
    { }
    
    public SquadEntityFaker(int seed, TournamentId tournamentId)
        : base(seed, tournamentId)
    {
        RuleFor(squad => squad.CashRatio, faker => faker.Random.Decimal(0.8m, 1.2m).OrNull(faker, 0.5f));

        RuleFor(squad => squad.FinalsRatio, faker => faker.Random.Decimal(6m, 10m));
        RuleFor(squad => squad.EntryFee, faker => faker.Finance.Amount(min: 50, max: 150, decimals: 0));
    }
}