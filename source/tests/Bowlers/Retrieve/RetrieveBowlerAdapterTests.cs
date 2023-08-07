
namespace NortheastMegabuck.Tests.Bowlers.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Bowlers.Retrieve.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Bowlers.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Bowlers.Retrieve.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Bowlers.Retrieve.Adapter(_businessLogic.Object);
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
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

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
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Id = BowlerId.New()
        };
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>())).ReturnsAsync(bowler);

        var bowlerId = BowlerId.New();

        var actual = await _adapter.ExecuteAsync(bowlerId, default).ConfigureAwait(false);

        Assert.That(actual.Id, Is.EqualTo(bowler.Id));
    }
}
