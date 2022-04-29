using FluentValidation;
using FluentValidation.TestHelper;

namespace NewEnglandClassic.Tests.Tournaments.Add;

[TestFixture]
internal class Validation
{
    private IValidator<NewEnglandClassic.Models.Tournament> _validator;

    [OneTimeSetUp]
    public void SetUp()
        => _validator = new NewEnglandClassic.Tournaments.Add.Validator();

    [Test]
    public void Name_NullEmptyWhitespace_HasError([Values(null, "", " ")] string name)
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            Name = name
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldHaveValidationErrorFor(tournament => tournament.Name).WithErrorMessage("Name is required");
    }

    [Test]
    public void Name_HasValue_NoError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            Name = "name"
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldNotHaveValidationErrorFor(tournament => tournament.Name);
    }

    [Test]
    public void Start_StartBeforeEnd_NoError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            Start = new DateOnly(2000,1,1),
            End = new DateOnly(2000,1,2)
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldNotHaveValidationErrorFor(tournament => tournament.Start);
    }

    [Test]
    public void Start_StartEqualsEnd_NoError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            Start = new DateOnly(2000, 1, 1),
            End = new DateOnly(2000, 1, 1)
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldNotHaveValidationErrorFor(tournament => tournament.Start);
    }

    [Test]
    public void Start_StartAfterEnd_HasError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            Start = new DateOnly(2000, 1, 2),
            End = new DateOnly(2000, 1, 1)
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldHaveValidationErrorFor(tournament => tournament.Start).WithErrorMessage("Start date must be before end date");
    }

    [Test]
    public void End_StartBeforeEnd_NoError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            Start = new DateOnly(2000, 1, 1),
            End = new DateOnly(2000, 1, 2)
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldNotHaveValidationErrorFor(tournament => tournament.End);
    }

    [Test]
    public void End_StartEqualsEnd_NoError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            Start = new DateOnly(2000, 1, 1),
            End = new DateOnly(2000, 1, 1)
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldNotHaveValidationErrorFor(tournament => tournament.End);
    }

    [Test]
    public void End_StartAfterEnd_HasError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            Start = new DateOnly(2000, 1, 2),
            End = new DateOnly(2000, 1, 1)
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldHaveValidationErrorFor(tournament => tournament.End).WithErrorMessage("End date must be after start date");
    }

    [Test]
    public void EntryFee_LessThanZero_HasError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            EntryFee = -1
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldHaveValidationErrorFor(tournament => tournament.EntryFee).WithErrorMessage("Entry fee must be greater than $0");
    }

    [Test]
    public void EntryFee_Zero_HasError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            EntryFee = 0
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldHaveValidationErrorFor(tournament => tournament.EntryFee).WithErrorMessage("Entry fee must be greater than $0");
    }

    [Test]
    public void EntryFee_GreaterThanZero_NoError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            EntryFee = 1
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldNotHaveValidationErrorFor(tournament => tournament.EntryFee);
    }

    [Test]
    public void Games_LessThanZero_HasError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            Games = -1
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldHaveValidationErrorFor(tournament => tournament.Games).WithErrorMessage("Must have at least one game");
    }

    [Test]
    public void Games_Zero_HasError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            Games = 0
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldHaveValidationErrorFor(tournament => tournament.Games).WithErrorMessage("Must have at least one game");
    }

    [Test]
    public void Games_GreaterThanZero_NoError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            Games = 1
        };
        
        var result = _validator.TestValidate(tournament);

        result.ShouldNotHaveValidationErrorFor(tournament => tournament.Games);
    }

    [Test]
    public void FinalsRatio_LessThanOne_HasError([Values(-1,0,.9)]decimal finalsRatio)
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            FinalsRatio = finalsRatio
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldHaveValidationErrorFor(tournament => tournament.FinalsRatio).WithErrorMessage("Finals ratio must be greater than 1");
    }

    [Test]
    public void FinalsRatio_One_HasError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            FinalsRatio = 1
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldHaveValidationErrorFor(tournament => tournament.FinalsRatio).WithErrorMessage("Finals ratio must be greater than 1");
    }

    [Test]
    public void FinalsRatio_GreaterThanOne_NoError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            FinalsRatio = 1.1m
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldNotHaveValidationErrorFor(tournament => tournament.FinalsRatio);
    }

    [Test]
    public void CashRatio_LessThanOne_HasError([Values(-1, 0, .9)] decimal cashRatio)
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            CashRatio = cashRatio
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldHaveValidationErrorFor(tournament => tournament.CashRatio).WithErrorMessage("Cash ratio must be greater than 1");
    }

    [Test]
    public void CashRatio_One_HasError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            CashRatio = 1
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldHaveValidationErrorFor(tournament => tournament.CashRatio).WithErrorMessage("Cash ratio must be greater than 1");
    }

    [Test]
    public void CashRatio_GreaterThanOne_NoError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            CashRatio = 1.1m
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldNotHaveValidationErrorFor(tournament => tournament.CashRatio);
    }

    [Test]
    public void BowlingCenter_NullEmptyWhitespace_HasError([Values(null, "", " ")] string bowlingCenter)
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            BowlingCenter = bowlingCenter
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldHaveValidationErrorFor(tournament => tournament.BowlingCenter).WithErrorMessage("Bowling center is required");
    }

    [Test]
    public void BowlingCenter_HasValue_NoError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            BowlingCenter = "bowlingCenter"
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldNotHaveValidationErrorFor(tournament => tournament.BowlingCenter);
    }

    [Test]
    public void Completed_True_HasError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            Completed = true
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldHaveValidationErrorFor(tournament => tournament.Completed).WithErrorMessage("Tournament is already completed");
    }

    [Test]
    public void Completed_False_NoError()
    {
        var tournament = new NewEnglandClassic.Models.Tournament
        {
            Completed = false
        };

        var result = _validator.TestValidate(tournament);

        result.ShouldNotHaveValidationErrorFor(tournament => tournament.Completed);
    }
}
