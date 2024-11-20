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
    public void BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNotNull_DivisionGenderNotNull_NoError([Values] NortheastMegabuck.Models.Gender gender)
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
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeNull_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = null,
            MaximumAge = null
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20))
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
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAgeNotNull_DivisionMaximumAgeNotNullOrValid_AgeTooYoung_HasError([Values(null, 99)] short? maximumAge)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = 40,
            MaximumAge = maximumAge
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20))
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today),
            Gender = bowler.Gender
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.DateOfBirth).WithErrorMessage("Bowler too young for selected division");
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAgeNullOrValid_DivisionMaximumAgeNotNull_AgeTooOld_HasError([Values(null, 40)] short? minimumAge)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = minimumAge,
            MaximumAge = 65
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-75))
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today),
            Gender = bowler.Gender
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.DateOfBirth).WithErrorMessage("Bowler too old for selected division");
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAgeNotNull_DivisionMaximumAgeNotNull_AgeValid_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = 40,
            MaximumAge = 50
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
            Gender = bowler.Gender
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.DateOfBirth);
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeNotValid_GenderIsValid_NoError([Values] NortheastMegabuck.Models.Gender gender)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = 40,
            MaximumAge = 50,
            Gender = gender
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-55)),
            Gender = gender
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
            results.ShouldNotHaveValidationErrorFor(registration => registration.DateOfBirth);
            results.ShouldNotHaveValidationErrorFor(registration => registration.Gender);
        });
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeValid_GenderIsNotValid_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = 40,
            MaximumAge = 50,
            Gender = NortheastMegabuck.Models.Gender.Female
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-45)),
            Gender = NortheastMegabuck.Models.Gender.Male
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
            results.ShouldNotHaveValidationErrorFor(registration => registration.DateOfBirth);
            results.ShouldNotHaveValidationErrorFor(registration => registration.Gender);
        });
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_GenderNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeAndGenderAreNotValid_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = 40,
            MaximumAge = 50,
            Gender = NortheastMegabuck.Models.Gender.Female
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-55)),
            Gender = NortheastMegabuck.Models.Gender.Male
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today),
            Gender = bowler.Gender
        };

        var result = _validator.TestValidate(registration);
        result.ShouldHaveValidationErrorFor(registration => registration.DateOfBirth).WithErrorMessage("Bowler too old for selected division");
    }

    [Test]
    public void BowlerUSBCId_HasValue_DivisionHandicapOrScratch_NoError([Values(null, .8)] decimal? handicapPercentage)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapPercentage = handicapPercentage
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            USBCId = "123-456"
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            Gender = bowler.Gender,
            USBCId = bowler.USBCId,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var result = _validator.TestValidate(registration);
        result.ShouldNotHaveValidationErrorFor(registration => registration.USBCId);
    }

    [Test]
    public void BowlerUSBCId_HasNoValue_DivisionScratch_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapPercentage = null
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            USBCId = null
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            Gender = bowler.Gender,
            USBCId = bowler.USBCId,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var result = _validator.TestValidate(registration);
        result.ShouldNotHaveValidationErrorFor(registration => registration.USBCId);
    }

    [Test]
    public void BowlerUSBCId_HasNoValue_DivisionHandicap_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapPercentage = .8m
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            USBCId = null
        };

        var registration = new NortheastMegabuck.Registrations.Update.UpdateRegistrationModel
        {
            DateOfBirth = bowler.DateOfBirth,
            Division = division,
            Gender = bowler.Gender,
            USBCId = bowler.USBCId,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var result = _validator.TestValidate(registration);
        result.ShouldHaveValidationErrorFor(registration => registration.USBCId).WithErrorMessage("USBC Id is required for Handicap Divisions");
    }

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNull_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            Gender = null
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
        results.ShouldNotHaveValidationErrorFor(registration => registration.Gender);
    }

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_MinimumAgeMet_NoError([Values] NortheastMegabuck.Models.Gender gender)
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
    public void BowlerGender_GenderNull_DivisionGenderNotNull_MaximumAgeMet_NoError([Values] NortheastMegabuck.Models.Gender gender)
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

    [TestCase(NortheastMegabuck.Models.Gender.Male)]
    [TestCase(null)]
    public void BowlerGender_GenderNullOrDoesNotMatch_DivisionGenderNotNullAndDoesNotMatch_AgeRequirementNotMet_ErrorOnAgeNotGender(NortheastMegabuck.Models.Gender? gender)

    {
        var division = new NortheastMegabuck.Models.Division
        {
            Gender = NortheastMegabuck.Models.Gender.Female,
            MinimumAge = 50,
            MaximumAge = 55
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-45)),
            Gender = gender
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

        Assert.Multiple(() =>
        {
            results.ShouldNotHaveValidationErrorFor(registration => registration.Gender);
            results.ShouldHaveValidationErrorFor(registration => registration.DateOfBirth).WithErrorMessage("Bowler too young for selected division");
        });
    }

    [Test]
    public void BowlerGender_GenderNotNull_DivisionGenderNull_NoError([Values] NortheastMegabuck.Models.Gender gender)
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
    public void BowlerGender_GenderNull_DivisionGenderNotNull_HasError([Values] NortheastMegabuck.Models.Gender gender)
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
    public void BowlerGender_GenderNotNull_DivisionGenderNotNull_GendersMatch_NoError([Values] NortheastMegabuck.Models.Gender gender)
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

    [Test]
    public void BowlerGender_GenderNotNull_DivisionGenderNotNull_GendersDoNotMatch_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            Gender = NortheastMegabuck.Models.Gender.Male
        };

        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Gender = NortheastMegabuck.Models.Gender.Female
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
        results.ShouldHaveValidationErrorFor(registration => registration.Gender).WithErrorMessage("Invalid gender for selected division");
    }
}
