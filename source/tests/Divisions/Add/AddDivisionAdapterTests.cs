namespace NewEnglandClassic.Tests.Divisions.Add;

[TestFixture]
internal class Adapter
{
    private Mock<NewEnglandClassic.Divisions.Add.IBusinessLogic> _businessLogic;

    private NewEnglandClassic.Divisions.Add.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NewEnglandClassic.Divisions.Add.IBusinessLogic>();

        _adapter = new NewEnglandClassic.Divisions.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var viewModel = new NewEnglandClassic.Divisions.ViewModel
        { 
            DivisionName = "name"
        };

        _adapter.Execute(viewModel);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<NewEnglandClassic.Models.Division>(tournament => tournament.Name == "name")), Times.Once);
    }

    [Test]
    public void Execute_Errors_SetToBusinessLogicErrors([Range(0, 2)] int count)
    {
        var errors = Enumerable.Repeat(new NewEnglandClassic.Models.ErrorDetail("error"), count);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var viewModel = new NewEnglandClassic.Divisions.ViewModel();

        _adapter.Execute(viewModel);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public void Execute_ReturnsBusinessLogicGuid()
    {
        var divisionId = DivisionId.New();
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NewEnglandClassic.Models.Division>())).Returns(divisionId);

        var viewModel = new NewEnglandClassic.Divisions.ViewModel();

        var result = _adapter.Execute(viewModel);

        Assert.That(result, Is.EqualTo(divisionId));
    }
}
