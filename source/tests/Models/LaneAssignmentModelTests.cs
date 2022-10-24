
using NortheastMegabuck.Squads;

namespace NortheastMegabuck.Tests.Models;
internal class LaneAssignment
{
    private NortheastMegabuck.Models.LaneAssignment _laneAssignment;

    [OneTimeSetUp]
    public void SetUp()
    {
        var entity = new NortheastMegabuck.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new NortheastMegabuck.Database.Entities.TournamentSquad(),
            RegistrationId = RegistrationId.New(),
            Registration = new NortheastMegabuck.Database.Entities.Registration
            {
                Bowler = new NortheastMegabuck.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new NortheastMegabuck.Database.Entities.Division
                {
                    Id = DivisionId.New()
                },
                Average = 200
            },
            LaneAssignment = "12C"
        };

        _laneAssignment = new NortheastMegabuck.Models.LaneAssignment(entity, new Mock<IHandicapCalculator>().Object);
    }

    [Test]
    public void Constructor_RegistrationIdMapped()
        => Assert.That(_laneAssignment.RegistrationId, Is.Not.EqualTo(RegistrationId.Empty));

    [Test]
    public void Constructor_SquadIdMapped()
        => Assert.That(_laneAssignment.SquadId, Is.Not.EqualTo(SquadId.Empty));

    [Test]
    public void Constructor_BowlerMapped()
        => Assert.That(_laneAssignment.Bowler.Id, Is.Not.EqualTo(BowlerId.Empty));

    [Test]
    public void Constructor_DivisionMapped()
        => Assert.That(_laneAssignment.Division.Id, Is.Not.EqualTo(NortheastMegabuck.DivisionId.Empty));

    [Test]
    public void Constructor_AverageMapped()
        => Assert.That(_laneAssignment.Average, Is.EqualTo(200));

    [Test]
    public void Constructor_PositionMapped()
        => Assert.That(_laneAssignment.Position, Is.EqualTo("12C"));

    [Test]
    public void Constructor_SquadIsTournamentSquad_HandicapCalculator_CalledCorrectly()
    {
        var entity = new NortheastMegabuck.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new NortheastMegabuck.Database.Entities.TournamentSquad(),
            RegistrationId = RegistrationId.New(),
            Registration = new NortheastMegabuck.Database.Entities.Registration
            {
                Bowler = new NortheastMegabuck.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new NortheastMegabuck.Database.Entities.Division
                {
                    Id = DivisionId.New()
                },
                Average = 200
            },
            LaneAssignment = "12C"
        };

        var handicapCalculator = new Mock<IHandicapCalculator>();

        new NortheastMegabuck.Models.LaneAssignment(entity, handicapCalculator.Object);

        handicapCalculator.Verify(calculator => calculator.Calculate(entity.Registration), Times.Once);
    }

    [Test]
    public void Constructor_SqaudIsTournamentSquad_HandicapMappedToCalculatorResult()
    {
        var entity = new NortheastMegabuck.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new NortheastMegabuck.Database.Entities.TournamentSquad(),
            RegistrationId = RegistrationId.New(),
            Registration = new NortheastMegabuck.Database.Entities.Registration
            {
                Bowler = new NortheastMegabuck.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new NortheastMegabuck.Database.Entities.Division
                {
                    Id = DivisionId.New()
                },
                Average = 200
            },
            LaneAssignment = "12C"
        };

        var handicapCalculator = new Mock<IHandicapCalculator>();
        handicapCalculator.Setup(calculator => calculator.Calculate(It.IsAny<NortheastMegabuck.Database.Entities.Registration>())).Returns(10);

        var model = new NortheastMegabuck.Models.LaneAssignment(entity, handicapCalculator.Object);

        Assert.That(model.Handicap, Is.EqualTo(10));
    }

    [Test]
    public void Constructor_SquadIsSweeperSquad_HandicapCalculator_NotCalled()
    {
        var entity = new NortheastMegabuck.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new NortheastMegabuck.Database.Entities.SweeperSquad
            { 
                Divisions = Enumerable.Empty<NortheastMegabuck.Database.Entities.SweeperDivision>().ToList()
            },
            RegistrationId = RegistrationId.New(),
            Registration = new NortheastMegabuck.Database.Entities.Registration
            {
                Bowler = new NortheastMegabuck.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new NortheastMegabuck.Database.Entities.Division
                {
                    Id = DivisionId.New()
                },
                Average = 200
            },
            LaneAssignment = "12C"
        };

        var handicapCalculator = new Mock<IHandicapCalculator>();

        new NortheastMegabuck.Models.LaneAssignment(entity, handicapCalculator.Object);

        handicapCalculator.Verify(calculator => calculator.Calculate(It.IsAny<NortheastMegabuck.Database.Entities.Registration>()), Times.Never);
    }

    [TestCase(null,0)]
    [TestCase(20,20)]
    public void Constructor_SqaudIsSweeperSquad_HandicapMappedToSweeperDivisionHandicap(int? bonusPinsPerGame, int handicap)
    {
        var divisionId = DivisionId.New();

        var entity = new NortheastMegabuck.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new NortheastMegabuck.Database.Entities.SweeperSquad
            { 
                Divisions = new[]
                { 
                    new NortheastMegabuck.Database.Entities.SweeperDivision {DivisionId = DivisionId.New(), BonusPinsPerGame = 5 },
                    new NortheastMegabuck.Database.Entities.SweeperDivision { DivisionId = divisionId, BonusPinsPerGame = bonusPinsPerGame},
                    new NortheastMegabuck.Database.Entities.SweeperDivision { DivisionId = DivisionId.New(), BonusPinsPerGame = 15}
                }
            },
            RegistrationId = RegistrationId.New(),
            Registration = new NortheastMegabuck.Database.Entities.Registration
            {
                Bowler = new NortheastMegabuck.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new NortheastMegabuck.Database.Entities.Division
                {
                    Id = divisionId
                },
                Average = 200
            },
            LaneAssignment = "12C"
        };

        var handicapCalculator = new Mock<IHandicapCalculator>();
        handicapCalculator.Setup(calculator => calculator.Calculate(It.IsAny<NortheastMegabuck.Database.Entities.Registration>())).Returns(10);

        var model = new NortheastMegabuck.Models.LaneAssignment(entity, handicapCalculator.Object);

        Assert.That(model.Handicap, Is.EqualTo(handicap));
    }

    [Test]
    public void Constructor_SquadIsTournamentSquad_SuperSweeperIsNull()
    {
        var entity = new NortheastMegabuck.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new NortheastMegabuck.Database.Entities.TournamentSquad(),
            RegistrationId = RegistrationId.New(),
            Registration = new NortheastMegabuck.Database.Entities.Registration
            {
                Bowler = new NortheastMegabuck.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new NortheastMegabuck.Database.Entities.Division
                {
                    Id = DivisionId.New()
                },
                Average = 200
            },
            LaneAssignment = "12C"
        };

        var handicapCalculator = new Mock<IHandicapCalculator>();

        var model = new NortheastMegabuck.Models.LaneAssignment(entity, handicapCalculator.Object);

        Assert.That(model.SuperSweeper, Is.Null);
    }

    public void Constructor_SqaudIsSweeperSquad_SuperSweeperMapped([Values]bool superSweeper)
    {
        var divisionId = DivisionId.New();

        var entity = new NortheastMegabuck.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new NortheastMegabuck.Database.Entities.SweeperSquad
            {
                Divisions = new[]
                {
                    new NortheastMegabuck.Database.Entities.SweeperDivision {DivisionId = DivisionId.New(), BonusPinsPerGame = 5 },
                    new NortheastMegabuck.Database.Entities.SweeperDivision { DivisionId = DivisionId.New(), BonusPinsPerGame = 15}
                }
            },
            RegistrationId = RegistrationId.New(),
            Registration = new NortheastMegabuck.Database.Entities.Registration
            {
                Bowler = new NortheastMegabuck.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new NortheastMegabuck.Database.Entities.Division
                {
                    Id = divisionId
                },
                Average = 200,
                SuperSweeper = superSweeper
            },
            LaneAssignment = "12C"
        };

        var handicapCalculator = new Mock<IHandicapCalculator>();
        handicapCalculator.Setup(calculator => calculator.Calculate(It.IsAny<NortheastMegabuck.Database.Entities.Registration>())).Returns(10);

        var model = new NortheastMegabuck.Models.LaneAssignment(entity, handicapCalculator.Object);

        Assert.That(model.SuperSweeper, Is.EqualTo(superSweeper));
    }
}
