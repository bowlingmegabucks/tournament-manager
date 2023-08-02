namespace NortheastMegabuck.Tests.Tournaments.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Tournaments.Retrieve.IDataLayer> _dataLayer;

    private NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Tournaments.Retrieve.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Tournaments.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecute_Called()
    {
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer=> dataLayer.ExecuteAsync(cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsResultFromDataLayer()
    {
        var tournaments = new Mock<IEnumerable<NortheastMegabuck.Models.Tournament>>();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<CancellationToken>())).ReturnsAsync(tournaments.Object);

        var result = await _businessLogic.ExecuteAsync(default).ConfigureAwait(false);

        Assert.That(result, Is.EqualTo(tournaments.Object));
    }

    [Test]
    public async Task ExecuteAsync_NoErrors_ErrorNull()
    {
        await _businessLogic.ExecuteAsync(default).ConfigureAwait(false);

        Assert.That(_businessLogic.Error, Is.Null);
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
            Assert.That(_businessLogic.Error.Message, Is.EqualTo(ex.Message));
            Assert.That(_businessLogic.Error.ReturnCode, Is.EqualTo(-1));
        });
    }

    [Test]
    public void Execute_Id_DataLayerExecute_Called()
    {
        var id = TournamentId.New();
        _businessLogic.Execute(id);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(id), Times.Once);
    }

    [Test]
    public void Execute_Id_ReturnsResultFromDataLayer()
    {
        var tournament = new NortheastMegabuck.Models.Tournament();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var id = TournamentId.New();
        var result = _businessLogic.Execute(id);

        Assert.That(result, Is.EqualTo(tournament));
    }

    [Test]
    public void Execute_Id_NoErrors_ErrorNull()
    {
        var id = TournamentId.New();
        _businessLogic.Execute(id);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_Id_DataLayerExecuteThrowsException_ReturnsNull()
    {
        var ex = new Exception();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Throws(ex);

        var id = TournamentId.New();
        var result = _businessLogic.Execute(id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Execute_Id_DataLayerExecuteThrowsException_ErrorPopulated()
    {
        var ex = new Exception("message");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Throws(ex);

        var id = TournamentId.New();
        _businessLogic.Execute(id);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.Error.Message, Is.EqualTo(ex.Message));
            Assert.That(_businessLogic.Error.ReturnCode, Is.EqualTo(-1));
        });
    }

    [Test]
    public void Execute_DivisionId_DataLayerExecute_DivisionId_Called()
    {
        var id = DivisionId.New();
        _businessLogic.Execute(id);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(id), Times.Once);
    }

    [Test]
    public void Execute_DivisionId_ReturnsResultFromDataLayer()
    {
        var tournament = new NortheastMegabuck.Models.Tournament();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<DivisionId>())).Returns(tournament);

        var id = DivisionId.New();
        var result = _businessLogic.Execute(id);

        Assert.That(result, Is.EqualTo(tournament));
    }

    [Test]
    public void Execute_DivisionId_NoErrors_ErrorNull()
    {
        var id = DivisionId.New();
        _businessLogic.Execute(id);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_DivisionId_DataLayerExecuteThrowsException_ReturnsNull()
    {
        var ex = new Exception();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<DivisionId>())).Throws(ex);

        var id = DivisionId.New();
        var result = _businessLogic.Execute(id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Execute_DivisionId_DataLayerExecuteThrowsException_ErrorPopulated()
    {
        var ex = new Exception("message");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<DivisionId>())).Throws(ex);

        var id = DivisionId.New();
        _businessLogic.Execute(id);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.Error.Message, Is.EqualTo(ex.Message));
            Assert.That(_businessLogic.Error.ReturnCode, Is.EqualTo(-1));
        });
    }

    [Test]
    public void Execute_SquadId_DataLayerExecute_SquadId_Called()
    {
        var id = SquadId.New();
        _businessLogic.Execute(id);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(id), Times.Once);
    }

    [Test]
    public void Execute_SquadId_ReturnsResultFromDataLayer()
    {
        var tournament = new NortheastMegabuck.Models.Tournament();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Returns(tournament);

        var id = SquadId.New();
        var result = _businessLogic.Execute(id);

        Assert.That(result, Is.EqualTo(tournament));
    }

    [Test]
    public void Execute_SquadId_NoErrors_ErrorNull()
    {
        var id = SquadId.New();
        _businessLogic.Execute(id);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_SquadId_DataLayerExecuteThrowsException_ReturnsNull()
    {
        var ex = new Exception();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Throws(ex);

        var id = SquadId.New();
        var result = _businessLogic.Execute(id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Execute_SquadId_DataLayerExecuteThrowsException_ErrorPopulated()
    {
        var ex = new Exception("message");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Throws(ex);

        var id = SquadId.New();
        _businessLogic.Execute(id);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.Error.Message, Is.EqualTo(ex.Message));
            Assert.That(_businessLogic.Error.ReturnCode, Is.EqualTo(-1));
        });
    }
}
