
namespace NortheastMegabuck.Tests.Registrations.Retrieve;

[TestFixture]
internal class DataLayer
{
    private Mock<NortheastMegabuck.Registrations.IRepository> _repository;

    private NortheastMegabuck.Registrations.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Registrations.IRepository>();

        _dataLayer = new NortheastMegabuck.Registrations.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public void Execute_TournamentId_RepositoryRetrieve_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();

        _dataLayer.Execute(tournamentId);

        _repository.Verify(repository => repository.Retrieve(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_TournamentId_ReturnsRepositoryRetrieve()
    {
        var registration1 = new NortheastMegabuck.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new NortheastMegabuck.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new NortheastMegabuck.Database.Entities.Division { Id = NortheastMegabuck.DivisionId.New() },
            Squads = Enumerable.Repeat(new NortheastMegabuck.Database.Entities.SquadRegistration(), 2).ToList(),
            SuperSweeper = true,
            Average = 200
        };

        var registration2 = new NortheastMegabuck.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new NortheastMegabuck.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new NortheastMegabuck.Database.Entities.Division { Id = NortheastMegabuck.DivisionId.New() },
            Squads = Enumerable.Repeat(new NortheastMegabuck.Database.Entities.SquadRegistration(), 2).ToList(),
            SuperSweeper = true,
            Average = 200
        };

        var registration3 = new NortheastMegabuck.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new NortheastMegabuck.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new NortheastMegabuck.Database.Entities.Division { Id = NortheastMegabuck.DivisionId.New() },
            Squads = Enumerable.Repeat(new NortheastMegabuck.Database.Entities.SquadRegistration(), 2).ToList(),
            SuperSweeper = true,
            Average = 200
        };

        var registrations = new[] { registration1, registration2, registration3 };

        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(registrations);

        var actual = _dataLayer.Execute(TournamentId.New()).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));

            Assert.That(actual.Count(registration => registration.Id == registration1.Id), Is.EqualTo(1));
            Assert.That(actual.Count(registration => registration.Id == registration2.Id), Is.EqualTo(1));
            Assert.That(actual.Count(registration => registration.Id == registration3.Id), Is.EqualTo(1));
        });
    }

    [Test]
    public void Execute_SquadId_RepositoryRetrieveForSquad_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _dataLayer.Execute(squadId);

        _repository.Verify(repository => repository.RetrieveForSquad(squadId), Times.Once);
    }

    [Test]
    public void Execute_SquadId_ReturnsRepositoryRetrieve()
    {
        var squadRegistration1 = new NortheastMegabuck.Database.Entities.SquadRegistration
        {
            Registration = new NortheastMegabuck.Database.Entities.Registration
            {
                Bowler = new NortheastMegabuck.Database.Entities.Bowler(),
                Division = new NortheastMegabuck.Database.Entities.Division()
            }
        };

        var squadRegistration2 = new NortheastMegabuck.Database.Entities.SquadRegistration
        {
            Registration = new NortheastMegabuck.Database.Entities.Registration
            {
                Bowler = new NortheastMegabuck.Database.Entities.Bowler(),
                Division = new NortheastMegabuck.Database.Entities.Division()
            }
        };

        var squadRegistration3 = new NortheastMegabuck.Database.Entities.SquadRegistration
        {
            Registration = new NortheastMegabuck.Database.Entities.Registration
            {
                Bowler = new NortheastMegabuck.Database.Entities.Bowler(),
                Division = new NortheastMegabuck.Database.Entities.Division()
            }
        };

        var registrations = new[] { squadRegistration1, squadRegistration2, squadRegistration3 };

        _repository.Setup(repository => repository.RetrieveForSquad(It.IsAny<SquadId>())).Returns(registrations);

        var actual = _dataLayer.Execute(SquadId.New()).ToList();

        Assert.That(actual, Has.Count.EqualTo(3));
    }
}
