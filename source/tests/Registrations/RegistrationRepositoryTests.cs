using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Registrations;

[TestFixture]
internal class Repository
{
    private Mock<NortheastMegabuck.Database.IDataContext> _dataContext;

    private NortheastMegabuck.Registrations.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NortheastMegabuck.Database.IDataContext>();
        _repository = new NortheastMegabuck.Registrations.Repository(_dataContext.Object);
    }

    [Test]
    public void Add_RegistrationAddedWithId()
    {
        _dataContext.Setup(dataContext => dataContext.Registrations).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.Registration>().SetUpDbContext());

        var registration = new NortheastMegabuck.Database.Entities.Registration();

        var id = _repository.Add(registration);

        Assert.That(registration.Id, Is.EqualTo(id));
    }

    [Test]
    public void Add_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Registrations).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.Registration>().SetUpDbContext());

        var registration = new NortheastMegabuck.Database.Entities.Registration();

        _repository.Add(registration);

        _dataContext.Verify(dataContext => dataContext.SaveChanges(), Times.Once());
    }

    [Test]
    public void Retrieve_TournamentId_ReturnsTournamentRegistrations()
    {
        var tournament1 = TournamentId.New();
        var tournament2 = TournamentId.New();

        var registrationId1 = RegistrationId.New();
        var registrationId2 = RegistrationId.New();
        var registrationId3 = RegistrationId.New();

        var registration1 = new NortheastMegabuck.Database.Entities.Registration
        {
            Id = registrationId1,
            Division = new NortheastMegabuck.Database.Entities.Division { TournamentId = tournament1 }
        };

        var registration2 = new NortheastMegabuck.Database.Entities.Registration
        {
            Id = registrationId2,
            Division = new NortheastMegabuck.Database.Entities.Division { TournamentId = tournament2 }
        };

        var registration3 = new NortheastMegabuck.Database.Entities.Registration
        {
            Id = registrationId3,
            Division = new NortheastMegabuck.Database.Entities.Division { TournamentId = tournament1 }
        };

        var registrations = new[] { registration1, registration2, registration3 };

        _dataContext.Setup(dataContext => dataContext.Registrations).Returns(registrations.SetUpDbContext());

        var actual = _repository.Retrieve(tournament1).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(2));

            Assert.That(actual.Count(registration => registration.Id == registrationId1), Is.EqualTo(1));
            Assert.That(actual.Count(registration => registration.Id == registrationId3), Is.EqualTo(1));
        });
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

        var actual = _repository.RetrieveForSquad(squad2.Id).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));

            Assert.That(actual.All(registration => registration.LaneAssignment.StartsWith("3")));
        });
    }
}
