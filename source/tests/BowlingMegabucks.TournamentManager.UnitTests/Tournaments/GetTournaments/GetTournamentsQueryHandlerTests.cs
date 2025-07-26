using BowlingMegabucks.TournamentManager.Database.Entities;
using BowlingMegabucks.TournamentManager.Tournaments;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournaments;
using Microsoft.Extensions.Logging.Abstractions;

namespace BowlingMegabucks.TournamentManager.UnitTests.Tournaments.GetTournaments;

[TestFixture]
public sealed class GetTournamentsQueryHandlerTests
{
    private Mock<IRepository> _repositoryMock;
    private GetTournamentsQueryHandler _handler;
    private GetTournamentsQueryHandlerLoggingDecorator _loggingDecorator;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IRepository>();
        _handler = new GetTournamentsQueryHandler(_repositoryMock.Object);
        _loggingDecorator = new GetTournamentsQueryHandlerLoggingDecorator(_handler, NullLogger<GetTournamentsQueryHandlerLoggingDecorator>.Instance);
    }

    [Test]
    public async Task HandleAsync_RepositoryRetrieveAll_Called()
    {
        await _loggingDecorator.HandleAsync(new GetTournamentsQuery(), CancellationToken.None);

        _repositoryMock.Verify(repo => repo.RetrieveAllAsync(CancellationToken.None), Times.Once);
    }

    [Test]
    public async Task HandleAsync_RepositoryRetrieveAll_ReturnsResult()
    {
        var expectedTournaments = new List<Tournament>
        {
            new() { Id = TournamentId.New(), Name = "Tournament 1" },
            new() { Id = TournamentId.New(), Name = "Tournament 2" }
        };

        _repositoryMock.Setup(repo => repo.RetrieveAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectedTournaments);

        var result = await _loggingDecorator.HandleAsync(new GetTournamentsQuery(), CancellationToken.None);

        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value.Count(), Is.EqualTo(expectedTournaments.Count));
        Assert.That(result.Value.First().Id, Is.EqualTo(expectedTournaments[0].Id));
        Assert.That(result.Value.Last().Id, Is.EqualTo(expectedTournaments[1].Id));
    }

    [Test]
    public async Task HandleAsync_RepositoryRetrieveAll_ThrowsException_ReturnsError()
    {
        var exception = new InvalidOperationException("Database error");
        _repositoryMock.Setup(repo => repo.RetrieveAllAsync(It.IsAny<CancellationToken>())).ThrowsAsync(exception);

        var result = await _loggingDecorator.HandleAsync(new GetTournamentsQuery(), CancellationToken.None);

        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Code, Is.EqualTo("GetTournamentsQueryHandler.Exception"));
        Assert.That(result.FirstError.Description, Is.EqualTo(exception.Message));
    }
}