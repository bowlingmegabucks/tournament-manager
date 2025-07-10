using Bogus;
using NortheastMegabuck.Api.Registrations.GetRegistration;

namespace NortheastMegabuck.Api.BogusData;

internal sealed class BogusRegistrationDetailDto
    : Faker<RegistrationDetailDto>
{
    public BogusRegistrationDetailDto()
    {
        RuleFor(dto => dto.Id, _ => RegistrationId.New());
        RuleFor(dto => dto.TournamentId, _ => TournamentId.New());

        RuleFor(dto => dto.Bowler, _ => new BogusBowlerDetailDto().Generate());
        RuleFor(dto => dto.Division, _ => new BogusDivisionDetailDto().Generate());

        RuleFor(dto => dto.Squads, f => new BogusSquadDetailDto().Generate(f.Random.Number(1, 8)));
        RuleFor(dto => dto.Sweepers, f => new BogusSweeperDetailDto().Generate(f.Random.Number(0, 3)));
        RuleFor(dto => dto.SuperSweeper, (f, dto) => dto.Sweepers.Any() && f.Random.Bool(0.5f));
    }
}