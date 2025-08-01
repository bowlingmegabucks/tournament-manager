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
/// 
/// </summary>
public interface IViewModel
{
    /// <summary>
    /// 
    /// </summary>
    BowlerId Id { get; }

    /// <summary>
    /// 
    /// </summary>
    string FirstName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string MiddleInitial { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string LastName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string Suffix { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string Street { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string City { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string State { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string ZipCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string Email { get; set; }

    /// <summary>
    /// 
    /// </summary>
    DateOnly? DateOfBirth { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string PhoneNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string SSN { get; set; }

    /// <summary>
    /// 
    /// </summary>
    Gender? Gender { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string USBCId { get; set; }
}