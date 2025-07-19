namespace BowlingMegabucks.TournamentManager.Tests.Tournaments.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<BowlingMegabucks.TournamentManager.Tournaments.Retrieve.IDataLayer> _dataLayer;

    private BowlingMegabucks.TournamentManager.Tournaments.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<BowlingMegabucks.TournamentManager.Tournaments.Retrieve.IDataLayer>();

        _businessLogic = new BowlingMegabucks.TournamentManager.Tournaments.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecute_Called()
    {
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsResultFromDataLayer()
    {
        var tournaments = new Mock<IEnumerable<BowlingMegabucks.TournamentManager.Models.Tournament>>();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<CancellationToken>())).ReturnsAsync(tournaments.Object);

        var result = await _businessLogic.ExecuteAsync(default).ConfigureAwait(false);

        Assert.That(result, Is.EqualTo(tournaments.Object));
    }

    [Test]
    public async Task ExecuteAsync_NoErrors_ErrorNull()
    {
        await _businessLogic.ExecuteAsync(default).ConfigureAwait(false);

        Assert.That(_businessLogic.ErrorDetail, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecuteThrowsException_ReturnsEmptyCollection()
    {
        var ex = new Exception();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var result = await _businessLogic.ExecuteAsync(default).ConfigureAwait(false);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task Execute_DataLayerExecuteThrowsException_ErrorPopulated()
    {
        var ex = new Exception("message");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        await _businessLogic.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo(ex.Message));
            Assert.That(_businessLogic.ErrorDetail.ReturnCode, Is.EqualTo(-1));
        });
    }

    [Test]
    public async Task ExecuteAsync_Id_DataLayerExecute_Called()
    {
        var id = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Id_ReturnsResultFromDataLayer()
    {
        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var id = TournamentId.New();
        var result = await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.That(result, Is.EqualTo(tournament));
    }

    [Test]
    public async Task ExecuteAsync_Id_NoErrors_ErrorNull()
    {
        var id = TournamentId.New();
        await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.That(_businessLogic.ErrorDetail, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_Id_DataLayerExecuteThrowsException_ReturnsNull()
    {
        var ex = new Exception();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var id = TournamentId.New();
        var result = await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_Id_DataLayerExecuteThrowsException_ErrorPopulated()
    {
        var ex = new Exception("message");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var id = TournamentId.New();
        await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo(ex.Message));
            Assert.That(_businessLogic.ErrorDetail.ReturnCode, Is.EqualTo(-1));
        });
    }

    [Test]
    public async Task ExecuteAsync_DivisionId_DataLayerExecute_DivisionId_Called()
    {
        var id = DivisionId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_DivisionId_ReturnsResultFromDataLayer()
    {
        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var id = DivisionId.New();
        var result = await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.That(result, Is.EqualTo(tournament));
    }

    [Test]
    public async Task ExecuteAsync_DivisionId_NoErrors_ErrorNull()
    {
        var id = DivisionId.New();
        await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.That(_businessLogic.ErrorDetail, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_DivisionId_DataLayerExecuteThrowsException_ReturnsNull()
    {
        var ex = new Exception();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var id = DivisionId.New();
        var result = await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_DivisionId_DataLayerExecuteThrowsException_ErrorPopulated()
    {
        var ex = new Exception("message");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var id = DivisionId.New();
        await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo(ex.Message));
            Assert.That(_businessLogic.ErrorDetail.ReturnCode, Is.EqualTo(-1));
        });
    }

    [Test]
    public async Task ExecuteAsync_SquadId_DataLayerExecute_SquadId_Called()
    {
        var id = SquadId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_ReturnsResultFromDataLayer()
    {
        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var id = SquadId.New();
        var result = await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.That(result, Is.EqualTo(tournament));
    }

    [Test]
    public async Task ExecuteAsync_SquadId_NoErrors_ErrorNull()
    {
        var id = SquadId.New();
        await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.That(_businessLogic.ErrorDetail, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_DataLayerExecuteThrowsException_ReturnsNull()
    {
        var ex = new Exception();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var id = SquadId.New();
        var result = await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_DataLayerExecuteThrowsException_ErrorPopulated()
    {
        var ex = new Exception("message");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var id = SquadId.New();
        await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo(ex.Message));
            Assert.That(_businessLogic.ErrorDetail.ReturnCode, Is.EqualTo(-1));
        });
    }
}
