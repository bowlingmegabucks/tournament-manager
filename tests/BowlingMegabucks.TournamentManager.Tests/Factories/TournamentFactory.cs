using Bogus;
using BowlingMegabucks.TournamentManager.Domain;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Tests.Factories;

public static class TournamentFactory
{
    private static readonly Faker<Tournament> s_faker = new Faker<Tournament>()
        .CustomInstantiator(faker => CreateFakeTournament(faker));

    public static Tournament FakeSingle()
        => s_faker.Generate();

    public static IEnumerable<Tournament> FakeMany(int count)
        => s_faker.Generate(count);

    private static Tournament CreateFakeTournament(Faker faker)
    {
        string tournamentName = faker.Company.CatchPhrase();
        DateOnlyRange tournamentDates = new(
            faker.Date.PastDateOnly(),
            faker.Date.FutureDateOnly()
        );
        decimal entryFee = faker.Finance.Amount(80, 200);
        short games = (short)faker.Random.Int(3, 8);
        Ratio finalsRatio = Ratio.Create(Math.Round(faker.Random.Decimal(6, 10),1)).Value;
        Ratio cashRatio = Ratio.Create(Math.Round(faker.Random.Decimal(4, 8),1)).Value;
        Ratio superSweeperCashRatio = Ratio.Create(Math.Round(faker.Random.Decimal(5, 9),1)).Value;
        string bowlingCenter = faker.Company.CompanyName();

        ErrorOr<Tournament> tournament = Tournament.Create(
            tournamentName,
            tournamentDates,
            entryFee,
            games,
            finalsRatio,
            cashRatio,
            superSweeperCashRatio,
            bowlingCenter
        );

        return !tournament.IsError
            ? tournament.Value
            : throw new InvalidOperationException("Failed to create tournament");
    }
}
