using System.Globalization;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using BowlingMegabucks.TournamentManager.Api.Middleware;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.AspNetCore;

namespace BowlingMegabucks.TournamentManager.Api.Extensions;

internal static class OpenTelemetryExtensions
{
    public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, ILoggingBuilder logging, ConfigureHostBuilder host, IWebHostEnvironment environment)
    {
        host.UseSerilog((context, loggerConfig)
            => loggerConfig.ReadFrom.Configuration(context.Configuration)
                .WriteTo.Console(formatProvider: CultureInfo.CurrentCulture)
                .WriteTo.OpenTelemetry(options => options.ResourceAttributes.Add("service.name", environment.ApplicationName))

                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProcessName()

                .Enrich.WithThreadId()

                .Enrich.WithClientIp()
                .Enrich.WithRequestHeader("User-Agent")

                .Enrich.WithCorrelationId());

        services.AddOpenTelemetry()
            .WithTracing(tracing => tracing
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddEntityFrameworkCoreInstrumentation( options => options.SetDbStatementForText = true)
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