using System.Globalization;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using BowlingMegabucks.TournamentManager.Api.Middleware;
using BowlingMegabucks.TournamentManager.Tournaments;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.AspNetCore;
using Serilog.Enrichers.Span;

namespace BowlingMegabucks.TournamentManager.Api.Extensions;

internal static class OpenTelemetryExtensions
{
    public static WebApplicationBuilder AddOpenTelemetry(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfig)
            => loggerConfig.ReadFrom.Configuration(context.Configuration)
                .WriteTo.Console(formatProvider: CultureInfo.CurrentCulture)
                .WriteTo.OpenTelemetry(options => options.ResourceAttributes.Add("service.name", builder.Environment.ApplicationName))

                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProcessName()

                .Enrich.WithThreadId()
                .Enrich.WithSpan()

                .Enrich.WithClientIp()
                .Enrich.WithRequestHeader("User-Agent")

                .Enrich.WithCorrelationId());

        var otel = builder.Services.AddOpenTelemetry()
            .WithTracing(tracing => tracing
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddEntityFrameworkCoreInstrumentation( options => options.SetDbStatementForText = !builder.Environment.IsProduction())
                .AddSqlClientInstrumentation()
                .AddMySqlDataInstrumentation(o => o.EnableConnectionLevelAttributes = !builder.Environment.IsProduction())
                .AddSource(TournamentsTelemetry.ActivitySourceName))
            .WithMetrics(metrics => metrics
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddRuntimeInstrumentation());

        builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;
        });

        if (builder.Environment.IsDevelopment())
        {
            otel.UseOtlpExporter();
        }
        else
        {
            otel.UseAzureMonitor();
        }

        return builder;
    }

    public static WebApplication UseLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }
}