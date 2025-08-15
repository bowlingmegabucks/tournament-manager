using BowlingMegabucks.TournamentManager.Registrations;
using BowlingMegabucks.TournamentManager.Registrations.GetRegistrationById;
using Microsoft.Extensions.Logging.Abstractions;

namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.GetRegistrationById;

[TestFixture]
public sealed class GetRegistrationByIdQueryHandlerTests
{
    private Mock<IRepository> _repositoryMock;
    private GetRegistrationByIdQueryHandler _handler;
    private GetRegistrationByIdQueryHandlerTelemetryDecorator _telemetryDecorator;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IRepository>();
        _handler = new GetRegistrationByIdQueryHandler(_repositoryMock.Object);
        _telemetryDecorator = new GetRegistrationByIdQueryHandlerTelemetryDecorator(_handler, NullLogger<GetRegistrationByIdQueryHandlerTelemetryDecorator>.Instance);
    }

    [Test]
    public async Task ExecuteAsync_Id_Repository_Called()
    {
        var id = RegistrationId.New();
        CancellationToken cancellationToken = default;

        await _telemetryDecorator.HandleAsync(new() { Id = id}, cancellationToken).ConfigureAwait(false);

        _repositoryMock.Verify(repository => repository.RetrieveAsync(id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Id_ReturnsResultFromRepository()
    {
        var registrationId = RegistrationId.New();
        var registrationEntity = new TournamentManager.Database.Entities.Registration
        {
            Id = registrationId,
            Bowler = new()
            {
                Id = BowlerId.New()
            },
            Division = new()
            {
                Id = DivisionId.New()
            },
            Squads = [],
            Payments = []
        };
        
        _repositoryMock.Setup(repository => repository.RetrieveAsync(It.IsAny<RegistrationId>(), It.IsAny<CancellationToken>())).ReturnsAsync(registrationEntity);

        var result = await _telemetryDecorator.HandleAsync(new() { Id = registrationId }, default).ConfigureAwait(false);

        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value.Id, Is.EqualTo(registrationId));
    }

    [Test]
    public async Task ExecuteAsync_Id_RepositoryExecuteThrowsException_ErrorResultReturned()
    {
        var ex = new Exception("something");
        _repositoryMock.Setup(repository => repository.RetrieveAsync(It.IsAny<RegistrationId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var id = RegistrationId.New();
        var result = await _telemetryDecorator.HandleAsync(new() { Id = id }, default).ConfigureAwait(false);

        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Description, Is.EqualTo(ex.Message));
    }
}