
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Registrations.DeleteRegistration;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.Delete;

[TestFixture]
internal sealed class Adapter
{
    private Mock<TournamentManager.Registrations.Delete.IBusinessLogic> _businessLogic;
    private Mock<ICommandHandler<DeleteRegistrationCommand, Deleted>> _commandHandler;

    private TournamentManager.Registrations.Delete.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<TournamentManager.Registrations.Delete.IBusinessLogic>();
        _commandHandler = new Mock<ICommandHandler<DeleteRegistrationCommand, Deleted>>();

        _adapter = new TournamentManager.Registrations.Delete.Adapter(_businessLogic.Object, _commandHandler.Object);
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
    public async Task ExecuteAsync_BowlerIdSquadId_ErrorSetToBusinessLogicError()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.ErrorDetail).Returns(error);

        await _adapter.ExecuteAsync(BowlerId.New(), SquadId.New(), default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public async Task ExecuteAsync_RegistrationId_CommandHandler_CalledCorrectly()
    {
        var registrationId = RegistrationId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(registrationId, cancellationToken).ConfigureAwait(false);

        _commandHandler
            .Verify(commandHandler => commandHandler.HandleAsync(It.Is<DeleteRegistrationCommand>(command => command.Id == registrationId), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RegistrationId_ErrorSetToResultError()
    {
        var error = Error.Failure("error");
        _commandHandler.Setup(commandHandler => commandHandler.HandleAsync(It.IsAny<DeleteRegistrationCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(error);

        await _adapter.ExecuteAsync(RegistrationId.New(), default).ConfigureAwait(false);

        Assert.That(_adapter.Error.Message, Is.EqualTo(error.Description));
    }
}
