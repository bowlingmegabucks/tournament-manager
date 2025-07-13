using Bogus;
using NortheastMegabuck.Api.Registrations.CreateRegistration;

namespace NortheastMegabuck.Api.BogusData;

internal sealed class BogusAddressInput
    : Faker<AddressInput>
{
    public BogusAddressInput()
    {
        RuleFor(input => input.Street, f => f.Address.StreetAddress());
        RuleFor(input => input.City, f => f.Address.City());
        RuleFor(input => input.State, f => f.Address.State());
        RuleFor(input => input.ZipCode, f => f.Address.ZipCode());
    }
}