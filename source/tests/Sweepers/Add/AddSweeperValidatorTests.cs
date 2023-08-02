using FluentValidation;
using FluentValidation.TestHelper;

namespace NortheastMegabuck.Tests.Sweepers.Add;

[TestFixture]
internal class Validator
{
    private IValidator<NortheastMegabuck.Models.Sweeper> _validator;

    [SetUp]
    public void SetUp()
        => _validator = new NortheastMegabuck.Sweepers.Add.Validator();

    [Test]
    public void TournamentId_Empty_HasError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            TournamentId = TournamentId.Empty
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldHaveValidationErrorFor(sweeper => sweeper.TournamentId).WithErrorMessage("Tournament Id is required");
    }

    [Test]
    public void Tournament_Null_HasError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            Tournament = null
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldHaveValidationErrorFor(sweeper => sweeper.Tournament).WithErrorMessage("Tournament is required");
    }

    [Test]
    public void Tournament_NotNull_NoError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            Tournament = new NortheastMegabuck.Models.Tournament
            {
                Id = TournamentId.New()
            }
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldNotHaveValidationErrorFor(sweeper => sweeper.Tournament);
    }

    [Test]
    public void TournamentId_NotEmpty_DoesNotMatchTournamentTournamentId_HasError()
    {
        var id1 = TournamentId.New();
        var id2 = TournamentId.New();

        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            TournamentId = id1,
            Tournament = new NortheastMegabuck.Models.Tournament
            {
                Id = id2
            }
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldHaveValidationErrorFor(sweeper => sweeper.TournamentId).WithErrorMessage("Tournament Id does not match");
    }

    [Test]
    public void TournamentId_NotEmpty_MatchesTournamentTournamentId_NoError()
    {
        var id = TournamentId.New();

        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            TournamentId = id,
            Tournament = new NortheastMegabuck.Models.Tournament
            {
                Id = id
            }
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldNotHaveValidationErrorFor(sweeper => sweeper.TournamentId);
    }

    [Test]
    public void Games_LessThan1_HasError([Values(-1, 0)] short games)
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            Games = games
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldHaveValidationErrorFor(sweeper => sweeper.Games).WithErrorMessage("Games must be greater than 0");
    }

    [Test]
    public void FinalsRatio_GreaterThanOrEqualTo1_NoError([Values(1,2)]short games)
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            Games = games
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldNotHaveValidationErrorFor(sweeper => sweeper.Games);
    }

    [Test]
    public void CashRatio_LessThanOrEqualTo1_HasError([Values(-1, 0, .5, 1)] decimal cashRatio)
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            CashRatio = cashRatio
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldHaveValidationErrorFor(sweeper => sweeper.CashRatio).WithErrorMessage("Cash ratio must be greater than 1");
    }

    [Test]
    public void CashRatio_GreaterThan1_NoError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            CashRatio = 1.1m
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldNotHaveValidationErrorFor(sweeper => sweeper.CashRatio);
    }

    [Test]
    public void Date_BeforeTournamentStart_HasError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            Date = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
            Tournament = new NortheastMegabuck.Models.Tournament
            {
                Start = new DateOnly(2018, 1, 2)
            }
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldHaveValidationErrorFor(sweeper => sweeper.Date).WithErrorMessage("Sweeper date must be after tournament start");
    }

    [Test]
    public void Date_OnTournamentStart_NoError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            Date = new DateTime(2018, 1, 2, 0, 0, 0, DateTimeKind.Unspecified),
            Tournament = new NortheastMegabuck.Models.Tournament
            {
                Start = new DateOnly(2018, 1, 2),
                End = new DateOnly(2018, 1, 3)
            }
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldNotHaveValidationErrorFor(sweeper => sweeper.Date);
    }

    [Test]
    public void Date_AfterTournamentStart_NoError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            Date = new DateTime(2018, 1, 3, 0, 0, 0, DateTimeKind.Unspecified),
            Tournament = new NortheastMegabuck.Models.Tournament
            {
                Start = new DateOnly(2018, 1, 2),
                End = new DateOnly(2018, 1, 4)
            }
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldNotHaveValidationErrorFor(sweeper => sweeper.Date);
    }

    [Test]
    public void Date_BeforeTournamentEnd_NoError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            Date = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
            Tournament = new NortheastMegabuck.Models.Tournament
            {
                End = new DateOnly(2018, 1, 2)
            }
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldNotHaveValidationErrorFor(sweeper => sweeper.Date);
    }

    [Test]
    public void Date_OnTournamentEnd_NoError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            Date = new DateTime(2018, 1, 2, 0, 0, 0, DateTimeKind.Unspecified),
            Tournament = new NortheastMegabuck.Models.Tournament
            {
                End = new DateOnly(2018, 1, 2)
            }
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldNotHaveValidationErrorFor(sweeper => sweeper.Date);
    }

    [Test]
    public void Date_AfterTournamentEnd_HasError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            Date = new DateTime(2018, 1, 3, 0, 0, 0, DateTimeKind.Unspecified),
            Tournament = new NortheastMegabuck.Models.Tournament
            {
                End = new DateOnly(2018, 1, 2)
            }
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldHaveValidationErrorFor(sweeper => sweeper.Date).WithErrorMessage("Sweeper date must be before tournament end");
    }

    [Test]
    public void MaxPerPair_LessThanOrEqualToZero_HasError([Values(-1, 0)] short maxPerPair)
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            MaxPerPair = maxPerPair
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldHaveValidationErrorFor(sweeper => sweeper.MaxPerPair).WithErrorMessage("Max per pair must be greater than 0");
    }

    [Test]
    public void MaxPerPair_GreaterThanZero_NoError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            MaxPerPair = 1
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldNotHaveValidationErrorFor(sweeper => sweeper.MaxPerPair);
    }

    [Test]
    public void StartingLane_LessThanOrEqualToZero_HasError([Values(-1, 0)] short startingLane)
    {
        var squad = new NortheastMegabuck.Models.Sweeper
        {
            StartingLane = startingLane
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.StartingLane).WithErrorMessage("Starting lane must be greater than 0");
    }

    [Test]
    public void StartingLane_Even_HasError()
    {
        var squad = new NortheastMegabuck.Models.Sweeper
        {
            StartingLane = 2
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.StartingLane).WithErrorMessage("Starting lane must be odd");
    }

    [Test]
    public void StartingLane_GreaterThanZeroAndOdd_NoError()
    {
        var squad = new NortheastMegabuck.Models.Sweeper
        {
            StartingLane = 1
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.StartingLane);
    }

    [Test]
    public void NumberOfLanes_LessThanOrEqualToZero_HasError([Values(-1, 0)] short numberOfLanes)
    {
        var squad = new NortheastMegabuck.Models.Sweeper
        {
            NumberOfLanes = numberOfLanes
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.NumberOfLanes).WithErrorMessage("Number of lanes must be greater than 0");
    }

    [Test]
    public void NumberOfLanes_Odd_HasError()
    {
        var squad = new NortheastMegabuck.Models.Sweeper
        {
            NumberOfLanes = 11
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.NumberOfLanes).WithErrorMessage("Number of lanes must be even");
    }

    [Test]
    public void NumberOfLanes_GreaterThanZeroAndEven_NoError()
    {
        var squad = new NortheastMegabuck.Models.Sweeper
        {
            NumberOfLanes = 10
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.NumberOfLanes);
    }

    [Test]
    public void Complete_False_NoError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            Complete = false
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldNotHaveValidationErrorFor(sweeper => sweeper.Complete);
    }

    [Test]
    public void Complete_True_HasError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            Complete = true
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldHaveValidationErrorFor(sweeper => sweeper.Complete).WithErrorMessage("Cannot add a completed sweeper");
    }

    [Test]
    public void EntryFee_LessThanOrEqualToZero_HasError([Values(-1, 0)] decimal entryFee)
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            EntryFee = entryFee
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldHaveValidationErrorFor(sweeper => sweeper.EntryFee).WithErrorMessage("Entry fee must be greater than $0");
    }

    [Test]
    public void EntryFee_GreaterThanZero_NoError()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            EntryFee = .1m
        };

        var result = _validator.TestValidate(sweeper);
        result.ShouldNotHaveValidationErrorFor(sweeper => sweeper.EntryFee);
    }
}
