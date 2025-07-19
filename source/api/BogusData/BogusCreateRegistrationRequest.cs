using Bogus;
using BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

namespace BowlingMegabucks.TournamentManager.Api.BogusData;

internal sealed class BogusCreateRegistrationRequest
    : Faker<CreateRegistrationRequest>
{
    public BogusCreateRegistrationRequest()
    {
        RuleFor(dto => dto.Registration, _ => new BogusRegistrationInput());
    }
}