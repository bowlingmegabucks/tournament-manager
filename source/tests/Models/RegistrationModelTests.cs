
namespace NewEnglandClassic.Tests.Models;

[TestFixture]
internal class Registration
{
    [Test]
    public void Constructor_BowlerSetToValue([Values]bool superSweeper)
    {
        var bowler = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        bowler.SetupGet(b => b.LastName).Returns("lastName");

        var divisionId = DivisionId.New();

        var squads = new List<SquadId> { SquadId.New(), SquadId.New() };
        var sweepers = new List<SquadId> { SquadId.New(), SquadId.New() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(new NewEnglandClassic.Models.Bowler(bowler.Object), divisionId, squads, sweepers, superSweeper, average);

        Assert.That(registration.Bowler.LastName, Is.EqualTo("lastName"));
    }

    [Test]
    public void Constructor_BowlerInstanciatedWithId([Values] bool superSweeper)
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<SquadId> { SquadId.New(), SquadId.New() };
        var sweepers = new List<SquadId> { SquadId.New(), SquadId.New() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers,superSweeper, average);

        Assert.That(registration.Bowler.Id, Is.EqualTo(bowlerId));
    }

    [Test]
    public void Constructor_DivisionInstanciatedWithId([Values] bool superSweeper)
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<SquadId> { SquadId.New(), SquadId.New() };
        var sweepers = new List<SquadId> { SquadId.New(), SquadId.New() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, superSweeper, average);

        Assert.That(registration.Division.Id, Is.EqualTo(divisionId));
    }

    [Test]
    public void Constructor_SquadsMapped([Values] bool superSweeper)
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<SquadId> { SquadId.New(), SquadId.New() };
        var sweepers = new List<SquadId> { SquadId.New(), SquadId.New() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, superSweeper, average);

        Assert.That(registration.Squads, Is.EqualTo(squads));
    }

    [Test]
    public void Constructor_SweepersMapped([Values] bool superSweeper)
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<SquadId> { SquadId.New(), SquadId.New() };
        var sweepers = new List<SquadId> { SquadId.New(), SquadId.New() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers,superSweeper, average);

        Assert.That(registration.Sweepers, Is.EqualTo(sweepers));
    }

    [Test]
    public void Constructor_AverageMapped([Values(null, 200)] int? average, [Values] bool superSweeper)
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<SquadId> { SquadId.New(), SquadId.New() };
        var sweepers = new List<SquadId> { SquadId.New(), SquadId.New() };

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, superSweeper, average);

        Assert.That(registration.Average, Is.EqualTo(average));
    }

    [Test]
    public void Constructor_SuperSweeperMapped([Values] bool superSweeper)
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<SquadId> { SquadId.New(), SquadId.New() };
        var sweepers = new List<SquadId> { SquadId.New(), SquadId.New() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, superSweeper, average);

        Assert.That(registration.SuperSweeper, Is.EqualTo(superSweeper));
    }
}
