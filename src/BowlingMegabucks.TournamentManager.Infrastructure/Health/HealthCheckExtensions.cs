using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Health;

internal static class HealthCheckExtensions
{
    private const string HealthCheckEndpoint = "/health";
    internal const string ReadyTag = "ready";
    internal const string LiveTag = "live";

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration config)
    {
        services.AddHealthChecks()
            .AddCheck<SelfHealthCheck>(
                name: "self",
                tags: [LiveTag])
            .AddMySql(
                connectionString: config.GetConnectionString("TournamentManager")
                    ?? throw new InvalidOperationException("Connection string 'TournamentManager' not found."),
                name: "tournament-manager-db",
                tags: [ReadyTag, "database"]);

        services
            .AddHealthChecksUI()
            .AddInMemoryStorage();

        return services;
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

            IEnumerable<HealthCheckDetail> details = report.Entries.Select(entry =>
                new HealthCheckDetail
                {
                    Name = entry.Key,
                    Status = entry.Value.Status.ToString(),
                    Description = entry.Value.Description,
                    Duration = entry.Value.Duration,
                });

            var result = new HealthCheckResponse
            {
                Status = report.Status.ToString(),
                Details = details,
            };

            return Results.Ok(result);
        })
        .WithTags("Health")
        .Produces<HealthCheckResponse>(StatusCodes.Status200OK)
        .Produces<HealthCheckResponse>(StatusCodes.Status500InternalServerError)
        .WithName("HealthCheck");

        return app;
    }
}
