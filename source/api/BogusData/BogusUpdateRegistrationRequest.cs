using Bogus;
using BowlingMegabucks.TournamentManager.Api.Registrations.UpdateRegistration;

namespace BowlingMegabucks.TournamentManager.Api.BogusData;

internal sealed class BogusUpdateRegistrationRequest
    : Faker<UpdateRegistrationRequest>
{
    public BogusUpdateRegistrationRequest()
    {
        RuleFor(request => request.Registration, _ => new BogusUpdateRegistrationInput().Generate());
        RuleFor(request => request.RegistrationId, (_, request) => request.Registration.RegistrationId);
    }
}