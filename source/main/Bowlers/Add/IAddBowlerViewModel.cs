
namespace NewEnglandClassic.Bowlers.Add;

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
}