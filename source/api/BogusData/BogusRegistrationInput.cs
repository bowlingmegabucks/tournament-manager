using Bogus;
using BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

namespace BowlingMegabucks.TournamentManager.Api.BogusData;

internal sealed class BogusRegistrationInput
    : Faker<RegistrationInput>
{
    public BogusRegistrationInput()
    {
        RuleFor(input => input.TournamentId, _ => TournamentId.New());

        RuleFor(input => input.Bowler, _ => new BogusBowlerInput());
        RuleFor(input => input.DivisionId, _ => DivisionId.New());

        RuleFor(input => input.Squads, f => f.Make(f.Random.Number(1, 8), () => SquadId.New()));
        RuleFor(input => input.Sweepers, f => f.Make(f.Random.Number(0, 3), () => SquadId.New()));
        RuleFor(input => input.SuperSweeper, (f, dto) => dto.Sweepers.Any() && f.Random.Bool(0.5f));

        RuleFor(input => input.Payment, _ => new BogusPaymentInput());
    }
}