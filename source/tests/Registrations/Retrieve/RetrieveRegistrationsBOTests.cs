namespace NortheastMegabuck.Tests.Registrations.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Registrations.Retrieve.IDataLayer> _dataLayer;

    private NortheastMegabuck.Registrations.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Registrations.Retrieve.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Registrations.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_DataLayerExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ReturnsDataLayerExecute()
    {
        var registrations = Enumerable.Repeat(new NortheastMegabuck.Models.Registration(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(registrations);

        var tournamentId = TournamentId.New();

        var actual = await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(registrations));
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("exception"));

        var tournamentId = TournamentId.New();

        var actual = await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Empty);
            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo("exception"));
        });
    }
}
