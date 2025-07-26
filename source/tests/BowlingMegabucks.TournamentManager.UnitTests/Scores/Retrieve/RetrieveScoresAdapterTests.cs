
namespace BowlingMegabucks.TournamentManager.UnitTests.Scores.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<TournamentManager.Scores.Retrieve.IBusinessLogic> _businessLogic;

    private TournamentManager.Scores.Retrieve.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<TournamentManager.Scores.Retrieve.IBusinessLogic>();

        _adapter = new TournamentManager.Scores.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(new[] { squadId }, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_AdapterError_SetToBusinessLogicError()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.ErrorDetail).Returns(error);

        await _adapter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public async Task ExecuteAsync_ReturnsAdapterExecute()
    {
        var score1 = new TournamentManager.Models.SquadScore
        {
            GameNumber = 1,
            Score = 200
        };

        var score2 = new TournamentManager.Models.SquadScore
        {
            GameNumber = 2,
            Score = 201
        };

        var scores = new[] { score1, score2 };
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>())).ReturnsAsync(scores);

        var actual = (await _adapter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(2));

            Assert.That(actual[0].GameNumber, Is.EqualTo(1));
            Assert.That(actual[1].Score, Is.EqualTo(201));
        });
    }
}
