using Bogus;
using BowlingMegabucks.TournamentManager.Api.Tournaments.GetTournaments;
using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.Api.BogusData;

internal sealed class BogusGetTournamentsDto
    : Faker<TournamentSummaryDto>
{
    public BogusGetTournamentsDto()
    {
        RuleFor(dto => dto.Id, _ => TournamentId.New());
        RuleFor(dto => dto.Name, f => f.Company.Random.Word() + " Tournament");
        RuleFor(dto => dto.StartDate, f => f.Date.FutureDateOnly());
        RuleFor(dto => dto.EndDate, (f, dto) => f.Date.FutureDateOnly(refDate: dto.StartDate));
        RuleFor(dto => dto.EntryFee, f => f.Finance.Amount(min: 50, max: 150, decimals: 0));
        RuleFor(dto => dto.BowlingCenter, f => f.Company.CompanyName());
        RuleFor(dto => dto.FinalsRatio, f => f.Finance.Amount(min: 6, max: 11, decimals: 0));
        RuleFor(dto => dto.CashRatio, f => f.Finance.Amount(min: 3, max: 4, decimals: 1));
        RuleFor(dto => dto.SuperSweeperCashRatio, f => f.Finance.Amount(min: 4, max: 6, decimals: 1));
    }
}