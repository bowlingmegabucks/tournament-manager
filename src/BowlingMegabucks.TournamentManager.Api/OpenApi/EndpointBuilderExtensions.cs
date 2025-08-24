using System.Net;
using Microsoft.OpenApi;

namespace BowlingMegabucks.TournamentManager.Api.OpenApi;

/// <summary>
/// Minimal OpenAPI convenience helpers for endpoints.
/// </summary>
internal static class EndpointBuilderExtensions
{
    /// <summary>
    /// Marks the endpoint as deprecated in the OpenAPI document.
    /// </summary>
    public static RouteHandlerBuilder Deprecated(this RouteHandlerBuilder builder)
        => builder.AddOpenApiOperationTransformer((operation, _, _) =>
        {
            operation.Deprecated = true;

            return Task.CompletedTask;
        });

    /// <summary>
    /// Adds standard error response descriptions (ProblemDetails implied).
    /// Call on endpoints that can return these statuses.
    /// </summary>
    public static RouteHandlerBuilder WithStandardErrorResponses(this RouteHandlerBuilder builder, params HttpStatusCode[] statusCodes)
        => builder.AddOpenApiOperationTransformer((operation, _, _) =>
        {
            operation.Responses ??= [];

            foreach (HttpStatusCode statusCode in statusCodes)
            {
                switch (statusCode)
                {
                    case HttpStatusCode.BadRequest:
                        Add(operation, "400", "Bad Request - validation or input error.");
                        break;
                    case HttpStatusCode.Unauthorized:
                        Add(operation, "401", "Unauthorized - authentication required or failed.");
                        break;
                    case HttpStatusCode.Forbidden:
                        Add(operation, "403", "Forbidden - authenticated but not permitted.");
                        break;
                    case HttpStatusCode.NotFound:
                        Add(operation, "404", "Not Found - resource does not exist.");
                        break;
                    case HttpStatusCode.Conflict:
                        Add(operation, "409", "Conflict - request conflicts with current state.");
                        break;
                    case HttpStatusCode.TooManyRequests:
                        Add(operation, "429", "Too Many Requests - rate limit exceeded.");
                        break;
                    case HttpStatusCode.InternalServerError:
                        Add(operation, "500", "Internal Server Error - unexpected failure.");
                        break;
                }
            }

            return Task.CompletedTask;

            static void Add(OpenApiOperation operation, string code, string description)
            {
                if (!operation.Responses!.ContainsKey(code))
                {
                    operation.Responses![code] = new OpenApiResponse()
                    {
                        Description = $"{description} Returns ProblemDetails.",
                    };
                }
            }
        });
}
