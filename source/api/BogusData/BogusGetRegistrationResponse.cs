using Bogus;
using NortheastMegabuck.Api.Registrations.GetRegistration;

namespace NortheastMegabuck.Api.BogusData;

internal sealed class BogusGetRegistrationResponse
    : Faker<GetRegistrationResponse>
{
    public BogusGetRegistrationResponse()
    {
        RuleFor(response => response.Registration, _ => new BogusRegistrationDetailDto());
    }
}