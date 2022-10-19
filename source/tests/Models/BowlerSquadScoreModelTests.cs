
namespace NortheastMegabuck.Tests.Models;

[TestFixture]
internal class BowlerSquadScore
{
    [Test]
    public void Constructor_IGroupingBowlerModelSquadScoreModel_BowlerMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Id = BowlerId.New()
        };

        var squadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = bowler,
            SquadId = SquadId.New(),
            GameNumber = 1,
            Score = 200
        };

        var squadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = bowler,
            SquadId = SquadId.New(),
            GameNumber = 2,
            Score = 201
        };

        var squadScore3 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = bowler,
            SquadId = SquadId.New(),
            GameNumber = 3,
            Score = 202
        };

        var squadScores = new[] { squadScore1, squadScore2, squadScore3 };

        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(squadScores.GroupBy(squadScore=> squadScore.Bowler).Single());

        Assert.That(bowlerSquadScore.Bowler, Is.EqualTo(bowler));    
    }

    /// <summary>
    /// In production, all SquadIds will be the same so it doesn't matter which element SquadId we use
    /// </summary>
    [Test]
    public void Constructor_IGroupingBowlerModelSquadScoreModel_SquadIdMappedToFirstSquadId()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Id = BowlerId.New()
        };

        var squadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = bowler,
            SquadId = SquadId.New(),
            GameNumber = 1,
            Score = 200
        };

        var squadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = bowler,
            SquadId = SquadId.New(),
            GameNumber = 2,
            Score = 201
        };

        var squadScore3 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = bowler,
            SquadId = SquadId.New(),
            GameNumber = 3,
            Score = 202
        };

        var squadScores = new[] { squadScore1, squadScore2, squadScore3 };

        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(squadScores.GroupBy(squadScore => squadScore.Bowler).Single());

        Assert.That(bowlerSquadScore.SquadId, Is.EqualTo(squadScore1.SquadId));
    }

    [Test]
    public void Constructor_IGroupingBowlerModelSquadScoreModel_ScoresMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Id = BowlerId.New()
        };

        var squadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = bowler,
            SquadId = SquadId.New(),
            GameNumber = 1,
            Score = 200
        };

        var squadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = bowler,
            SquadId = SquadId.New(),
            GameNumber = 2,
            Score = 201
        };

        var squadScore3 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = bowler,
            SquadId = SquadId.New(),
            GameNumber = 3,
            Score = 202
        };

        var squadScores = new[] { squadScore1, squadScore2, squadScore3 };

        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(squadScores.GroupBy(squadScore => squadScore.Bowler).Single());

        Assert.Multiple(() =>
        {
            Assert.That(bowlerSquadScore.Scores, Has.Count.EqualTo(3));

            Assert.That(bowlerSquadScore.Scores[1], Is.EqualTo(200));
            Assert.That(bowlerSquadScore.Scores[2], Is.EqualTo(201));
            Assert.That(bowlerSquadScore.Scores[3], Is.EqualTo(202));
        });
    }
}
