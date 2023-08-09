using NortheastMegabuck.Sweepers;

namespace NortheastMegabuck.Tests.Sweepers.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<IEntityMapper> _mapper;
    private Mock<IRepository> _repository;
    
    private NortheastMegabuck.Sweepers.Add.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<IEntityMapper>();
        _repository = new Mock<IRepository>();

        _dataLayer = new NortheastMegabuck.Sweepers.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public void Execute_MapperExecute_CalledCorrectly()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper();
        _dataLayer.Execute(sweeper);

        _mapper.Verify(mapper => mapper.Execute(sweeper), Times.Once);
    }

    [Test]
    public void Execute_RepositoryExecute_CalledCorrectly()
    {
        var entity = new NortheastMegabuck.Database.Entities.SweeperSquad();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NortheastMegabuck.Models.Sweeper>())).Returns(entity);

        var model = new NortheastMegabuck.Models.Sweeper();
        _dataLayer.Execute(model);

        _repository.Verify(repository => repository.Add(entity), Times.Once);
    }

    [Test]
    public void Execute_ReturnsRepositoryAddResponse()
    {
        var id = SquadId.New();
        _repository.Setup(repository => repository.Add(It.IsAny<NortheastMegabuck.Database.Entities.SweeperSquad>())).Returns(id);

        var model = new NortheastMegabuck.Models.Sweeper();
        var actual = _dataLayer.Execute(model);

        Assert.That(actual, Is.EqualTo(id));
    }
}
