using Bogus;
using BowlingMegabucks.TournamentManager.Api.Sweepers;

namespace BowlingMegabucks.TournamentManager.Api.BogusData;

internal sealed class BogusSweeperDetailDto
    : Faker<SweeperDetailDto>
{ 
    public BogusSweeperDetailDto()
    {
        RuleFor(dto => dto.Id, _ => SquadId.New());
        RuleFor(dto => dto.Date, f => f.Date.Future());
        RuleFor(dto => dto.EntryFee, f => f.Finance.Amount(min: 50, max: 150, decimals: 0));
        RuleFor(dto => dto.Games, f => f.Random.Short(4, 8));
    }
}