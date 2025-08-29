using BowlingMegabucks.TournamentManager.Application.Tournaments.GetAllTournaments;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.Infrastructure.Queries;
using BowlingMegabucks.TournamentManager.Infrastructure.Tests.Fixtures;
using BowlingMegabucks.TournamentManager.Tests.Factories;
using BowlingMegabucks.TournamentManager.Tests.Infrastructure;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Tests.Queries;

public sealed class TournamentQueriesTests
    : IClassFixture<DatabaseContainer>, IAsyncLifetime
{
    private readonly DatabaseContainer _databaseContainer;
    private QueryTestFixture _queryTestFixture = null!;
    private TournamentQueries _tournamentQueries = null!;

    public TournamentQueriesTests(DatabaseContainer databaseContainer)
    {
        _databaseContainer = databaseContainer;
    }

    public async ValueTask InitializeAsync()
    {
        _queryTestFixture = new QueryTestFixture(_databaseContainer);
        await _queryTestFixture.InitializeAsync();

        _tournamentQueries = new TournamentQueries(_queryTestFixture.ApplicationDbContext);
    }

    public async ValueTask DisposeAsync()
        => await _queryTestFixture.DisposeAsync();

    [Fact]
    public async Task GetAllTournamentsAsync_ShouldReturnAnEmptyCollection_WhenThereAreNoTournaments()
    {
        // Arrange

        // Act
        IEnumerable<TournamentSummaryDto> result = await _tournamentQueries.GetAllTournamentsAsync(TestContext.Current.CancellationToken);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllTournamentsAsync_ShouldReturnAllTournaments_WhenQueried()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(5);
        _queryTestFixture.ApplicationDbContext.Tournaments.AddRange(tournaments);

        await _queryTestFixture.ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        IEnumerable<TournamentSummaryDto> result = await _tournamentQueries.GetAllTournamentsAsync(TestContext.Current.CancellationToken);

        // Assert
        result.Should().HaveCount(5);
    }

    [Fact]
    public async Task GetAllTournamentsAsync_ShouldMapFields_WhenTournamentsAreFound()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(3);
        _queryTestFixture.ApplicationDbContext.Tournaments.AddRange(tournaments);

        await _queryTestFixture.ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        IEnumerable<TournamentSummaryDto> result = await _tournamentQueries.GetAllTournamentsAsync(TestContext.Current.CancellationToken);

        // Assert
        result.Should().BeEquivalentTo(tournaments.Select(t => new TournamentSummaryDto
        {
            Id = t.Id,
            Name = t.Name,
            StartDate = t.TournamentDates.StartDate,
            EndDate = t.TournamentDates.EndDate,
            BowlingCenter = t.BowlingCenter,
            EntryFee = t.EntryFee,
            Completed = t.Completed
        }));
    }
}
