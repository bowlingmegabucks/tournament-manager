using BowlingMegabucks.TournamentManager.Application.Tournaments;
using BowlingMegabucks.TournamentManager.Infrastructure.Database;
using BowlingMegabucks.TournamentManager.Infrastructure.Health;
using BowlingMegabucks.TournamentManager.Infrastructure.Middleware;
using BowlingMegabucks.TournamentManager.Infrastructure.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Infrastructure;

/// <summary>
/// Provides extension methods for configuring infrastructure services and middleware.
/// </summary>
public static class InfrastructureExtensions
{
    /// <summary>
    /// Adds infrastructure services to the specified <see cref="WebApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder">The web application builder to configure.</param>
    /// <returns>The configured web application builder.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="builder"/> is null.</exception>
    public static WebApplicationBuilder AddInfrastructureServices(
        this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.AddHealthChecks();

        builder.Services
            .AddErrorHandling()
            .AddDatabase(builder.Configuration, builder.Environment)
            .AddQueries();

        return builder;
    }

    /// <summary>
    /// Configures the application to use infrastructure middleware.
    /// </summary>
    /// <param name="app">The web application to configure.</param>
    /// <returns>The configured web application.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="app"/> is null.</exception>
    public static WebApplication UseInfrastructure(
        this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseExceptionHandler();

        app.MapHealthCheckRoute();

        return app;
    }

    private static IServiceCollection AddErrorHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails(options =>
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
                context.ProblemDetails.Extensions.Add("requestId", context.HttpContext.TraceIdentifier);
            });

        return services;
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<ITournamentQueries, TournamentQueries>();

        return services;
    }
}
