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
    public void Execute_DivisionIdDataLayerExecute_DivisionIdCalled()
    {
        var id = TournamentId.New();
        _businessLogic.Execute(id);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(id), Times.Once);
    }

    [Test]
    public void Execute_DivisionIdReturnsResultFromDataLayer()
    {
        var tournament = new NortheastMegabuck.Models.Tournament();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var id = TournamentId.New();
        var result = _businessLogic.Execute(id);

        Assert.That(result, Is.EqualTo(tournament));
    }

    [Test]
    public void Execute_DivisionIdNoErrors_ErrorNull()
    {
        var id = TournamentId.New();
        _businessLogic.Execute(id);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_DivisionIdDataLayerExecuteThrowsException_ReturnsNull()
    {
        var ex = new Exception();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Throws(ex);

        var id = TournamentId.New();
        var result = _businessLogic.Execute(id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Execute_DivisionIdDataLayerExecuteThrowsException_ErrorPopulated()
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
}
