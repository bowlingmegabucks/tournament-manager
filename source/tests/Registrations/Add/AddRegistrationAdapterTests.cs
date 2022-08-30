
namespace NortheastMegabuck.Tests.Registrations.Add;

[TestFixture]
internal class Adapter
{
    private Mock<NortheastMegabuck.Registrations.Add.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Registrations.Add.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Registrations.Add.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Registrations.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_AddBowlerViewModel_BusinessLogicExecute_CalledCorrectly([Values]bool superSweeper)
    {
        var bowler = new Mock<NortheastMegabuck.Bowlers.Add.IViewModel>();
        bowler.SetupGet(b => b.LastName).Returns("lastName");
        var divisionId = NortheastMegabuck.Divisions.Id.New();
        var squads = Enumerable.Empty<SquadId>();
        var sweepers = Enumerable.Empty<SquadId>();
        var average = 200;

        _adapter.Execute(bowler.Object, divisionId, squads, sweepers, superSweeper, average);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<NortheastMegabuck.Models.Registration>(registration => registration.Bowler.LastName == "lastName" &&
                                                                                                                                registration.Division.Id == divisionId &&
                                                                                                                                registration.Sweepers.Select(sweeper=> sweeper.Id) == sweepers &&
                                                                                                                                registration.SuperSweeper == superSweeper &&
                                                                                                                                registration.Squads.Select(squad=> squad.Id) == squads &&
                                                                                                                                registration.Average == average)), Times.Once);
    }

    [Test]
    public void Execute_AddBowlerView_ErrorsSetToBusinessLogicErrors([Values] bool superSweeper)
    {
        var errors = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("error"), 5);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var bowler = new Mock<NortheastMegabuck.Bowlers.Add.IViewModel>();
        var divisionId = NortheastMegabuck.Divisions.Id.New();
        var squads = Enumerable.Empty<SquadId>();
        var sweepers = Enumerable.Empty<SquadId>();
        var average = 200;

        _adapter.Execute(bowler.Object, divisionId, squads, sweepers,superSweeper, average);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public void Execute_AddBowlerView_ReturnsBusinessLogicExecute([Values] bool superSweeper)
    {
        var id = RegistrationId.New();
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NortheastMegabuck.Models.Registration>())).Returns(id);

        var bowler = new Mock<NortheastMegabuck.Bowlers.Add.IViewModel>();
        var divisionId = NortheastMegabuck.Divisions.Id.New();
        var squads = Enumerable.Empty<SquadId>();
        var sweepers = Enumerable.Empty<SquadId>();
        var average = 200;

        var actual = _adapter.Execute(bowler.Object, divisionId, squads, sweepers, superSweeper, average);

        Assert.That(actual, Is.EqualTo(id));
    }
}
