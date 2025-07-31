using System.Net;
using System.Net.Http.Json;
using BowlingMegabucks.TournamentManager.Api.Tournaments.GetTournaments;
using BowlingMegabucks.TournamentManager.IntegrationTests.Infrastructure;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Tournaments;

public sealed class GetTournamentsTests
    : IntegrationTestFixture
{
    public GetTournamentsTests(TournamentManagerWebAppFactory factory)
        : base(factory)
    { }

    [Fact]
    public async Task GetTournaments_ShouldReturnOk_WhenEndpointIsCalled()
    {
        // Arrange
        var tournamentSeeds = TournamentEntityFactory.Bogus(7);
        await _dbContext.Tournaments.AddRangeAsync(tournamentSeeds, TestContext.Current.CancellationToken);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        using var request = new HttpRequestMessage(HttpMethod.Get, "/v1/tournaments");

        // Act
        var response = await CreateClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.EnsureSuccessStatusCode();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetTournaments_ShouldReturnTournaments_WhenTournamentsExist()
    {
        // Arrange
        var tournamentSeeds = TournamentEntityFactory.Bogus(10);
        await _dbContext.Tournaments.AddRangeAsync(tournamentSeeds, TestContext.Current.CancellationToken);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        using var request = new HttpRequestMessage(HttpMethod.Get, "/v1/tournaments");

        // Act
        var response = await CreateClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var tournaments = await response.Content.ReadFromJsonAsync<GetTournamentsResponse>(TestContext.Current.CancellationToken);
        tournaments.Should().NotBeNull();
        tournaments.TotalCount.Should().Be(10);
        tournaments.Tournaments.Should().HaveCount(10);
    }
}