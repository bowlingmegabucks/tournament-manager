using FluentValidation;
using FluentValidation.TestHelper;

namespace BowlingMegabucks.TournamentManager.Tests.Squads.Add;

[TestFixture]
internal sealed class Validator
{
    private IValidator<BowlingMegabucks.TournamentManager.Models.Squad> _validator;

    [SetUp]
    public void SetUp()
        => _validator = new BowlingMegabucks.TournamentManager.Squads.Add.Validator();

    [Test]
    public void TournamentId_Empty_HasError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.Empty
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.TournamentId).WithErrorMessage("Tournament Id is required");
    }

    [Test]
    public void Tournament_Null_HasError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Tournament = null
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.Tournament).WithErrorMessage("Tournament is required");
    }

    [Test]
    public void Tournament_NotNull_NoError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Tournament = new BowlingMegabucks.TournamentManager.Models.Tournament
            {
                Id = TournamentId.New()
            }
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.Tournament);
    }

    [Test]
    public void TournamentId_NotEmpty_DoesNotMatchTournamentTournamentId_HasError()
    {
        var id1 = TournamentId.New();
        var id2 = TournamentId.New();

        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            TournamentId = id1,
            Tournament = new BowlingMegabucks.TournamentManager.Models.Tournament
            {
                Id = id2
            }
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.TournamentId).WithErrorMessage("Tournament Id does not match");
    }

    [Test]
    public void TournamentId_NotEmpty_MatchesTournamentTournamentId_NoError()
    {
        var id = TournamentId.New();

        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            TournamentId = id,
            Tournament = new BowlingMegabucks.TournamentManager.Models.Tournament
            {
                Id = id
            }
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.TournamentId);
    }

    [Test]
    public void FinalsRatio_Null_NoError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            FinalsRatio = null
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.FinalsRatio);
    }

    [Test]
    public void FinalsRatio_LessThanOrEqualTo1_HasError([Values(-1, 0, .5, 1)] decimal finalsRatio)
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            FinalsRatio = finalsRatio
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.FinalsRatio).WithErrorMessage("Finals ratio must be greater than 1");
    }

    [Test]
    public void FinalsRatio_GreaterThan1_NoError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            FinalsRatio = 1.1m
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.FinalsRatio);
    }

    [Test]
    public void CashRatio_Null_NoError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            CashRatio = null
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.CashRatio);
    }

    [Test]
    public void CashRatio_LessThanOrEqualTo1_HasError([Values(-1, 0, .5, 1)] decimal cashRatio)
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            CashRatio = cashRatio
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.CashRatio).WithErrorMessage("Cash ratio must be greater than 1");
    }

    [Test]
    public void CashRatio_GreaterThan1_NoError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            CashRatio = 1.1m
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.CashRatio);
    }

    [Test]
    public void Date_BeforeTournamentStart_HasError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Date = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
            Tournament = new BowlingMegabucks.TournamentManager.Models.Tournament
            {
                Start = new DateOnly(2018, 1, 2)
            }
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.Date).WithErrorMessage("Squad date must be after tournament start");
    }

    [Test]
    public void Date_OnTournamentStart_NoError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Date = new DateTime(2018, 1, 2, 0, 0, 0, DateTimeKind.Unspecified),
            Tournament = new BowlingMegabucks.TournamentManager.Models.Tournament
            {
                Start = new DateOnly(2018, 1, 2),
                End = new DateOnly(2018, 1, 3)
            }
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.Date);
    }

    [Test]
    public void Date_AfterTournamentStart_NoError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Date = new DateTime(2018, 1, 3, 0, 0, 0, DateTimeKind.Unspecified),
            Tournament = new BowlingMegabucks.TournamentManager.Models.Tournament
            {
                Start = new DateOnly(2018, 1, 2),
                End = new DateOnly(2018, 1, 4)
            }
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.Date);
    }

    [Test]
    public void Date_BeforeTournamentEnd_NoError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Date = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
            Tournament = new BowlingMegabucks.TournamentManager.Models.Tournament
            {
                End = new DateOnly(2018, 1, 2)
            }
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.Date);
    }

    [Test]
    public void Date_OnTournamentEnd_NoError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Date = new DateTime(2018, 1, 2, 0, 0, 0, DateTimeKind.Unspecified),
            Tournament = new BowlingMegabucks.TournamentManager.Models.Tournament
            {
                End = new DateOnly(2018, 1, 2)
            }
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.Date);
    }

    [Test]
    public void Date_AfterTournamentEnd_HasError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Date = new DateTime(2018, 1, 3, 0, 0, 0, DateTimeKind.Unspecified),
            Tournament = new BowlingMegabucks.TournamentManager.Models.Tournament
            {
                End = new DateOnly(2018, 1, 2)
            }
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.Date).WithErrorMessage("Squad date must be before tournament end");
    }

    [Test]
    public void MaxPerPair_LessThanOrEqualToZero_HasError([Values(-1, 0)] short maxPerPair)
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            MaxPerPair = maxPerPair
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.MaxPerPair).WithErrorMessage("Max per pair must be greater than 0");
    }

    [Test]
    public void MaxPerPair_GreaterThanZero_NoError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            MaxPerPair = 1
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.MaxPerPair);
    }

    [Test]
    public void StartingLane_LessThanOrEqualToZero_HasError([Values(-1, 0)] short startingLane)
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            StartingLane = startingLane
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.StartingLane).WithErrorMessage("Starting lane must be greater than 0");
    }

    [Test]
    public void StartingLane_Even_HasError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            StartingLane = 2
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.StartingLane).WithErrorMessage("Starting lane must be odd");
    }

    [Test]
    public void StartingLane_GreaterThanZeroAndOdd_NoError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            StartingLane = 1
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.StartingLane);
    }

    [Test]
    public void NumberOfLanes_LessThanOrEqualToZero_HasError([Values(-1, 0)] short numberOfLanes)
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            NumberOfLanes = numberOfLanes
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.NumberOfLanes).WithErrorMessage("Number of lanes must be greater than 0");
    }

    [Test]
    public void NumberOfLanes_Odd_HasError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            NumberOfLanes = 3
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.NumberOfLanes).WithErrorMessage("Number of lanes must be even");
    }

    [Test]
    public void NumberOfLanes_GreaterThanZeroAndEven_NoError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            NumberOfLanes = 10
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.NumberOfLanes);
    }

    [Test]
    public void Complete_False_NoError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Complete = false
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.Complete);
    }

    [Test]
    public void Complete_True_HasError()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Complete = true
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.Complete).WithErrorMessage("Cannot add a completed squad");
    }
}
