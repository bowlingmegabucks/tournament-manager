using Bogus;
using NortheastMegabuck.Api.Registrations.UpdateRegistration;

namespace NortheastMegabuck.Api.BogusData;

internal sealed class BogusUpdateRegistrationRequest
    : Faker<UpdateRegistrationRequest>
{
    public BogusUpdateRegistrationRequest()
    {
        RuleFor(request => request.Registration, _ => new BogusUpdateRegistrationInput().Generate());
        RuleFor(request => request.Id, (_, request) => request.Registration.RegistrationId);
    }
}