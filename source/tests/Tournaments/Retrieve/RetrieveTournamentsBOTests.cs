namespace NewEnglandClassic.Tests.Tournaments.Retrieve;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NewEnglandClassic.Tournaments.Retrieve.IDataLayer> _dataLayer;

    private NewEnglandClassic.Tournaments.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NewEnglandClassic.Tournaments.Retrieve.IDataLayer>();

        _businessLogic = new NewEnglandClassic.Tournaments.Retrieve.BusinessLogic(_dataLayer.Object);
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
        var tournaments = new Mock<IEnumerable<NewEnglandClassic.Models.Tournament>>();
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
        var id = Guid.NewGuid();
        _businessLogic.Execute(id);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(id), Times.Once);
    }

    [Test]
    public void Execute_Id_ReturnsResultFromDataLayer()
    {
        var tournament = new NewEnglandClassic.Models.Tournament();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<Guid>())).Returns(tournament);

        var id = Guid.NewGuid();
        var result = _businessLogic.Execute(id);

        Assert.That(result, Is.EqualTo(tournament));
    }

    [Test]
    public void Execute_Id_NoErrors_ErrorNull()
    {
        var id = Guid.NewGuid();
        _businessLogic.Execute(id);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_Id_DataLayerExecuteThrowsException_ReturnsNull()
    {
        var ex = new Exception();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<Guid>())).Throws(ex);

        var id = Guid.NewGuid();
        var result = _businessLogic.Execute(id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Execute_Id_DataLayerExecuteThrowsException_ErrorPopulated()
    {
        var ex = new Exception("message");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<Guid>())).Throws(ex);

        var id = Guid.NewGuid();
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
        var id = Guid.NewGuid();
        _businessLogic.Execute(id);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(id), Times.Once);
    }

    [Test]
    public void Execute_DivisionIdReturnsResultFromDataLayer()
    {
        var tournament = new NewEnglandClassic.Models.Tournament();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<Guid>())).Returns(tournament);

        var id = Guid.NewGuid();
        var result = _businessLogic.Execute(id);

        Assert.That(result, Is.EqualTo(tournament));
    }

    [Test]
    public void Execute_DivisionIdNoErrors_ErrorNull()
    {
        var id = Guid.NewGuid();
        _businessLogic.Execute(id);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_DivisionIdDataLayerExecuteThrowsException_ReturnsNull()
    {
        var ex = new Exception();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<Guid>())).Throws(ex);

        var id = Guid.NewGuid();
        var result = _businessLogic.Execute(id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Execute_DivisionIdDataLayerExecuteThrowsException_ErrorPopulated()
    {
        var ex = new Exception("message");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<Guid>())).Throws(ex);

        var id = Guid.NewGuid();
        _businessLogic.Execute(id);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.Error.Message, Is.EqualTo(ex.Message));
            Assert.That(_businessLogic.Error.ReturnCode, Is.EqualTo(-1));
        });
    }
}
