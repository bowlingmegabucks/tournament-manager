
using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.Bowlers;

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
    string FirstName { get; }

    /// <summary>
    /// 
    /// </summary>
    string MiddleInitial { get; }

    /// <summary>
    /// 
    /// </summary>
    string LastName { get; }

    /// <summary>
    /// 
    /// </summary>
    string Suffix { get; }

    /// <summary>
    /// 
    /// </summary>
    string StreetAddress { get; }

    /// <summary>
    /// 
    /// </summary>
    string CityAddress { get; }

    /// <summary>
    /// 
    /// </summary>
    string StateAddress { get; }

    /// <summary>
    /// 
    /// </summary>
    string ZipCode { get; }

    /// <summary>
    /// 
    /// </summary>
    string EmailAddress { get; }

    /// <summary>
    /// 
    /// </summary>
    string PhoneNumber { get; }

    /// <summary>
    /// 
    /// </summary>
    string USBCId { get; }

    /// <summary>
    /// 
    /// </summary>
    DateOnly? DateOfBirth { get; }

    /// <summary>
    /// 
    /// </summary>
    Models.Gender? Gender { get; }

    /// <summary>
    /// 
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