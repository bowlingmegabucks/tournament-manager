using Bogus;
using NortheastMegabuck.Api.Tournaments.GetTournaments;
using NortheastMegabuck.Models;

namespace NortheastMegabuck.Api.BogusData;

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
    }
}