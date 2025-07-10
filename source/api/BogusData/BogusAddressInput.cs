using Bogus;
using NortheastMegabuck.Api.Registrations.CreateRegistration;

namespace NortheastMegabuck.Api.BogusData;

internal sealed class BogusAddressInput
    : Faker<AddressInput>
{
    public BogusAddressInput()
    {
        RuleFor(dto => dto.Street, f => f.Address.StreetAddress());
        RuleFor(dto => dto.City, f => f.Address.City());
        RuleFor(dto => dto.State, f => f.Address.State());
        RuleFor(dto => dto.ZipCode, f => f.Address.ZipCode());
    }
}