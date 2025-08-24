using Bogus;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Bowlers;

internal static class BowlerEntityFactory
{
    public static Database.Entities.Bowler Bogus()
        => new BowlerEntityFaker().Generate();

    public static IEnumerable<Database.Entities.Bowler> Bogus(int count)
        => new BowlerEntityFaker().Generate(count);
}

internal sealed class BowlerEntityFaker
    : Faker<Database.Entities.Bowler>
{
    public BowlerEntityFaker()
    {
        RuleFor(bowler => bowler.Id, f => BowlerId.New());
        RuleFor(bowler => bowler.FirstName, f => f.Person.FirstName);
        RuleFor(bowler => bowler.LastName, f => f.Person.LastName);
        RuleFor(bowler => bowler.StreetAddress, f => f.Person.Address.Street);
        RuleFor(bowler => bowler.CityAddress, f => f.Person.Address.City);
        RuleFor(bowler => bowler.StateAddress, f => f.Address.StateAbbr());
        RuleFor(bowler => bowler.ZipCode, f => f.Address.ZipCode("#####"));
        RuleFor(bowler => bowler.EmailAddress, (f, b) => f.Person.Email);
        RuleFor(bowler => bowler.PhoneNumber, f => f.Phone.PhoneNumber("##########"));
        RuleFor(bowler => bowler.USBCId, f => f.Random.Replace("####-######"));
        RuleFor(bowler => bowler.DateOfBirth, f => f.Date.PastDateOnly(50, DateOnly.FromDateTime(DateTime.Now)));
        RuleFor(bowler => bowler.Gender, f => f.Person.Gender == Bogus.DataSets.Name.Gender.Male
            ? Models.Gender.Male
            : Models.Gender.Female);
    }
}