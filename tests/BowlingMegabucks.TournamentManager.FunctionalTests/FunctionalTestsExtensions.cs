using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace BowlingMegabucks.TournamentManager.FunctionalTests;

internal static class FunctionalTestsExtensions
{
    public static async Task VerifyResponseWhenDatabaseFailsAsync(this HttpResponseMessage response)
    {
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

        string responseContent = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        responseContent.Should().NotBeEmpty();

        ProblemDetails? problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseContent, s_jsonSerializerOptions);

        problemDetails.Should().NotBeNull();
        problemDetails!.Title.Should().Be("An unexpected error occurred.");
        problemDetails.Status.Should().Be(500);
        problemDetails.Type.Should().Be("RetryLimitExceededException");
        problemDetails.Extensions.Should().ContainKey("traceId");
        problemDetails.Extensions["traceId"].Should().NotBeNull();
        problemDetails.Extensions.Should().ContainKey("requestId");
        problemDetails.Extensions["requestId"].Should().NotBeNull();
    }

    public static async Task VerifyNotFoundProblemDetails(this HttpResponseMessage response, Uri requestUri)
    {
        ProblemDetails? problemDetails = await response.Content.Serialize<ProblemDetails>(TestContext.Current.CancellationToken);

        problemDetails.Should().NotBeNull();
        problemDetails!.Title.Should().Be("Resource not found");
        problemDetails.Status.Should().Be(404);
        problemDetails.Type.Should().Be("https://tools.ietf.org/html/rfc9110#section-15.5.5");
        problemDetails.Instance.Should().Be($"{response.RequestMessage!.Method} /{requestUri}");
        problemDetails.Extensions.Should().ContainKey("traceId");
        problemDetails.Extensions["traceId"].Should().NotBeNull();
        problemDetails.Extensions.Should().ContainKey("requestId");
        problemDetails.Extensions["requestId"].Should().NotBeNull();
    }

    public static async Task VerifyBadRequestProblemDetails_InvalidId(this HttpResponseMessage response, Uri requestUri)
    {
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        ProblemDetails? problemDetails = await response.Content.Serialize<ProblemDetails>(TestContext.Current.CancellationToken);

        problemDetails.Should().NotBeNull();
        problemDetails!.Title.Should().Be("Invalid ID format");
        problemDetails.Status.Should().Be(400);
        problemDetails.Type.Should().Be("https://tools.ietf.org/html/rfc9110#section-15.5.1");
        problemDetails.Instance.Should().Be($"{response.RequestMessage!.Method} /{requestUri}");
        problemDetails.Extensions.Should().ContainKey("errors");
        problemDetails.Extensions["traceId"].Should().NotBeNull();
        problemDetails.Extensions.Should().ContainKey("requestId");
        problemDetails.Extensions["requestId"].Should().NotBeNull();
    }

    private static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static async Task<T> Serialize<T>(this HttpContent content, CancellationToken cancellationToken)
        => JsonSerializer.Deserialize<T>(await content.ReadAsStringAsync(cancellationToken), s_jsonSerializerOptions)!;
}
