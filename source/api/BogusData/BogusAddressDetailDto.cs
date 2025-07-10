using Bogus;
using NortheastMegabuck.Api.Registrations.GetRegistration;

namespace NortheastMegabuck.Api.BogusData;

internal sealed class BogusAddressDetailDto
    : Faker<AddressDetailDto>
{ 
    public BogusAddressDetailDto()
    {
        RuleFor(dto => dto.Street, f => f.Address.StreetAddress());
        RuleFor(dto => dto.City, f => f.Address.City());
        RuleFor(dto => dto.State, f => f.Address.State());
        RuleFor(dto => dto.ZipCode, f => f.Address.ZipCode());
    }
}