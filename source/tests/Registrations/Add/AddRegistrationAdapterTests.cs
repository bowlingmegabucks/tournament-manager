
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
        var divisionId = DivisionId.New();
        var squads = Enumerable.Empty<SquadId>();
        var sweepers = Enumerable.Empty<SquadId>();
        var average = 200;

        _adapter.Execute(bowler.Object, divisionId, squads, sweepers, superSweeper, average);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<NortheastMegabuck.Models.Registration>(registration => registration.Bowler.Name.Last == "lastName" &&
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
        var divisionId = DivisionId.New();
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
        var divisionId = DivisionId.New();
        var squads = Enumerable.Empty<SquadId>();
        var sweepers = Enumerable.Empty<SquadId>();
        var average = 200;

        var actual = _adapter.Execute(bowler.Object, divisionId, squads, sweepers, superSweeper, average);

        Assert.That(actual, Is.EqualTo(id));
    }

    [Test]
    public void Execute_BowlerIdSquadId_BusinessLogicExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        _adapter.Execute(bowlerId, squadId);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(bowlerId, squadId), Times.Once);
    }

    [Test]
    public void Execute_BowlerIdSquadId_ErrorsSetToBusinessLogicErrors()
    {
        var errors = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("error"), 5);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        _adapter.Execute(bowlerId, squadId);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public void Execute_BowlerIdSquadId_BusinessLogicExecuteReturnsNull_ReturnsNull()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var actual = _adapter.Execute(bowlerId, squadId);

        Assert.That(actual, Is.Null);
    }

    [Test]
    public void Execute_BowlerIdSquadId_BusinessLogicExecuteHasNoError_ReturnsLaneAssignment()
    {
        var registration = new NortheastMegabuck.Models.Registration { Average = 200};
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<BowlerId>(), It.IsAny<SquadId>())).Returns(registration);

        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var actual = _adapter.Execute(bowlerId, squadId);

        Assert.That(actual.Average, Is.EqualTo(200));
    }
}
