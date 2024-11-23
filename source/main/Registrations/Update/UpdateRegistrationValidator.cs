using FluentValidation;

namespace NortheastMegabuck.Registrations.Update;
internal class Validator : AbstractValidator<UpdateRegistrationModel>
{
    public Validator()
    {
        RuleFor(registration => registration.Average).NotNull().When(registration => registration.Division.MinimumAverage.HasValue || registration.Division.MaximumAverage.HasValue).WithMessage("Average is required for selected division");
        RuleFor(registration => registration.Average).GreaterThanOrEqualTo(registration => registration.Division.MinimumAverage!.Value).When(registration => registration.Division.MinimumAverage.HasValue).WithMessage("Minimum average requirement for division not met");
        RuleFor(registration => registration.Average).LessThanOrEqualTo(registration => registration.Division.MaximumAverage!.Value).When(registration => registration.Division.MaximumAverage.HasValue).WithMessage("Maximum average requirement for division not met");

        RuleFor(registration => registration.DateOfBirth).Must(dateOfBirth => dateOfBirth.HasValue)
            .When(registration => (registration.Division.MinimumAge.HasValue || registration.Division.MaximumAge.HasValue) && !registration.Division.Gender.HasValue)
            .WithMessage("Date of birth required for selected division");
        RuleFor(registration => registration.DateOfBirth).Must((registration, dateOfBirth) => registration.AgeOn(registration.TournamentStartDate) >= registration.Division.MinimumAge!.Value)
            .When(registration => registration.Division.MinimumAge.HasValue)
            .When(registration => (registration.Division.Gender.HasValue && registration.Division.Gender.Value != registration.Gender) || !registration.Division.Gender.HasValue)
            .WithMessage("Bowler too young for selected division");
        RuleFor(registration => registration.DateOfBirth).Must((registration, dateOfBirth) => registration.AgeOn(registration.TournamentStartDate) <= registration.Division.MaximumAge!.Value)
            .When(registration => registration.Division.MaximumAge.HasValue)
            .When(registration => (registration.Division.Gender.HasValue && registration.Division.Gender.Value != registration.Gender) || !registration.Division.Gender.HasValue)
            .WithMessage("Bowler too old for selected division");

        RuleFor(registration => registration.USBCId).NotEmpty().When(registration => registration.Division.HandicapPercentage.HasValue).WithMessage("USBC Id is required for Handicap Divisions");

        RuleFor(registration => registration.Gender).Must(gender => gender.HasValue)
            .When(registration => registration.Division.Gender.HasValue)
            .When(registration => !(registration.Division.MinimumAge.HasValue || registration.Division.MaximumAge.HasValue))
            .WithMessage("Gender is required for selected division");
        RuleFor(registration => registration.Gender).Must((registration, gender) => registration.Division.Gender!.Value == gender)
            .When(registration => registration.Division.Gender.HasValue)
            .When(registration => !(registration.Division.MinimumAge.HasValue || registration.Division.MaximumAge.HasValue))
            .WithMessage("Invalid gender for selected division");
    }
}
