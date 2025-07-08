namespace NortheastMegabuck.Tests.Bowlers.Search;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Bowlers.Search.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Bowlers.Search.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Bowlers.Search.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Bowlers.Search.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(searchCriteria, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(searchCriteria, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ErrorsSetToBusinessLogicErrors([Range(0, 1)] int errorCount)
    {
        var error = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("test"), errorCount).SingleOrDefault();
        _businessLogic.SetupGet(businessLogic => businessLogic.ErrorDetail).Returns(error);

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria();

        await _adapter.ExecuteAsync(searchCriteria, default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public async Task ExecuteAsync_ReturnsBowlersFromBusinessLogic()
    {
        var bowler1 = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { Last = "Bowler 1" } };
        var bowler2 = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { Last = "Bowler 2" } };
        var bowlers = new[] { bowler1, bowler2 };

        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.BowlerSearchCriteria>(), It.IsAny<CancellationToken>())).ReturnsAsync(bowlers);

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria();

        var actual = (await _adapter.ExecuteAsync(searchCriteria, default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual[0].LastName, Is.EqualTo(bowler1.Name.Last));
            Assert.That(actual[1].LastName, Is.EqualTo(bowler2.Name.Last));
        });
    }
}
