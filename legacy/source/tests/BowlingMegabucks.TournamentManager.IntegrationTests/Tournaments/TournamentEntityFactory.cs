using Bogus;
using BowlingMegabucks.TournamentManager.IntegrationTests.Divisions;
using BowlingMegabucks.TournamentManager.IntegrationTests.Squads;
using BowlingMegabucks.TournamentManager.IntegrationTests.Sweepers;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Tournaments;

internal static class TournamentEntityFactory
{
    public static IEnumerable<Database.Entities.Tournament> Bogus(int count)
        => new TournamentEntityFaker().Generate(count);

    public static Database.Entities.Tournament Bogus(int divisionCount = 0, int squadCount = 0, int sweeperCount = 0)
    {
        var tournament = new TournamentEntityFaker().Generate();

        tournament.Divisions = [.. DivisionEntityFactory.Bogus(divisionCount, tournament.Id)];
        tournament.Squads = [.. SquadEntityFactory.Bogus(squadCount, tournament.Id)];
        tournament.Sweepers = [.. SweeperEntityFactory.Bogus(sweeperCount, tournament.Id)];

        return tournament;
    }
}

internal sealed class TournamentEntityFaker
    : Faker<Database.Entities.Tournament>
{
    public TournamentEntityFaker()
        : this(DateTime.UtcNow.GetHashCode())
    { }

    public TournamentEntityFaker(int seed)
    {
        UseSeed(seed);

        RuleFor(tournament => tournament.Id, _ => TournamentId.New());
        RuleFor(tournament => tournament.Name, faker => $"{faker.Company.Random.Word()} Tournament");

        RuleFor(tournament => tournament.Start, faker => faker.Date.FutureDateOnly());
        RuleFor(tournament => tournament.End, (faker, tournament) => faker.Date.FutureDateOnly(refDate: tournament.Start));

        RuleFor(tournament => tournament.EntryFee, faker => faker.Finance.Amount(min: 50, max: 150, decimals: 0));
        RuleFor(tournament => tournament.Games, faker => faker.Random.Short(3, 8));
        RuleFor(tournament => tournament.BowlingCenter, faker => faker.Company.CompanyName());

        RuleFor(tournament => tournament.FinalsRatio, faker => faker.Random.Decimal(6m, 10m));
        RuleFor(tournament => tournament.CashRatio, faker => faker.Random.Decimal(3m, 5m));
        RuleFor(tournament => tournament.SuperSweperCashRatio, faker => faker.Random.Decimal(1m, 3m));
    }
}