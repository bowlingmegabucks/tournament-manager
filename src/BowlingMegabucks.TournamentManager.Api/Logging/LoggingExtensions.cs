using Serilog;

namespace BowlingMegabucks.TournamentManager.Api.Logging;

internal static class LoggingExtensions
{
    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfig)
            => loggerConfig.ReadFrom.Configuration(context.Configuration));

        return builder;
    }

    public static WebApplication UseLogging(this WebApplication app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();
        app.UseSerilogRequestLogging();

        return app;
    }
}
