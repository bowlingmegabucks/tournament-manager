namespace NortheastMegabuck.Tests.Tournaments.Add;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Tournaments.Add.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Tournaments.Add.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Tournaments.Add.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Tournaments.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var viewModel = new NortheastMegabuck.Tournaments.ViewModel
        { 
            TournamentName = "name"
        };

        _adapter.Execute(viewModel);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<NortheastMegabuck.Models.Tournament>(tournament => tournament.Name == "name")), Times.Once);
    }

    [Test]
    public void Execute_Errors_SetToBusinessLogicErrors([Range(0, 2)] int count)
    {
        var errors = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("error"), count);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var viewModel = new NortheastMegabuck.Tournaments.ViewModel();

        _adapter.Execute(viewModel);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public void Execute_ReturnsBusinessLogicId()
    {
        var id = TournamentId.New();
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NortheastMegabuck.Models.Tournament>())).Returns(id);

        var viewModel = new NortheastMegabuck.Tournaments.ViewModel();

        var result = _adapter.Execute(viewModel);

        Assert.That(result, Is.EqualTo(id));
    }
}
