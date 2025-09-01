using BowlingMegabucks.TournamentManager.Api;
using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournamentById;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.IntegrationTests.Application.Infrastructure;
using BowlingMegabucks.TournamentManager.Tests.Factories;
using BowlingMegabucks.TournamentManager.Tests.Infrastructure;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Application.Tournaments;

public sealed class GetTournamentByIdQueryHandlerTests
    : BaseIntegrationTest
{
    private readonly IQueryHandler<GetTournamentByIdQuery, TournamentDetailDto?> _handler;

    public GetTournamentByIdQueryHandlerTests(TournamentManagerWebAppFactory<IApiAssemblyMarker> factory)
        : base(factory)
    {
        _handler = GetRequiredService<IQueryHandler<GetTournamentByIdQuery, TournamentDetailDto?>>();
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnNotFound_WhenTournamentDoesNotExist()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(10);
        ApplicationDbContext.Tournaments.AddRange(tournaments);

        await ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var tournamentId = TournamentId.New();
        var query = new GetTournamentByIdQuery
        {
            Id = tournamentId
        };

        // Act
        ErrorOr<TournamentDetailDto?> result = await _handler.HandleAsync(query, TestContext.Current.CancellationToken);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e == TournamentErrors.TournamentNotFound(tournamentId));
        result.FirstError.Metadata.Should().ContainKey("TournamentId").And.ContainValue(tournamentId);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnTournamentDetailDto_WhenTournamentExists()
    {
        // Arrange
        List<Tournament> tournaments = [.. TournamentFactory.FakeMany(10)];
        ApplicationDbContext.Tournaments.AddRange(tournaments);

        await ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        Tournament existingTournament = tournaments[6];
        var query = new GetTournamentByIdQuery
        {
            Id = existingTournament.Id
        };

        // Act
        ErrorOr<TournamentDetailDto?> result = await _handler.HandleAsync(query, TestContext.Current.CancellationToken);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeNull();

        TournamentDetailDto dto = result.Value!;

        dto.Id.Should().Be(existingTournament.Id);
        dto.Name.Should().Be(existingTournament.Name);
    }
}
