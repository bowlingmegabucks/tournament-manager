namespace NewEnglandClassic.Tests.Sweepers.Add;

[TestFixture]
internal class Adapter
{
    private Mock<NewEnglandClassic.Sweepers.Add.IBusinessLogic> _businessLogic;

    private NewEnglandClassic.Sweepers.Add.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NewEnglandClassic.Sweepers.Add.IBusinessLogic>();

        _adapter = new NewEnglandClassic.Sweepers.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var id = SquadId.New();

        var viewModel = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.Id).Returns(id);

        _adapter.Execute(viewModel.Object);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<NewEnglandClassic.Models.Sweeper>(sweeper => sweeper.Id == id)), Times.Once);
    }

    [Test]
    public void Execute_ErrorsSetToBusinessLogicErrors([Range(0, 2)] int errorCount)
    {
        var errors = Enumerable.Repeat(new NewEnglandClassic.Models.ErrorDetail("error"), errorCount);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var viewModel = new Mock<NewEnglandClassic.Sweepers.IViewModel>();

        _adapter.Execute(viewModel.Object);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public void Execute_BusinessLogicExecuteReturnsNull_NullReturned()
    {
        SquadId? noGuid = null;
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NewEnglandClassic.Models.Sweeper>())).Returns(noGuid);

        var viewModel = new Mock<NewEnglandClassic.Sweepers.IViewModel>();

        var actual = _adapter.Execute(viewModel.Object);

        Assert.That(actual, Is.Null);
    }

    [Test]
    public void Execute_BusinessLogicExecuteReturnsGuid_GuidReturned()
    {
        var id = SquadId.New();
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NewEnglandClassic.Models.Sweeper>())).Returns(id);

        var viewModel = new Mock<NewEnglandClassic.Sweepers.IViewModel>();

        var actual = _adapter.Execute(viewModel.Object);

        Assert.That(actual, Is.EqualTo(id));
    }
}
