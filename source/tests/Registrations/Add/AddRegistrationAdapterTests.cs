
namespace NortheastMegabuck.Tests.Registrations.Add;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Registrations.Add.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Registrations.Add.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Registrations.Add.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Registrations.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_AddBowlerViewModel_BusinessLogicExecute_CalledCorrectly([Values] bool superSweeper)
    {
        var bowler = new Mock<NortheastMegabuck.Bowlers.IViewModel>();
        bowler.SetupGet(b => b.LastName).Returns("lastName");
        var divisionId = DivisionId.New();
        var squads = Enumerable.Empty<SquadId>();
        var sweepers = Enumerable.Empty<SquadId>();
        var average = 200;
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(bowler.Object, divisionId, squads, sweepers, superSweeper, average, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(It.Is<NortheastMegabuck.Models.Registration>(registration => registration.Bowler.Name.Last == "lastName" &&
                                                                                                                                registration.Division.Id == divisionId &&
                                                                                                                                registration.Sweepers.Select(sweeper => sweeper.Id) == sweepers &&
                                                                                                                                registration.SuperSweeper == superSweeper &&
                                                                                                                                registration.Squads.Select(squad => squad.Id) == squads &&
                                                                                                                                registration.Average == average), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_AddBowlerViewModel_ErrorsSetToBusinessLogicErrors([Values] bool superSweeper)
    {
        var errors = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("error"), 5);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var bowler = new Mock<NortheastMegabuck.Bowlers.IViewModel>();
        var divisionId = DivisionId.New();
        var squads = Enumerable.Empty<SquadId>();
        var sweepers = Enumerable.Empty<SquadId>();
        var average = 200;

        await _adapter.ExecuteAsync(bowler.Object, divisionId, squads, sweepers, superSweeper, average, default).ConfigureAwait(false);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public async Task ExecuteAsync_AddBowlerView_ReturnsBusinessLogicExecute([Values] bool superSweeper)
    {
        var id = RegistrationId.New();
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.Registration>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var bowler = new Mock<NortheastMegabuck.Bowlers.IViewModel>();
        var divisionId = DivisionId.New();
        var squads = Enumerable.Empty<SquadId>();
        var sweepers = Enumerable.Empty<SquadId>();
        var average = 200;

        var actual = await _adapter.ExecuteAsync(bowler.Object, divisionId, squads, sweepers, superSweeper, average, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(id));
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_BusinessLogicExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(bowlerId, squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_ErrorsSetToBusinessLogicErrors()
    {
        var errors = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("error"), 5);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        await _adapter.ExecuteAsync(bowlerId, squadId, default).ConfigureAwait(false);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_BusinessLogicExecuteReturnsNull_ReturnsNull()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var actual = await _adapter.ExecuteAsync(bowlerId, squadId, default).ConfigureAwait(false);

        Assert.That(actual, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_BusinessLogicExecuteHasNoError_ReturnsLaneAssignment()
    {
        var registration = new NortheastMegabuck.Models.Registration { Average = 200 };
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(registration);

        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var actual = await _adapter.ExecuteAsync(bowlerId, squadId, default).ConfigureAwait(false);

        Assert.That(actual.Average, Is.EqualTo(200));
    }
}
