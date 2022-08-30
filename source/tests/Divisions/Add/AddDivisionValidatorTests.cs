using FluentValidation;
using FluentValidation.TestHelper;

namespace NortheastMegabuck.Tests.Divisions.Add;

[TestFixture]
internal class Validator
{
    private IValidator<NortheastMegabuck.Models.Division> _validator;

    [SetUp]
    public void SetUp()
        => _validator = new NortheastMegabuck.Divisions.Add.Validator();

    [Test]
    public void Number_LessThanOrEqualToZero_HasError([Values(-1, 0)] short number)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            Number = number
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.Number).WithErrorMessage("Division number must be greater than 0");
    }

    [Test]
    public void Number_GreaterThanZero_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            Number = 1
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.Number);
    }

    [Test]
    public void Name_NullEmptyOrWhitespace_HasError([Values(null, "", " ")] string name)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            Name = name
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.Name).WithErrorMessage("Division name is required");
    }

    [Test]
    public void Name_HasValue_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            Name = "Division 1"
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.Name);
    }

    [Test]
    public void MinimumAge_LessThanEqualToZero_HasError([Values(-1, 0)] short minimumAge)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = minimumAge
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MinimumAge).WithErrorMessage("Minimum age must be greater than 0");
    }

    [Test]
    public void MinimumAge_GreaterThanZero_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = 1
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MinimumAge);
    }

    [Test]
    public void MinimumAge_Null_MaximumAgeNullOrValue_NoError([Values(null,50)]short maximumAge)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = null,
            MaximumAge = maximumAge
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MinimumAge);
    }

    [Test]
    public void MinimumAge_GreaterThanMaximumAge_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = 50,
            MaximumAge = 49
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MinimumAge).WithErrorMessage("Minimum age must be less than or equal to maximum age");
    }

    [Test]
    public void MinimumAge_LessThanOrEqualToMaximumAge_NoError([Values(49, 50)] short minimumAge)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAge = minimumAge,
            MaximumAge = 50
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MinimumAge);
    }

    [Test]
    public void MaximumAge_LessThanOrEqualToZero_HasError([Values(-1, 0)] short maximumAge)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumAge = maximumAge
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MaximumAge).WithErrorMessage("Maximum age must be greater than 0");
    }
    
    [Test]
    public void MaximumAge_GreaterThanZero_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumAge = 1
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MaximumAge);
    }

    [Test]
    public void MaximumAge_Null_MinimumAgeNullOrHasValue_NoError([Values(null, 50)] short? minimumAge)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumAge = null,
            MinimumAge = minimumAge
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MaximumAge);
    }

    [Test]
    public void MaximumAge_LessThanMinimumAge_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumAge = 49,
            MinimumAge = 50
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MaximumAge).WithErrorMessage("Maximum age must be greater than or equal to minimum age");
    }

    [Test]
    public void MaximumAge_GreaterThanOrEqualToMinimumAge_NoError([Values(50, 51)] short maximumAge)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumAge = maximumAge,
            MinimumAge = 50
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MaximumAge);
    }
    
    [Test]
    public void MinimumAverage_LessThanOrEqualToZero_HasError([Values(-1, 0)] int minimumAverage)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = minimumAverage
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MinimumAverage).WithErrorMessage("Minimum average must be greater than 0");
    }

    [Test]
    public void MinimumAverage_GreaterThanEqualTo300_HasError([Values(300,301)] int minimumAverage)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = minimumAverage
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MinimumAverage).WithErrorMessage("Minimum average must be less than 300");
    }

    [Test]
    public void MinimumAverage_BetweenZeroAnd300_NoError([Values(1, 299)] int minimumAverage)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = minimumAverage
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MinimumAverage);
    }

    [Test]
    public void MinimumAverage_Null_MaximumAverageNullOrHasValue_NoError([Values(null, 200)] int? maximumAverage)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = null,
            MaximumAverage = maximumAverage
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MinimumAverage);
    }

    [Test]
    public void MinimumAverage_GreaterThanMaximumAverage_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = 200,
            MaximumAverage = 199
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MinimumAverage).WithErrorMessage("Minimum average must be less than or equal to maximum average");
    }

    [Test]
    public void MinimumAverage_LessThanOrEqualToMaximumAverage_NoError([Values(199, 200)] int minimumAverage)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MinimumAverage = minimumAverage,
            MaximumAverage = 200
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MinimumAverage);
    }

    [Test]
    public void MaximumAverage_LessThanOrEqualToZero_HasError([Values(-1, 0)] int maximumAverage)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumAverage = maximumAverage
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MaximumAverage).WithErrorMessage("Maximum average must be greater than 0");
    }

    [Test]
    public void MaximumAverage_GreaterThanEqualTo300_HasError([Values(300, 301)] int maximumAverage)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumAverage = maximumAverage
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MaximumAverage).WithErrorMessage("Maximum average must be less than 300");
    }

    [Test]
    public void MaximumAverage_Null_MinimumAverageNullOrHasValue_NoError([Values(null, 200)] int? minimumAverage)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumAverage = null,
            MinimumAverage = minimumAverage
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MaximumAverage);
    }

    [Test]
    public void MaximumAverage_GreaterThanOrEqualToMinimumAverage_NoError([Values(200, 201)] int maximumAverage)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumAverage = maximumAverage,
            MinimumAverage = 200
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MaximumAverage);
    }

    [Test]
    public void MaximumAverage_LessThanMinumumAverage_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumAverage = 199,
            MinimumAverage = 200
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MaximumAverage).WithErrorMessage("Maximum average must be greater than or equal to minimum average");
    }

    [Test]
    public void HandicapPercentage_LessThanZero_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapPercentage = -.1m
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.HandicapPercentage).WithErrorMessage("Handicap percentage must be greater than or equal to 0");
    }

    [Test]
    public void HandicapPercentage_GreaterThanOrEqualToZero_NoError([Values(0,.1)]decimal handicapPercentage)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapPercentage = handicapPercentage
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.HandicapPercentage);
    }

    [Test]
    public void HandicapPercentage_Null_HandicapBase_Null_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapPercentage = null,
            HandicapBase = null
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.HandicapPercentage);
    }

    [Test]
    public void HandicapPercentage_Null_HandicapBase_NotNull_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapPercentage = null,
            HandicapBase = 200
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.HandicapPercentage).WithErrorMessage("Handicap percentage must be specified if handicap base is specified");
    }

    [Test]
    public void HandicapPercentage_HasValue_HandicapBase_HasValue_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapPercentage = .1m,
            HandicapBase = 200
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.HandicapPercentage);
    }

    [Test]
    public void HandicapBase_LessThanZero_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapBase = -1
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.HandicapBase).WithErrorMessage("Handicap base must be between 0 and 300");
    }

    [Test]
    public void HandicapBase_GreaterThan300_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapBase = 301
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.HandicapBase).WithErrorMessage("Handicap base must be between 0 and 300");
    }

    [Test]
    public void HandicapBase_BetweenZeroAnd300_NoError([Values(0, 300)] int handicapBase)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapBase = handicapBase
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.HandicapBase);
    }

    [Test]
    public void HandicapBase_Null_HandicapPercentage_Null_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapBase = null,
            HandicapPercentage = null
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.HandicapBase);
    }

    [Test]
    public void HandicapBase_Null_HandicapPercentage_NotNull_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapBase = null,
            HandicapPercentage = .1m
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.HandicapBase).WithErrorMessage("Handicap base must be specified if handicap percentage is specified");
    }

    [Test]
    public void HandicapBase_HasValue_HandicapPercentage_HasValue_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            HandicapBase = 200,
            HandicapPercentage = .1m
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.HandicapBase);
    }

    [Test]
    public void MaximumHandicapPerGame_LessThanOrEqualToZero_HasError([Values(-1, 0)] int maximumHandicapPerGame)
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumHandicapPerGame = maximumHandicapPerGame
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MaximumHandicapPerGame).WithErrorMessage("Maximum handicap per game must be greater than 0");
    }

    [Test]
    public void MaximumHandicapPerGame_GreaterThanZero_HandicapPercentageHasValue_HandicapBaseHasValue_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumHandicapPerGame = 1,
            HandicapBase = 200,
            HandicapPercentage = .1m
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MaximumHandicapPerGame);
    }

    [Test]
    public void MaximumHandicapPerGame_NotNull_HandicapPercentageNull_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumHandicapPerGame = 1,
            HandicapPercentage = null,
            HandicapBase = 200
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MaximumHandicapPerGame).WithErrorMessage("Maximum handicap per game can only be specified if both Handicap base and percentage are specified");
    }

    [Test]
    public void MaximumHandicapPerGame_NotNull_HandicapBaseNull_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumHandicapPerGame = 1,
            HandicapPercentage = .1m,
            HandicapBase = null
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MaximumHandicapPerGame).WithErrorMessage("Maximum handicap per game can only be specified if both Handicap base and percentage are specified");
    }

    [Test]
    public void MaximumHandicapPerGame_NotNull_HandicapBaseNull_HandicapPercentageNull_HasError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumHandicapPerGame = 1,
            HandicapPercentage = null,
            HandicapBase = null
        };

        var result = _validator.TestValidate(division);
        result.ShouldHaveValidationErrorFor(division => division.MaximumHandicapPerGame).WithErrorMessage("Maximum handicap per game can only be specified if both Handicap base and percentage are specified");
    }

    [Test]
    public void MaximumHandicapPerGame_Null_HandicapBaseNotNull_HandicapPercentageNotNull_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumHandicapPerGame = null,
            HandicapPercentage = .1m,
            HandicapBase = 200
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MaximumHandicapPerGame);
    }

    [Test]
    public void MaximumHandicapPerGame_NotNull_HandicapBaseNotNull_HandicapPercentageNotNull_NoError()
    {
        var division = new NortheastMegabuck.Models.Division
        {
            MaximumHandicapPerGame = 1,
            HandicapPercentage = .1m,
            HandicapBase = 200
        };

        var result = _validator.TestValidate(division);
        result.ShouldNotHaveValidationErrorFor(division => division.MaximumHandicapPerGame);
    }
}
