
namespace NortheastMegabuck.Tests.Bowlers.Retrieve;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NortheastMegabuck.Bowlers.Retrieve.IDataLayer> _dataLayer;

    private NortheastMegabuck.Bowlers.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Bowlers.Retrieve.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Bowlers.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public void Execute_BowlerId_DataLayerExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();

        _businessLogic.Execute(bowlerId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(bowlerId), Times.Once);
    }

    [Test]
    public void Execute_BowlerId_DataLayerExecuteSuccessful_ReturnsBowler()
    {
        var bowler = new NortheastMegabuck.Models.Bowler();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<BowlerId>())).Returns(bowler);

        var actual = _businessLogic.Execute(BowlerId.New());

        Assert.That(actual, Is.EqualTo(bowler));
    }

    [Test]
    public void Execute_BowlerId_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        var ex = new Exception("ex");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<BowlerId>())).Throws(ex);

        var actual = _businessLogic.Execute(BowlerId.New());

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Null);

            Assert.That(_businessLogic.Error.Message, Is.EqualTo("ex"));
        });
    }
}
