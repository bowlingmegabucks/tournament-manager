using Bogus;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;

namespace BowlingMegabucks.TournamentManager.Tests.Factories;

public static class TournamentSummaryViewModelFactory
{
    private static readonly TournamentSummaryViewModelFaker s_faker = new();

    public static TournamentSummaryViewModel FakeSingle()
        => s_faker.Generate();

    public static IEnumerable<TournamentSummaryViewModel> FakeMany(int count)
        => s_faker.Generate(count);
}

internal sealed class TournamentSummaryViewModelFaker
    : Faker<TournamentSummaryViewModel>
{
    public TournamentSummaryViewModelFaker()
    {
        RuleFor(t => t.Id, _ => TournamentId.New());
        RuleFor(t => t.Name, f => f.Company.CatchPhrase());
        RuleFor(t => t.StartDate, f => f.Date.PastDateOnly());
        RuleFor(t => t.EndDate, (f, t) => f.Date.FutureDateOnly(refDate: t.StartDate));
        RuleFor(t => t.EntryFee, f => f.Finance.Amount(80, 200));
        RuleFor(t => t.BowlingCenter, f => f.Company.CompanyName());
    }
}
