
using System;

namespace NewEnglandClassic.Tests.Squads.Add;

[TestFixture]
internal class Adapter
{
    private Mock<NewEnglandClassic.Squads.Add.IBusinessLogic> _businessLogic;

    private NewEnglandClassic.Squads.Add.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NewEnglandClassic.Squads.Add.IBusinessLogic>();

        _adapter = new NewEnglandClassic.Squads.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var guid = Guid.NewGuid();

        var viewModel = new Mock<NewEnglandClassic.Squads.IViewModel>();
        viewModel.SetupGet(v => v.Id).Returns(guid);

        _adapter.Execute(viewModel.Object);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<NewEnglandClassic.Models.Squad>(squad => squad.Id == guid)), Times.Once);
    }

    [Test]
    public void Execute_ErrorsSetToBusinessLogicErrors([Range(0, 2)] int errorCount)
    {
        var errors = Enumerable.Repeat(new NewEnglandClassic.Models.ErrorDetail("error"), errorCount);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var viewModel = new Mock<NewEnglandClassic.Squads.IViewModel>();

        _adapter.Execute(viewModel.Object);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public void Execute_BusinessLogicExecuteReturnsNull_NullReturned()
    {
        Guid? noGuid = null;
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NewEnglandClassic.Models.Squad>())).Returns(noGuid);

        var viewModel = new Mock<NewEnglandClassic.Squads.IViewModel>();

        var actual = _adapter.Execute(viewModel.Object);

        Assert.That(actual, Is.Null);
    }

    [Test]
    public void Execute_BusinessLogicExecuteReturnsGuid_GuidReturned()
    {
        var guid = Guid.NewGuid();
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NewEnglandClassic.Models.Squad>())).Returns(guid);

        var viewModel = new Mock<NewEnglandClassic.Squads.IViewModel>();

        var actual = _adapter.Execute(viewModel.Object);

        Assert.That(actual, Is.EqualTo(guid));
    }
}
