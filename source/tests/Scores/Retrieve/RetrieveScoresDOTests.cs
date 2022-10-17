
namespace NortheastMegabuck.Tests.Scores.Retrieve;

[TestFixture]
internal class DataLayer
{
    private Mock<NortheastMegabuck.Scores.IRepository> _repository;

    private NortheastMegabuck.Scores.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Scores.IRepository>();

        _dataLayer = new NortheastMegabuck.Scores.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public void Execute_RepositoryRetrieve_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _dataLayer.Execute(squadId);

        _repository.Verify(repository => repository.Retrieve(squadId), Times.Once);
    }

    [Test]
    public void Execute_ReturnsRepositoryRetrieve()
    {
        var entity1 = new NortheastMegabuck.Database.Entities.SquadScore
        {
            SquadId = SquadId.New(),
            BowlerId = BowlerId.New()
        };

        var entity2 = new NortheastMegabuck.Database.Entities.SquadScore
        {
            SquadId = SquadId.New(),
            BowlerId = BowlerId.New()
        };

        var entities = new[] { entity1, entity2 };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<SquadId>())).Returns(entities);

        var actual = _dataLayer.Execute(SquadId.New()).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(2));

            Assert.That(actual[0].BowlerId, Is.EqualTo(entity1.BowlerId));
            Assert.That(actual[1].SquadId, Is.EqualTo(entity2.SquadId));
        });
    }
}
