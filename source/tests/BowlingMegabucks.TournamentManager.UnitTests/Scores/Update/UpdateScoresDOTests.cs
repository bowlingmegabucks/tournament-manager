
namespace BowlingMegabucks.TournamentManager.UnitTests.Scores.Update;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<TournamentManager.Scores.IEntityMapper> _mapper;
    private Mock<TournamentManager.Scores.IRepository> _repository;

    private TournamentManager.Scores.Update.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<TournamentManager.Scores.IEntityMapper>();
        _repository = new Mock<TournamentManager.Scores.IRepository>();

        _dataLayer = new TournamentManager.Scores.Update.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_EntityMapperExecute_CalledCorrectly()
    {
        var scores = new List<TournamentManager.Models.SquadScore>
        {
            new() {Bowler = new TournamentManager.Models.Bowler { Id = BowlerId.New() } },
            new() {Bowler = new TournamentManager.Models.Bowler { Id = BowlerId.New() } }
        };

        await _dataLayer.ExecuteAsync(scores, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _mapper.Verify(mapper => mapper.Execute(It.IsAny<TournamentManager.Models.SquadScore>()), Times.Exactly(2));

            _mapper.Verify(mapper => mapper.Execute(It.Is<TournamentManager.Models.SquadScore>(score => score.Bowler.Id == scores[0].Bowler.Id)), Times.Once);
            _mapper.Verify(mapper => mapper.Execute(It.Is<TournamentManager.Models.SquadScore>(score => score.Bowler.Id == scores[1].Bowler.Id)), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_RepositoryUpdate_CalledCorrectly()
    {
        static TournamentManager.Database.Entities.SquadScore mapper(TournamentManager.Models.SquadScore model) => new() { BowlerId = model.Bowler.Id };
        _mapper.Setup(map => map.Execute(It.IsAny<TournamentManager.Models.SquadScore>())).Returns(mapper);

        var scores = new List<TournamentManager.Models.SquadScore>
        {
            new() {Bowler = new TournamentManager.Models.Bowler { Id = BowlerId.New() } },
            new() {Bowler = new TournamentManager.Models.Bowler { Id = BowlerId.New() } }
        };

        CancellationToken cancellationToken = default;
        await _dataLayer.ExecuteAsync(scores, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _repository.Verify(repository => repository.UpdateAsync(It.IsAny<ICollection<TournamentManager.Database.Entities.SquadScore>>(), cancellationToken), Times.Once);

            _repository.Verify(repository => repository.UpdateAsync(It.Is<ICollection<TournamentManager.Database.Entities.SquadScore>>(squadScores => squadScores.Any(score => score.BowlerId == scores[0].Bowler.Id)), cancellationToken), Times.Once);
            _repository.Verify(repository => repository.UpdateAsync(It.Is<ICollection<TournamentManager.Database.Entities.SquadScore>>(squadScores => squadScores.Any(score => score.BowlerId == scores[1].Bowler.Id)), cancellationToken), Times.Once);
        });
    }
}