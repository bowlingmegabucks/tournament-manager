using MockQueryable;

namespace BowlingMegabucks.TournamentManager.UnitTests.LaneAssignments.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<TournamentManager.LaneAssignments.IRepository> _repository;
    private Mock<TournamentManager.Squads.IHandicapCalculatorInternal> _handicapCalculator;

    private TournamentManager.LaneAssignments.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<TournamentManager.LaneAssignments.IRepository>();
        _handicapCalculator = new Mock<TournamentManager.Squads.IHandicapCalculatorInternal>();

        _dataLayer = new TournamentManager.LaneAssignments.Retrieve.DataLayer(_repository.Object, _handicapCalculator.Object);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryRetrieve_CalledCorrectly()
    {
        _repository.Setup(repository => repository.Retrieve(It.IsAny<SquadId>())).Returns(Array.Empty<TournamentManager.Database.Entities.SquadRegistration>().BuildMock());
        var squadId = SquadId.New();

        await _dataLayer.ExecuteAsync(squadId, default).ConfigureAwait(false);

        _repository.Verify(repository => repository.Retrieve(squadId), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsRepositoryRetrieve()
    {
        var laneAssignments = Enumerable.Repeat(new TournamentManager.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new TournamentManager.Database.Entities.TournamentSquad(),
            RegistrationId = RegistrationId.New(),
            Registration = new TournamentManager.Database.Entities.Registration
            {
                Bowler = new TournamentManager.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new TournamentManager.Database.Entities.Division(),
                Average = 200
            },
            LaneAssignment = "12C"
        }, 3).ToList().BuildMock();

        _repository.Setup(repository => repository.Retrieve(It.IsAny<SquadId>())).Returns(laneAssignments);

        var actual = (await _dataLayer.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));
            Assert.That(actual.TrueForAll(laneAssignment => laneAssignment.Position == "12C"));
        });
    }
}
