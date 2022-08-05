
namespace NewEnglandClassic.Tests.Models;

[TestFixture]
internal class Registration
{
    [Test]
    public void Constructor_BowlerSetToValue()
    {
        var bowler = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        bowler.SetupGet(b => b.LastName).Returns("lastName");

        var divisionId = DivisionId.New();

        var squads = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var sweepers = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(new NewEnglandClassic.Models.Bowler(bowler.Object), divisionId, squads, sweepers, average);

        Assert.That(registration.Bowler.LastName, Is.EqualTo("lastName"));
    }

    [Test]
    public void Constructor_BowlerInstanciatedWithId()
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var sweepers = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, average);

        Assert.That(registration.Bowler.Id, Is.EqualTo(bowlerId));
    }

    [Test]
    public void Constructor_DivisionInstanciatedWithId()
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var sweepers = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, average);

        Assert.That(registration.Division.Id, Is.EqualTo(divisionId));
    }

    [Test]
    public void Constructor_SquadsMapped()
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var sweepers = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, average);

        Assert.That(registration.Squads, Is.EqualTo(squads));
    }

    [Test]
    public void Constructor_SweepersMapped()
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var sweepers = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, average);

        Assert.That(registration.Sweepers, Is.EqualTo(sweepers));
    }

    [Test]
    public void Constructor_AverageMapped([Values(null, 200)] int? average)
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var sweepers = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, average);

        Assert.That(registration.Average, Is.EqualTo(average));
    }
}
