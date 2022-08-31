using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests.Registrations.Retrieve;

[TestFixture]
internal class SquadRegistrationViewModel
{
    [Test]
    public void Constructor_BowlerIdMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.SquadRegistration
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

        var viewModel = new NortheastMegabuck.Registrations.Retrieve.SquadRegistrationViewModel(squadRegistration);

        Assert.That(viewModel.BowlerId, Is.EqualTo(squadRegistration.Bowler.Id));
    }

    [Test]
    public void Constructor_BowlerNameMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.SquadRegistration
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

        var viewModel = new NortheastMegabuck.Registrations.Retrieve.SquadRegistrationViewModel(squadRegistration);

        Assert.That(viewModel.BowlerName, Is.EqualTo(squadRegistration.Bowler.ToString()));
    }

    [Test]
    public void Constructor_DivisionNameMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.SquadRegistration
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

        var viewModel = new NortheastMegabuck.Registrations.Retrieve.SquadRegistrationViewModel(squadRegistration);

        Assert.That(viewModel.DivisionName, Is.EqualTo(squadRegistration.Division.Name));
    }

    [Test]
    public void Constructor_DivisionNumberMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.SquadRegistration
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

        var viewModel = new NortheastMegabuck.Registrations.Retrieve.SquadRegistrationViewModel(squadRegistration);

        Assert.That(viewModel.DivisionNumber, Is.EqualTo(squadRegistration.Division.Number));
    }

    [Test]
    public void Constructor_LaneAssignmentMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.SquadRegistration
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
            LaneAssignment = "21A"
        };

        var viewModel = new NortheastMegabuck.Registrations.Retrieve.SquadRegistrationViewModel(squadRegistration);

        Assert.That(viewModel.LaneAssignment, Is.EqualTo(squadRegistration.LaneAssignment));
    }

    [Test]
    public void Constructor_AverageNull_AverageMappedAsZero()
    {
        var squadRegistration = new NortheastMegabuck.Models.SquadRegistration
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
            LaneAssignment = "21A",
            Average = null
        };

        var viewModel = new NortheastMegabuck.Registrations.Retrieve.SquadRegistrationViewModel(squadRegistration);

        Assert.That(viewModel.Average, Is.EqualTo(0));
    }

    [Test]
    public void Constructor_AverageNotNull_AverageMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.SquadRegistration
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
            LaneAssignment = "21A",
            Average = 200
        };

        var viewModel = new NortheastMegabuck.Registrations.Retrieve.SquadRegistrationViewModel(squadRegistration);

        Assert.That(viewModel.Average, Is.EqualTo(200));
    }

    [Test]
    public void Constructor_HandicapMapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.SquadRegistration
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
            LaneAssignment = "21A",
            Average = 200,
            Handicap = 50
        };

        var viewModel = new NortheastMegabuck.Registrations.Retrieve.SquadRegistrationViewModel(squadRegistration);

        Assert.That(viewModel.Handicap, Is.EqualTo(50));
    }

    [Test]
    public void ToString_Mapped()
    {
        var squadRegistration = new NortheastMegabuck.Models.SquadRegistration
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
            LaneAssignment = "21A",
            Average = 200,
            Handicap = 50
        };

        var viewModel = new NortheastMegabuck.Registrations.Retrieve.SquadRegistrationViewModel(squadRegistration);

        var expected = $"{viewModel.LaneAssignment}\t\t{viewModel.BowlerId}\t{viewModel.DivisionNumber}\t{viewModel.Handicap}";
        var actual = viewModel.ToString();

        Assert.That(actual, Is.EqualTo(expected));
    }
}
