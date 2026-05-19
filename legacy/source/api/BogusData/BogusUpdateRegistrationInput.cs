using Bogus;
using BowlingMegabucks.TournamentManager.Api.Registrations.UpdateRegistration;

namespace BowlingMegabucks.TournamentManager.Api.BogusData;

internal sealed class BogusUpdateRegistrationInput
    : Faker<UpdateRegistrationInput>
{
    public BogusUpdateRegistrationInput()
    {
        RuleFor(input => input.RegistrationId, _ => RegistrationId.New());

        RuleFor(input => input.SquadIds, f => f.Make(f.Random.Number(1, 8), () => SquadId.New()).OrNull(f));
        RuleFor(input => input.SweeperIds, f => f.Make(f.Random.Number(0, 3), () => SquadId.New()).OrNull(f));
        RuleFor(input => input.SuperSweeper, f => f.Random.Bool().OrNull(f));

        RuleFor(input => input.Payment, _ => new BogusPaymentInput().Generate());
    }
}