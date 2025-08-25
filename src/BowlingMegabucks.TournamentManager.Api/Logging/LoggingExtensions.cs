using System.Globalization;
using Serilog;
using Serilog.Enrichers.Span;

namespace BowlingMegabucks.TournamentManager.Api.Logging;

internal static class LoggingExtensions
{
    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
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

        return builder;
    }

    public static WebApplication UseLogging(this WebApplication app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();
        app.UseSerilogRequestLogging();

        return app;
    }
}
