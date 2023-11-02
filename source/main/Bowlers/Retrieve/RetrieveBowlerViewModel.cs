using NortheastMegabuck.Models;

namespace NortheastMegabuck.Bowlers.Retrieve;
internal class ViewModel : IViewModel
{
    public BowlerId Id { get; set; }

    public string FirstName { get; set; }

    public string MiddleInitial { get; set; }

    public string LastName { get; set; }

    public string Suffix { get; set; }

    public string Street { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string ZipCode { get; set; }

    public string Email { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string PhoneNumber { get; set; }

    public string SSN { get; set; }

    public Gender? Gender { get; set; }

    public string USBCId { get; set; }

    public ViewModel(Models.Bowler bowler)
    {
        Id = bowler.Id;

        FirstName = bowler.Name.First;
        MiddleInitial = bowler.Name.MiddleInitial;
        LastName = bowler.Name.Last;
        Suffix = bowler.Name.Suffix;
        Street = bowler.StreetAddress;
        City = bowler.CityAddress;
        State = bowler.StateAddress;
        ZipCode = bowler.ZipCode;
        Email = bowler.EmailAddress;
        DateOfBirth = bowler.DateOfBirth;
        PhoneNumber = bowler.PhoneNumber;
        SSN = bowler.SocialSecurityNumber;
        Gender = bowler.Gender;
        USBCId = bowler.USBCId;
    }
}

internal interface IViewModel
{
    BowlerId Id { get; }

    string FirstName { get; set; }

    string MiddleInitial { get; set; }

    string LastName { get; set; }

    string Suffix { get; set; }

    string Street { get; set; }

    string City { get; set; }

    string State { get; set; }

    string ZipCode { get; set; }

    string Email { get; set; }

    DateOnly? DateOfBirth { get; set; }

    string PhoneNumber { get; set; }

    string SSN { get; set; }

    Gender? Gender { get; set; }

    string USBCId { get; set; }
}