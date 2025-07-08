
using NortheastMegabuck.Models;

namespace NortheastMegabuck.Bowlers;

internal interface IViewModel
{
    BowlerId Id { get; }

    string FirstName { get; }

    string MiddleInitial { get; }

    string LastName { get; }

    string Suffix { get; }

    string StreetAddress { get; }

    string CityAddress { get; }

    string StateAddress { get; }

    string ZipCode { get; }

    string EmailAddress { get; }

    string PhoneNumber { get; }

    string USBCId { get; }

    DateOnly? DateOfBirth { get; }

    Models.Gender? Gender { get; }

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