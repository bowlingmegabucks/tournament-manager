namespace NortheastMegabuck.Tests.Tournaments.Add;

[TestFixture]
internal class DataLayer
{
    private Mock<NortheastMegabuck.Tournaments.IEntityMapper> _mapper;
    private Mock<NortheastMegabuck.Tournaments.IRepository> _repository;

    private NortheastMegabuck.Tournaments.Add.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<NortheastMegabuck.Tournaments.IEntityMapper>();
        _repository = new Mock<NortheastMegabuck.Tournaments.IRepository>();

        _dataLayer = new NortheastMegabuck.Tournaments.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public void Execute_MapperExecute_CalledCorrectly()
    {
        var tournament = new NortheastMegabuck.Models.Tournament();

        _dataLayer.Execute(tournament);
        
        _mapper.Verify(mapper => mapper.Execute(tournament), Times.Once);
    }

    [Test]
    public void Execute_RepositoryAdd_CalledCorrectly()
    {
        var entity = new NortheastMegabuck.Database.Entities.Tournament();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NortheastMegabuck.Models.Tournament>())).Returns(entity);
        
        var tournament = new NortheastMegabuck.Models.Tournament();

        _dataLayer.Execute(tournament);

        _repository.Verify(repository => repository.Add(entity), Times.Once);
    }

    [Test]
    public void Execute_ReturnsNewGUID()
    {
        var id = TournamentId.New();
        _repository.Setup(repository => repository.Add(It.IsAny<NortheastMegabuck.Database.Entities.Tournament>())).Returns(id);

        var tournament = new NortheastMegabuck.Models.Tournament();

        var result = _dataLayer.Execute(tournament);

        Assert.That(result, Is.EqualTo(id));
    }
}
