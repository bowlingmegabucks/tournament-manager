namespace NortheastMegabuck.Tests.Squads.Add;

[TestFixture]
internal class Adapter
{
    private Mock<NortheastMegabuck.Squads.Add.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Squads.Add.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Squads.Add.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Squads.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var id = SquadId.New();

        var viewModel = new Mock<NortheastMegabuck.Squads.IViewModel>();
        viewModel.SetupGet(v => v.Id).Returns(id);

        _adapter.Execute(viewModel.Object);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<NortheastMegabuck.Models.Squad>(squad => squad.Id == id)), Times.Once);
    }

    [Test]
    public void Execute_ErrorsSetToBusinessLogicErrors([Range(0, 2)] int errorCount)
    {
        var errors = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("error"), errorCount);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var viewModel = new Mock<NortheastMegabuck.Squads.IViewModel>();

        _adapter.Execute(viewModel.Object);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public void Execute_BusinessLogicExecuteReturnsNull_NullReturned()
    {
        SquadId? noId = null;
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NortheastMegabuck.Models.Squad>())).Returns(noId);

        var viewModel = new Mock<NortheastMegabuck.Squads.IViewModel>();

        var actual = _adapter.Execute(viewModel.Object);

        Assert.That(actual, Is.Null);
    }

    [Test]
    public void Execute_BusinessLogicExecuteReturnsId_IdReturned()
    {
        var id = SquadId.New();
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NortheastMegabuck.Models.Squad>())).Returns(id);

        var viewModel = new Mock<NortheastMegabuck.Squads.IViewModel>();

        var actual = _adapter.Execute(viewModel.Object);

        Assert.That(actual, Is.EqualTo(id));
    }
}
