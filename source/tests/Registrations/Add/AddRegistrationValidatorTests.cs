using FluentValidation.TestHelper;

namespace BowlingMegabucks.TournamentManager.Tests.Registrations.Add;

[TestFixture]
internal sealed class Validator
{
    private BowlingMegabucks.TournamentManager.Registrations.Add.Validator _validator;

    [OneTimeSetUp]
    public void SetUp()
        => _validator = new BowlingMegabucks.TournamentManager.Registrations.Add.Validator();

    [Test]
    public void Bowler_HasBowlerValidator()
        => _validator.ShouldHaveChildValidator(registration => registration.Bowler, typeof(BowlingMegabucks.TournamentManager.Bowlers.Validator));

    [Test]
    public void AverageNull_MinimumAverageAndMaximumAverageForDivisionNull_NoError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAverage = null,
            MaximumAverage = null
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Average = null,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Average);
    }

    [Test]
    public void AverageNull_MinimumAverageNull_MaximumAverageNotNull_HasError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAverage = null,
            MaximumAverage = 200
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Average = null,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Average is required for selected division");
    }

    [Test]
    public void AverageNull_MinimumAverageNotNull_MaximumAverageNull_HasError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAverage = 175,
            MaximumAverage = null
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Average = null,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Average is required for selected division");
    }

    [Test]
    public void AverageNull_MinimumAverageNotNull_MaximumAverageNotNull_HasError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAverage = 175,
            MaximumAverage = 200
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Average = null,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Average is required for selected division");
    }

    [Test]
    public void AverageNotNull_MinimumAverageAndMaximumAverageForDivisionNull_NoError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAverage = null,
            MaximumAverage = null
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Average = 200,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Average);
    }

    [Test]
    public void AverageNotNull_MinimumAverageNull_MaximumAverageNotNull_AverageLessThanOrEqualToMaximumAverage_NoError([Values(199, 200)] int average)
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAverage = null,
            MaximumAverage = 200
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Average = average,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Average);
    }

    [Test]
    public void AverageNotNull_MinimumAverageNull_MaximumAverageNotNull_AverageGreaterThanMaximumAverage_HasError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAverage = null,
            MaximumAverage = 200
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Average = 201,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Maximum average requirement for division not met");
    }

    [Test]
    public void AverageNotNull_MinimumAverageNotNull_MaximumAverageNull_AverageLessThanMinimumAverage_HasError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAverage = 200,
            MaximumAverage = null
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Average = 199,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Minimum average requirement for division not met");
    }

    [Test]
    public void AverageNotNull_MinimumAverageNotNull_MaximumAverageNull_AverageGreaterThanOrEqualToMinimumAverage_NoError([Values(200, 201)] int average)
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAverage = 200,
            MaximumAverage = null
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Average = average,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Average);
    }

    [Test]
    public void AverageNotNull_MinimumAverageNotNull_MaximumAverageNotNull_AverageLessThanMinimumAverage_HasError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAverage = 200,
            MaximumAverage = 202
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Average = 199,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Minimum average requirement for division not met");
    }

    [Test]
    public void AverageNotNull_MinimumAverageNotNull_MaximumAverageNotNull_AverageBetweenMinimumAndMaximumAverage_NoError([Values(200, 202)] int average)
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAverage = 200,
            MaximumAverage = 202
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Average = average,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Average);
    }

    [Test]
    public void AverageNotNull_MinimumAverageNotNull_MaximumAverageNotNull_AverageGreaterThanMaximumAverage_HasError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAverage = 200,
            MaximumAverage = 202
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Average = 203,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Average).WithErrorMessage("Maximum average requirement for division not met");
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNull_NoError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAge = null,
            MaximumAge = null
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = null
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.DateOfBirth);
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNotNull_DivisionGenderNotNull_NoError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAge = null,
            MaximumAge = null,
            Gender = BowlingMegabucks.TournamentManager.Models.Gender.Male
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = null
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.DateOfBirth);
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNotNull_DivisionGenderNotNull_NoError_Male()
        => BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNotNull_DivisionGenderNotNull_NoError(BowlingMegabucks.TournamentManager.Models.Gender.Male);

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNotNull_DivisionGenderNotNull_NoError_Female()
        => BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNotNull_DivisionGenderNotNull_NoError(BowlingMegabucks.TournamentManager.Models.Gender.Female);

    private void BowlerDateOfBirth_DateOfBirthNull_DivisionMinimumAndMaximumAgeNotNull_DivisionGenderNotNull_NoError(BowlingMegabucks.TournamentManager.Models.Gender gender)
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAge = null,
            MaximumAge = null,
            Gender = gender
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = null
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.DateOfBirth);
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeNull_NoError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAge = null,
            MaximumAge = null
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20))
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.DateOfBirth);
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAgeNotNull_DivisionMaximumAgeNotNull_AgeTooYoung_HasError([Values(null, 99)] short? maximumAge)
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAge = 40,
            MaximumAge = maximumAge
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20))
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Bowler.DateOfBirth).WithErrorMessage("Bowler too young for selected division");
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAgeNullOrValid_DivisionMaximumAgeNotNull_AgeTooOld_HasError([Values(null, 40)] short? minimumAge)
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAge = minimumAge,
            MaximumAge = 65
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-75))
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Bowler.DateOfBirth).WithErrorMessage("Bowler too old for selected division");
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAgeNotNull_DivisionMaximumAgeNotNull_AgeValid_NoError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAge = 40,
            MaximumAge = 50
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-45))
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.DateOfBirth);
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeNotValid_GenderIsValid_NoError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAge = 40,
            MaximumAge = 50,
            Gender = BowlingMegabucks.TournamentManager.Models.Gender.Male
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-55)),
            Gender = BowlingMegabucks.TournamentManager.Models.Gender.Male
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);

        Assert.Multiple(() =>
        {
            results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.DateOfBirth);
            results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.Gender);
        });
    }

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeNotValid_GenderIsValid_NoError_Male()
        => BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeNotValid_GenderIsValid_NoError(BowlingMegabucks.TournamentManager.Models.Gender.Male);

    [Test]
    public void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeNotValid_GenderIsValid_NoError_Female()
        => BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeNotValid_GenderIsValid_NoError(BowlingMegabucks.TournamentManager.Models.Gender.Female);

    private void BowlerDateOfBirth_DateOfBirthNotNull_DivisionMinimumAndMaximumAgeSet_DivisionGenderSet_AgeNotValid_GenderIsValid_NoError(BowlingMegabucks.TournamentManager.Models.Gender gender)
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAge = 40,
            MaximumAge = 50,
            Gender = gender
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-55)),
            Gender = gender
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);

        Assert.Multiple(() =>
        {
            results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.DateOfBirth);
            results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.Gender);
        });
    }

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_MinimumAgeMet_NoError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            Gender = BowlingMegabucks.TournamentManager.Models.Gender.Male,
            MinimumAge = 55
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-65))
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.Gender);
    }

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_MinimumAgeMet_NoError_Male()
        => BowlerGender_GenderNull_DivisionGenderNotNull_MinimumAgeMet_NoError(BowlingMegabucks.TournamentManager.Models.Gender.Male);

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_MinimumAgeMet_NoError_Female()
        => BowlerGender_GenderNull_DivisionGenderNotNull_MinimumAgeMet_NoError(BowlingMegabucks.TournamentManager.Models.Gender.Female);

    private void BowlerGender_GenderNull_DivisionGenderNotNull_MinimumAgeMet_NoError(BowlingMegabucks.TournamentManager.Models.Gender gender)
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            Gender = gender,
            MinimumAge = 55
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-65))
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.Gender);
    }

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_MaximumAgeMet_NoError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            Gender = BowlingMegabucks.TournamentManager.Models.Gender.Male,
            MaximumAge = 55
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-45))
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.Gender);
    }

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_MaximumAgeMet_NoError_Male()
        => BowlerGender_GenderNull_DivisionGenderNotNull_MaximumAgeMet_NoError(BowlingMegabucks.TournamentManager.Models.Gender.Male);

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_MaximumAgeMet_NoError_Female()
        => BowlerGender_GenderNull_DivisionGenderNotNull_MaximumAgeMet_NoError(BowlingMegabucks.TournamentManager.Models.Gender.Female);

    private void BowlerGender_GenderNull_DivisionGenderNotNull_MaximumAgeMet_NoError(BowlingMegabucks.TournamentManager.Models.Gender gender)
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            Gender = gender,
            MaximumAge = 55
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-45))
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division,
            TournamentStartDate = DateOnly.FromDateTime(DateTime.Today)
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.Gender);
    }

    [Test]
    public void BowlerGender_GenderNotNull_DivisionGenderNull_NoError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            Gender = null
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            Gender = BowlingMegabucks.TournamentManager.Models.Gender.Male
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.Gender);
    }

    [Test]
    public void BowlerGender_GenderNull_DivisionGenderNotNull_HasError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            Gender = BowlingMegabucks.TournamentManager.Models.Gender.Male
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            Gender = null
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Bowler.Gender).WithErrorMessage("Gender is required for selected division");
    }

    [Test]
    public void BowlerGender_GenderNotNull_DivisionGenderNotNull_GendersMatch_NoError()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            Gender = BowlingMegabucks.TournamentManager.Models.Gender.Male
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            Gender = BowlingMegabucks.TournamentManager.Models.Gender.Male
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.Gender);
    }

    [Test]
    public void BowlerGender_GenderNotNull_DivisionGenderNotNull_GendersMatch_NoError_Male()
        => BowlerGender_GenderNotNull_DivisionGenderNotNull_GendersMatch_NoError(BowlingMegabucks.TournamentManager.Models.Gender.Male);

    [Test]
    public void BowlerGender_GenderNotNull_DivisionGenderNotNull_GendersMatch_NoError_Female()
        => BowlerGender_GenderNotNull_DivisionGenderNotNull_GendersMatch_NoError(BowlingMegabucks.TournamentManager.Models.Gender.Female);

    private void BowlerGender_GenderNotNull_DivisionGenderNotNull_GendersMatch_NoError(BowlingMegabucks.TournamentManager.Models.Gender gender)
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            Gender = gender
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            Gender = gender
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Division = division
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Bowler.Gender);
    }

    [Test]
    public void Squads_SquadsEmpty_HasError()
    {
        var squads = Enumerable.Empty<BowlingMegabucks.TournamentManager.Models.Squad>();

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Squads = squads
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.Squads).WithErrorMessage("Bowler must enter at least one squad");
    }

    [Test]
    public void Squads_SquadsNotEmpty_NoError([Range(1, 3)] int count)
    {
        var squads = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Models.Squad(), count);

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Squads = squads
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.Squads);
    }

    [Test]
    public void SuperSweeper_BowlerEntersZeroToAllSweepers_DoesNotEnterSuperSweeper_NoError([Range(0, 3)] int count)
    {
        var sweepers = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Models.Sweeper { Id = SquadId.New() }, count);

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Sweepers = sweepers,
            SuperSweeper = false
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.SuperSweeper);
    }

    [Test]
    public void SuperSweeper_BowlerEntersZeroToAllButOneSweeper_EntersSuperSweeper_HasError([Range(0, 2)] int count)
    {
        var sweepers = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Models.Sweeper { Id = SquadId.New() }, count);

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Sweepers = sweepers,
            TournamentSweeperCount = 3,
            SuperSweeper = true
        };

        var results = _validator.TestValidate(registration);
        results.ShouldHaveValidationErrorFor(registration => registration.SuperSweeper).WithErrorMessage("Must enter all sweepers to enter Super Sweeper");
    }

    [Test]
    public void SuperSweeper_BowlerEntersAllSweepers_EntersSuperSweeper_NoError()
    {
        var sweepers = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Models.Sweeper { Id = SquadId.New() }, 3);

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Sweepers = sweepers,
            TournamentSweeperCount = 3,
            SuperSweeper = true
        };

        var results = _validator.TestValidate(registration);
        results.ShouldNotHaveValidationErrorFor(registration => registration.SuperSweeper);
    }

    [Test]
    [Category("Real")]
    public void AshlieInWomensDivision_Allowed()
    {
        var squads = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Models.Squad(), 2);

        var division = new BowlingMegabucks.TournamentManager.Models.Division
        {
            MinimumAge = 55,
            Gender = BowlingMegabucks.TournamentManager.Models.Gender.Female
        };

        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            Name = new BowlingMegabucks.TournamentManager.Models.PersonName { First = "Ashlie", MiddleInitial = "S", Last = "Kipperman" },
            StreetAddress = "123 Anywhere Rd",
            CityAddress = "Hartford",
            StateAddress = "CT",
            ZipCode = "12345",
            EmailAddress = "email@gmail.com",
            PhoneNumber = "1234567890",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
            Gender = BowlingMegabucks.TournamentManager.Models.Gender.Female,
            USBCId = "123-456"
        };

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = bowler,
            Average = null,
            Division = division,
            Squads = squads,
            Sweepers = [],
            TournamentStartDate = new DateOnly(DateTime.Now.Year, 11, 24)
        };

        var results = _validator.Validate(registration);

        Assert.That(results.IsValid, Is.True);
    }
}
