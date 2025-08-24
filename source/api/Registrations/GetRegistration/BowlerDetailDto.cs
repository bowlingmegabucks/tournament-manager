using System.Text.Json.Serialization;
using Ardalis.SmartEnum.SystemTextJson;
using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.GetRegistration;

/// <summary>
/// Represents the details of a bowler in a registration.
/// </summary>
public sealed record BowlerDetailDto
{
    /// <summary>
    /// The unique identifier of the bowler.
    /// </summary>
    public required BowlerId Id { get; init; }

    /// <summary>
    /// The bowler's first name.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// The bowler's middle initial, if applicable.
    /// </summary>
    public string? MiddleInitial { get; init; }

    /// <summary>
    /// The bowler's last name.
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// The suffix of the bowler's name, if applicable (e.g., Jr., Sr., III).
    /// </summary>
    public string? Suffix { get; init; }

    /// <summary>
    /// The bowler's address details.
    /// </summary>
    public required AddressDetailDto Address { get; init; }

    /// <summary>
    /// The bowler's email address.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The bowler's phone number, if provided.
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// The USBC (United States Bowling Congress) ID of the bowler.
    /// </summary>
    public required string UsbcId { get; init; }

    /// <summary>
    /// The date of birth of the bowler, if provided.
    /// </summary>
    public DateOnly? DateOfBirth { get; init; }

    /// <summary>
    /// The gender of the bowler, if provided.
    /// </summary>
    [JsonConverter(typeof(SmartEnumNameConverter<Models.Gender, int>))]
    public Models.Gender? Gender { get; init; }
}

internal static class BowlerExtensions
{
    public static BowlerDetailDto ToDto(this Bowler bowler)
        => new()
        {
            Id = bowler.Id,
            FirstName = bowler.Name.First,
            MiddleInitial = bowler.Name.MiddleInitial,
            LastName = bowler.Name.Last,
            Suffix = bowler.Name.Suffix,
            Address = new()
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
            Gender = bowler.Gender
        };
}