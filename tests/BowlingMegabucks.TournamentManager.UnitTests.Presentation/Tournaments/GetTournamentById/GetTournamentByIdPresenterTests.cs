using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournamentById;
using BowlingMegabucks.TournamentManager.Tests.Factories;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.UnitTests.Presentation.Tournaments.GetTournamentById;

public sealed class GetTournamentByIdPresenterTests
{
    private readonly Mock<IGetTournamentByIdAdapter> _mockAdapter;

    private readonly GetTournamentByIdPresenter _presenter;

    public GetTournamentByIdPresenterTests()
    {
        _mockAdapter = new Mock<IGetTournamentByIdAdapter>(MockBehavior.Strict);
        _presenter = new GetTournamentByIdPresenter(_mockAdapter.Object);
    }

    [Fact]
    public async Task GetTournamentAsync_ShouldShowErrors_WhenAdapterCallHasError()
    {
        // Arrange
        var tournamentId = TournamentId.New();
        using CancellationTokenSource cancellationTokenSource = new();

        var mockView = new Mock<IGetTournamentByIdView>(MockBehavior.Strict);
        mockView.Setup(view => view.ShowProcessingMessage("Loading tournament...", cancellationTokenSource));

        Error error = Error.Failure();

        _mockAdapter.Setup(adapter => adapter.ExecuteAsync(tournamentId, cancellationTokenSource.Token))
            .ReturnsAsync(error);

        mockView.Setup(view => view.DisplayErrorMessage(new[] { error }));
        mockView.Setup(view => view.HideProcessingMessage());

        // Act
        await _presenter.GetTournamentAsync(mockView.Object, tournamentId, cancellationTokenSource);

        // Assert
        mockView.VerifyAll();
        _mockAdapter.VerifyAll();
    }

    [Fact]
    public async Task GetTournamentAsync_ShouldBindTournament_WhenAdapterCallIsSuccessful()
    {
        // Arrange
        var tournamentId = TournamentId.New();
        using CancellationTokenSource cancellationTokenSource = new();

        var mockView = new Mock<IGetTournamentByIdView>(MockBehavior.Strict);
        mockView.Setup(view => view.ShowProcessingMessage("Loading tournament...", cancellationTokenSource));

        TournamentDetailViewModel tournament = TournamentDetailViewModelFactory.FakeSingle();

        _mockAdapter.Setup(adapter => adapter.ExecuteAsync(tournamentId, cancellationTokenSource.Token))
            .ReturnsAsync(tournament);

        mockView.Setup(view => view.BindTournament(tournament));
        mockView.Setup(view => view.HideProcessingMessage());

        // Act
        await _presenter.GetTournamentAsync(mockView.Object, tournamentId, cancellationTokenSource);

        // Assert
        mockView.VerifyAll();
        _mockAdapter.VerifyAll();
    }
}
