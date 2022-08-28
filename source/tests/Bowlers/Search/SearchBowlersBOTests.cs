namespace NewEnglandClassic.Tests.Bowlers.Search;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NewEnglandClassic.Bowlers.Search.IDataLayer> _dataLayer;

    private NewEnglandClassic.Bowlers.Search.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NewEnglandClassic.Bowlers.Search.IDataLayer>();

        _businessLogic = new NewEnglandClassic.Bowlers.Search.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public void Execute_DataLayerExecute_CalledCorrectly()
    {
        var searchCriteria = new NewEnglandClassic.Models.BowlerSearchCriteria();

        _businessLogic.Execute(searchCriteria);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(searchCriteria), Times.Once);
    }

    [Test]
    public void Execute_ReturnsDataLayerExecuteResults()
    {
        var divisions = Enumerable.Repeat(new NewEnglandClassic.Models.Bowler { Id = BowlerId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.BowlerSearchCriteria>())).Returns(divisions);

        var searchCriteria = new NewEnglandClassic.Models.BowlerSearchCriteria();

        var actual = _businessLogic.Execute(searchCriteria);

        Assert.That(actual, Is.EqualTo(divisions));
    }

    [Test]
    public void Execute_DataLayerExecuteNoException_ErrorNull()
    {
        var divisions = Enumerable.Repeat(new NewEnglandClassic.Models.Bowler { Id = BowlerId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.BowlerSearchCriteria>())).Returns(divisions);

        var searchCriteria = new NewEnglandClassic.Models.BowlerSearchCriteria();

         _businessLogic.Execute(searchCriteria);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.BowlerSearchCriteria>())).Throws(ex);

        var searchCriteria = new NewEnglandClassic.Models.BowlerSearchCriteria();

        var actual = _businessLogic.Execute(searchCriteria);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Empty);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }
}