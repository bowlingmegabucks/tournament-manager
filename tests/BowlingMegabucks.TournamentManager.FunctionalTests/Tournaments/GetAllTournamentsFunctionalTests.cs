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

public sealed partial class GetAllTournamentsFunctionalTests
    : BaseFunctionalTest
{
    private static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public GetAllTournamentsFunctionalTests(TournamentManagerWebAppFactory<IApiAssemblyMarker> factory)
        : base(factory ?? throw new ArgumentNullException(nameof(factory)))
    {
    }

    [Fact]
    public async Task GetAllTournaments_ShouldReturnOkResponse_WhenTournamentsExist()
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
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        string responseContent = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

        OffsetPaginationQueryResponse<TournamentSummary>? paginatedResponse = JsonSerializer.Deserialize<OffsetPaginationQueryResponse<TournamentSummary>>(responseContent, s_jsonSerializerOptions);

        paginatedResponse.Should().NotBeNull();
        paginatedResponse!.TotalItems.Should().Be(10);
        paginatedResponse.TotalPages.Should().Be(1);
        paginatedResponse.CurrentPage.Should().Be(1);
        paginatedResponse.PageSize.Should().Be(20);
        paginatedResponse.Items.Should().HaveCount(10);
        paginatedResponse.Items.Should().BeEquivalentTo(tournaments.Select(t => new TournamentSummary
        {
            Id = t.Id.Value,
            Name = t.Name,
            StartDate = t.TournamentDates.StartDate,
            EndDate = t.TournamentDates.EndDate,
            EntryFee = t.EntryFee,
            BowlingCenter = t.BowlingCenter,
            Completed = t.Completed
        }));
    }

    [Fact]
    public async Task GetAllTournaments_ShouldReturn500_WhenThereIsAnInternalServerError()
    {
        // Arrange - Create a factory with invalid database connection
        await using var factoryWithInvalidDb = new InvalidDatabaseWebAppFactory<IApiAssemblyMarker>();

        using HttpClient client = factoryWithInvalidDb.CreateClient();
        var requestUri = new Uri("/api/v1/tournaments?page=1&pageSize=20", UriKind.Relative);

        // Act
        HttpResponseMessage response = await client.GetAsync(requestUri, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

        string responseContent = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        responseContent.Should().NotBeEmpty();

        // Verify it returns a ProblemDetails response for 500 errors
        // The actual error will be about database connection/retry failures
        responseContent.Should().ContainAny("RetryLimitExceededException", "database", "error", "status\":500");
    }
}
