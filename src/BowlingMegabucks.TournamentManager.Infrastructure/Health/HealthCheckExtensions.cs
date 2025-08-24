using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Health;

internal static class HealthCheckExtensions
{
    private const string HealthCheckEndpoint = "/health";
    internal const string ReadyTag = "ready";
    internal const string LiveTag = "live";

    public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddCheck<SelfHealthCheck>(
                name: "self",
                tags: [ReadyTag, LiveTag]);

        builder.Services
            .AddHealthChecksUI()
            .AddInMemoryStorage();

        return builder;
    }

    public static WebApplication MapHealthCheckRoute(this WebApplication app)
    {
        app.MapHealthChecks(HealthCheckEndpoint, new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });

        app.MapHealthChecks($"{HealthCheckEndpoint}/{ReadyTag}", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains(ReadyTag),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });

        app.MapHealthChecks($"{HealthCheckEndpoint}/{LiveTag}", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains(LiveTag),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });

        app.MapHealthChecksUI(setup =>
        {
            setup.UIPath = "/health-ui";
            setup.PageTitle = "Tournament Manager Health Dashboard";
        });

        app.MapGet("/healthz", async (HealthCheckService healthChecks, CancellationToken cancellationToken) =>
        {
            HealthReport report = await healthChecks.CheckHealthAsync(cancellationToken);

            var details = report.Entries.ToDictionary(
                entry => entry.Key,
                entry => new HealthCheckDetail(entry.Value.Status.ToString(), entry.Value.Description, entry.Value.Duration), StringComparer.OrdinalIgnoreCase);

            var result = new HealthCheckResponse(report.Status.ToString(), details);

            return Results.Ok(result);
        })
        .WithTags("Health")
        .Produces<HealthCheckResponse>(StatusCodes.Status200OK)
        .Produces<HealthCheckResponse>(StatusCodes.Status500InternalServerError)
        .WithName("HealthCheck");

        return app;
    }
}
