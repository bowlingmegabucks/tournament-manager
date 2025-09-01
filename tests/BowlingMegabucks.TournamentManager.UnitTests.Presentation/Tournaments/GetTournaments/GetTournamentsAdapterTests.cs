using System.Net;
using BowlingMegabucks.TournamentManager.Contracts;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation;
using BowlingMegabucks.TournamentManager.Presentation.Services;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using BowlingMegabucks.TournamentManager.Tests.Factories;
using ErrorOr;
using Refit;

namespace BowlingMegabucks.TournamentManager.UnitTests.Presentation.Tournaments.GetTournaments;

public sealed class GetTournamentsAdapterTests
{
    private readonly Mock<ITournamentManagerApi> _mockTournamentManagerApi;

    private readonly GetTournamentsAdapter _getTournamentsAdapter;

    public GetTournamentsAdapterTests()
    {
        _mockTournamentManagerApi = new Mock<ITournamentManagerApi>(MockBehavior.Strict);
        _getTournamentsAdapter = new GetTournamentsAdapter(_mockTournamentManagerApi.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnTournaments_WhenApiCallIsSuccessful()
    {
        // Arrange
        var tournamentSummaries = TournamentSummaryFactory.FakeMany(3).ToList();

        int page = 1;
        int pageSize = 10;

        _mockTournamentManagerApi.Setup(api => api.GetTournamentsAsync(
                page,
                pageSize,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new OffsetPaginationResponse<TournamentSummary>
            {
                Items = tournamentSummaries,
                TotalPages = 1,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = tournamentSummaries.Count
            });

        // Act
        ErrorOr<OffsetPagingResult<TournamentSummaryViewModel>> result =
            await _getTournamentsAdapter.ExecuteAsync(page, pageSize, TestContext.Current.CancellationToken);

        // Assert
        result.IsError.Should().BeFalse();

        OffsetPagingResult<TournamentSummaryViewModel> pagingResult = result.Value;

        pagingResult.Items.Should().BeEquivalentTo(tournamentSummaries.Select(ts => new TournamentSummaryViewModel
        {
            Id = ts.Id,
            Name = ts.Name,
            StartDate = ts.StartDate,
            EndDate = ts.EndDate,
            EntryFee = ts.EntryFee,
            BowlingCenter = ts.BowlingCenter,
            Completed = ts.Completed
        }));
        pagingResult.TotalPages.Should().Be(1);
        pagingResult.CurrentPage.Should().Be(page);
        pagingResult.PageSize.Should().Be(pageSize);
        pagingResult.TotalItems.Should().Be(tournamentSummaries.Count);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnError_WhenApiCallFails()
    {
        // Arrange
        int page = 1;
        int pageSize = 10;

        using HttpResponseMessage httpResponseMessage = new(HttpStatusCode.InternalServerError);

        _mockTournamentManagerApi.Setup(api => api.GetTournamentsAsync(
                page,
                pageSize,
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(await new InvalidOperationException("Mock Exception").AsApiException(httpResponseMessage));

        // Act
        ErrorOr<OffsetPagingResult<TournamentSummaryViewModel>> result =
            await _getTournamentsAdapter.ExecuteAsync(page, pageSize, TestContext.Current.CancellationToken);

        // Assert
        result.IsError.Should().BeTrue();

        result.Errors.Should().ContainSingle();
        result.FirstError.Type.Should().Be(ErrorType.Failure);
        result.FirstError.Code.Should().Be("Tournaments.GetAllException");
        result.FirstError.Description.Should().Be("Error fetching tournaments");
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnError_WhenApiCallThrowsException()
    {
        // Arrange
        int page = 1;
        int pageSize = 10;

        _mockTournamentManagerApi.Setup(api => api.GetTournamentsAsync(
                page,
                pageSize,
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("Mock Exception"));

        // Act
        ErrorOr<OffsetPagingResult<TournamentSummaryViewModel>> result =
            await _getTournamentsAdapter.ExecuteAsync(page, pageSize, TestContext.Current.CancellationToken);

        // Assert
        result.IsError.Should().BeTrue();

        result.Errors.Should().ContainSingle();
        result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        result.FirstError.Code.Should().Be("Tournaments.GetAllRequest");
        result.FirstError.Description.Should().Be("Mock Exception");
    }
}
