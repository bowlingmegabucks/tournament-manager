
namespace NortheastMegabuck.Registrations.Update;

internal class UpdateRegistrationModel
{
    public required Models.Division Division { get; init; }

    public DateOnly? DateOfBirth { get; init; }

    public int? Average { get; init; }

    public required DateOnly TournamentStartDate { get; init; }
}
