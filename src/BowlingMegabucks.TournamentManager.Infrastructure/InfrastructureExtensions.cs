using System.Threading.Tasks;
using BowlingMegabucks.TournamentManager.Application.Tournaments;
using BowlingMegabucks.TournamentManager.Infrastructure.Database;
using BowlingMegabucks.TournamentManager.Infrastructure.Health;
using BowlingMegabucks.TournamentManager.Infrastructure.Middleware;
using BowlingMegabucks.TournamentManager.Infrastructure.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BowlingMegabucks.TournamentManager.Infrastructure;

/// <summary>
/// Provides extension methods for configuring infrastructure services and middleware.
/// </summary>
public static class InfrastructureExtensions
{
    /// <summary>
    /// Adds infrastructure services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to register services with.</param>
    /// <param name="config">The application configuration.</param>
    /// <param name="environment">The web hosting environment.</param>
    /// <returns>The same service collection for chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration config, IWebHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(environment);

        services.AddHealthChecks();

        services
            .AddErrorHandling()
            .AddDatabase(config, environment)
            .AddQueries();

        return services;
    }

    /// <summary>
    /// Configures the application to use infrastructure middleware and routing.
    /// </summary>
    /// <param name="app">The web application to configure.</param>
    /// <returns>The same web application for chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="app"/> is null.</exception>
    public static async Task<WebApplication> UseInfrastructure(
        this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        if (app.Environment.IsDevelopment())
        {
            await using AsyncServiceScope scope = app.Services.CreateAsyncScope();
            ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            await dbContext.Database.MigrateAsync(app.Lifetime.ApplicationStarted);
        }

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
