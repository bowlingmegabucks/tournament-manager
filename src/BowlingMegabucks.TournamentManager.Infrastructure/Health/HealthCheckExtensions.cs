using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Health;

internal static class HealthCheckExtensions
{
    private const string HealthCheckEndpoint = "/health";
    private const string ReadyTag = "ready";
    private const string LiveTag = "live";

    public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddCheck(
                name: "self",
                check: ()
                    => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy("Basic liveness check for Tournament Manager API."),
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

        app.MapHealthChecks($"{HealthCheckEndpoint}/ready", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("ready"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });

        app.MapHealthChecks($"{HealthCheckEndpoint}/live", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("live"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });

        app.MapHealthChecksUI(setup =>
        {
            setup.UIPath = "/health-ui";
            setup.PageTitle = "Tournament Manager Health Dashboard";
        });

        return app;
    }
}
