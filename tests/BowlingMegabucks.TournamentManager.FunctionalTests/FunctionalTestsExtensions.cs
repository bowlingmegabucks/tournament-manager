using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace BowlingMegabucks.TournamentManager.FunctionalTests;

internal static class FunctionalTestsExtensions
{
    public static async Task VerifyResponseWhenDatabaseFailsAsync(this HttpResponseMessage response, JsonSerializerOptions jsonSerializerOptions)
    {
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

        string responseContent = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        responseContent.Should().NotBeEmpty();

        ProblemDetails? problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseContent, jsonSerializerOptions);

        problemDetails.Should().NotBeNull();
        problemDetails!.Title.Should().Be("An unexpected error occurred.");
        problemDetails.Status.Should().Be(500);
        problemDetails.Type.Should().Be("RetryLimitExceededException");
        problemDetails.Extensions.Should().ContainKey("traceId");
        problemDetails.Extensions["traceId"].Should().NotBeNull();
        problemDetails.Extensions.Should().ContainKey("requestId");
        problemDetails.Extensions["requestId"].Should().NotBeNull();
    }
}
