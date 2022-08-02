
namespace NewEnglandClassic.Tests.Bowlers.Retrieve;

[TestFixture]
internal class Adapter
{
    private Mock<NewEnglandClassic.Bowlers.Retrieve.IBusinessLogic> _businessLogic;

    private NewEnglandClassic.Bowlers.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NewEnglandClassic.Bowlers.Retrieve.IBusinessLogic>();

        _adapter = new NewEnglandClassic.Bowlers.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();

        _adapter.Execute(bowlerId.Value);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(bowlerId), Times.Once);
    }

    [Test]
    public void Execute_BusinessLogicHasError_ErrorFlow()
    {
        var error = new NewEnglandClassic.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        var bowlerId = Guid.NewGuid();

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
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            Id = BowlerId.New()
        };
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<BowlerId>())).Returns(bowler);

        var bowlerId = Guid.NewGuid();

        var actual = _adapter.Execute(bowlerId);

        Assert.That(actual.Id, Is.EqualTo(bowler.Id.Value));
    }
}
