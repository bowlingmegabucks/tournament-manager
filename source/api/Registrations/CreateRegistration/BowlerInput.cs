using System.Text.Json.Serialization;
using Ardalis.SmartEnum.SystemTextJson;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

/// <summary>
/// BowlerInput is a record that represents the input data required to create or update a bowler's registration.
/// It includes personal information such as name, address, contact details, USBC ID, date
/// </summary>
public sealed record BowlerInput
{
    /// <summary>
    /// The first name of the bowler.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// The middle initial of the bowler, if applicable.
    /// </summary>
    public string? MiddleInitial { get; init; }

    /// <summary>
    /// The last name of the bowler.
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// The suffix of the bowler's name, if applicable (e.g., Jr., Sr., III).
    /// </summary>
    public string? Suffix { get; init; }

    /// <summary>
    /// The address of the bowler, encapsulated in an AddressInput record.
    /// </summary>
    public required AddressInput Address { get; init; }

    /// <summary>
    /// The email address of the bowler, which is required for registration.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The phone number of the bowler, which is optional.
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// The USBC (United States Bowling Congress) ID of the bowler, which is required for registration.
    /// </summary>
    public required string UsbcId { get; init; }

    /// <summary>
    /// The date of birth of the bowler, which is optional.
    /// </summary>
    public DateOnly? DateOfBirth { get; init; }

    /// <summary>
    /// The gender of the bowler. Valid values are "M" (Male), "F" (Female), or null.
    /// </summary>
    /// <remarks>
    /// Valid values:
    /// <list type="bullet">
    /// <item><description>"M" - Male</description></item>
    /// <item><description>"F" - Female</description></item>
    /// <item><description>null - Not specified</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// "M"
    /// </example>
    public string? Gender { get; init; }
}

internal static class BowlerInputExtensions
{
    /// <summary>
    /// Converts the BowlerInput to a Bowler model.
    /// </summary>
    /// <param name="input">The BowlerInput instance.</param>
    /// <returns>A Bowler model populated with the data from the input.</returns>
    public static Models.Bowler ToModel(this BowlerInput input)
        => new()
        {
            Name = new Models.PersonName
            {
                First = input.FirstName,
                MiddleInitial = input.MiddleInitial ?? "",
                Last = input.LastName,
                Suffix = input.Suffix ?? ""
            },

            StreetAddress = input.Address.Street,
            CityAddress = input.Address.City,
            StateAddress = input.Address.State,
            ZipCode = input.Address.ZipCode,

            EmailAddress = input.Email,
            PhoneNumber = input.PhoneNumber ?? "",
            DateOfBirth = input.DateOfBirth,

            USBCId = input.UsbcId,
            Gender = input.Gender == "M" ? Models.Gender.Male : Models.Gender.Female
        };
}