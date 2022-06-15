
namespace NewEnglandClassic.Models;
internal class Bowler
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string MiddleInitial { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Suffix { get; set; } = string.Empty;

    public string StreetAddress { get; set; } = string.Empty;

    public string CityAddress { get; set; } = string.Empty;

    public string StateAddress { get; set; } = string.Empty;

    public string ZipCode { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string USBCId { get; set; } = string.Empty;

    public DateOnly? DateOfBirth { get; set; }

    public Gender? Gender { get; set; }

    public Bowler(Database.Entities.Bowler bowler)
    {
        Id = bowler.Id;
        FirstName = bowler.FirstName;
        MiddleInitial = bowler.MiddleInitial;
        LastName = bowler.LastName;
        Suffix = bowler.Suffix;
        StreetAddress = bowler.StreetAddress;
        CityAddress = bowler.CityAddress;
        StateAddress = bowler.StateAddress;
        ZipCode = bowler.ZipCode;
        EmailAddress = bowler.EmailAddress;
        USBCId = bowler.USBCId;
        PhoneNumber = bowler.PhoneNumber;
        DateOfBirth = bowler.DateOfBirth;
        Gender = bowler.Gender;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal Bowler()
    {

    }
}
