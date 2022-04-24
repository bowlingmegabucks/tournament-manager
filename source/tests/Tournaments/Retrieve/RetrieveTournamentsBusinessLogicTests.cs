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
    public void Execute_NoErrors_ErrorDetailNull()
    {
        _businessLogic.Execute();

        Assert.That(_businessLogic.ErrorDetail, Is.Null);
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
    public void Execute_DataLayerExecuteThrowsException_ErrorDetailPopulated()
    {
        var ex = new Exception("message");
        _dataLayer.Setup(dataLayer => dataLayer.Execute()).Throws(ex);

        _businessLogic.Execute();

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo(ex.Message));
            Assert.That(_businessLogic.ErrorDetail.ReturnCode, Is.EqualTo(-1));
        });
    }
}
