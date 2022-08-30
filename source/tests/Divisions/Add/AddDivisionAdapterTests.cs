namespace NortheastMegabuck.Tests.Divisions.Add;

[TestFixture]
internal class Adapter
{
    private Mock<NortheastMegabuck.Divisions.Add.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Divisions.Add.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Divisions.Add.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Divisions.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var viewModel = new NortheastMegabuck.Divisions.ViewModel
        { 
            DivisionName = "name"
        };

        _adapter.Execute(viewModel);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<NortheastMegabuck.Models.Division>(tournament => tournament.Name == "name")), Times.Once);
    }

    [Test]
    public void Execute_Errors_SetToBusinessLogicErrors([Range(0, 2)] int count)
    {
        var errors = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("error"), count);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var viewModel = new NortheastMegabuck.Divisions.ViewModel();

        _adapter.Execute(viewModel);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public void Execute_ReturnsBusinessLogicId()
    {
        var divisionId = NortheastMegabuck.Divisions.Id.New();
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NortheastMegabuck.Models.Division>())).Returns(divisionId);

        var viewModel = new NortheastMegabuck.Divisions.ViewModel();

        var result = _adapter.Execute(viewModel);

        Assert.That(result, Is.EqualTo(divisionId));
    }
}
