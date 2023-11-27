
namespace NortheastMegabuck.Registrations.Update;

internal class UpdateRegistrationModel
{
    public required Models.Division Division { get; init; }

    public DateOnly? DateOfBirth { get; init; }

    public required Models.Gender? Gender { get; init; }

    public string? USBCId { get; init; }

    public int? Average { get; init; }

    public required DateOnly TournamentStartDate { get; init; }

    internal int? AgeOn(DateOnly date)
    {
        if (!DateOfBirth.HasValue)
        {
            return null;
        }

        var age = date.Year - DateOfBirth.Value.Year;

        if (DateOfBirth > date.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
