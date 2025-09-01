using Bogus;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;

namespace BowlingMegabucks.TournamentManager.Tests.Factories;

public static class TournamentDetailFactory
{
    private static readonly TournamentDetailFaker s_faker = new();

    public static TournamentDetail FakeSingle()
        => s_faker.Generate();

    public static IEnumerable<TournamentDetail> FakeMany(int count)
        => s_faker.Generate(count);
}

internal sealed class TournamentDetailFaker
    : Faker<TournamentDetail>
{
    public TournamentDetailFaker()
    {
        RuleFor(tournament => tournament.Id, _ => TournamentId.New());
        RuleFor(tournament => tournament.Name, f => f.Company.CatchPhrase());
        RuleFor(tournament => tournament.StartDate, f => f.Date.PastDateOnly());
        RuleFor(tournament => tournament.EndDate, (f, t) => f.Date.FutureDateOnly(refDate: t.StartDate));
        RuleFor(tournament => tournament.EntryFee, f => f.Finance.Amount(80, 200));
        RuleFor(tournament => tournament.BowlingCenter, f => f.Company.CompanyName());
        RuleFor(tournament => tournament.Games, f => (short)f.Random.Int(3, 8));
        RuleFor(tournament => tournament.FinalsRatio, f => Math.Round(f.Random.Decimal(6, 9), 1));
        RuleFor(tournament => tournament.CashRatio, f => Math.Round(f.Random.Decimal(4, 7), 1));
        RuleFor(tournament => tournament.SuperSweeperCashRatio, f => Math.Round(f.Random.Decimal(2, 5), 1));
        RuleFor(tournament => tournament.Completed, f => f.Random.Bool(.4f));
    }
}
