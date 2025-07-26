
namespace BowlingMegabucks.TournamentManager.Tests.Bowlers.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<BowlingMegabucks.TournamentManager.Bowlers.Retrieve.IBusinessLogic> _businessLogic;

    private BowlingMegabucks.TournamentManager.Bowlers.Retrieve.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Retrieve.IBusinessLogic>();

        _adapter = new BowlingMegabucks.TournamentManager.Bowlers.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(bowlerId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(bowlerId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicHasError_ErrorFlow()
    {
        var error = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.ErrorDetail).Returns(error);

        var bowlerId = BowlerId.New();

        var actual = await _adapter.ExecuteAsync(bowlerId, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(_adapter.Error, Is.EqualTo(error));

            Assert.That(actual, Is.Null);
        });
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicSuccess_ReturnsMappedViewModel()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            Id = BowlerId.New()
        };
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>())).ReturnsAsync(bowler);

        var bowlerId = BowlerId.New();

        var actual = await _adapter.ExecuteAsync(bowlerId, default).ConfigureAwait(false);

        Assert.That(actual.Id, Is.EqualTo(bowler.Id));
    }
}
