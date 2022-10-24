
namespace NortheastMegabuck.Tests.Squads.Results;

[TestFixture]
internal class ViewModel
{
    [Test]
    public void Constructor_PlaceMapped()
    {
        short place = 5;

        var model = new NortheastMegabuck.Squads.Results.ViewModel(new NortheastMegabuck.Models.BowlerSquadScore(200), new DateTime(), place, false, false);

        Assert.That(model.Place, Is.EqualTo(place));
    }

    [Test]
    public void Constructor_SquadIdMapped()
    {
        var squadDate = new DateTime(2000, 1, 2, 9, 30, 0);
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200)
        {
            SquadId = SquadId.New()
        };

        var model = new NortheastMegabuck.Squads.Results.ViewModel(bowlerSquadScore, squadDate, 0, false, false);

        Assert.That(model.SquadId, Is.EqualTo(bowlerSquadScore.SquadId));
    }

    [Test]
    public void Constructor_SquadDateMapped()
    {
        var squadDate = new DateTime(2000, 1, 2, 9, 30, 0);
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200)
        {
            SquadId = SquadId.New()
        };

        var model = new NortheastMegabuck.Squads.Results.ViewModel(bowlerSquadScore, squadDate, 0, false, false);

        Assert.That(model.SquadDate, Is.EqualTo(squadDate));
    }

    [Test]
    public void Contructor_DivisionNameMapped()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200)
        {
            Division = new NortheastMegabuck.Models.Division { Name = "divisionName" }
        };

        var model = new NortheastMegabuck.Squads.Results.ViewModel(bowlerSquadScore, new DateTime(), 0, false, false);

        Assert.That(model.DivisionName, Is.EqualTo(bowlerSquadScore.Division.Name));
    }

    [Test]
    public void Constructor_BowlerNameMapped()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200);

        var model = new NortheastMegabuck.Squads.Results.ViewModel(bowlerSquadScore, new DateTime(), 0, false, false);

        Assert.That(model.BowlerName, Is.EqualTo(bowlerSquadScore.Bowler.ToString()));
    }

    [Test]
    public void Constructor_ScoreMapped()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200, 200, 200);

        var model = new NortheastMegabuck.Squads.Results.ViewModel(bowlerSquadScore, new DateTime(), 0, false, false);

        Assert.That(model.Score, Is.EqualTo(600));
    }

    [Test]
    public void Constructor_ScratchScoreMapped()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200, 200, 200);

        var model = new NortheastMegabuck.Squads.Results.ViewModel(bowlerSquadScore, new DateTime(), 0, false, false);

        Assert.That(model.ScratchScore, Is.EqualTo(600));
    }

    [Test]
    public void Constructor_HighGameMapped()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200, 200, 201);

        var model = new NortheastMegabuck.Squads.Results.ViewModel(bowlerSquadScore, new DateTime(), 0, false, false);

        Assert.That(model.HighGame, Is.EqualTo(201));
    }

    [Test]
    public void Constructor_HighGameScratchMapped()
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200, 200, 201);

        var model = new NortheastMegabuck.Squads.Results.ViewModel(bowlerSquadScore, new DateTime(), 0, false, false);

        Assert.That(model.HighGameScratch, Is.EqualTo(201));
    }

    [Test]
    public void Constructor__AdvancerMapped([Values]bool advancer)
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200);

        var model = new NortheastMegabuck.Squads.Results.ViewModel(bowlerSquadScore, new DateTime(), 0, advancer, false);

        Assert.That(model.Advancer, Is.EqualTo(advancer));
    }

    [Test]
    public void Constructor_PlaceLessThanOrEqualToAdvancer_CasherMappedFalse([Values]bool casher)
    {
        var bowlerSquadScore = new NortheastMegabuck.Models.BowlerSquadScore(200);

        var model = new NortheastMegabuck.Squads.Results.ViewModel(bowlerSquadScore, new DateTime(), 0, false, casher);

        Assert.That(model.Casher, Is.EqualTo(casher));
    }
}
