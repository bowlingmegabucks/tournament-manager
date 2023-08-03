namespace NortheastMegabuck.Tests.Divisions.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Divisions.IEntityMapper> _mapper;
    private Mock<NortheastMegabuck.Divisions.IRepository> _repository;

    private NortheastMegabuck.Divisions.Add.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<NortheastMegabuck.Divisions.IEntityMapper>();
        _repository = new Mock<NortheastMegabuck.Divisions.IRepository>();

        _dataLayer = new NortheastMegabuck.Divisions.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public void Execute_MapperExecute_CalledCorrectly()
    {
        var division = new NortheastMegabuck.Models.Division();

        _dataLayer.Execute(division);

        _mapper.Verify(mapper => mapper.Execute(division), Times.Once);
    }

    [Test]
    public void Execute_RepositoryAdd_CalledCorrectly()
    {
        var entity = new NortheastMegabuck.Database.Entities.Division();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NortheastMegabuck.Models.Division>())).Returns(entity);

        var division = new NortheastMegabuck.Models.Division();

        _dataLayer.Execute(division);

        _repository.Verify(repository => repository.Add(entity), Times.Once);
    }

    [Test]
    public void Execute_ReturnsNewGUID()
    {
        var divisionId = NortheastMegabuck.DivisionId.New();
        _repository.Setup(repository => repository.Add(It.IsAny<NortheastMegabuck.Database.Entities.Division>())).Returns(divisionId);

        var division = new NortheastMegabuck.Models.Division();

        var result = _dataLayer.Execute(division);

        Assert.That(result, Is.EqualTo(divisionId));
    }
}
