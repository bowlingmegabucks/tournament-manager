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
        RuleFor(tournament => tournament.Id, _ => TournamentId.New());
        RuleFor(tournament => tournament.Name, f => f.Company.CatchPhrase());
        RuleFor(tournament => tournament.StartDate, f => f.Date.PastDateOnly());
        RuleFor(tournament => tournament.EndDate, (f, t) => f.Date.FutureDateOnly(refDate: t.StartDate));
        RuleFor(tournament => tournament.EntryFee, f => f.Finance.Amount(80, 200));
        RuleFor(tournament => tournament.BowlingCenter, f => f.Company.CompanyName());
    }
}
