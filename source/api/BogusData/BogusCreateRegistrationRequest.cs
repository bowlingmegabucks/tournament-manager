using Bogus;
using NortheastMegabuck.Api.Registrations.CreateRegistration;

namespace NortheastMegabuck.Api.BogusData;

internal sealed class BogusCreateRegistrationRequest
    : Faker<CreateRegistrationRequest>
{
    public BogusCreateRegistrationRequest()
    {
        RuleFor(dto => dto.Registration, _ => new BogusRegistrationInput());
    }
}