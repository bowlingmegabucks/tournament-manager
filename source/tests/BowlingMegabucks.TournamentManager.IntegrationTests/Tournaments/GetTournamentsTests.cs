using System.Net;
using System.Net.Http.Json;
using BowlingMegabucks.TournamentManager.Api.Tournaments.GetTournaments;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Tournaments;

public sealed class GetTournamentsTests
    : BaseIntegrationTests
{
    private readonly HttpClient _client;

    public GetTournamentsTests(ApiFactory factory)
        : base(factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTournaments_ShouldReturnOk_WhenEndpointIsCalled()
    {
        // Arrange
        using var request = new HttpRequestMessage(HttpMethod.Get, "/v1/tournaments");

        // Act
        var response = await _client.SendAsync(request, TestContext.Current.CancellationToken);

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
        var response = await _client.SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var tournaments = await response.Content.ReadFromJsonAsync<GetTournamentsResponse>(TestContext.Current.CancellationToken);
        tournaments.Should().NotBeNull();
        tournaments.TotalCount.Should().Be(10);
        tournaments.Tournaments.Should().HaveCount(10);
    }
}