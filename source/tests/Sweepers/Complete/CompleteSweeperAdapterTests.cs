
namespace NortheastMegabuck.Tests.Sweepers.Complete;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Sweepers.Complete.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Sweepers.Complete.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Sweepers.Complete.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Sweepers.Complete.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ErrorSetToBusinessLogicError()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        await _adapter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }
}
