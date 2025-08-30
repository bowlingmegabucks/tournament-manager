using BowlingMegabucks.TournamentManager.Api;
using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournaments;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.IntegrationTests.Infrastructure;
using BowlingMegabucks.TournamentManager.Tests.Factories;
using BowlingMegabucks.TournamentManager.Tests.Infrastructure;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Tournaments;

public sealed class GetTournamentsQueryHandlerTests
    : BaseIntegrationTest
{
    private readonly IOffsetPaginationQueryHandler<GetTournamentsQuery, TournamentSummaryDto> _handler;

    public GetTournamentsQueryHandlerTests(TournamentManagerWebAppFactory<IApiAssemblyMarker> factory)
        : base(factory ?? throw new ArgumentNullException(nameof(factory)))
    {
        _handler = GetRequiredService<IOffsetPaginationQueryHandler<GetTournamentsQuery, TournamentSummaryDto>>();
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnOffsetPaginationResponse_WhenAllItemsAreOnOnePage()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(10);
        ApplicationDbContext.Tournaments.AddRange(tournaments);
        await ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var query = new GetTournamentsQuery
        {
            Page = 1,
            PageSize = 20
        };

        // Act
        ErrorOr<OffsetPaginationQueryResponse<TournamentSummaryDto>> result =
            await _handler.HandleAsync(query, TestContext.Current.CancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();

        OffsetPaginationQueryResponse<TournamentSummaryDto> response = result.Value;

        response.TotalItems.Should().Be(10);
        response.TotalPages.Should().Be(1);
        response.CurrentPage.Should().Be(1);
        response.PageSize.Should().Be(20);
        response.Items.Should().BeEquivalentTo(tournaments.Select(t => new TournamentSummaryDto
        {
            Id = t.Id,
            Name = t.Name,
            StartDate = t.TournamentDates.StartDate,
            EndDate = t.TournamentDates.EndDate,
            EntryFee = t.EntryFee,
            BowlingCenter = t.BowlingCenter,
            Completed = t.Completed
        }));
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnCorrectOffsetPaginationResponse_WhenPageSizeIsFactorOfTotalCount()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(30);
        ApplicationDbContext.Tournaments.AddRange(tournaments);
        await ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var query = new GetTournamentsQuery
        {
            Page = 2,
            PageSize = 10
        };

        // Act
        ErrorOr<OffsetPaginationQueryResponse<TournamentSummaryDto>> result =
            await _handler.HandleAsync(query, TestContext.Current.CancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();

        OffsetPaginationQueryResponse<TournamentSummaryDto> response = result.Value;

        response.TotalItems.Should().Be(30);
        response.TotalPages.Should().Be(3);
        response.CurrentPage.Should().Be(2);
        response.PageSize.Should().Be(10);
        response.Items.Should().HaveCount(10);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnCorrectOffsetPaginationResponse_WhenPageSizeIsNotFactorOfTotalCount()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(25);
        ApplicationDbContext.Tournaments.AddRange(tournaments);
        await ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var query = new GetTournamentsQuery
        {
            Page = 3,
            PageSize = 10
        };

        // Act
        ErrorOr<OffsetPaginationQueryResponse<TournamentSummaryDto>> result =
            await _handler.HandleAsync(query, TestContext.Current.CancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();

        OffsetPaginationQueryResponse<TournamentSummaryDto> response = result.Value;

        response.TotalItems.Should().Be(25);
        response.TotalPages.Should().Be(3);
        response.CurrentPage.Should().Be(3);
        response.PageSize.Should().Be(10);
        response.Items.Should().HaveCount(5);
    }
}
