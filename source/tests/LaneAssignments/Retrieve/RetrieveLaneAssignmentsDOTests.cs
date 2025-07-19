using MockQueryable;

namespace BowlingMegabucks.TournamentManager.Tests.LaneAssignments.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<BowlingMegabucks.TournamentManager.LaneAssignments.IRepository> _repository;
    private Mock<BowlingMegabucks.TournamentManager.Squads.IHandicapCalculatorInternal> _handicapCalculator;

    private BowlingMegabucks.TournamentManager.LaneAssignments.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<BowlingMegabucks.TournamentManager.LaneAssignments.IRepository>();
        _handicapCalculator = new Mock<BowlingMegabucks.TournamentManager.Squads.IHandicapCalculatorInternal>();

        _dataLayer = new BowlingMegabucks.TournamentManager.LaneAssignments.Retrieve.DataLayer(_repository.Object, _handicapCalculator.Object);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryRetrieve_CalledCorrectly()
    {
        _repository.Setup(repository => repository.Retrieve(It.IsAny<SquadId>())).Returns(Enumerable.Empty<BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration>().BuildMock());
        var squadId = SquadId.New();

        await _dataLayer.ExecuteAsync(squadId, default).ConfigureAwait(false);

        _repository.Verify(repository => repository.Retrieve(squadId), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsRepositoryRetrieve()
    {
        var laneAssignments = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad(),
            RegistrationId = RegistrationId.New(),
            Registration = new BowlingMegabucks.TournamentManager.Database.Entities.Registration
            {
                Bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new BowlingMegabucks.TournamentManager.Database.Entities.Division(),
                Average = 200
            },
            LaneAssignment = "12C"
        }, 3).BuildMock();

        _repository.Setup(repository => repository.Retrieve(It.IsAny<SquadId>())).Returns(laneAssignments);

        var actual = (await _dataLayer.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));
            Assert.That(actual.TrueForAll(laneAssignment => laneAssignment.Position == "12C"));
        });
    }
}
