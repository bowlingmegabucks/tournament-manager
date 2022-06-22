
namespace NewEnglandClassic.Tests.Models;

[TestFixture]
internal class Registration
{
    [Test]
    public void Constructor_BowlerInstanciatedWithId()
    {
        var bowlerId = Guid.NewGuid();
        var divisionId = Guid.NewGuid();

        var squads = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var sweepers = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, average);

        Assert.That(registration.Bowler.Id, Is.EqualTo(bowlerId));
    }

    [Test]
    public void Constructor_DivisionInstanciatedWithId()
    {
        var bowlerId = Guid.NewGuid();
        var divisionId = Guid.NewGuid();

        var squads = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var sweepers = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, average);

        Assert.That(registration.Division.Id, Is.EqualTo(divisionId));
    }

    [Test]
    public void Constructor_SquadsMapped()
    {
        var bowlerId = Guid.NewGuid();
        var divisionId = Guid.NewGuid();

        var squads = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var sweepers = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, average);

        Assert.That(registration.Squads, Is.EqualTo(squads));
    }

    [Test]
    public void Constructor_SweepersMapped()
    {
        var bowlerId = Guid.NewGuid();
        var divisionId = Guid.NewGuid();

        var squads = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var sweepers = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        var average = 200;

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, average);

        Assert.That(registration.Sweepers, Is.EqualTo(sweepers));
    }

    [Test]
    public void Constructor_AverageMapped([Values(null, 200)] int? average)
    {
        var bowlerId = Guid.NewGuid();
        var divisionId = Guid.NewGuid();

        var squads = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var sweepers = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        var registration = new NewEnglandClassic.Models.Registration(bowlerId, divisionId, squads, sweepers, average);

        Assert.That(registration.Average, Is.EqualTo(average));
    }
}
