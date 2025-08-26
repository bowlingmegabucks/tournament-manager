using BowlingMegabucks.TournamentManager.Infrastructure.Database.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database;

internal static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<SlowQueryOptions>(config.GetSection("QueryPerformance"));
        services.AddScoped<SlowQueryInterceptor>();

        services.AddScoped<AuditInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) => options
            .UseMySql(config.GetConnectionString("TournamentManager") ?? throw new InvalidOperationException("Cannot get connection string TournamentManager"),
                new MySqlServerVersion(new Version(11, 4, 7)), mySqlOptions => mySqlOptions.EnableRetryOnFailure(3))
            .AddInterceptors(
                sp.GetRequiredService<AuditInterceptor>(),
                sp.GetRequiredService<SlowQueryInterceptor>()));

        return services;
    }
}
