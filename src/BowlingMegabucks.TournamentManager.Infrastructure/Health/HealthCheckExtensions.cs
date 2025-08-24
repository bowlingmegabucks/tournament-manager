using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Health;

internal static class HealthCheckExtensions
{
    private const string HealthCheckEndpoint = "/health";

    public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddCheck(
                name: "self",
                check: ()
                    => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy("Basic liveness check for Tournament Manager API."),
                tags: ["ready"]);

        builder.Services
            .AddHealthChecksUI(setup =>
            {
                setup.AddHealthCheckEndpoint("Tournament Manager Api", HealthCheckEndpoint);

                setup.MaximumHistoryEntriesPerEndpoint(50);
                setup.SetEvaluationTimeInSeconds(30); // Poll every 30 seconds (default is 10)
                setup.SetApiMaxActiveRequests(1);     // Limit concurrent API requests to avoid overload
                setup.SetHeaderText("Tournament Manager Health Dashboard");
            })
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

        app.MapHealthChecksUI(setup =>
        {
            setup.UIPath = "/health-ui";
            setup.PageTitle = "Tournament Manager Health Dashboard";
        });

        return app;
    }
}
