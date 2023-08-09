namespace NortheastMegabuck.Tests.Squads.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Squads.IEntityMapper> _mapper;
    private Mock<NortheastMegabuck.Squads.IRepository> _repository;

    private NortheastMegabuck.Squads.Add.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<NortheastMegabuck.Squads.IEntityMapper>();
        _repository = new Mock<NortheastMegabuck.Squads.IRepository>();

        _dataLayer = new NortheastMegabuck.Squads.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public void Execute_MapperExecute_CalledCorrectly()
    {
        var squad = new NortheastMegabuck.Models.Squad();
        _dataLayer.Execute(squad);

        _mapper.Verify(mapper => mapper.Execute(squad), Times.Once);
    }

    [Test]
    public void Execute_RepositoryExecute_CalledCorrectly()
    {
        var entity = new NortheastMegabuck.Database.Entities.TournamentSquad();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NortheastMegabuck.Models.Squad>())).Returns(entity);

        var model = new NortheastMegabuck.Models.Squad();
        _dataLayer.Execute(model);

        _repository.Verify(repository => repository.Add(entity), Times.Once);
    }

    [Test]
    public void Execute_ReturnsRepositoryAddResponse()
    {
        var id = SquadId.New();
        _repository.Setup(repository => repository.Add(It.IsAny<NortheastMegabuck.Database.Entities.TournamentSquad>())).Returns(id);

        var model = new NortheastMegabuck.Models.Squad();
        var actual = _dataLayer.Execute(model);

        Assert.That(actual, Is.EqualTo(id));
    }
}
