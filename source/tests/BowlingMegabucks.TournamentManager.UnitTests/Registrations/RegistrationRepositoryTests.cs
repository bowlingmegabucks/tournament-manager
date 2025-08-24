using BowlingMegabucks.TournamentManager.UnitTests.Extensions;

namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations;

[TestFixture]
internal sealed class Repository
{
    private Mock<TournamentManager.Database.IDataContext> _dataContext;

    private TournamentManager.Registrations.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<TournamentManager.Database.IDataContext>();
        _repository = new TournamentManager.Registrations.Repository(_dataContext.Object);
    }

    [Test]
    public async Task AddAsync_RegistrationAddedWithId()
    {
        _dataContext.Setup(dataContext => dataContext.Registrations).Returns(Array.Empty<TournamentManager.Database.Entities.Registration>().SetUpDbContext());

        var registration = new TournamentManager.Database.Entities.Registration()
        { 
            BowlerId = BowlerId.New(),
        };

        var id = await _repository.AddAsync(registration, default).ConfigureAwait(false);

        Assert.That(registration.Id, Is.EqualTo(id));
    }

    [Test]
    public async Task AddAsync_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Registrations).Returns(Array.Empty<TournamentManager.Database.Entities.Registration>().SetUpDbContext());

        var registration = new TournamentManager.Database.Entities.Registration()
        { 
            BowlerId = BowlerId.New(),
        };
        
        CancellationToken cancellationToken = default;

        await _repository.AddAsync(registration, cancellationToken).ConfigureAwait(false);

        _dataContext.Verify(dataContext => dataContext.SaveChangesAsync(cancellationToken), Times.Once());
    }

    [Test]
    public void Retrieve_TournamentId_ReturnsTournamentRegistrations()
    {
        var tournament1 = TournamentId.New();
        var tournament2 = TournamentId.New();

        var registrationId1 = RegistrationId.New();
        var registrationId2 = RegistrationId.New();
        var registrationId3 = RegistrationId.New();

        var registration1 = new TournamentManager.Database.Entities.Registration
        {
            Id = registrationId1,
            Division = new TournamentManager.Database.Entities.Division { TournamentId = tournament1 }
        };

        var registration2 = new TournamentManager.Database.Entities.Registration
        {
            Id = registrationId2,
            Division = new TournamentManager.Database.Entities.Division { TournamentId = tournament2 }
        };

        var registration3 = new TournamentManager.Database.Entities.Registration
        {
            Id = registrationId3,
            Division = new TournamentManager.Database.Entities.Division { TournamentId = tournament1 }
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
}
