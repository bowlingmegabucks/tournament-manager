using Bogus;
using NortheastMegabuck.Api.Registrations.CreateRegistration;

namespace NortheastMegabuck.Api.BogusData;

internal sealed class BogusRegistrationInput
    : Faker<RegistrationInput>
{
    public BogusRegistrationInput()
    {
        RuleFor(dto => dto.TournamentId, _ => TournamentId.New());

        RuleFor(dto => dto.Bowler, _ => new BogusBowlerInput());
        RuleFor(dto => dto.DivisionId, _ => DivisionId.New());

        RuleFor(dto => dto.Squads, f => f.Make(f.Random.Number(1,8), () => SquadId.New()));
        RuleFor(dto => dto.Sweepers, f => f.Make(f.Random.Number(0,3), () => SquadId.New()));
        RuleFor(dto => dto.SuperSweeper, (f, dto) => dto.Sweepers.Any() && f.Random.Bool(0.5f));
    }
}