using System.Globalization;
using Bogus;
using BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

namespace BowlingMegabucks.TournamentManager.Registrations;

internal static class BowlerInputFactory
{
    public static BowlerInput Bogus()
        => new BowlerInputFaker().Generate();

    public static BowlerInput Create(
        string? firstName = null,
        string? middleInitial = null,
        string? lastName = null,
        string? suffix = null,
        string? street = null,
        string? city = null,
        string? state = null,
        string? zipCode = null,
        string? email = null,
        string? phoneNumber = null,
        string? usbcId = null,
        DateOnly? dateOfBirth = null,
        Models.Gender? gender = null)
            => new()
            {
                FirstName = firstName ?? "John",
                MiddleInitial = middleInitial,
                LastName = lastName ?? "Doe",
                Suffix = suffix,
                Address = new AddressInput
                {
                    Street = street ?? "123 Main St",
                    City = city ?? "Anytown",
                    State = state ?? "CA",
                    ZipCode = zipCode ?? "90210"
                },
                Email = email ?? "john.doe@email.com",
                PhoneNumber = phoneNumber ?? "555-555-5555",
                UsbcId = usbcId ?? "12345-67890",
                DateOfBirth = dateOfBirth ?? new DateOnly(2000, 1, 1),
                Gender = gender?.Name.Substring(0,1) ?? "M"
            };
}

internal sealed class BowlerInputFaker
        : Faker<BowlerInput>
{
    public BowlerInputFaker()
        : this(DateTime.UtcNow.GetHashCode())
    { }

    public BowlerInputFaker(int seed)
    {
        UseSeed(seed);

        RuleFor(bowler => bowler.FirstName, faker => faker.Name.FirstName());
        RuleFor(bowler => bowler.MiddleInitial, faker => faker.Random.Char().ToString().OrNull(faker, 0.6f));
        RuleFor(bowler => bowler.LastName, faker => faker.Name.LastName());
        RuleFor(bowler => bowler.Suffix, faker => faker.Name.Suffix().OrNull(faker, 0.7f));

        RuleFor(bowler => bowler.Address, faker => new AddressInput
        {
            Street = faker.Person.Address.Street,
            City = faker.Person.Address.City,
            State = faker.Person.Address.State,
            ZipCode = faker.Person.Address.ZipCode
        });
        RuleFor(bowler => bowler.PhoneNumber, faker => faker.Phone.PhoneNumber("###-###-####"));
        RuleFor(bowler => bowler.Email, faker => faker.Person.Email);
        RuleFor(bowler => bowler.DateOfBirth, faker => DateOnly.FromDateTime(faker.Person.DateOfBirth));
        RuleFor(bowler => bowler.Gender, faker => faker.Person.Gender == Bogus.DataSets.Name.Gender.Male
            ? Models.Gender.Male.Name
            : Models.Gender.Female.Name);

        RuleFor(bowler => bowler.UsbcId, f =>
        {
            // Generate part 1: 1 to 5 digits
            var part1Length = f.Random.Int(2, 5);
            var part1 = f.Random.Number((int)Math.Pow(10, part1Length - 1), (int)Math.Pow(10, part1Length) - 1).ToString(CultureInfo.CurrentCulture);

            // Generate part 2: 1 to 7 digits
            var part2Length = f.Random.Int(3, 7);
            var part2 = f.Random.Number((int)Math.Pow(10, part2Length - 1), (int)Math.Pow(10, part2Length) - 1).ToString(CultureInfo.CurrentCulture);

            return $"{part1}-{part2}";
        });
    }
}

internal static class BowlerInputExtensions
{
    public static BowlerInput ToInput(this Database.Entities.Bowler bowler)
        => new()
        {
            FirstName = bowler.FirstName,
            MiddleInitial = bowler.MiddleInitial,
            LastName = bowler.LastName,
            Suffix = bowler.Suffix,
            Address = new AddressInput
            {
                Street = bowler.StreetAddress,
                City = bowler.CityAddress,
                State = bowler.StateAddress,
                ZipCode = bowler.ZipCode
            },
            Email = bowler.EmailAddress,
            PhoneNumber = bowler.PhoneNumber,
            UsbcId = bowler.USBCId,
            DateOfBirth = bowler.DateOfBirth,
            Gender = bowler.Gender?.Name.Substring(0, 1)
        };
}