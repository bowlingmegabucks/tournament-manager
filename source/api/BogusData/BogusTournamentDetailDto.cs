using Bogus;
using NortheastMegabuck.Api.Tournaments.GetTournament;

namespace NortheastMegabuck.Api.BogusData;

internal sealed class BogusTournamentDetailDto
    : Faker<TournamentDetailDto>
{
    public BogusTournamentDetailDto(TournamentId tournamentId)
    {
        RuleFor(dto => dto.Id, _ => tournamentId);
        RuleFor(dto => dto.Name, f => f.Company.Random.Word() + " Tournament");
        RuleFor(dto => dto.StartDate, f => f.Date.FutureDateOnly());
        RuleFor(dto => dto.EndDate, (f, dto) => f.Date.FutureDateOnly(refDate: dto.StartDate));
        RuleFor(dto => dto.EntryFee, f => f.Finance.Amount(min: 50, max: 150, decimals: 0));
        RuleFor(dto => dto.BowlingCenter, f => f.Company.CompanyName());

        RuleFor(dto => dto.Divisions, f => new BogusDivisionDetailDto().GenerateBetween(1, 5).AsReadOnly());
        RuleFor(dto => dto.Sweepers, f => new BogusSweeperDetailDto().GenerateBetween(1, 3).AsReadOnly());
        RuleFor(dto => dto.Squads, f => new BogusSquadDetailDto().GenerateBetween(1, 10).AsReadOnly()); 
    }
}