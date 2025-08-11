using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Registrations.Create;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.Add;

[TestFixture]
internal sealed class Adapter
{
    private Mock<ICommandHandler<CreateRegistrationCommand, RegistrationId>> _commandHandler;
    private Mock<TournamentManager.Registrations.Add.IBusinessLogic> _businessLogic;

    private TournamentManager.Registrations.Add.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _commandHandler = new Mock<ICommandHandler<CreateRegistrationCommand, RegistrationId>>();
        _businessLogic = new Mock<TournamentManager.Registrations.Add.IBusinessLogic>();

        _adapter = new TournamentManager.Registrations.Add.Adapter(_commandHandler.Object, _businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_AddBowlerViewModel_BusinessLogicExecute_CalledCorrectly([Values] bool superSweeper)
    {
        var bowler = new Mock<TournamentManager.Bowlers.IViewModel>();
        bowler.SetupGet(b => b.LastName).Returns("lastName");
        var tournamentId = TournamentId.New();
        var divisionId = DivisionId.New();
        var squads = Enumerable.Empty<SquadId>();
        var sweepers = Enumerable.Empty<SquadId>();
        var average = 200;
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(bowler.Object, tournamentId, divisionId, squads, sweepers, superSweeper, average, cancellationToken).ConfigureAwait(false);

        _commandHandler.Verify(commandHandler => commandHandler.HandleAsync(It.Is<CreateRegistrationCommand>(registration => registration.Bowler.Name.Last == "lastName" &&
                                                                                                                                registration.TournamentId == tournamentId &&
                                                                                                                                registration.DivisionId == divisionId &&
                                                                                                                                registration.Sweepers == sweepers &&
                                                                                                                                registration.SuperSweeper == superSweeper &&
                                                                                                                                registration.Squads == squads &&
                                                                                                                                registration.Average == average &&
                                                                                                                                registration.Payment.Amount == 0 &&
                                                                                                                                registration.Payment.ConfirmationCode.StartsWith("Manual_", StringComparison.OrdinalIgnoreCase)), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_AddBowlerViewModel_ErrorsSetToBusinessLogicErrors([Values] bool superSweeper)
    {
        var error = Error.Failure(description: "command handler error");
        _commandHandler.Setup(commandHandler => commandHandler.HandleAsync(It.IsAny<CreateRegistrationCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(error);

        var bowler = new Mock<TournamentManager.Bowlers.IViewModel>();
        var tournamentId = TournamentId.New();
        var divisionId = DivisionId.New();
        var squads = Enumerable.Empty<SquadId>();
        var sweepers = Enumerable.Empty<SquadId>();
        var average = 200;

        var result = await _adapter.ExecuteAsync(bowler.Object, tournamentId, divisionId, squads, sweepers, superSweeper, average, default).ConfigureAwait(false);

        Assert.That(result, Is.Null);
        Assert.That(_adapter.Errors.First().Message, Is.EqualTo("command handler error"));
    }

    [Test]
    public async Task ExecuteAsync_AddBowlerView_ReturnsCommandHandlerResult([Values] bool superSweeper)
    {
        var id = RegistrationId.New();
        _commandHandler.Setup(commandHandler => commandHandler.HandleAsync(It.IsAny<CreateRegistrationCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var bowler = new Mock<TournamentManager.Bowlers.IViewModel>();
        var tournamentId = TournamentId.New();
        var divisionId = DivisionId.New();
        var squads = Enumerable.Empty<SquadId>();
        var sweepers = Enumerable.Empty<SquadId>();
        var average = 200;

        var actual = await _adapter.ExecuteAsync(bowler.Object, tournamentId, divisionId, squads, sweepers, superSweeper, average, default).ConfigureAwait(false);

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
        var errors = Enumerable.Repeat(new TournamentManager.Models.ErrorDetail("error"), 5);
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
        var registration = new TournamentManager.Models.Registration { Average = 200 };
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(registration);

        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var actual = await _adapter.ExecuteAsync(bowlerId, squadId, default).ConfigureAwait(false);

        Assert.That(actual.Average, Is.EqualTo(200));
    }
}
