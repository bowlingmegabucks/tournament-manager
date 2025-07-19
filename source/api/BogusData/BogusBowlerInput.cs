using Bogus;
using BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;
using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.Api.BogusData;

internal sealed class BogusBowlerInput
    : Faker<BowlerInput>
{
    public BogusBowlerInput()
    {
        RuleFor(input => input.FirstName, f => f.Person.FirstName);
        RuleFor(input => input.MiddleInitial, f => f.Person.Random.Char('A', 'Z').OrNull(f)?.ToString());
        RuleFor(input => input.LastName, f => f.Person.LastName);
        RuleFor(input => input.Suffix, f => f.PickRandom("Jr.", "Sr.", "III").OrNull(f, .95f));

        RuleFor(input => input.Address, _ => new BogusAddressInput());

        RuleFor(input => input.Email, f => f.Person.Email);
        RuleFor(input => input.PhoneNumber, f => f.Phone.PhoneNumber().OrNull(f, .1f));

        RuleFor(input => input.UsbcId, f => $"{f.Random.Number(10, 9999)}-{f.Random.Number(100, 99999)}");
        RuleFor(input => input.DateOfBirth, f => DateOnly.FromDateTime(f.Person.DateOfBirth).OrNull(f, .1f));
        RuleFor(input => input.Gender, f => f.Person.Gender == Bogus.DataSets.Name.Gender.Male ? Gender.Male.Name : Gender.Female.Name);
    }
}