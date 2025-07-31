using System.Net;
using System.Net.Http.Json;
using BowlingMegabucks.TournamentManager.Api.Tournaments.GetTournament;
using BowlingMegabucks.TournamentManager.IntegrationTests.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Tournaments;

public sealed class GetTournamentTests
    : IntegrationTestFixture
{
    public GetTournamentTests(TournamentManagerWebAppFactory apiFactory)
        : base(apiFactory)
    { }

    [Fact]
    public async Task GetTournament_ShouldReturn404_WhenNoTournamentIsFound()
    {
        // Arrange
        var tournamentSeeds = TournamentEntityFactory.Bogus(5);
        await _dbContext.Tournaments.AddRangeAsync(tournamentSeeds, TestContext.Current.CancellationToken);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        using var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/tournaments/{TournamentId.New()}");

        // Act
        var response = await CreateClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>(TestContext.Current.CancellationToken);

        problemDetails.Should().NotBeNull();
        problemDetails!.Status.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTournament_ShouldReturnTournament_WhenTournamentExists()
    {
        // Arrange
        var tournamentSeeds = TournamentEntityFactory.Bogus(8);
        var tournamentSeed = TournamentEntityFactory.Bogus(3, 3, 3);
        await _dbContext.Tournaments.AddAsync(tournamentSeed, TestContext.Current.CancellationToken);
        await _dbContext.Tournaments.AddRangeAsync(tournamentSeeds, TestContext.Current.CancellationToken);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        using var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/tournaments/{tournamentSeed.Id}");

        // Act
        var response = await CreateClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var tournamentResponse = await response.Content.ReadFromJsonAsync<GetTournamentResponse>(TestContext.Current.CancellationToken);
        tournamentResponse!.Tournament.Should().NotBeNull();

        var tournament = tournamentResponse.Tournament;

        tournament.Should().NotBeNull();
        tournament.Id.Should().Be(tournamentSeed.Id);
        
        tournament.Divisions.Should().HaveCount(3);
        tournament.Squads.Should().HaveCount(3);
        tournament.Sweepers.Should().HaveCount(3);
    }
}