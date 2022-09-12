namespace NortheastMegabuck.Tests.Tournaments.Retrieve;

[TestFixture]
internal class BusinessLogic
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
    public void Execute_DataLayerExecute_Called()
    {
        _businessLogic.Execute();

        _dataLayer.Verify(dataLayer=> dataLayer.Execute(), Times.Once);
    }

    [Test]
    public void Execute_ReturnsResultFromDataLayer()
    {
        var tournaments = new Mock<IEnumerable<NortheastMegabuck.Models.Tournament>>();
        _dataLayer.Setup(dataLayer => dataLayer.Execute()).Returns(tournaments.Object);

        var result = _businessLogic.Execute();

        Assert.That(result, Is.EqualTo(tournaments.Object));
    }

    [Test]
    public void Execute_NoErrors_ErrorNull()
    {
        _businessLogic.Execute();

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_DataLayerExecuteThrowsException_ReturnsEmptyCollection()
    {
        var ex = new Exception();
        _dataLayer.Setup(dataLayer => dataLayer.Execute()).Throws(ex);

        var result = _businessLogic.Execute();

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Execute_DataLayerExecuteThrowsException_ErrorPopulated()
    {
        var ex = new Exception("message");
        _dataLayer.Setup(dataLayer => dataLayer.Execute()).Throws(ex);

        _businessLogic.Execute();
        
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
