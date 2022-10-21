
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
    public void Constructor_IGroupingBowlerModelSquadScoreModel_GameScoresMapped()
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
            Assert.That(bowlerSquadScore.GameScores, Has.Count.EqualTo(3));

            Assert.That(bowlerSquadScore.GameScores[1].Single(), Is.EqualTo(200));
            Assert.That(bowlerSquadScore.GameScores[2].Single(), Is.EqualTo(201));
            Assert.That(bowlerSquadScore.GameScores[3].Single(), Is.EqualTo(202));

            Assert.That(bowlerSquadScore.Score, Is.EqualTo(603));
            Assert.That(bowlerSquadScore.HighGame, Is.EqualTo(202));
        });
    }

    [Test]
    public void Equals_ObjNull_ReturnsFalse()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore();

#pragma warning disable CA1508 // Avoid dead conditional code
        Assert.That(bowlerSquadScore.Equals(null), Is.False);
#pragma warning restore CA1508 // Avoid dead conditional code
    }

    [Test]
    public void Equals_ObjNotBowlerSquadScore_ReturnsFalse()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore();

        Assert.That(bowlerSquadScore.Equals(new NortheastMegabuck.Models.LaneAssignment()), Is.False);
    }

    [Test]
    public void Equals_ObjBowlerSquadScore_DifferentBowlers_ReturnsFalse()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore();
        bowlerSquadScore.Bowler.Id = BowlerId.New();

        var other = new NortheastMegabuck.Models.BowlerSquadScore();
        bowlerSquadScore.Bowler.Id = BowlerId.New();

        Assert.That(bowlerSquadScore.Equals(other), Is.False);
    }

    [Test]
    public void Equals_ObjBowlerSquadScore_SameBowler_DifferentSquad_ReturnsFalse()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore();
        bowlerSquadScore.Bowler.Id = BowlerId.New();
        bowlerSquadScore.SquadId = SquadId.New();

        var other = new NortheastMegabuck.Models.BowlerSquadScore();
        other.Bowler.Id = bowlerSquadScore.Bowler.Id;
        other.SquadId = SquadId.New();

        Assert.That(bowlerSquadScore.Equals(other), Is.False);
    }

    [Test]
    public void Equals_ObjBowlerSquadScore_SameBowler_SameSquad_ReturnsTrue()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore();
        bowlerSquadScore.Bowler.Id = BowlerId.New();
        bowlerSquadScore.SquadId = SquadId.New();

        var other = new NortheastMegabuck.Models.BowlerSquadScore();
        other.Bowler.Id = bowlerSquadScore.Bowler.Id;
        other.SquadId = bowlerSquadScore.SquadId;

        Assert.That(bowlerSquadScore.Equals(other), Is.True);
    }

    [Test]
    public void GetHashCode_ReturnsBowlerIdCrossSquadId()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore();
        bowlerSquadScore.Bowler.Id = bowlerId;
        bowlerSquadScore.SquadId = squadId;

        Assert.That(bowlerSquadScore.GetHashCode(), Is.EqualTo(bowlerId.GetHashCode() ^ squadId.GetHashCode()));
    }

    [Test]
    public void CompareTo_OtherNull_Returns1()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore();

        Assert.That(bowlerSquadScore.CompareTo(null), Is.EqualTo(1));
    }

    [Test]
    public void CompareTo_OtherHasMoreGames_Returns1()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200,201)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        var other = new NortheastMegabuck.Models.BowlerSquadScore(200,201,202)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() },
        };

        Assert.That(bowlerSquadScore.CompareTo(other), Is.EqualTo(1));
    }

    [Test]
    public void CompareTo_OtherHasLessGames_ReturnsNegative1()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200,201)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() },
        };

        var other = new NortheastMegabuck.Models.BowlerSquadScore(200)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        Assert.That(bowlerSquadScore.CompareTo(other), Is.EqualTo(-1));
    }

    [Test]
    public void CompareTo_SameTotalGames_OtherHasHigherScore_Returns1()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200,201,202)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() },
        };

        var other = new NortheastMegabuck.Models.BowlerSquadScore(200,201,203)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        Assert.That(bowlerSquadScore.CompareTo(other), Is.EqualTo(1));
    }

    [Test]
    public void CompareTo_SameTotalGames_OtherHasLowerScore_ReturnsNegative1()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200,201,202)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        var other = new NortheastMegabuck.Models.BowlerSquadScore(200,201,200)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        Assert.That(bowlerSquadScore.CompareTo(other), Is.EqualTo(-1));
    }

    [Test]
    public void CompareTo_SameTotalGames_SameTotalScore_OtherHasHigherGame_Returns1()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200,200,200)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        var other = new NortheastMegabuck.Models.BowlerSquadScore(200,201,199)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        Assert.That(bowlerSquadScore.CompareTo(other), Is.EqualTo(1));
    }

    [Test]
    public void CompareTo_SameTotalGames_SameTotalScore_OtherHasLowerGame_ReturnsNegative1()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200,199,201)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        var other = new NortheastMegabuck.Models.BowlerSquadScore(200,200,200)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        Assert.That(bowlerSquadScore.CompareTo(other), Is.EqualTo(-1));
    }

    [Test]
    public void CompareTo_SameTotalGames_SameTotalScore_SameHighGame_Other2ndHighIsHigher_Returns1()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200,200,200,300)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        var other = new NortheastMegabuck.Models.BowlerSquadScore(200,199,201,300)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        Assert.That(bowlerSquadScore.CompareTo(other), Is.EqualTo(1));
    }

    [Test]
    public void CompareTo_SameTotalGames_SameTotalScore_SameHighGame_Other2ndHighIsLower_ReturnsNegative1()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200,200,200,300)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        var other = new NortheastMegabuck.Models.BowlerSquadScore(200,201,199,300)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        Assert.That(bowlerSquadScore.CompareTo(other), Is.EqualTo(1));
    }
}
