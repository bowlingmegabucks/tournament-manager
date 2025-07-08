using MockQueryable;

namespace NortheastMegabuck.Tests.LaneAssignments.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.LaneAssignments.IRepository> _repository;
    private Mock<NortheastMegabuck.Squads.IHandicapCalculatorInternal> _handicapCalculator;

    private NortheastMegabuck.LaneAssignments.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.LaneAssignments.IRepository>();
        _handicapCalculator = new Mock<NortheastMegabuck.Squads.IHandicapCalculatorInternal>();

        _dataLayer = new NortheastMegabuck.LaneAssignments.Retrieve.DataLayer(_repository.Object, _handicapCalculator.Object);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryRetrieve_CalledCorrectly()
    {
        _repository.Setup(repository => repository.Retrieve(It.IsAny<SquadId>())).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.SquadRegistration>().BuildMock());
        var squadId = SquadId.New();

        await _dataLayer.ExecuteAsync(squadId, default).ConfigureAwait(false);

        _repository.Verify(repository => repository.Retrieve(squadId), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsRepositoryRetrieve()
    {
        var laneAssignments = Enumerable.Repeat(new NortheastMegabuck.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new NortheastMegabuck.Database.Entities.TournamentSquad(),
            RegistrationId = RegistrationId.New(),
            Registration = new NortheastMegabuck.Database.Entities.Registration
            {
                Bowler = new NortheastMegabuck.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new NortheastMegabuck.Database.Entities.Division(),
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
