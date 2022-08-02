
namespace NewEnglandClassic.Bowlers;
internal class EntityMapper : IEntityMapper
{
    public Database.Entities.Bowler Execute(Models.Bowler bowler)
        => new()
        {
            Id = bowler.Id,
            FirstName = bowler.FirstName,
            MiddleInitial = bowler.MiddleInitial,
            LastName = bowler.LastName,
            Suffix = bowler.Suffix,
            StreetAddress = bowler.StreetAddress,
            CityAddress = bowler.CityAddress,
            StateAddress = bowler.StateAddress,
            ZipCode = bowler.ZipCode,
            EmailAddress = bowler.EmailAddress,
            DateOfBirth = bowler.DateOfBirth,
            Gender = bowler.Gender,
            USBCId = bowler.USBCId,
            PhoneNumber = bowler.PhoneNumber
        };
}

internal interface IEntityMapper
{
    Database.Entities.Bowler Execute(Models.Bowler bowler);
}