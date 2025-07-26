using BowlingMegabucks.TournamentManager.Database.Entities;
using BowlingMegabucks.TournamentManager.Tournaments;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournaments;
using Microsoft.Extensions.Logging.Abstractions;

namespace BowlingMegabucks.TournamentManager.Tests.Tournaments.GetTournaments;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CA1515 
#pragma warning disable CA1707

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

        _repositoryMock.Verify(repo => repo.RetrieveAll(), Times.Once);
    }

    [Test]
    public async Task HandleAsync_RepositoryRetrieveAll_ReturnsResult()
    {
        var expectedTournaments = new List<Tournament>
        {
            new() { Id = TournamentId.New(), Name = "Tournament 1" },
            new() { Id = TournamentId.New(), Name = "Tournament 2" }
        }.AsQueryable();

        _repositoryMock.Setup(repo => repo.RetrieveAll()).Returns(expectedTournaments);

        var result = await _loggingDecorator.HandleAsync(new GetTournamentsQuery(), CancellationToken.None);

        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value, Is.EqualTo(expectedTournaments));
    }

    [Test]
    public async Task HandleAsync_RepositoryRetrieveAll_ThrowsException_ReturnsError()
    {
        var exception = new Exception("Database error");
        _repositoryMock.Setup(repo => repo.RetrieveAll()).Throws(exception);

        var result = await _loggingDecorator.HandleAsync(new GetTournamentsQuery(), CancellationToken.None);

        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Code, Is.EqualTo("GetTournamentsQueryHandler.Exception"));
        Assert.That(result.FirstError.Description, Is.EqualTo(exception.Message));
    }
}