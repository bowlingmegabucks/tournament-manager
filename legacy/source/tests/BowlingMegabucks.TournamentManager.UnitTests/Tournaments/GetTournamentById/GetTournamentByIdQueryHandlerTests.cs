using BowlingMegabucks.TournamentManager.Tournaments;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;
using Microsoft.Extensions.Logging.Abstractions;

namespace BowlingMegabucks.TournamentManager.UnitTests.Tournaments.GetTournamentById;

[TestFixture]
public sealed class GetTournamentByIdQueryHandlerTests
{
    private Mock<IRepository> _repositoryMock;
    private GetTournamentByIdQueryHandler _handler;
    private GetTournamentByIdQueryHandlerTelemetryDecorator _telemetryDecorator;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IRepository>();
        _handler = new GetTournamentByIdQueryHandler(_repositoryMock.Object);
        _telemetryDecorator = new GetTournamentByIdQueryHandlerTelemetryDecorator(_handler, NullLogger<GetTournamentByIdQueryHandlerTelemetryDecorator>.Instance);
    }

    [Test]
    public async Task ExecuteAsync_Id_Repository_Called()
    {
        var id = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _telemetryDecorator.HandleAsync(new() { Id = id}, cancellationToken).ConfigureAwait(false);

        _repositoryMock.Verify(repository => repository.RetrieveAsync(id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Id_ReturnsResultFromRepository()
    {
        var tournamentId = TournamentId.New();
        var tournamentEntity = new TournamentManager.Database.Entities.Tournament() { Id = tournamentId };
        _repositoryMock.Setup(repository => repository.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournamentEntity);

        var result = await _telemetryDecorator.HandleAsync(new() { Id = tournamentId }, default).ConfigureAwait(false);

        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value.Id, Is.EqualTo(tournamentId));
    }

    [Test]
    public async Task ExecuteAsync_Id_RepositoryExecuteThrowsException_ErrorResultReturned()
    {
        var ex = new Exception("something");
        _repositoryMock.Setup(repository => repository.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var id = TournamentId.New();
        var result = await _telemetryDecorator.HandleAsync(new() { Id = id }, default).ConfigureAwait(false);

        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Description, Is.EqualTo(ex.Message));
    }
}