using System.Net;
using BowlingMegabucks.TournamentManager.Api;
using BowlingMegabucks.TournamentManager.Contracts;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.FunctionalTests.Infrastructure;
using BowlingMegabucks.TournamentManager.Tests.Factories;
using BowlingMegabucks.TournamentManager.Tests.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BowlingMegabucks.TournamentManager.FunctionalTests.Tournaments;

public sealed class GetTournamentByIdFunctionalTests
    : BaseFunctionalTest
{
    public GetTournamentByIdFunctionalTests(TournamentManagerWebAppFactory<IApiAssemblyMarker> factory)
        : base(factory ?? throw new ArgumentNullException(nameof(factory)))
    { }

    [Fact]
    public async Task GetTournamentById_ShouldReturnOk_WhenTournamentExists()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(10).ToList();
        ApplicationDbContext.Tournaments.AddRange(tournaments);

        await ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var requestUri = new Uri($"api/v1/tournaments/{tournaments.First().Id}", UriKind.Relative);

        // Act
        HttpResponseMessage response = await HttpClient.GetAsync(requestUri, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        ApiResponse<TournamentDetail> apiResponse = await response.Content.Serialize<ApiResponse<TournamentDetail>>(TestContext.Current.CancellationToken);

        apiResponse.Should().NotBeNull();
        apiResponse.Data.Should().NotBeNull();

        apiResponse.Data.Id.Should().Be(tournaments.First().Id);
        apiResponse.Data.Name.Should().Be(tournaments.First().Name);
        apiResponse.Data.StartDate.Should().Be(tournaments.First().TournamentDates.StartDate);
        apiResponse.Data.EndDate.Should().Be(tournaments.First().TournamentDates.EndDate);
        apiResponse.Data.EntryFee.Should().Be(tournaments.First().EntryFee);
        apiResponse.Data.BowlingCenter.Should().Be(tournaments.First().BowlingCenter);
        apiResponse.Data.Games.Should().Be(tournaments.First().Games);
        apiResponse.Data.Completed.Should().Be(tournaments.First().Completed);
        apiResponse.Data.CashRatio.Should().Be(tournaments.First().CashRatio.Value);
        apiResponse.Data.FinalsRatio.Should().Be(tournaments.First().FinalsRatio.Value);
        apiResponse.Data.SuperSweeperCashRatio.Should().Be(tournaments.First().SuperSweeperCashRatio.Value);
    }

    [Fact]
    public async Task GetTournamentById_ShouldReturnBadRequest_WhenTournamentIdIsNotValid()
    {
        // Arrange
        var requestUri = new Uri("api/v1/tournaments/invalid-id", UriKind.Relative);

        // Act
        HttpResponseMessage response = await HttpClient.GetAsync(requestUri, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await response.VerifyBadRequestProblemDetails_InvalidId(requestUri);
    }

    [Fact]
    public async Task GetTournamentById_ShouldReturnNotFound_WhenTournamentDoesNotExist()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(10).ToList();
        ApplicationDbContext.Tournaments.AddRange(tournaments);

        await ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var requestUri = new Uri($"api/v1/tournaments/{Guid.NewGuid()}", UriKind.Relative);

        // Act
        HttpResponseMessage response = await HttpClient.GetAsync(requestUri, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        await response.VerifyNotFoundProblemDetails(requestUri);
    }

    [Fact]
    public async Task GetTournamentById_ShouldReturnInternalServerError_WhenThereIsAnInternalServerError()
    {
        // Arrange - Create a factory with invalid database connection
        await using var factoryWithInvalidDb = new InvalidDatabaseWebAppFactory<IApiAssemblyMarker>();

        using HttpClient client = factoryWithInvalidDb.CreateClient();
        var requestUri = new Uri($"/api/v1/tournaments/{TournamentId.New()}", UriKind.Relative);

        // Act
        HttpResponseMessage response = await client.GetAsync(requestUri, TestContext.Current.CancellationToken);

        // Assert
        await response.VerifyResponseWhenDatabaseFailsAsync();
    }
}
