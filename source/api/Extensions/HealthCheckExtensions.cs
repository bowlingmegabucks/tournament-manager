using System.Text.Json;
using Azure.Identity;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace BowlingMegabucks.TournamentManager.Api.Extensions;

#pragma warning disable CA1861 // Suppressing CA1861 because the constant array allocations are small and acceptable for health check configuration.
internal static class HealthCheckExtensions
{
    public static IServiceCollection AddApiHealthChecks(this IServiceCollection services, IConfigurationManager config)
    {
        var healthChecks = services.AddHealthChecks();

        var keyVaultUrl = config.GetValue<string>("KEYVAULT_URL");

        if (!string.IsNullOrEmpty(keyVaultUrl))
        {
            var uri = new Uri(keyVaultUrl);
            var credential = new DefaultAzureCredential();

            config.AddAzureKeyVault(uri, credential);

            healthChecks.AddAzureKeyVault(uri, credential, options =>
            {
                options.AddSecret("ConnectionStrings--Default");
                options.AddSecret("Authentication--ApiKey");
                options.AddSecret("EncryptionKey");
            }, name: "Azure Key Vault", tags: new[] { "secrets", "azure" });
        }

        healthChecks.AddMySql(config.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Connection string 'Default' is required for MySQL health check configuration."),
            name: "MySQL",
            tags: new[] { "db", "mysql" });

        var appInsightsConnectionString = config["APPLICATIONINSIGHTS_CONNECTION_STRING"];

        if (!string.IsNullOrWhiteSpace(appInsightsConnectionString))
        {
            var instrumentationKey = string.Empty;
            var match = Regex.Match(appInsightsConnectionString, @"InstrumentationKey=([^;]+)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                instrumentationKey = match.Groups[1].Value.Trim();
            }

            healthChecks.AddAzureApplicationInsights(instrumentationKey,
                name: "Application Insights",
                tags: new[] { "monitoring", "azure" });
        }

        return services;
    }

    public static IApplicationBuilder MapApiHealthChecks(this IApplicationBuilder app)
    { 
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(e => new
                    {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        description = e.Value.Description,
                        duration = e.Value.Duration.TotalMilliseconds,
                        data = e.Value.Data
                    }),
                    totalDuration = report.TotalDuration.TotalMilliseconds
                });
                await context.Response.WriteAsync(result);
            }
        });

        return app;
    }
}
#pragma warning restore CA1861