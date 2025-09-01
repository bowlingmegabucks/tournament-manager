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
        RuleFor(tournament => tournament.Id, _ => TournamentId.New());
        RuleFor(tournament => tournament.Name, f => f.Company.CatchPhrase());
        RuleFor(tournament => tournament.StartDate, f => f.Date.PastDateOnly());
        RuleFor(tournament => tournament.EndDate, (f, t) => f.Date.FutureDateOnly(refDate: t.StartDate));
        RuleFor(tournament => tournament.EntryFee, f => f.Finance.Amount(80, 200));
        RuleFor(tournament => tournament.BowlingCenter, f => f.Company.CompanyName());
    }
}
