
namespace NortheastMegabuck.Tests.Scores.Update;

[TestFixture]
internal class DataLayer
{
    private Mock<NortheastMegabuck.Scores.IEntityMapper> _mapper;
    private Mock<NortheastMegabuck.Scores.IRepository> _repository;

    private NortheastMegabuck.Scores.Update.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<NortheastMegabuck.Scores.IEntityMapper>();
        _repository = new Mock<NortheastMegabuck.Scores.IRepository>();

        _dataLayer = new NortheastMegabuck.Scores.Update.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public void Execute_EntityMapperExecute_CalledCorrectly()
    {
        var scores = new List<NortheastMegabuck.Models.SquadScore>
        { 
            new NortheastMegabuck.Models.SquadScore {BowlerId = BowlerId.New() },
            new NortheastMegabuck.Models.SquadScore {BowlerId = BowlerId.New() }
        };

        _dataLayer.Execute(scores);

        Assert.Multiple(() =>
        {
            _mapper.Verify(mapper => mapper.Execute(It.IsAny<NortheastMegabuck.Models.SquadScore>()), Times.Exactly(2));

            _mapper.Verify(mapper => mapper.Execute(It.Is<NortheastMegabuck.Models.SquadScore>(score => score.BowlerId == scores[0].BowlerId)), Times.Once);
            _mapper.Verify(mapper => mapper.Execute(It.Is<NortheastMegabuck.Models.SquadScore>(score => score.BowlerId == scores[1].BowlerId)), Times.Once);
        });
    }

    [Test]
    public void Execute_RepositoryUpdate_CalledCorrectly()
    {
        static NortheastMegabuck.Database.Entities.SquadScore mapper(NortheastMegabuck.Models.SquadScore model) => new() { BowlerId = model.BowlerId };
        _mapper.Setup(map => map.Execute(It.IsAny<NortheastMegabuck.Models.SquadScore>())).Returns(mapper);

        var scores = new List<NortheastMegabuck.Models.SquadScore>
        {
            new NortheastMegabuck.Models.SquadScore {BowlerId = BowlerId.New() },
            new NortheastMegabuck.Models.SquadScore {BowlerId = BowlerId.New() }
        };

        _dataLayer.Execute(scores);

        Assert.Multiple(() =>
        {
            _repository.Verify(repository => repository.Update(It.IsAny<IEnumerable<NortheastMegabuck.Database.Entities.SquadScore>>()), Times.Once);

            _repository.Verify(repository => repository.Update(It.Is<IEnumerable<NortheastMegabuck.Database.Entities.SquadScore>>(squadScores => squadScores.Any(score=> score.BowlerId == scores[0].BowlerId))), Times.Once);
            _repository.Verify(repository => repository.Update(It.Is<IEnumerable<NortheastMegabuck.Database.Entities.SquadScore>>(squadScores => squadScores.Any(score => score.BowlerId == scores[1].BowlerId))), Times.Once);
        });
    }
}
