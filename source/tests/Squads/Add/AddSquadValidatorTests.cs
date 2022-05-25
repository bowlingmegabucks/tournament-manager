using FluentValidation;
using FluentValidation.TestHelper;

namespace NewEnglandClassic.Tests.Squads.Add;

[TestFixture]
internal class Validator
{
    private IValidator<NewEnglandClassic.Models.Squad> _validator;

    [SetUp]
    public void SetUp()
        => _validator = new NewEnglandClassic.Squads.Add.Validator();

    [Test]
    public void TournamentId_Empty_HasError()
    {
        var squad = new NewEnglandClassic.Models.Squad
        {
            TournamentId = Guid.Empty
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.TournamentId).WithErrorMessage("Tournament Id is required");
    }

    [Test]
    public void TournamentId_NotEmpty_DoesNotMatchTournamentTournamentId_HasError()
    {
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();

        var squad = new NewEnglandClassic.Models.Squad
        {
            TournamentId = id1,
            Tournament = new NewEnglandClassic.Models.Tournament
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
        var id = Guid.NewGuid();

        var squad = new NewEnglandClassic.Models.Squad
        {
            TournamentId = id,
            Tournament = new NewEnglandClassic.Models.Tournament
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
        var squad = new NewEnglandClassic.Models.Squad
        {
            FinalsRatio = null
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.FinalsRatio);
    }

    [Test]
    public void FinalsRatio_LessThanOrEqualTo1_HasError([Values(-1, 0, .5, 1)] decimal finalsRatio)
    {
        var squad = new NewEnglandClassic.Models.Squad
        {
            FinalsRatio = finalsRatio
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.FinalsRatio).WithErrorMessage("Finals ratio must be greater than 1");
    }

    [Test]
    public void FinalsRatio_GreaterThan1_NoError()
    {
        var squad = new NewEnglandClassic.Models.Squad
        {
            FinalsRatio = 1.1m
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.FinalsRatio);
    }

    [Test]
    public void CashRatio_Null_NoError()
    {
        var squad = new NewEnglandClassic.Models.Squad
        {
            CashRatio = null
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.CashRatio);
    }

    [Test]
    public void CashRatio_LessThanOrEqualTo1_HasError([Values(-1, 0, .5, 1)] decimal cashRatio)
    {
        var squad = new NewEnglandClassic.Models.Squad
        {
            CashRatio = cashRatio
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.CashRatio).WithErrorMessage("Cash ratio must be greater than 1");
    }

    [Test]
    public void CashRatio_GreaterThan1_NoError()
    {
        var squad = new NewEnglandClassic.Models.Squad
        {
            CashRatio = 1.1m
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.CashRatio);
    }

    [Test]
    public void Date_BeforeTournamentStart_HasError()
    {
        var squad = new NewEnglandClassic.Models.Squad
        {
            Date = new DateTime(2018, 1, 1),
            Tournament = new NewEnglandClassic.Models.Tournament
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
        var squad = new NewEnglandClassic.Models.Squad
        {
            Date = new DateTime(2018, 1, 2),
            Tournament = new NewEnglandClassic.Models.Tournament
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
        var squad = new NewEnglandClassic.Models.Squad
        {
            Date = new DateTime(2018, 1, 3),
            Tournament = new NewEnglandClassic.Models.Tournament
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
        var squad = new NewEnglandClassic.Models.Squad
        {
            Date = new DateTime(2018, 1, 1),
            Tournament = new NewEnglandClassic.Models.Tournament
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
        var squad = new NewEnglandClassic.Models.Squad
        {
            Date = new DateTime(2018, 1, 2),
            Tournament = new NewEnglandClassic.Models.Tournament
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
        var squad = new NewEnglandClassic.Models.Squad
        {
            Date = new DateTime(2018, 1, 3),
            Tournament = new NewEnglandClassic.Models.Tournament
            {
                End = new DateOnly(2018, 1, 2)
            }
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.Date).WithErrorMessage("Squad date must be before tournament end");
    }

    [Test]
    public void MaxPerPair_LessThanOrEqualToZero_HasError([Values(-1, 0)] int maxPerPair)
    {
        var squad = new NewEnglandClassic.Models.Squad
        {
            MaxPerPair = maxPerPair
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.MaxPerPair).WithErrorMessage("Max per pair must be greater than 0");
    }

    [Test]
    public void MaxPerPair_GreaterThanZero_NoError()
    {
        var squad = new NewEnglandClassic.Models.Squad
        {
            MaxPerPair = 1
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.MaxPerPair);
    }

    [Test]
    public void Complete_False_NoError()
    {
        var squad = new NewEnglandClassic.Models.Squad
        {
            Complete = false
        };

        var result = _validator.TestValidate(squad);
        result.ShouldNotHaveValidationErrorFor(squad => squad.Complete);
    }

    [Test]
    public void Complete_True_HasError()
    {
        var squad = new NewEnglandClassic.Models.Squad
        {
            Complete = true
        };

        var result = _validator.TestValidate(squad);
        result.ShouldHaveValidationErrorFor(squad => squad.Complete).WithErrorMessage("Cannot add a completed squad");
    }
}
