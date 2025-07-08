
namespace NortheastMegabuck.Tests.Registrations.Delete;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Registrations.Delete.IDataLayer> _dataLayer;

    private NortheastMegabuck.Registrations.Delete.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Registrations.Delete.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Registrations.Delete.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_DataLayerExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(bowlerId, squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_DataLayerExecuteThrowsException_ErrorMapped()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        await _businessLogic.ExecuteAsync(BowlerId.New(), SquadId.New(), default).ConfigureAwait(false);

        Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo(ex.Message));
    }

    [Test]
    public async Task ExecuteAsync_RegistrationId_DataLayerExecute_CalledCorrectly()
    {
        var registrationId = RegistrationId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(registrationId, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(registrationId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RegistrationId_DataLayerExecuteThrowsException_ErrorMapped()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<RegistrationId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        await _businessLogic.ExecuteAsync(RegistrationId.New(), default).ConfigureAwait(false);

        Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo(ex.Message));
    }
}
