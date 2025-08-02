using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.Bowlers;

/// <summary>
/// Represents a view model containing all relevant information about a bowler for display or data transfer.
/// </summary>
public interface IViewModel
{
    /// <summary>
    /// Gets the unique identifier for the bowler.
    /// </summary>
    BowlerId Id { get; }

    /// <summary>
    /// Gets the first name of the bowler.
    /// </summary>
    string FirstName { get; }

    /// <summary>
    /// Gets the middle initial of the bowler, if any.
    /// </summary>
    string MiddleInitial { get; }

    /// <summary>
    /// Gets the last name of the bowler.
    /// </summary>
    string LastName { get; }

    /// <summary>
    /// Gets the suffix of the bowler's name, if any (e.g., Jr., Sr., III).
    /// </summary>
    string Suffix { get; }

    /// <summary>
    /// Gets the street address of the bowler.
    /// </summary>
    string StreetAddress { get; }

    /// <summary>
    /// Gets the city of the bowler's address.
    /// </summary>
    string CityAddress { get; }

    /// <summary>
    /// Gets the state of the bowler's address.
    /// </summary>
    string StateAddress { get; }

    /// <summary>
    /// Gets the zip code of the bowler's address.
    /// </summary>
    string ZipCode { get; }

    /// <summary>
    /// Gets the email address of the bowler.
    /// </summary>
    string EmailAddress { get; }

    /// <summary>
    /// Gets the phone number of the bowler.
    /// </summary>
    string PhoneNumber { get; }

    /// <summary>
    /// Gets the USBC (United States Bowling Congress) ID of the bowler.
    /// </summary>
    string USBCId { get; }

    /// <summary>
    /// Gets the date of birth of the bowler, if provided.
    /// </summary>
    DateOnly? DateOfBirth { get; }

    /// <summary>
    /// Gets the gender of the bowler, if provided.
    /// </summary>
    Models.Gender? Gender { get; }

    /// <summary>
    /// Gets the Social Security Number of the bowler.
    /// </summary>
    string SocialSecurityNumber { get; }
}

internal static class ViewModelExtensions
{
    public static Bowler ToModel(this IViewModel viewModel)
    {
        return new Bowler
        {
            Id = viewModel.Id,

            Name = new PersonName
            {
                First = viewModel.FirstName,
                MiddleInitial = viewModel.MiddleInitial,
                Last = viewModel.LastName,
                Suffix = viewModel.Suffix,
            },

            StreetAddress = viewModel.StreetAddress,
            CityAddress = viewModel.CityAddress,
            StateAddress = viewModel.StateAddress,
            ZipCode = viewModel.ZipCode,
            EmailAddress = viewModel.EmailAddress,
            PhoneNumber = viewModel.PhoneNumber,
            USBCId = viewModel.USBCId,
            DateOfBirth = viewModel.DateOfBirth,
            Gender = viewModel.Gender,
            SocialSecurityNumber = viewModel.SocialSecurityNumber
        };
    }
}