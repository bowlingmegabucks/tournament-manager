
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Registrations.GetRegistrationById;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<TournamentManager.Registrations.Retrieve.IBusinessLogic> _businessLogic;
    private Mock<IQueryHandler<GetRegistrationByIdQuery, TournamentManager.Models.Registration>> _getRegistrationByIdQueryHandler;

    private TournamentManager.Registrations.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<TournamentManager.Registrations.Retrieve.IBusinessLogic>();
        _getRegistrationByIdQueryHandler = new Mock<IQueryHandler<GetRegistrationByIdQuery, TournamentManager.Models.Registration>>();

        _adapter = new TournamentManager.Registrations.Retrieve.Adapter(_businessLogic.Object, _getRegistrationByIdQueryHandler.Object);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_BusinessLogicExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ErrorSetToBusinessLogicError([Range(0, 1)] int errorCount)
    {
        var error = Enumerable.Repeat(new TournamentManager.Models.ErrorDetail("test"), errorCount).SingleOrDefault();
        _businessLogic.SetupGet(businessLogic => businessLogic.ErrorDetail).Returns(error);

        var tournamentId = TournamentId.New();

        await _adapter.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ReturnsRegistrations()
    {
        var registrations = new[]
        {
            new TournamentManager.Models.Registration{ Id = RegistrationId.New()},
            new TournamentManager.Models.Registration{ Id = RegistrationId.New()}
        };

        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(registrations);

        var tournamentId = TournamentId.New();

        var actual = await _adapter.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual.First().Id, Is.EqualTo(registrations[0].Id));
            Assert.That(actual.Last().Id, Is.EqualTo(registrations[registrations.Length - 1].Id));
        });
    }

    [Test]
    public async Task ExecuteAsync_RegistrationId_QueryHandlerHandle_CalledCorrectly()
    {
        var registrationId = RegistrationId.New();
        CancellationToken cancellationToken = default;

        _getRegistrationByIdQueryHandler.Setup(handler => handler.HandleAsync(It.IsAny<GetRegistrationByIdQuery>(), cancellationToken))
            .ReturnsAsync(new TournamentManager.Models.Registration { Id = registrationId });

        await _adapter.ExecuteAsync(registrationId, cancellationToken).ConfigureAwait(false);

        _getRegistrationByIdQueryHandler.Verify(handler => handler.HandleAsync(It.Is<GetRegistrationByIdQuery>(query => query.Id == registrationId), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RegistrationId_ErrorSetToQueryHandlerError()
    {
        var error = Error.Unexpected(description: "query error");

        _getRegistrationByIdQueryHandler.Setup(handler => handler.HandleAsync(It.IsAny<GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(error);

        var registrationId = RegistrationId.New();

        var result = await _adapter.ExecuteAsync(registrationId, default).ConfigureAwait(false);

        Assert.That(_adapter.Error.Message, Is.EqualTo(error.Description));
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_RegistrationId_ReturnsRegistrationViewModel()
    {
        var registration = new TournamentManager.Models.Registration { Id = RegistrationId.New() };

        _getRegistrationByIdQueryHandler.Setup(handler => handler.HandleAsync(It.IsAny<GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(registration);

        var registrationId = RegistrationId.New();

        var result = await _adapter.ExecuteAsync(registrationId, default).ConfigureAwait(false);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(registration.Id));
    }
}
