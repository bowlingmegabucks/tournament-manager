
using BowlingMegabucks.TournamentManager.Bowlers;

namespace BowlingMegabucks.TournamentManager.UnitTests.Models;

[TestFixture]
internal sealed class Registration
{
    [Test]
    public void Constructor_BowlerSetToValue([Values] bool superSweeper)
    {
        var bowler = new Mock<IViewModel>();
        bowler.SetupGet(b => b.LastName).Returns("lastName");

        var divisionId = DivisionId.New();

        var squads = new List<SquadId> { SquadId.New(), SquadId.New() };
        var sweepers = new List<SquadId> { SquadId.New(), SquadId.New() };

        var average = 200;

        var registration = new TournamentManager.Models.Registration(bowler.Object.ToModel(), divisionId, squads, sweepers, superSweeper, average);

        Assert.That(registration.Bowler.Name.Last, Is.EqualTo("lastName"));
    }

    [Test]
    public void Constructor_BowlerInstantiatedWithId([Values] bool superSweeper)
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<SquadId> { SquadId.New(), SquadId.New() };
        var sweepers = new List<SquadId> { SquadId.New(), SquadId.New() };

        var average = 200;

        var registration = new TournamentManager.Models.Registration(bowlerId, divisionId, squads, sweepers, superSweeper, average);

        Assert.That(registration.Bowler.Id, Is.EqualTo(bowlerId));
    }

    [Test]
    public void Constructor_DivisionInstantiatedWithId([Values] bool superSweeper)
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<SquadId> { SquadId.New(), SquadId.New() };
        var sweepers = new List<SquadId> { SquadId.New(), SquadId.New() };

        var average = 200;

        var registration = new TournamentManager.Models.Registration(bowlerId, divisionId, squads, sweepers, superSweeper, average);

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

        var registration = new TournamentManager.Models.Registration(bowlerId, divisionId, squads, sweepers, superSweeper, average);

        Assert.That(registration.Squads.Select(squad => squad.Id), Is.EqualTo(squads));
    }

    [Test]
    public void Constructor_SweepersMapped([Values] bool superSweeper)
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<SquadId> { SquadId.New(), SquadId.New() };
        var sweepers = new List<SquadId> { SquadId.New(), SquadId.New() };

        var average = 200;

        var registration = new TournamentManager.Models.Registration(bowlerId, divisionId, squads, sweepers, superSweeper, average);

        Assert.That(registration.Sweepers.Select(sweeper => sweeper.Id), Is.EqualTo(sweepers));
    }

    [Test]
    public void Constructor_AverageMapped([Values(null, 200)] int? average, [Values] bool superSweeper)
    {
        var bowlerId = BowlerId.New();
        var divisionId = DivisionId.New();

        var squads = new List<SquadId> { SquadId.New(), SquadId.New() };
        var sweepers = new List<SquadId> { SquadId.New(), SquadId.New() };

        var registration = new TournamentManager.Models.Registration(bowlerId, divisionId, squads, sweepers, superSweeper, average);

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

        var registration = new TournamentManager.Models.Registration(bowlerId, divisionId, squads, sweepers, superSweeper, average);

        Assert.That(registration.SuperSweeper, Is.EqualTo(superSweeper));
    }

    [Test]
    public void Constructor_Entity_IdMapped([Values] bool superSweeper)
    {
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId1 = SquadId.New();
        var sweeperId2 = SquadId.New();

        var entity = new TournamentManager.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new TournamentManager.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new TournamentManager.Database.Entities.Division { Id = DivisionId.New() },
            Average = 200,
            Squads =
            [
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId1} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad {Id = sweeperId1, CashRatio = 5, Divisions = []} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId2} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad { Id = sweeperId2, CashRatio = 5, Divisions = []} }
            ],
            SuperSweeper = superSweeper
        };

        var model = new TournamentManager.Models.Registration(entity);

        Assert.That(model.Id, Is.EqualTo(entity.Id));
    }

    [Test]
    public void Constructor_Entity_BowlerMapped([Values] bool superSweeper)
    {
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId1 = SquadId.New();
        var sweeperId2 = SquadId.New();

        var entity = new TournamentManager.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new TournamentManager.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new TournamentManager.Database.Entities.Division { Id = DivisionId.New() },
            Average = 200,
            Squads =
            [
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId1} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad {Id = sweeperId1, CashRatio = 5, Divisions = []} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId2} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad { Id = sweeperId2, CashRatio = 5, Divisions = []} }
            ],
            SuperSweeper = superSweeper
        };

        var model = new TournamentManager.Models.Registration(entity);

        Assert.That(model.Bowler.Id, Is.EqualTo(entity.Bowler.Id));
    }

    [Test]
    public void Constructor_Entity_DivisionMapped([Values] bool superSweeper)
    {
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId1 = SquadId.New();
        var sweeperId2 = SquadId.New();

        var entity = new TournamentManager.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new TournamentManager.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new TournamentManager.Database.Entities.Division { Id = DivisionId.New() },
            Average = 200,
            Squads =
            [
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId1} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad {Id = sweeperId1, CashRatio = 5, Divisions = []} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId2} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad { Id = sweeperId2, CashRatio = 5, Divisions = []} }
            ],
            SuperSweeper = superSweeper
        };

        var model = new TournamentManager.Models.Registration(entity);

        Assert.That(model.Division.Id, Is.EqualTo(entity.Division.Id));
    }

    [Test]
    public void Constructor_Entity_AverageMapped([Values] bool superSweeper, [Values(null, 200)] int? average)
    {
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId1 = SquadId.New();
        var sweeperId2 = SquadId.New();

        var entity = new TournamentManager.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new TournamentManager.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new TournamentManager.Database.Entities.Division { Id = DivisionId.New() },
            Average = average,
            Squads =
            [
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId1} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad {Id = sweeperId1, CashRatio = 5, Divisions = []} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId2} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad { Id = sweeperId2, CashRatio = 5, Divisions = []} }
            ],
            SuperSweeper = superSweeper
        };

        var model = new TournamentManager.Models.Registration(entity);

        Assert.That(model.Average, Is.EqualTo(entity.Average));
    }

    [Test]
    public void Constructor_Entity_SquadsMapped([Values] bool superSweeper)
    {
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId1 = SquadId.New();
        var sweeperId2 = SquadId.New();

        var entity = new TournamentManager.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new TournamentManager.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new TournamentManager.Database.Entities.Division { Id = DivisionId.New() },
            Average = 200,
            Squads =
            [
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId1} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad {Id = sweeperId1, CashRatio = 5, Divisions = []} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId2} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad { Id = sweeperId2, CashRatio = 5, Divisions = []} }
            ],
            SuperSweeper = superSweeper
        };

        var model = new TournamentManager.Models.Registration(entity);

        Assert.Multiple(() =>
        {
            Assert.That(model.Squads.Count(), Is.EqualTo(2));

            Assert.That(model.Squads.Count(squad => squad.Id == squadId1), Is.EqualTo(1));
            Assert.That(model.Squads.Count(squad => squad.Id == squadId2), Is.EqualTo(1));
        });
    }

    [Test]
    public void Constructor_Entity_SweepersMapped([Values] bool superSweeper)
    {
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId1 = SquadId.New();
        var sweeperId2 = SquadId.New();

        var entity = new TournamentManager.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new TournamentManager.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new TournamentManager.Database.Entities.Division { Id = DivisionId.New() },
            Average = 200,
            Squads =
            [
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId1} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad {Id = sweeperId1, CashRatio = 5, Divisions = []} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId2} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad { Id = sweeperId2, CashRatio = 5, Divisions = []} }
            ],
            SuperSweeper = superSweeper
        };

        var model = new TournamentManager.Models.Registration(entity);

        Assert.Multiple(() =>
        {
            Assert.That(model.Sweepers.Count(), Is.EqualTo(2));

            Assert.That(model.Sweepers.Count(sweeper => sweeper.Id == sweeperId1), Is.EqualTo(1));
            Assert.That(model.Sweepers.Count(sweeper => sweeper.Id == sweeperId2), Is.EqualTo(1));
        });
    }

    [Test]
    public void Constructor_Entity_SuperSweeperMapped([Values] bool superSweeper)
    {
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId1 = SquadId.New();
        var sweeperId2 = SquadId.New();

        var entity = new TournamentManager.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new TournamentManager.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new TournamentManager.Database.Entities.Division { Id = DivisionId.New() },
            Average = 200,
            Squads =
            [
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId1} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad {Id = sweeperId1, CashRatio = 5, Divisions = []} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.TournamentSquad { Id = squadId2} },
                new TournamentManager.Database.Entities.SquadRegistration { Squad = new TournamentManager.Database.Entities.SweeperSquad { Id = sweeperId2, CashRatio = 5, Divisions = []} }
            ],
            SuperSweeper = superSweeper
        };

        var model = new TournamentManager.Models.Registration(entity);

        Assert.That(model.SuperSweeper, Is.EqualTo(entity.SuperSweeper));
    }
}
