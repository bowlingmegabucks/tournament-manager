using Bogus;
using NortheastMegabuck.Api.Registrations.GetRegistration;
using NortheastMegabuck.Models;

namespace NortheastMegabuck.Api.BogusData;

internal sealed class BogusBowlerDetailDto
    : Faker<BowlerDetailDto>
{
    public BogusBowlerDetailDto()
    {
        RuleFor(dto => dto.Id, _ => BowlerId.New());

        RuleFor(dto => dto.FirstName, f => f.Person.FirstName);
        RuleFor(dto => dto.MiddleInitial, f => f.Person.Random.Char('A', 'Z').OrNull(f)?.ToString());
        RuleFor(dto => dto.LastName, f => f.Person.LastName);
        RuleFor(dto => dto.Suffix, f => f.PickRandom("Jr.", "Sr.", "III").OrNull(f, .95f));

        RuleFor(dto => dto.Address, _ => new BogusAddressDetailDto().Generate());

        RuleFor(dto => dto.Email, f => f.Person.Email);
        RuleFor(dto => dto.PhoneNumber, f => f.Phone.PhoneNumber().OrNull(f, .1f));

        RuleFor(dto => dto.UsbcId, f => $"{f.Random.Number(10, 9999)}-{f.Random.Number(100, 99999)}");
        RuleFor(dto => dto.DateOfBirth, f => DateOnly.FromDateTime(f.Person.DateOfBirth).OrNull(f, .1f));
        RuleFor(dto => dto.Gender, f => f.Person.Gender == Bogus.DataSets.Name.Gender.Male ? Gender.Male : Gender.Female);
    }
}