using Bogus;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;

namespace BowlingMegabucks.TournamentManager.Tests.Factories;

public static class TournamentSummaryFactory
{
    private static readonly TournamentSummaryFaker s_faker = new();

    public static TournamentSummary FakeSingle()
        => s_faker.Generate();

    public static IEnumerable<TournamentSummary> FakeMany(int count)
        => s_faker.Generate(count);
}

internal sealed class TournamentSummaryFaker
    : Faker<TournamentSummary>
{
    public TournamentSummaryFaker()
    {
        RuleFor(t => t.Id, _ => TournamentId.New());
        RuleFor(t => t.Name, f => f.Company.CatchPhrase());
        RuleFor(t => t.StartDate, f => f.Date.PastDateOnly());
        RuleFor(t => t.EndDate, (f, t) => f.Date.FutureDateOnly(refDate: t.StartDate));
        RuleFor(t => t.EntryFee, f => f.Finance.Amount(80, 200));
        RuleFor(t => t.BowlingCenter, f => f.Company.CompanyName());
    }
}
