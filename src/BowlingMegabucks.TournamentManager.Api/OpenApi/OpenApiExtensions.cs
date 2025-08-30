using BowlingMegabucks.TournamentManager.Api.Versioning;
using Scalar.AspNetCore;

namespace BowlingMegabucks.TournamentManager.Api.OpenApi;

internal static class OpenApiExtensions
{
    public static WebApplicationBuilder AddOpenApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi(VersioningExtensions.Version1, options => options
            .AddDocumentTransformer<BasicOpenApiDocumentTransformer>()
            .AddSchemaTransformer<ExampleSchemaTransformer>());

        builder.Services.Configure<ScalarOptions>(options =>
        {
            options
                .WithTitle("Bowling Megabucks Tournament Manager API");
        });

        return builder;
    }

    public static WebApplication UseOpenApi(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference("docs", options => options.WithTestRequestButton(!app.Environment.IsProduction()).AddDocuments(VersioningExtensions.Version1));

        return app;
    }
}
