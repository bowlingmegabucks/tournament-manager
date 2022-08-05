
namespace NewEnglandClassic.Tests.Registrations.Add;

[TestFixture]
internal class Adapter
{
    private Mock<NewEnglandClassic.Registrations.Add.IBusinessLogic> _businessLogic;

    private NewEnglandClassic.Registrations.Add.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NewEnglandClassic.Registrations.Add.IBusinessLogic>();

        _adapter = new NewEnglandClassic.Registrations.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_AddBowlerViewModel_BusinessLogicExecute_CalledCorrectly()
    {
        var bowler = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        bowler.SetupGet(b => b.LastName).Returns("lastName");
        var divisionId = DivisionId.New();
        var squads = Enumerable.Empty<Guid>();
        var sweepers = Enumerable.Empty<Guid>();
        var average = 200;

        _adapter.Execute(bowler.Object, divisionId, squads, sweepers, average);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<NewEnglandClassic.Models.Registration>(registration => registration.Bowler.LastName == "lastName" &&
                                                                                                                                registration.Division.Id == divisionId &&
                                                                                                                                registration.Sweepers == sweepers &&
                                                                                                                                registration.Squads == squads &&
                                                                                                                                registration.Average == average)), Times.Once);
    }

    [Test]
    public void Execute_AddBowlerView_ErrorsSetToBusinessLogicErrors()
    {
        var errors = Enumerable.Repeat(new NewEnglandClassic.Models.ErrorDetail("error"), 5);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var bowler = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        var divisionId = DivisionId.New();
        var squads = Enumerable.Empty<Guid>();
        var sweepers = Enumerable.Empty<Guid>();
        var average = 200;

        _adapter.Execute(bowler.Object, divisionId, squads, sweepers, average);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public void Execute_AddBowlerView_ReturnsBusinessLogicExecute()
    {
        var id = RegistrationId.New();
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NewEnglandClassic.Models.Registration>())).Returns(id);

        var bowler = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        var divisionId = DivisionId.New();
        var squads = Enumerable.Empty<Guid>();
        var sweepers = Enumerable.Empty<Guid>();
        var average = 200;

        var actual = _adapter.Execute(bowler.Object, divisionId, squads, sweepers, average);

        Assert.That(actual, Is.EqualTo(id));
    }
}
