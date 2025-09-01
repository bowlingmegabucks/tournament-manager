using System.Net;
using System.Text.Json;
using BowlingMegabucks.TournamentManager.Api;
using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.FunctionalTests.Infrastructure;
using BowlingMegabucks.TournamentManager.Tests.Factories;
using BowlingMegabucks.TournamentManager.Tests.Infrastructure;

namespace BowlingMegabucks.TournamentManager.FunctionalTests.Tournaments;

public sealed class GetAllTournamentsFunctionalTests
    : BaseFunctionalTest
{
    public GetAllTournamentsFunctionalTests(TournamentManagerWebAppFactory<IApiAssemblyMarker> factory)
        : base(factory ?? throw new ArgumentNullException(nameof(factory)))
    {  }

    [Fact]
    public async Task GetAllTournaments_ShouldReturnOk_WhenTournamentsExist()
    {
        // Arrange
        IEnumerable<Tournament> tournaments = TournamentFactory.FakeMany(10);
        ApplicationDbContext.Tournaments.AddRange(tournaments);
        await ApplicationDbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var requestUri = new Uri("/api/v1/tournaments?page=1&pageSize=20", UriKind.Relative);

        // Act
        HttpResponseMessage response = await HttpClient.GetAsync(requestUri, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);;

        OffsetPaginationQueryResponse<TournamentSummary>? paginatedResponse =
            await response.Content.Serialize<OffsetPaginationQueryResponse<TournamentSummary>>(TestContext.Current.CancellationToken);

        paginatedResponse.Should().NotBeNull();
        paginatedResponse!.TotalItems.Should().Be(10);
        paginatedResponse.TotalPages.Should().Be(1);
        paginatedResponse.CurrentPage.Should().Be(1);
        paginatedResponse.PageSize.Should().Be(20);
        paginatedResponse.Items.Should().HaveCount(10);
        paginatedResponse.Items.Should().BeEquivalentTo(tournaments.Select(t => new TournamentSummary
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
    public async Task GetAllTournaments_ShouldReturnInternalServerError_WhenThereIsAnInternalServerError()
    {
        // Arrange - Create a factory with invalid database connection
        await using var factoryWithInvalidDb = new InvalidDatabaseWebAppFactory<IApiAssemblyMarker>();

        using HttpClient client = factoryWithInvalidDb.CreateClient();
        var requestUri = new Uri("/api/v1/tournaments?page=1&pageSize=20", UriKind.Relative);

        // Act
        HttpResponseMessage response = await client.GetAsync(requestUri, TestContext.Current.CancellationToken);

        // Assert
        await response.VerifyResponseWhenDatabaseFailsAsync();
    }
}
