using System.Text.Json;
using System.Text.RegularExpressions;
using Azure.Identity;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace BowlingMegabucks.TournamentManager.Api.Extensions;

#pragma warning disable CA1861 // Suppressing CA1861 because the constant array allocations are small and acceptable for health check configuration.
internal static partial class HealthCheckExtensions
{
    public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
    {
        var healthChecks = builder.Services.AddHealthChecks();

        var keyVaultUrl = builder.Configuration.GetValue<string>("KEYVAULT_URL");

        if (!string.IsNullOrEmpty(keyVaultUrl))
        {
            var uri = new Uri(keyVaultUrl);
            var credential = new DefaultAzureCredential();

            builder.Configuration.AddAzureKeyVault(uri, credential);

            healthChecks.AddAzureKeyVault(uri, credential, options =>
            {
                options.AddSecret("ConnectionStrings--Default");
                options.AddSecret("Authentication--ApiKey");
                options.AddSecret("EncryptionKey");
            }, name: "Azure Key Vault", tags: new[] { "secrets", "azure" });
        }

        healthChecks.AddMySql(builder.Configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Connection string 'Default' is required for MySQL health check configuration."),
            name: "MySQL",
            tags: new[] { "db", "mysql" });

        var appInsightsConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];

        if (!string.IsNullOrWhiteSpace(appInsightsConnectionString))
        {
            var instrumentationKey = string.Empty;
            var match = InstrumentationKeyRegex().Match(appInsightsConnectionString);
            if (match.Success)
            {
                instrumentationKey = match.Groups[1].Value.Trim();
            }

            healthChecks.AddAzureApplicationInsights(instrumentationKey,
                name: "Application Insights",
                tags: new[] { "monitoring", "azure" });
        }

        return builder;
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

    [GeneratedRegex(@"InstrumentationKey=([^;]+)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex InstrumentationKeyRegex();
}
#pragma warning restore CA1861