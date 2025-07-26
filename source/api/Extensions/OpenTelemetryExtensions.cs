using System.Globalization;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using BowlingMegabucks.TournamentManager.Api.Middleware;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers;

namespace BowlingMegabucks.TournamentManager.Api.Extensions;

internal static class OpenTelemetryExtensions
{
    public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, ILoggingBuilder logging, ConfigureHostBuilder host, IWebHostEnvironment environment)
    {
        host.UseSerilog((context, loggerConfig)
            => loggerConfig.ReadFrom.Configuration(context.Configuration)
                .WriteTo.Console(formatProvider: CultureInfo.CurrentCulture)
                .WriteTo.OpenTelemetry()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId());

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(environment.ApplicationName))
            .WithTracing(tracing => tracing
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                .AddSqlClientInstrumentation()
                .AddMySqlDataInstrumentation())
            .WithMetrics(metrics => metrics
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddRuntimeInstrumentation());

        logging.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;
        });

        if (environment.IsDevelopment())
        {
            services.AddOpenTelemetry().UseOtlpExporter();
        }
        else
        {
            services.AddOpenTelemetry().UseAzureMonitor();
        }

        return services;
    }

    public static WebApplication UseLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }
}