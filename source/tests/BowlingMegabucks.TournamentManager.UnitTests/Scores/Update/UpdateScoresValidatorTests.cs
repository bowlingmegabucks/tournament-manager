using FluentValidation.TestHelper;

namespace BowlingMegabucks.TournamentManager.Tests.Scores.Update;

[TestFixture]
internal sealed class Validator
{
    private BowlingMegabucks.TournamentManager.Scores.Update.Validator _validator;

    [OneTimeSetUp]
    public void SetUp()
        => _validator = new BowlingMegabucks.TournamentManager.Scores.Update.Validator();

    [Test]
    public void HappyPath_NoErrors()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var score1 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 1,
            Score = 200
        };

        var score2 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 2,
            Score = 220
        };

        var score3 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 3,
            Score = 240
        };

        var scores = new[] { score1, score2, score3 };

        var result = _validator.TestValidate(scores);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void BowlerIdEmptyOnScore_HasError()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var score1 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 1,
            Score = 200
        };

        var score2 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = BowlerId.Empty },
            SquadId = squadId,
            GameNumber = 2,
            Score = 220
        };

        var score3 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 3,
            Score = 240
        };

        var scores = new[] { score1, score2, score3 };

        var result = _validator.TestValidate(scores);

        result.ShouldHaveValidationErrorFor("squadScores[1].Bowler.Id").WithErrorMessage("Bowler Id is required");
    }

    [Test]
    public void SquadIdEmptyOnScore_HasError()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var score1 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 1,
            Score = 200
        };

        var score2 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = SquadId.Empty,
            GameNumber = 2,
            Score = 220
        };

        var score3 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 3,
            Score = 240
        };

        var scores = new[] { score1, score2, score3 };

        var result = _validator.TestValidate(scores);

        result.ShouldHaveValidationErrorFor("squadScores[1].SquadId").WithErrorMessage("Squad Id is required");
    }

    [Test]
    public void GameNumberLessThanOne_HasError()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var score1 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 1,
            Score = 200
        };

        var score2 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 2,
            Score = 220
        };

        var score3 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 0,
            Score = 240
        };

        var scores = new[] { score1, score2, score3 };

        var result = _validator.TestValidate(scores);

        result.ShouldHaveValidationErrorFor("squadScores[2].GameNumber").WithErrorMessage("Game number must be greater than zero");
    }

    [Test]
    public void ScoreLessThanOne_HasError()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var score1 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 1,
            Score = 0
        };

        var score2 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 2,
            Score = 220
        };

        var score3 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 3,
            Score = 240
        };

        var scores = new[] { score1, score2, score3 };

        var result = _validator.TestValidate(scores);

        result.ShouldHaveValidationErrorFor("squadScores[0].Score");
    }

    [Test]
    public void ScoreGreaterThan300_HasError()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var score1 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 1,
            Score = 200
        };

        var score2 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 2,
            Score = 301
        };

        var score3 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 3,
            Score = 240
        };

        var scores = new[] { score1, score2, score3 };

        var result = _validator.TestValidate(scores);

        result.ShouldHaveValidationErrorFor("squadScores[1].Score");
    }

    [Test]
    public void ScoresMissingGame_HasError()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var score1 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 4,
            Score = 200
        };

        var score2 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 2,
            Score = 220
        };

        var score3 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 3,
            Score = 240
        };

        var scores = new[] { score1, score2, score3 };

        var result = _validator.TestValidate(scores);

        result.ShouldHaveValidationErrorFor(string.Empty).WithErrorMessage("Missing game for bowler");
    }

    [Test]
    public void ScoresForDifferentBowlers_HasError()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var score1 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 1,
            Score = 200
        };

        var score2 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 2,
            Score = 220
        };

        var score3 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = BowlerId.New() },
            SquadId = squadId,
            GameNumber = 3,
            Score = 240
        };

        var scores = new[] { score1, score2, score3 };

        var result = _validator.TestValidate(scores);
        result.ShouldHaveValidationErrorFor(string.Empty).WithErrorMessage("Scores must be for the same bowler");
    }

    [Test]
    public void ScoresForDifferentSquadds_HasError()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var score1 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = SquadId.New(),
            GameNumber = 1,
            Score = 200
        };

        var score2 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 2,
            Score = 220
        };

        var score3 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 3,
            Score = 240
        };

        var scores = new[] { score1, score2, score3 };

        var result = _validator.TestValidate(scores);
        result.ShouldHaveValidationErrorFor(string.Empty).WithErrorMessage("Scores must be for the same squad");
    }

    [Test]
    public void MultipleScoresForSameGame_HasError()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var score1 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 1,
            Score = 200
        };

        var score2 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 2,
            Score = 220
        };

        var score3 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = bowlerId },
            SquadId = squadId,
            GameNumber = 2,
            Score = 240
        };

        var scores = new[] { score1, score2, score3 };

        var result = _validator.TestValidate(scores);
        result.ShouldHaveValidationErrorFor(string.Empty).WithErrorMessage("Duplicate game for bowler");
    }
}
