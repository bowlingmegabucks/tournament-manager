namespace NewEnglandClassic.Tests.Divisions.Add;

[TestFixture]
internal class DataLayer
{
    private Mock<NewEnglandClassic.Divisions.IEntityMapper> _mapper;
    private Mock<NewEnglandClassic.Divisions.IRepository> _repository;

    private NewEnglandClassic.Divisions.Add.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<NewEnglandClassic.Divisions.IEntityMapper>();
        _repository = new Mock<NewEnglandClassic.Divisions.IRepository>();

        _dataLayer = new NewEnglandClassic.Divisions.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public void Execute_MapperExecute_CalledCorrectly()
    {
        var division = new NewEnglandClassic.Models.Division();

        _dataLayer.Execute(division);

        _mapper.Verify(mapper => mapper.Execute(division), Times.Once);
    }

    [Test]
    public void Execute_RepositoryAdd_CalledCorrectly()
    {
        var entity = new NewEnglandClassic.Database.Entities.Division();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NewEnglandClassic.Models.Division>())).Returns(entity);

        var division = new NewEnglandClassic.Models.Division();

        _dataLayer.Execute(division);

        _repository.Verify(repository => repository.Add(entity), Times.Once);
    }

    [Test]
    public void Execute_ReturnsNewGUID()
    {
        var divisionId = NewEnglandClassic.Divisions.Id.New();
        _repository.Setup(repository => repository.Add(It.IsAny<NewEnglandClassic.Database.Entities.Division>())).Returns(divisionId);

        var division = new NewEnglandClassic.Models.Division();

        var result = _dataLayer.Execute(division);

        Assert.That(result, Is.EqualTo(divisionId));
    }
}
