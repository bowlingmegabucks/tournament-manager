namespace NortheastMegabuck.Tests.LaneAssignments;

[TestFixture]
internal class ViewModel
{
    [Test]
    public void Constructor_LaneAssignment_BowlerIdMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.LaneAssignment
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New()
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Id = DivisionId.New()
            }
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(squadRegistration);

        Assert.That(viewModel.BowlerId, Is.EqualTo(squadRegistration.Bowler.Id));
    }

    [Test]
    public void Constructor_LaneAssignment_BowlerNameMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.LaneAssignment
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "firstName",
                LastName = "lastName"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Id = DivisionId.New()
            }
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(squadRegistration);

        Assert.That(viewModel.BowlerName, Is.EqualTo(squadRegistration.Bowler.ToString()));
    }

    [Test]
    public void Constructor_LaneAssignment_DivisionNameMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.LaneAssignment
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "firstName",
                LastName = "lastName"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Id = DivisionId.New(),
                Name = "name"
            }
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(squadRegistration);

        Assert.That(viewModel.DivisionName, Is.EqualTo(squadRegistration.Division.Name));
    }

    [Test]
    public void Constructor_LaneAssignment_DivisionNumberMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.LaneAssignment
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "firstName",
                LastName = "lastName"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Id = DivisionId.New(),
                Name = "name",
                Number = 5
            }
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(squadRegistration);

        Assert.That(viewModel.DivisionNumber, Is.EqualTo(squadRegistration.Division.Number));
    }

    [Test]
    public void Constructor_LaneAssignment_LaneAssignmentMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.LaneAssignment
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "firstName",
                LastName = "lastName"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Id = DivisionId.New(),
                Name = "name",
                Number = 5
            },
            Position = "21A"
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(squadRegistration);

        Assert.That(viewModel.LaneAssignment, Is.EqualTo(squadRegistration.Position));
    }

    [Test]
    public void Constructor_LaneAssignment_AverageNull_AverageMappedAsZero()
    {
        var squadRegistration = new NortheastMegabuck.Models.LaneAssignment
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "firstName",
                LastName = "lastName"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Id = DivisionId.New(),
                Name = "name",
                Number = 5
            },
            Position = "21A",
            Average = null
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(squadRegistration);

        Assert.That(viewModel.Average, Is.EqualTo(0));
    }

    [Test]
    public void Constructor_LaneAssignment_AverageNotNull_AverageMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.LaneAssignment
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "firstName",
                LastName = "lastName"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Id = DivisionId.New(),
                Name = "name",
                Number = 5
            },
            Position = "21A",
            Average = 200
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(squadRegistration);

        Assert.That(viewModel.Average, Is.EqualTo(200));
    }

    [Test]
    public void Constructor_LaneAssignment_HandicapMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.LaneAssignment
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "firstName",
                LastName = "lastName"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Id = DivisionId.New(),
                Name = "name",
                Number = 5
            },
            Position = "21A",
            Average = 200,
            Handicap = 50
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(squadRegistration);

        Assert.That(viewModel.Handicap, Is.EqualTo(50));
    }

    [Test]
    public void Constructor_Registration_BowlerIdMapped([Values(null,200)]int? average)
    {
        var handicapCalculator = new Mock<IHandicapCalculator>();
        var registration = new NortheastMegabuck.Models.Registration
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            { 
                Id = BowlerId.New(),
                FirstName = "first",
                LastName = "last"
            },
            Division = new NortheastMegabuck.Models.Division
            { 
                Name = "division",
                Number = 5
            },
            Average = average
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(registration, handicapCalculator.Object);

        Assert.That(viewModel.BowlerId, Is.EqualTo(registration.Bowler.Id));
    }

    [Test]
    public void Constructor_Registration_BowlerNameMapped([Values(null, 200)] int? average)
    {
        var handicapCalculator = new Mock<IHandicapCalculator>();
        var registration = new NortheastMegabuck.Models.Registration
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "first",
                LastName = "last"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Name = "division",
                Number = 5
            },
            Average = average
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(registration, handicapCalculator.Object);

        Assert.That(viewModel.BowlerName, Is.EqualTo(registration.Bowler.ToString()));
    }

    [Test]
    public void Constructor_Registration_DivisionNameMapped([Values(null, 200)] int? average)
    {
        var handicapCalculator = new Mock<IHandicapCalculator>();
        var registration = new NortheastMegabuck.Models.Registration
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "first",
                LastName = "last"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Name = "division",
                Number = 5
            },
            Average = average
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(registration, handicapCalculator.Object);

        Assert.That(viewModel.DivisionName, Is.EqualTo(registration.Division.Name));
    }

    [Test]
    public void Constructor_Registration_DivisionNumberMapped([Values(null, 200)] int? average)
    {
        var handicapCalculator = new Mock<IHandicapCalculator>();
        var registration = new NortheastMegabuck.Models.Registration
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "first",
                LastName = "last"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Name = "division",
                Number = 5
            },
            Average = average
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(registration, handicapCalculator.Object);

        Assert.That(viewModel.DivisionNumber, Is.EqualTo(registration.Division.Number));
    }

    [Test]
    public void Constructor_Registration_LaneAssignmentEmpty([Values(null, 200)] int? average)
    {
        var handicapCalculator = new Mock<IHandicapCalculator>();
        var registration = new NortheastMegabuck.Models.Registration
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "first",
                LastName = "last"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Name = "division",
                Number = 5
            },
            Average = average
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(registration, handicapCalculator.Object);

        Assert.That(viewModel.LaneAssignment, Is.Empty);
    }

    [TestCase(null,0)]
    [TestCase(200,200)]
    public void Constructor_Registration_AverageMapped(int? average, int expected)
    {
        var handicapCalculator = new Mock<IHandicapCalculator>();
        var registration = new NortheastMegabuck.Models.Registration
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "first",
                LastName = "last"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Name = "division",
                Number = 5
            },
            Average = average
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(registration, handicapCalculator.Object);

        Assert.That(viewModel.Average, Is.EqualTo(expected));
    }

    [Test]
    public void Constructor_Registration_HandicapCalculatorCalculate_CalledCorrectly([Values(null, 200)] int? average)
    {
        var handicapCalculator = new Mock<IHandicapCalculator>();
        var registration = new NortheastMegabuck.Models.Registration
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "first",
                LastName = "last"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Name = "division",
                Number = 5
            },
            Average = average
        };

        new NortheastMegabuck.LaneAssignments.ViewModel(registration, handicapCalculator.Object);

        handicapCalculator.Verify(calculator => calculator.Calculate(registration), Times.Once);
    }

    [Test]
    public void Constructor_Registration_HandicapMapped_ToHandicapCalculatorCalculate([Values(null, 200)] int? average)
    {
        var handicapCalculator = new Mock<IHandicapCalculator>();
        handicapCalculator.Setup(calculator => calculator.Calculate(It.IsAny<NortheastMegabuck.Models.Registration>())).Returns(10);
        var registration = new NortheastMegabuck.Models.Registration
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "first",
                LastName = "last"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Name = "division",
                Number = 5
            },
            Average = average
        };

        var actual = new NortheastMegabuck.LaneAssignments.ViewModel(registration, handicapCalculator.Object);

        Assert.That(actual.Handicap, Is.EqualTo(10));
    }

    [Test]
    public void ToString_Mapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.LaneAssignment
        {
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Id = BowlerId.New(),
                FirstName = "firstName",
                LastName = "lastName"
            },
            Division = new NortheastMegabuck.Models.Division
            {
                Id = DivisionId.New(),
                Name = "name",
                Number = 5
            },
            Position = "21A",
            Average = 200,
            Handicap = 50
        };

        var viewModel = new NortheastMegabuck.LaneAssignments.ViewModel(squadRegistration);

        var expected = $"{viewModel.LaneAssignment}\t{viewModel.BowlerId}\t{viewModel.BowlerName}\t{viewModel.DivisionNumber}\t{viewModel.Handicap}";
        var actual = viewModel.ToString();

        Assert.That(actual, Is.EqualTo(expected));
    }
}
