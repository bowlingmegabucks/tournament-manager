
namespace NewEnglandClassic.Tests.Registrations.Retrieve;

[TestFixture]
internal class DataLayer
{
    private Mock<NewEnglandClassic.Registrations.IRepository> _repository;

    private NewEnglandClassic.Registrations.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NewEnglandClassic.Registrations.IRepository>();

        _dataLayer = new NewEnglandClassic.Registrations.Retrieve.DataLayer(_repository.Object);
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
        var registration1 = new NewEnglandClassic.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new NewEnglandClassic.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new NewEnglandClassic.Database.Entities.Division { Id = DivisionId.New() },
            Squads = Enumerable.Repeat(new NewEnglandClassic.Database.Entities.SquadRegistration(), 2).ToList(),
            SuperSweeper = true,
            Average = 200
        };

        var registration2 = new NewEnglandClassic.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new NewEnglandClassic.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new NewEnglandClassic.Database.Entities.Division { Id = DivisionId.New() },
            Squads = Enumerable.Repeat(new NewEnglandClassic.Database.Entities.SquadRegistration(), 2).ToList(),
            SuperSweeper = true,
            Average = 200
        };

        var registration3 = new NewEnglandClassic.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new NewEnglandClassic.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new NewEnglandClassic.Database.Entities.Division { Id = DivisionId.New() },
            Squads = Enumerable.Repeat(new NewEnglandClassic.Database.Entities.SquadRegistration(), 2).ToList(),
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
}
