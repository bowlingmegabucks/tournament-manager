using BowlingMegabucks.TournamentManager.UnitTests.Extensions;

namespace BowlingMegabucks.TournamentManager.UnitTests.LaneAssignments;
internal sealed class LaneAssignmentsRepositoryTests
{
    private Mock<TournamentManager.Database.IDataContext> _dataContext;

    private TournamentManager.LaneAssignments.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<TournamentManager.Database.IDataContext>();
        _repository = new TournamentManager.LaneAssignments.Repository(_dataContext.Object);
    }

    [Test]
    public void Retrieve_SquadId_ReturnsSquadRegistrations()
    {
        var squad1 = new TournamentManager.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            Registrations =
            [
                new TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "1A" },
                new TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "1B" }
            ]
        };

        var squad2 = new TournamentManager.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            Registrations =
            [
                new TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "3A" },
                new TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "3B" },
                new TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "3C" }
            ]
        };

        var squad3 = new TournamentManager.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            Registrations =
            [
                new TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5A"},
                new TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5B" },
                new TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5C" },
                new TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5D" },
                new TournamentManager.Database.Entities.SquadRegistration{RegistrationId = RegistrationId.New(), LaneAssignment = "5E" }
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
