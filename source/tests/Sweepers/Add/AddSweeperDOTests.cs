namespace NewEnglandClassic.Tests.Sweepers.Add;

[TestFixture]
internal class DataLayer
{
    private Mock<NewEnglandClassic.Sweepers.Add.IEntityMapper> _mapper;
    private Mock<NewEnglandClassic.Sweepers.IRepository> _repository;
    
    private NewEnglandClassic.Sweepers.Add.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<NewEnglandClassic.Sweepers.Add.IEntityMapper>();
        _repository = new Mock<NewEnglandClassic.Sweepers.IRepository>();

        _dataLayer = new NewEnglandClassic.Sweepers.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public void Execute_MapperExecute_CalledCorrectly()
    {
        var sweeper = new NewEnglandClassic.Models.Sweeper();
        _dataLayer.Execute(sweeper);

        _mapper.Verify(mapper => mapper.Execute(sweeper), Times.Once);
    }

    [Test]
    public void Execute_RepositoryExecute_CalledCorrectly()
    {
        var entity = new NewEnglandClassic.Database.Entities.SweeperSquad();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NewEnglandClassic.Models.Sweeper>())).Returns(entity);

        var model = new NewEnglandClassic.Models.Sweeper();
        _dataLayer.Execute(model);

        _repository.Verify(repository => repository.Add(entity), Times.Once);
    }

    [Test]
    public void Execute_ReturnsRepositoryAddResponse()
    {
        var guid = Guid.NewGuid();
        _repository.Setup(repository => repository.Add(It.IsAny<NewEnglandClassic.Database.Entities.SweeperSquad>())).Returns(guid);

        var model = new NewEnglandClassic.Models.Sweeper();
        var actual = _dataLayer.Execute(model);

        Assert.That(actual, Is.EqualTo(guid));
    }
}
