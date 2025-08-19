using FluentValidation;
using BowlingMegabucks.TournamentManager.Database.Entities;

namespace BowlingMegabucks.TournamentManager.Registrations.UpdateRegistration;

internal sealed class Validator
    : AbstractValidator<UpdateRegistrationRecord>
{
    public Validator()
    {
        RuleFor(record => record.Average).NotNull().When(record => record.Division.MinimumAverage.HasValue || record.Division.MaximumAverage.HasValue).WithMessage("Average is required for selected division");
        RuleFor(record => record.Average).GreaterThanOrEqualTo(record => record.Division.MinimumAverage!.Value).When(record => record.Division.MinimumAverage.HasValue).WithMessage("Minimum average requirement for division not met");
        RuleFor(record => record.Average).LessThanOrEqualTo(record => record.Division.MaximumAverage!.Value).When(record => record.Division.MaximumAverage.HasValue).WithMessage("Maximum average requirement for division not met");

        RuleFor(record => record.Bowler.DateOfBirth).Must(dateOfBirth => dateOfBirth.HasValue)
            .When(record => (record.Division.MinimumAge.HasValue || record.Division.MaximumAge.HasValue) && record.Division.Gender is null)
            .WithMessage("Date of birth required for selected division");
        RuleFor(record => record.Bowler.DateOfBirth).Must((record, dateOfBirth) => record.Bowler.AgeOn(record.Tournament.Start) >= record.Division.MinimumAge!.Value)
            .When(record => record.Division.MinimumAge.HasValue)
            .When(record => (record.Division.Gender is not null && record.Division.Gender != record.Bowler.Gender) || record.Division.Gender is null)
            .WithMessage("Bowler too young for selected division");
        RuleFor(record => record.Bowler.DateOfBirth).Must((record, dateOfBirth) => record.Bowler.AgeOn(record.Tournament.Start) <= record.Division.MaximumAge!.Value)
            .When(record => record.Division.MaximumAge.HasValue)
            .When(record => (record.Division.Gender is not null && record.Division.Gender != record.Bowler.Gender) || record.Division.Gender is null)
            .WithMessage("Bowler too old for selected division");

        RuleFor(registration => registration.Bowler.USBCId).NotEmpty().When(registration => registration.Division.HandicapPercentage.HasValue).WithMessage("USBC Id is required for Handicap Divisions");

        RuleFor(registration => registration.Bowler.Gender).Must(gender => gender is not null)
            .When(registration => registration.Division.Gender is not null)
            .When(registration => !(registration.Division.MinimumAge.HasValue || registration.Division.MaximumAge.HasValue))
            .WithMessage("Gender is required for selected division");
        RuleFor(registration => registration.Bowler.Gender).Must((registration, gender) => registration.Division.Gender!.Value == gender)
            .When(registration => registration.Division.Gender is not null)
            .When(registration => !(registration.Division.MinimumAge.HasValue || registration.Division.MaximumAge.HasValue))
            .WithMessage("Invalid gender for selected division");
    }
}

internal sealed record UpdateRegistrationRecord(Bowler Bowler, Tournament Tournament, Division Division, int? Average);

internal static class UpdateRegistrationRecordExtensions
{
    public static int? AgeOn(this Bowler bowler, DateOnly date)
    {
        if (!bowler.DateOfBirth.HasValue)
        {
            return null;
        }

        var age = date.Year - bowler.DateOfBirth.Value.Year;

        if (bowler.DateOfBirth > date.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}