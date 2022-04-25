using NewEnglandClassic.Tests.Extensions;

namespace NewEnglandClassic.Tests.Tournaments;

[TestFixture]
internal class TournamentsRepositoryTests
{
    private Mock<NewEnglandClassic.Database.IDataContext> _dataContext;

    private NewEnglandClassic.Tournaments.IRepository _tournamentsRepository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NewEnglandClassic.Database.IDataContext>();

        _tournamentsRepository = new NewEnglandClassic.Tournaments.Repository(_dataContext.Object);
    }

    [Test]
    public void RetrieveAll_ReturnsAllTournaments()
    {
        var tournament1 = new NewEnglandClassic.Database.Entities.Tournament { Id = Guid.NewGuid() };
        var tournament2 = new NewEnglandClassic.Database.Entities.Tournament { Id = Guid.NewGuid() };
        var tournament3 = new NewEnglandClassic.Database.Entities.Tournament { Id = Guid.NewGuid() };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(tournaments.SetUpDbContext());

        var actual = _tournamentsRepository.RetrieveAll();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Any(tournament => tournament.Id == tournament1.Id), Is.True, "tournament1 Id Not Found");
            Assert.That(actual.Any(tournament => tournament.Id == tournament2.Id), Is.True, "tournament2 Id Not Found");
            Assert.That(actual.Any(tournament => tournament.Id == tournament3.Id), Is.True, "tournament3 Id Not Found");
        });
    }

    [Test]
    public void Add_TournamentAddedWithGuid()
    {
        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.Tournament>().SetUpDbContext());
        
        var tournament = new NewEnglandClassic.Database.Entities.Tournament();

        var guid = _tournamentsRepository.Add(tournament);

        Assert.That(tournament.Id, Is.EqualTo(guid));
    }

    [Test]
    public void Add_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.Tournament>().SetUpDbContext());

        var tournament = new NewEnglandClassic.Database.Entities.Tournament();

        _tournamentsRepository.Add(tournament);

        _dataContext.Verify(dataContext => dataContext.SaveChanges(), Times.Once);
    }
}
