using BowlingMegabucks.TournamentManager.Infrastructure.Database.Interceptors;
using EntityFramework.Exceptions.MySQL.Pomelo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database;

internal static class DatabaseExtensions
{
    internal static readonly MySqlServerVersion s_mariaDbServerVersion = new(new Version(11, 4, 7));

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config, IWebHostEnvironment environment)
    {
        services.Configure<SlowQueryOptions>(config.GetSection("QueryPerformance"));
        services.AddScoped<SlowQueryInterceptor>();

        services.AddScoped<AuditInterceptor>();

        services.AddDbContextPool<ApplicationDbContext>((sp, options) => options
            .ConfigureDbContext(config, environment, sp),
            poolSize: 64);

        services.AddDbContextFactory<ApplicationDbContext>((sp, options) => options
            .ConfigureDbContext(config, environment, sp));

        return services;
    }

    private static void ConfigureDbContext(
        this DbContextOptionsBuilder options,
        IConfiguration config,
        IWebHostEnvironment environment,
        IServiceProvider serviceProvider)
    {
        options.UseMySql(
                config.GetConnectionString("TournamentManager")
                    ?? throw new InvalidOperationException("Cannot get connection string TournamentManager"),
                s_mariaDbServerVersion,
                mySqlOptions => mySqlOptions.EnableRetryOnFailure(3))
            .EnableSensitiveDataLogging(environment.IsDevelopment())
            .EnableDetailedErrors(environment.IsDevelopment())
            .AddInterceptors(
                serviceProvider.GetRequiredService<AuditInterceptor>(),
                serviceProvider.GetRequiredService<SlowQueryInterceptor>())
            .UseExceptionProcessor();
    }
}
