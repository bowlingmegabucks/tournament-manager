using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournamentById;
using BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournaments;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.Infrastructure.Queries;
using BowlingMegabucks.TournamentManager.IntegrationTests.Infrastructure.Fixtures;
using BowlingMegabucks.TournamentManager.Tests;
using BowlingMegabucks.TournamentManager.Tests.Factories;
using BowlingMegabucks.TournamentManager.Tests.Infrastructure;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Infrastructure.Queries;

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

        // Reset the database state before each test
        await _queryTestFixture.ResetDatabaseAsync();
    }

    public async ValueTask DisposeAsync()
        => await _queryTestFixture.DisposeAsync();

    [Fact]
    public async Task GetAllTournamentsAsync_ShouldReturnAnEmptyCollection_WhenThereAreNoTournaments()
    {
        // Arrange
        Mock<IOffsetPaginationQuery> paginationMock = new(MockBehavior.Strict);
        paginationMock.SetupPagination();

        // Act
        IEnumerable<TournamentSummaryDto> result = await _tournamentQueries.GetAllTournamentsAsync(paginationMock.Object, TestContext.Current.CancellationToken);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllTournamentsAsync_ShouldReturnAllTournaments_WhenQueried()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(5);
        _queryTestFixture.ApplicationDbContext.Tournaments.AddRange(tournaments);

        Mock<IOffsetPaginationQuery> paginationMock = new(MockBehavior.Strict);
        paginationMock.SetupPagination(1, 10);

        await _queryTestFixture.ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        IEnumerable<TournamentSummaryDto> result = await _tournamentQueries.GetAllTournamentsAsync(paginationMock.Object, TestContext.Current.CancellationToken);

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

        Mock<IOffsetPaginationQuery> paginationMock = new(MockBehavior.Strict);
        paginationMock.SetupPagination(1, 10);

        // Act
        IEnumerable<TournamentSummaryDto> result = await _tournamentQueries.GetAllTournamentsAsync(paginationMock.Object, TestContext.Current.CancellationToken);

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

    [Fact]
    public async Task GetAllTournamentsAsync_ShouldReturnSubset_WhenPaginationPageSizeIsSmallerThanTotalCount()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(50);
        _queryTestFixture.ApplicationDbContext.Tournaments.AddRange(tournaments);

        await _queryTestFixture.ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        Mock<IOffsetPaginationQuery> paginationMock = new(MockBehavior.Strict);
        paginationMock.SetupPagination(1, 10);

        // Act
        IEnumerable<TournamentSummaryDto> result = await _tournamentQueries.GetAllTournamentsAsync(paginationMock.Object, TestContext.Current.CancellationToken);

        // Assert
        result.Should().HaveCount(10);
    }

    [Fact]
    public async Task GetAllTournamentsAsync_ShouldReturnLaterSet_WhenPaginationPageSizeIsSmallerThanTotalCountAndPageIsGreaterThanOne()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(50);
        _queryTestFixture.ApplicationDbContext.Tournaments.AddRange(tournaments);

        await _queryTestFixture.ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        Mock<IOffsetPaginationQuery> paginationMock = new(MockBehavior.Strict);
        paginationMock.SetupPagination(2, 3);

        Mock<IOffsetPaginationQuery> paginationMock2 = new(MockBehavior.Strict);
        paginationMock2.SetupPagination(1, 100);

        // Act
        IEnumerable<TournamentSummaryDto> pagedResults = await _tournamentQueries.GetAllTournamentsAsync(paginationMock.Object, TestContext.Current.CancellationToken);
        IList<TournamentSummaryDto> fullResults = [.. await _tournamentQueries.GetAllTournamentsAsync(paginationMock2.Object, TestContext.Current.CancellationToken)];

        // Assert
        pagedResults.Should().SatisfyRespectively(
            first => first.Id.Should().Be(fullResults[3].Id),
            second => second.Id.Should().Be(fullResults[4].Id),
            third => third.Id.Should().Be(fullResults[5].Id)
        );
    }

    [Fact]
    public async Task GetTotalTournamentCountAsync_ShouldReturnZero_WhenThereAreNoTournaments()
    {
        // Act
        int count = await _tournamentQueries.GetTotalTournamentCountAsync(TestContext.Current.CancellationToken);

        // Assert
        count.Should().Be(0);
    }

    [Fact]
    public async Task GetTotalTournamentCountAsync_ShouldReturnCorrectCount_WhenThereAreTournaments()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(7);
        _queryTestFixture.ApplicationDbContext.Tournaments.AddRange(tournaments);

        await _queryTestFixture.ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        int count = await _tournamentQueries.GetTotalTournamentCountAsync(TestContext.Current.CancellationToken);

        // Assert
        count.Should().Be(7);
    }

    [Fact]
    public async Task GetTournamentAsync_TournamentId_ShouldReturnNull_WhenTournamentDoesNotExist()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(10);
        _queryTestFixture.ApplicationDbContext.Tournaments.AddRange(tournaments);

        await _queryTestFixture.ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var tournamentId = TournamentId.New();

        // Act
        TournamentDetailDto? result = await _tournamentQueries.GetTournamentAsync(tournamentId, TestContext.Current.CancellationToken);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetTournamentAsync_TournamentId_ShouldReturnValue_WhenTournamentExists()
    {
        // Arrange
        List<Tournament> tournaments = [.. TournamentFactory.FakeMany(10)];
        _queryTestFixture.ApplicationDbContext.Tournaments.AddRange(tournaments);

        await _queryTestFixture.ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        TournamentId tournamentId = tournaments[3].Id;

        // Act
        TournamentDetailDto? result = await _tournamentQueries.GetTournamentAsync(tournamentId, TestContext.Current.CancellationToken);

        // Assert
        result.Should().NotBeNull();

        Tournament existingTournament = tournaments[3];

        result.Id.Should().Be(tournamentId);
        result.Name.Should().Be(existingTournament.Name);

        result.StartDate.Should().Be(existingTournament.TournamentDates.StartDate);
        result.EndDate.Should().Be(existingTournament.TournamentDates.EndDate);

        result.EntryFee.Should().Be(existingTournament.EntryFee);
        result.BowlingCenter.Should().Be(existingTournament.BowlingCenter);
        result.Games.Should().Be(existingTournament.Games);

        result.FinalsRatio.Should().Be(existingTournament.FinalsRatio.Value);
        result.CashRatio.Should().Be(existingTournament.CashRatio.Value);
        result.SuperSweeperCashRatio.Should().Be(existingTournament.SuperSweeperCashRatio.Value);

        result.Completed.Should().Be(existingTournament.Completed);
    }
}
