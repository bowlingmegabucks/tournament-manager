
using NortheastMegabuck.Scores;

namespace NortheastMegabuck.Tests.Scores.Update;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Scores.Update.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Scores.Update.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Scores.Update.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Scores.Update.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();

        var score1 = new NortheastMegabuck.Scores.ViewModel
        {
            SquadId = squadId,
            BowlerId = BowlerId.New(),
            GameNumber = 1,
            Score = 200
        };

        var score2 = new NortheastMegabuck.Scores.ViewModel
        {
            SquadId = squadId,
            BowlerId = score1.BowlerId,
            GameNumber = 2,
            Score = 201
        };

        var score3 = new NortheastMegabuck.Scores.ViewModel
        {
            SquadId = squadId,
            BowlerId = BowlerId.New(),
            GameNumber = 1,
            Score = 202
        };

        var scores = new[] { score1, score2, score3 };
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(scores, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.Count() == 3), cancellationToken), Times.Once);

            _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.All(squadScore => squadScore.SquadId == squadId)), cancellationToken), Times.Once);
            _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.Count(squadScore => squadScore.Bowler.Id == score1.BowlerId) == 2), cancellationToken), Times.Once);
            _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.Count(squadScore => squadScore.Bowler.Id == score3.BowlerId) == 1), cancellationToken), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_ErrorsSetToBusinessLogicErrors()
    {
        var errors = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("error"), 2);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        await _adapter.ExecuteAsync([], default).ConfigureAwait(false);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public async Task ExecuteAsync_InvalidScoresReturnedIfAny()
    {
        var invalidScore1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() },
            GameNumber = 1,
            Score = 200
        };

        var invalidScore2 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() },
            GameNumber = 2,
            Score = 201
        };

        var invalidScores = new[] { invalidScore1, invalidScore2 };
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<IEnumerable<NortheastMegabuck.Models.SquadScore>>(), It.IsAny<CancellationToken>())).ReturnsAsync(invalidScores);

        var result = (await _adapter.ExecuteAsync([], default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result.Count(score => score.BowlerId == invalidScore1.Bowler.Id), Is.EqualTo(1));
            Assert.That(result.Count(score => score.BowlerId == invalidScore2.Bowler.Id), Is.EqualTo(1));
        });
    }
}
