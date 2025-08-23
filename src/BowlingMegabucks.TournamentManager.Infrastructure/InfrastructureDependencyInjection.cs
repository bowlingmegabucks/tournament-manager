using BowlingMegabucks.TournamentManager.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static WebApplicationBuilder AddInfrastructureServices(
        this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddErrorHandling();

        return builder;
    }

    public static WebApplication UseInfrastructure(
        this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseExceptionHandler();

        return app;
    }

    private static IServiceCollection AddErrorHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
