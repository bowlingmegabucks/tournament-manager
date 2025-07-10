using FluentValidation.TestHelper;

namespace NortheastMegabuck.Tests.Registrations.Update;

[TestFixture]
internal sealed class Validator
{
    private NortheastMegabuck.Registrations.Update.Validator _validator;

    [OneTimeSetUp]
    public void SetUp()
        => _validator = new NortheastMegabuck.Registrations.Update.Validator();

    [Test]
    public void AverageNull_MinimumAverageAndMaximumAverageForDivisionNull_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = null,
            MaximumAverage = null
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            Average = null,
            Division = division,
            Gender = null,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Average);
    }

    [Test]
    public void AverageNull_MinimumAverageNull_MaximumAverageNotNull_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = null,
            MaximumAverage = 200
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            Average = null,
            Division = division,
            Gender = null,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Average is required for selected division");
    }

    [Test]
    public void AverageNull_MinimumAverageNotNull_MaximumAverageNull_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = 175,
            MaximumAverage = null
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            Average = null,
            Division = division,
            Gender = null,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Average is required for selected division");
    }

    [Test]
    public void AverageNull_MinimumAverageNotNull_MaximumAverageNotNull_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = 175,
            MaximumAverage = 200
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            Average = null,
            Division = division,
            Gender = null,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Average is required for selected division");
    }

    [Test]
    public void AverageNotNull_MinimumAverageAndMaximumAverageForDivisionNull_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = null,
            MaximumAverage = null
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            Average = 200,
            Division = division,
            Gender = null,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Average);
    }

    [Test]
    public void AverageNotNull_MinimumAverageNull_MaximumAverageNotNull_AverageLessThanOrEqualToMaximumAverage_NoError([Values(199, 200)] int average)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = null,
            MaximumAverage = 200
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            Average = average,
            Division = division,
            Gender = null,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Average);
    }

    [Test]
    public void AverageNotNull_MinimumAverageNull_MaximumAverageNotNull_AverageGreaterThanMaximumAverage_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = null,
            MaximumAverage = 200
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            Average = 201,
            Division = division,
            Gender = null,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Maximum average requirement for division not met");
    }

    [Test]
    public void AverageNotNull_MinimumAverageNotNull_MaximumAverageNull_AverageLessThanMinimumAverage_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = 200,
            MaximumAverage = null
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            Average = 199,
            Division = division,
            Gender = null,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Minimum average requirement for division not met");
    }

    [Test]
    public void AverageNotNull_MinimumAverageNotNull_MaximumAverageNull_AverageGreaterThanOrEqualToMinimumAverage_NoError([Values(200, 201)] int average)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = 200,
            MaximumAverage = null
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            Average = average,
            Division = division,
            Gender = null,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Average);
    }

    [Test]
    public void AverageNotNull_MinimumAverageNotNull_MaximumAverageNotNull_AverageLessThanMinimumAverage_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = 200,
            MaximumAverage = 202
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            Average = 199,
            Division = division,
            Gender = null,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Minimum average requirement for division not met");
    }

    [Test]
    public void AverageNotNull_MinimumAverageNotNull_MaximumAverageNotNull_AverageBetweenMinimumAndMaximumAverage_NoError([Values(200, 202)] int average)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = 200,
            MaximumAverage = 202
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            Average = average,
            Division = division,
            Gender = null,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Average);
    }

    [Test]
    public void AverageNotNull_MinimumAverageNotNull_MaximumAverageNotNull_AverageGreaterThanMaximumAverage_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = 200,
            MaximumAverage = 202
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            Average = 203,
            Division = division,
            Gender = null,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Maximum average requirement for division not met");
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNull_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = null,
            MaximumAge = null
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = null
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today),
            Gender = bowler.Gender
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.DateOfBirth);
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNotNull_DivisionGenderNotNull_NoError_Male()
        => BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNotNull_DivisionGenderNotNull_NoError(NortheastMegabuck.Models.Gender.Male);

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNotNull_DivisionGenderNotNull_NoError_Female()
        => BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNotNull_DivisionGenderNotNull_NoError(NortheastMegabuck.Models.Gender.Female);

    private void BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNotNull_DivisionGenderNotNull_NoError(NortheastMegabuck.Models.Gender gender)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = null,
            MaximumAge = null,
            Gender = gender
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = null
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today),
            Gender = bowler.Gender
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.DateOfBirth);
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeNotValid_GenderIsValid_NoError_Male()
        => BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeNotValid_GenderIsValid_NoError(NortheastMegabuck.Models.Gender.Male);

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeNotValid_GenderIsValid_NoError_Female()
        => BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeNotValid_GenderIsValid_NoError(NortheastMegabuck.Models.Gender.Female);

    private void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeNotValid_GenderIsValid_NoError(NortheastMegabuck.Models.Gender gender)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = 40,
            MaximumAge = 50,
            Gender = gender
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-55))
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today),
            Gender = bowler.Gender
        };

        var results = _validator.TestValidate(registration);

        Assert.Multiple(() =>
        {
            results.ShouldHaveValidationErrorFor(registration => registration.DateOfBirth);
            results.ShouldNotHaveValidationErrorFor(registration => registration.Gender);
        });
    }

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_MinimumAgeMet_NoError_Male()
        => BowlerGender_GenderNull_DivisionGenderNotNull_MinimumAgeMet_NoError(NortheastMegabuck.Models.Gender.Male);

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_MinimumAgeMet_NoError_Female()
        => BowlerGender_GenderNull_DivisionGenderNotNull_MinimumAgeMet_NoError(NortheastMegabuck.Models.Gender.Female);

    private void BowlerGender_GenderNull_DivisionGenderNotNull_MinimumAgeMet_NoError(NortheastMegabuck.Models.Gender gender)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            Gender = gender,
            MinimumAge = 55
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-65))
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today),
            Gender = bowler.Gender,
            USBCId = bowler.USBCId
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Gender);
    }

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_MaximumAgeMet_NoError_Male()
        => BowlerGender_GenderNull_DivisionGenderNotNull_MaximumAgeMet_NoError(NortheastMegabuck.Models.Gender.Male);

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_MaximumAgeMet_NoError_Female()
        => BowlerGender_GenderNull_DivisionGenderNotNull_MaximumAgeMet_NoError(NortheastMegabuck.Models.Gender.Female);

    private void BowlerGender_GenderNull_DivisionGenderNotNull_MaximumAgeMet_NoError(NortheastMegabuck.Models.Gender gender)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            Gender = gender,
            MaximumAge = 55
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-45))
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today),
            Gender = bowler.Gender,
            USBCId = bowler.USBCId
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Gender);
    }

    [Test]
    public void BowlerGender_GenderNotNull_DivisionGenderNull_NoError_Male()
        => BowlerGender_GenderNotNull_DivisionGenderNull_NoError(NortheastMegabuck.Models.Gender.Male);

    [Test]
    public void BowlerGender_GenderNotNull_DivisionGenderNull_NoError_Female()
        => BowlerGender_GenderNotNull_DivisionGenderNull_NoError(NortheastMegabuck.Models.Gender.Female);

    private void BowlerGender_GenderNotNull_DivisionGenderNull_NoError(NortheastMegabuck.Models.Gender gender)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            Gender = null
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Gender = gender
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            Gender = bowler.Gender,
            USBCId = bowler.USBCId,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Gender);
    }

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_HasError_Male()
        => BowlerGender_GenderNull_DivisionGenderNotNull_HasError(NortheastMegabuck.Models.Gender.Male);

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_HasError_Female()
        => BowlerGender_GenderNull_DivisionGenderNotNull_HasError(NortheastMegabuck.Models.Gender.Female);

    private void BowlerGender_GenderNull_DivisionGenderNotNull_HasError(NortheastMegabuck.Models.Gender gender)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            Gender = gender
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Gender = null
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            Gender = bowler.Gender,
            USBCId = bowler.USBCId,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Gender).WithErrorMessage("Gender is required for selected division");
    }

    [Test]
    public void BowlerGender_GenderNotNull_DivisionGenderNotNull_GendersMatch_NoError_Male()
        => BowlerGender_GenderNotNull_DivisionGenderNotNull_GendersMatch_NoError(NortheastMegabuck.Models.Gender.Male);

    [Test]
    public void BowlerGender_GenderNotNull_DivisionGenderNotNull_GendersMatch_NoError_Female()
        => BowlerGender_GenderNotNull_DivisionGenderNotNull_GendersMatch_NoError(NortheastMegabuck.Models.Gender.Female);

    private void BowlerGender_GenderNotNull_DivisionGenderNotNull_GendersMatch_NoError(NortheastMegabuck.Models.Gender gender)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            Gender = gender
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Gender = gender
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            Gender = bowler.Gender,
            USBCId = bowler.USBCId,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Gender);
    }
}
