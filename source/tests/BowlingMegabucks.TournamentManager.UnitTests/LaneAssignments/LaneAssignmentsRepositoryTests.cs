using BowlingMegabucks.TournamentManager.Tests.Extensions;

namespace BowlingMegabucks.TournamentManager.Tests.LaneAssignments;
internal sealed class LaneAssignmentsRepositoryTests
{
    private Mock<BowlingMegabucks.TournamentManager.Database.IDataContext> _dataContext;

    private BowlingMegabucks.TournamentManager.LaneAssignments.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<BowlingMegabucks.TournamentManager.Database.IDataContext>();
        _repository = new BowlingMegabucks.TournamentManager.LaneAssignments.Repository(_dataContext.Object);
    }

    [Test]
    public void Retrieve_SquadId_ReturnsSquadRegistrations()
    {
        var squad1 = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            Registrations =
            [
                new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "1A" },
                new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "1B" }
            ]
        };

        var squad2 = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            Registrations =
            [
                new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "3A" },
                new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "3B" },
                new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "3C" }
            ]
        };

        var squad3 = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            Registrations =
            [
                new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5A"},
                new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5B" },
                new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5C" },
                new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5D" },
                new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5E" }
            ]
        };

        var squads = new[] { squad1, squad2, squad3 };

        _dataContext.Setup(dataContext => dataContext.Squads).Returns(squads.SetUpDbContext());

        var actual = _repository.Retrieve(squad2.Id).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));

            Assert.That(actual.TrueForAll(registration => registration.LaneAssignment.StartsWith('3')));
        });
    }
}
