using System.Net;
using BowlingMegabucks.TournamentManager.Contracts;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation;
using BowlingMegabucks.TournamentManager.Presentation.Services;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournamentById;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using BowlingMegabucks.TournamentManager.Tests.Factories;
using ErrorOr;
using Refit;

namespace BowlingMegabucks.TournamentManager.UnitTests.Presentation.Tournaments.GetTournamentById;

public sealed class GetTournamentByIdAdapterTests
{
    private readonly Mock<ITournamentManagerApi> _mockTournamentManagerApi;

    private readonly GetTournamentByIdAdapter _getTournamentByIdAdapter;

    public GetTournamentByIdAdapterTests()
    {
        _mockTournamentManagerApi = new Mock<ITournamentManagerApi>(MockBehavior.Strict);
        _getTournamentByIdAdapter = new GetTournamentByIdAdapter(_mockTournamentManagerApi.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnTournaments_WhenApiCallIsSuccessful()
    {
        // Arrange
        TournamentDetail tournament = TournamentDetailFactory.FakeSingle();

        _mockTournamentManagerApi.Setup(api => api.GetTournamentByIdAsync(
                tournament.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResponse.Ok(tournament));

        // Act
        ErrorOr<TournamentDetailViewModel> result =
            await _getTournamentByIdAdapter.ExecuteAsync(tournament.Id, TestContext.Current.CancellationToken);

        // Assert
        result.IsError.Should().BeFalse();

        TournamentDetailViewModel tournamentViewModel = result.Value;

        tournamentViewModel.Should().BeEquivalentTo(new TournamentDetailViewModel
        {
            Id = tournament.Id,
            Name = tournament.Name,
            StartDate = tournament.StartDate,
            EndDate = tournament.EndDate,
            EntryFee = tournament.EntryFee,
            BowlingCenter = tournament.BowlingCenter,
            Games = tournament.Games,
            FinalsRatio = tournament.FinalsRatio,
            CashRatio = tournament.CashRatio,
            SuperSweeperCashRatio = tournament.SuperSweeperCashRatio,
            Completed = tournament.Completed
        });
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnError_WhenApiCallFails()
    {
        // Arrange
        TournamentDetail tournament = TournamentDetailFactory.FakeSingle();

        using HttpResponseMessage httpResponseMessage = new(HttpStatusCode.InternalServerError);

        _mockTournamentManagerApi.Setup(api => api.GetTournamentByIdAsync(
                tournament.Id,
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(await new InvalidOperationException("Mock Exception").AsApiException(httpResponseMessage));

        // Act
        ErrorOr<TournamentDetailViewModel> result =
            await _getTournamentByIdAdapter.ExecuteAsync(tournament.Id, TestContext.Current.CancellationToken);

        // Assert
        result.IsError.Should().BeTrue();

        result.Errors.Should().ContainSingle();
        result.FirstError.Type.Should().Be(ErrorType.Failure);
        result.FirstError.Code.Should().Be("Tournaments.GetById");
        result.FirstError.Description.Should().Be("Error fetching tournament");
    }
}
