using Bogus;
using BowlingMegabucks.TournamentManager.Api.Squads;

namespace BowlingMegabucks.TournamentManager.Api.BogusData;

internal sealed class BogusSquadDetailDto
    : Faker<SquadDetailDto>
{
    public BogusSquadDetailDto()
    {
        RuleFor(dto => dto.Id, _ => SquadId.New());
        RuleFor(dto => dto.Date, f => f.Date.Future());
        RuleFor(dto => dto.EntryFee, f => f.Finance.Amount(min: 50, max: 150, decimals: 0).OrNull(f));
    }
}