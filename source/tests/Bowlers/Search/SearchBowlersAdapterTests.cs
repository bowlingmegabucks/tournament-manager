namespace NortheastMegabuck.Tests.Bowlers.Search;

[TestFixture]
internal class Adapter
{
    private Mock<NortheastMegabuck.Bowlers.Search.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Bowlers.Search.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Bowlers.Search.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Bowlers.Search.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria();

        _adapter.Execute(searchCriteria);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(searchCriteria), Times.Once);
    }

    [Test]
    public void Execute_ErrorsSetToBusinessLogicErrors([Range(0, 1)] int errorCount)
    {
        var error = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("test"), errorCount).SingleOrDefault();
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria();

        _adapter.Execute(searchCriteria);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public void Execute_ReturnsBowlersFromBusinessLogic()
    {
        var bowler1 = new NortheastMegabuck.Models.Bowler { LastName = "Bowler 1" };
        var bowler2 = new NortheastMegabuck.Models.Bowler { LastName = "Bowler 2" };
        var bowlers = new[] { bowler1, bowler2 };

        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NortheastMegabuck.Models.BowlerSearchCriteria>())).Returns(bowlers);

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria();

        var actual = _adapter.Execute(searchCriteria).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual[0].LastName, Is.EqualTo(bowler1.LastName));
            Assert.That(actual[1].LastName, Is.EqualTo(bowler2.LastName));
        });
    }
}
