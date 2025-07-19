using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.Bowlers.Retrieve;
internal class ViewModel(Bowler bowler) : IViewModel
{
    public BowlerId Id { get; set; } = bowler.Id;

    public string FirstName { get; set; } = bowler.Name.First;

    public string MiddleInitial { get; set; } = bowler.Name.MiddleInitial;

    public string LastName { get; set; } = bowler.Name.Last;

    public string Suffix { get; set; } = bowler.Name.Suffix;

    public string Street { get; set; } = bowler.StreetAddress;

    public string City { get; set; } = bowler.CityAddress;

    public string State { get; set; } = bowler.StateAddress;

    public string ZipCode { get; set; } = bowler.ZipCode;

    public string Email { get; set; } = bowler.EmailAddress;

    public DateOnly? DateOfBirth { get; set; } = bowler.DateOfBirth;

    public string PhoneNumber { get; set; } = bowler.PhoneNumber;

    public string SSN { get; set; } = bowler.SocialSecurityNumber;

    public Gender? Gender { get; set; } = bowler.Gender;

    public string USBCId { get; set; } = bowler.USBCId;
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