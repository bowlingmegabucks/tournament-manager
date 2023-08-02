
namespace NortheastMegabuck.Tests.Bowlers.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Bowlers.Retrieve.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Bowlers.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Bowlers.Retrieve.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Bowlers.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();

        _adapter.Execute(bowlerId);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(bowlerId), Times.Once);
    }

    [Test]
    public void Execute_BusinessLogicHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        var bowlerId = BowlerId.New();

        var actual = _adapter.Execute(bowlerId);

        Assert.Multiple(() =>
        {
            Assert.That(_adapter.Error, Is.EqualTo(error));

            Assert.That(actual, Is.Null);
        });
    }

    [Test]
    public void Execute_BusinessLogicSuccess_ReturnsMappedViewModel()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Id = BowlerId.New()
        };
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<BowlerId>())).Returns(bowler);

        var bowlerId = BowlerId.New();

        var actual = _adapter.Execute(bowlerId);

        Assert.That(actual.Id, Is.EqualTo(bowler.Id));
    }
}
