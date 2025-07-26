
using BowlingMegabucks.TournamentManager.Squads;

namespace BowlingMegabucks.TournamentManager.UnitTests.Models;
internal sealed class LaneAssignment
{
    private TournamentManager.Models.LaneAssignment _laneAssignment;

    [OneTimeSetUp]
    public void SetUp()
    {
        var entity = new TournamentManager.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new TournamentManager.Database.Entities.TournamentSquad(),
            RegistrationId = RegistrationId.New(),
            Registration = new TournamentManager.Database.Entities.Registration
            {
                Bowler = new TournamentManager.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new TournamentManager.Database.Entities.Division
                {
                    Id = DivisionId.New()
                },
                Average = 200
            },
            LaneAssignment = "12C"
        };

        _laneAssignment = new TournamentManager.Models.LaneAssignment(entity, new Mock<IHandicapCalculatorInternal>().Object);
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
        => Assert.That(_laneAssignment.Division.Id, Is.Not.EqualTo(DivisionId.Empty));

    [Test]
    public void Constructor_AverageMapped()
        => Assert.That(_laneAssignment.Average, Is.EqualTo(200));

    [Test]
    public void Constructor_PositionMapped()
        => Assert.That(_laneAssignment.Position, Is.EqualTo("12C"));

    [Test]
    public void Constructor_SquadIsTournamentSquad_HandicapCalculator_CalledCorrectly()
    {
        var entity = new TournamentManager.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new TournamentManager.Database.Entities.TournamentSquad(),
            RegistrationId = RegistrationId.New(),
            Registration = new TournamentManager.Database.Entities.Registration
            {
                Bowler = new TournamentManager.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new TournamentManager.Database.Entities.Division(),
                Average = 200
            },
            LaneAssignment = "12C"
        };

        var handicapCalculator = new Mock<IHandicapCalculatorInternal>();

        _ = new TournamentManager.Models.LaneAssignment(entity, handicapCalculator.Object);

        handicapCalculator.Verify(calculator => calculator.Calculate(entity.Registration), Times.Once);
    }

    [Test]
    public void Constructor_SquadIsTournamentSquad_HandicapMappedToCalculatorResult()
    {
        var entity = new TournamentManager.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new TournamentManager.Database.Entities.TournamentSquad(),
            RegistrationId = RegistrationId.New(),
            Registration = new TournamentManager.Database.Entities.Registration
            {
                Bowler = new TournamentManager.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new TournamentManager.Database.Entities.Division(),
                Average = 200
            },
            LaneAssignment = "12C"
        };

        var handicapCalculator = new Mock<IHandicapCalculatorInternal>();
        handicapCalculator.Setup(calculator => calculator.Calculate(It.IsAny<TournamentManager.Database.Entities.Registration>())).Returns(10);

        var model = new TournamentManager.Models.LaneAssignment(entity, handicapCalculator.Object);

        Assert.That(model.Handicap, Is.EqualTo(10));
    }

    [Test]
    public void Constructor_SquadIsSweeperSquad_HandicapCalculator_NotCalled()
    {
        var entity = new TournamentManager.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new TournamentManager.Database.Entities.SweeperSquad
            {
                Divisions = Enumerable.Empty<TournamentManager.Database.Entities.SweeperDivision>().ToList()
            },
            RegistrationId = RegistrationId.New(),
            Registration = new TournamentManager.Database.Entities.Registration
            {
                Bowler = new TournamentManager.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new TournamentManager.Database.Entities.Division(),
                Average = 200
            },
            LaneAssignment = "12C"
        };

        var handicapCalculator = new Mock<IHandicapCalculatorInternal>();

        _ = new TournamentManager.Models.LaneAssignment(entity, handicapCalculator.Object);

        handicapCalculator.Verify(calculator => calculator.Calculate(It.IsAny<TournamentManager.Database.Entities.Registration>()), Times.Never);
    }

    [TestCase(null, 0)]
    [TestCase(20, 20)]
    public void Constructor_SquadIsSweeperSquad_HandicapMappedToSweeperDivisionHandicap(int? bonusPinsPerGame, int handicap)
    {
        var divisionId = DivisionId.New();

        var entity = new TournamentManager.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new TournamentManager.Database.Entities.SweeperSquad
            {
                Divisions =
                [
                    new TournamentManager.Database.Entities.SweeperDivision {DivisionId = DivisionId.New(), BonusPinsPerGame = 5 },
                    new TournamentManager.Database.Entities.SweeperDivision { DivisionId = divisionId, BonusPinsPerGame = bonusPinsPerGame},
                    new TournamentManager.Database.Entities.SweeperDivision { DivisionId = DivisionId.New(), BonusPinsPerGame = 15}
                ]
            },
            RegistrationId = RegistrationId.New(),
            Registration = new TournamentManager.Database.Entities.Registration
            {
                Bowler = new TournamentManager.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new TournamentManager.Database.Entities.Division
                {
                    Id = divisionId
                },
                Average = 200
            },
            LaneAssignment = "12C"
        };

        var handicapCalculator = new Mock<IHandicapCalculatorInternal>();
        handicapCalculator.Setup(calculator => calculator.Calculate(It.IsAny<TournamentManager.Database.Entities.Registration>())).Returns(10);

        var model = new TournamentManager.Models.LaneAssignment(entity, handicapCalculator.Object);

        Assert.That(model.Handicap, Is.EqualTo(handicap));
    }

    [Test]
    public void Constructor_SquadIsTournamentSquad_SuperSweeperIsNull()
    {
        var entity = new TournamentManager.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new TournamentManager.Database.Entities.TournamentSquad(),
            RegistrationId = RegistrationId.New(),
            Registration = new TournamentManager.Database.Entities.Registration
            {
                Bowler = new TournamentManager.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new TournamentManager.Database.Entities.Division(),
                Average = 200
            },
            LaneAssignment = "12C"
        };

        var handicapCalculator = new Mock<IHandicapCalculatorInternal>();

        var model = new TournamentManager.Models.LaneAssignment(entity, handicapCalculator.Object);

        Assert.That(model.SuperSweeper, Is.Null);
    }

    [Test]
    public void Constructor_SquadIsSweeperSquad_SuperSweeperMapped([Values] bool superSweeper)
    {
        var divisionId = DivisionId.New();

        var entity = new TournamentManager.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new TournamentManager.Database.Entities.SweeperSquad
            {
                Divisions =
                [
                    new TournamentManager.Database.Entities.SweeperDivision {DivisionId = DivisionId.New(), BonusPinsPerGame = 5 },
                    new TournamentManager.Database.Entities.SweeperDivision { DivisionId = DivisionId.New(), BonusPinsPerGame = 15}
                ]
            },
            RegistrationId = RegistrationId.New(),
            Registration = new TournamentManager.Database.Entities.Registration
            {
                Bowler = new TournamentManager.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new TournamentManager.Database.Entities.Division
                {
                    Id = divisionId
                },
                Average = 200,
                SuperSweeper = superSweeper
            },
            LaneAssignment = "12C"
        };

        var handicapCalculator = new Mock<IHandicapCalculatorInternal>();
        handicapCalculator.Setup(calculator => calculator.Calculate(It.IsAny<TournamentManager.Database.Entities.Registration>())).Returns(10);

        var model = new TournamentManager.Models.LaneAssignment(entity, handicapCalculator.Object);

        Assert.That(model.SuperSweeper, Is.EqualTo(superSweeper));
    }
}
