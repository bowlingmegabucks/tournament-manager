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

/// <summary>
/// Represents the view model for a bowler, exposing bowler details for presentation and data transfer.
/// </summary>
public interface IViewModel
{
    /// <summary>
    /// Gets the unique identifier for the bowler.
    /// </summary>
    BowlerId Id { get; }

    /// <summary>
    /// Gets or sets the first name of the bowler.
    /// </summary>
    string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the middle initial of the bowler, if applicable.
    /// </summary>
    string MiddleInitial { get; set; }

    /// <summary>
    /// Gets or sets the last name of the bowler.
    /// </summary>
    string LastName { get; set; }

    /// <summary>
    /// Gets or sets the suffix of the bowler's name, if applicable (e.g., Jr., Sr., III).
    /// </summary>
    string Suffix { get; set; }

    /// <summary>
    /// Gets or sets the street address of the bowler.
    /// </summary>
    string Street { get; set; }

    /// <summary>
    /// Gets or sets the city of the bowler's address.
    /// </summary>
    string City { get; set; }

    /// <summary>
    /// Gets or sets the state of the bowler's address.
    /// </summary>
    string State { get; set; }

    /// <summary>
    /// Gets or sets the zip code of the bowler's address.
    /// </summary>
    string ZipCode { get; set; }

    /// <summary>
    /// Gets or sets the email address of the bowler.
    /// </summary>
    string Email { get; set; }

    /// <summary>
    /// Gets or sets the date of birth of the bowler, if provided.
    /// </summary>
    DateOnly? DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the bowler, if provided.
    /// </summary>
    string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the Social Security Number of the bowler.
    /// </summary>
    string SSN { get; set; }

    /// <summary>
    /// Gets or sets the gender of the bowler, if provided.
    /// </summary>
    Gender? Gender { get; set; }

    /// <summary>
    /// Gets or sets the USBC (United States Bowling Congress) ID of the bowler.
    /// </summary>
    string USBCId { get; set; }
}