using BowlingMegabucks.TournamentManager.Presentation;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using BowlingMegabucks.TournamentManager.Tests.Factories;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.UnitTests.Presentation.Tournaments.GetTournaments;

public sealed class GetTournamentsPresenterTests
{
    private readonly Mock<IGetTournamentsView> _mockView;
    private readonly Mock<IGetTournamentsAdapter> _mockAdapter;
    private readonly GetTournamentsPresenter _presenter;

    public GetTournamentsPresenterTests()
    {
        _mockView = new Mock<IGetTournamentsView>(MockBehavior.Strict);
        _mockAdapter = new Mock<IGetTournamentsAdapter>(MockBehavior.Strict);
        _presenter = new GetTournamentsPresenter(_mockView.Object, _mockAdapter.Object);
    }

    [Fact]
    public async Task GetTournamentsAsync_ShouldPerformErrorFlow_WhenAdapterReturnsErrors()
    {
        // Arrange
        int page = 1;
        int pageSize = 10;
        var errors = new List<Error>
        {
            Error.Failure("Test", "Adapter error")
        };

        _mockAdapter.Setup(adapter => adapter.ExecuteAsync(page, pageSize, It.IsAny<CancellationToken>()))
            .ReturnsAsync(errors);

        _mockView.Setup(view => view.DisplayErrorMessage(errors));
        _mockView.Setup(view => view.DisableOpenTournament());

        // Act
        await _presenter.GetTournamentsAsync(page, pageSize, TestContext.Current.CancellationToken);

        // Assert
        _mockView.VerifyAll();
    }

    [Fact]
    public async Task GetTournamentsAsync_ShouldPerformNoTournamentsFlow_WhenAdapterReturnsNoTournaments()
    {
        // Arrange
        int page = 1;
        int pageSize = 10;
        var emptyResult = new OffsetPagingResult<TournamentSummaryViewModel>
        {
            Items = [],
            TotalItems = 0,
            TotalPages = 0,
            CurrentPage = 0,
            PageSize = 0
        };

        _mockAdapter.Setup(adapter => adapter.ExecuteAsync(page, pageSize, It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyResult);

        _mockView.Setup(view => view.DisableOpenTournament());

        // Act
        await _presenter.GetTournamentsAsync(page, pageSize, TestContext.Current.CancellationToken);

        // Assert
        _mockView.VerifyAll();
    }

    [Fact]
    public async Task GetTournamentsAsync_ShouldBindTournaments_WhenAdapterReturnsTournaments()
    {
        // Arrange
        int page = 1;
        int pageSize = 10;
        var tournaments = TournamentSummaryViewModelFactory.FakeMany(5).ToList();
        var result = new OffsetPagingResult<TournamentSummaryViewModel>
        {
            Items = tournaments,
            TotalItems = tournaments.Count,
            TotalPages = 1,
            CurrentPage = 1,
            PageSize = tournaments.Count
        };

        _mockAdapter.Setup(adapter => adapter.ExecuteAsync(page, pageSize, It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        _mockView.Setup(view => view.BindTournaments(tournaments));

        // Act
        await _presenter.GetTournamentsAsync(page, pageSize, TestContext.Current.CancellationToken);

        // Assert
        _mockView.VerifyAll();
    }
}

