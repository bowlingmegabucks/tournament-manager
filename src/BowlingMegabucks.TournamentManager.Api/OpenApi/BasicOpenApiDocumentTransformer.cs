using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace BowlingMegabucks.TournamentManager.Api.OpenApi;

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
internal sealed class BasicOpenApiDocumentTransformer
    : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Info = new OpenApiInfo
        {
            Title = "Bowling Megabucks Tournament Manager API",
            Version = "1.0.0", // Informational only; versioned docs feature is planned separately.
            Description = "HTTP API for managing tournaments, registrations, bowlers, squads, scoring, and related resources.",
            Contact = new OpenApiContact
            {
                Name = "Support",
                Email = "northeastmegabucks@hotmail.com",
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
            },
        };

        return Task.CompletedTask;
    }
}
