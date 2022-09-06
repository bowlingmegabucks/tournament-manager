using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.LaneAssignments;
internal class LaneAssignmentsRepositoryTests
{
    private Mock<NortheastMegabuck.Database.IDataContext> _dataContext;

    private NortheastMegabuck.LaneAssignments.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NortheastMegabuck.Database.IDataContext>();
        _repository = new NortheastMegabuck.LaneAssignments.Repository(_dataContext.Object);
    }

    [Test]
    public void Retrieve_SquadId_ReturnsSquadRegistrations()
    {
        var squad1 = new NortheastMegabuck.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            Registrations = new[]
            {
                new NortheastMegabuck.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "1A" },
                new NortheastMegabuck.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "1B" }
            }.ToList()
        };

        var squad2 = new NortheastMegabuck.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            Registrations = new[]
            {
                new NortheastMegabuck.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "3A" },
                new NortheastMegabuck.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "3B" },
                new NortheastMegabuck.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "3C" }
            }.ToList()
        };

        var squad3 = new NortheastMegabuck.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            Registrations = new[]
            {
                new NortheastMegabuck.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5A"},
                new NortheastMegabuck.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5B" },
                new NortheastMegabuck.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5C" },
                new NortheastMegabuck.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5D" },
                new NortheastMegabuck.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5E" }
            }.ToList()
        };

        var squads = new[] { squad1, squad2, squad3 };

        _dataContext.Setup(dataContext => dataContext.Squads).Returns(squads.SetUpDbContext());

        var actual = _repository.Retrieve(squad2.Id).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));

            Assert.That(actual.All(registration => registration.LaneAssignment.StartsWith("3", StringComparison.Ordinal)));
        });
    }
}
