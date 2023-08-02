
namespace NortheastMegabuck.Bowlers;
internal class EntityMapper : IEntityMapper
{
    public Database.Entities.Bowler Execute(Models.Bowler bowler)
        => new()
        {
            Id = bowler.Id,
            FirstName = bowler.Name.First,
            MiddleInitial = bowler.Name.MiddleInitial,
            LastName = bowler.Name.Last,
            Suffix = bowler.Name.Suffix,
            StreetAddress = bowler.StreetAddress,
            CityAddress = bowler.CityAddress,
            StateAddress = bowler.StateAddress,
            ZipCode = bowler.ZipCode,
            EmailAddress = bowler.EmailAddress,
            DateOfBirth = bowler.DateOfBirth,
            Gender = bowler.Gender,
            USBCId = bowler.USBCId,
            PhoneNumber = bowler.PhoneNumber,
            SocialSecurityNumber = bowler.SocialSecurityNumber
        };
}

internal interface IEntityMapper
{
    Database.Entities.Bowler Execute(Models.Bowler bowler);
}