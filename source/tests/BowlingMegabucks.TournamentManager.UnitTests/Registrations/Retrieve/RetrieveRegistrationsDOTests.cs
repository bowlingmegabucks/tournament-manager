
using MockQueryable;

namespace BowlingMegabucks.TournamentManager.Tests.Registrations.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<BowlingMegabucks.TournamentManager.Registrations.IRepository> _repository;

    private BowlingMegabucks.TournamentManager.Registrations.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<BowlingMegabucks.TournamentManager.Registrations.IRepository>();

        _dataLayer = new BowlingMegabucks.TournamentManager.Registrations.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_RepositoryRetrieve_CalledCorrectly()
    {
        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(Enumerable.Empty<BowlingMegabucks.TournamentManager.Database.Entities.Registration>().BuildMock());
        var tournamentId = TournamentId.New();

        await _dataLayer.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        _repository.Verify(repository => repository.Retrieve(tournamentId), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ReturnsRepositoryRetrieve()
    {
        var registration1 = new BowlingMegabucks.TournamentManager.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new BowlingMegabucks.TournamentManager.Database.Entities.Division { Id = BowlingMegabucks.TournamentManager.DivisionId.New() },
            Squads = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration(), 2).ToList(),
            SuperSweeper = true,
            Average = 200
        };

        var registration2 = new BowlingMegabucks.TournamentManager.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new BowlingMegabucks.TournamentManager.Database.Entities.Division { Id = BowlingMegabucks.TournamentManager.DivisionId.New() },
            Squads = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration(), 2).ToList(),
            SuperSweeper = true,
            Average = 200
        };

        var registration3 = new BowlingMegabucks.TournamentManager.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler { Id = BowlerId.New() },
            Division = new BowlingMegabucks.TournamentManager.Database.Entities.Division { Id = BowlingMegabucks.TournamentManager.DivisionId.New() },
            Squads = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration(), 2).ToList(),
            SuperSweeper = true,
            Average = 200
        };

        var registrations = new[] { registration1, registration2, registration3 };

        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(registrations.BuildMock());

        var actual = (await _dataLayer.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));

            Assert.That(actual.Count(registration => registration.Id == registration1.Id), Is.EqualTo(1));
            Assert.That(actual.Count(registration => registration.Id == registration2.Id), Is.EqualTo(1));
            Assert.That(actual.Count(registration => registration.Id == registration3.Id), Is.EqualTo(1));
        });
    }
}
