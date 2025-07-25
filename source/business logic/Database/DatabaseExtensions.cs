using BowlingMegabucks.TournamentManager.Database.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Database;

/// <summary>
/// Provides extension methods for configuring the database services.
/// </summary>
public static class DatabaseExtensions
{
    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<SlowQueryOptions>(config.GetSection("QueryPerformance"));

        services.AddScoped<SlowQueryInterceptor>();

        services.AddDbContext<DataContext>((sp, options) =>
            options.UseMySql(config.GetConnectionString("Default") ?? throw new InvalidOperationException("Cannot get connection string Default"),
            new MySqlServerVersion(new Version(11, 4, 7)), mySqlOptions => mySqlOptions.EnableRetryOnFailure(3))
            .AddInterceptors(sp.GetRequiredService<SlowQueryInterceptor>()));

        services.AddTransient<IDataContext, DataContext>();

        return services;
    }

    /// <summary>
    /// Applies pending migrations to the database.
    /// </summary>
    public static async Task ApplyMigrationsAsync(this IServiceScope scope)
    {
        ArgumentNullException.ThrowIfNull(scope);
        
        await using var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<DataContext>>();

        try
        {
            await dbContext.Database.MigrateAsync();
            DatabaseLogger.LogMigrationSuccess(logger);
        }
        catch (Exception ex)
        {
            DatabaseLogger.LogMigrationError(logger, ex);
            throw;
        }
    }
}

internal static partial class DatabaseLogger
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Database migration completed successfully.", EventName = "DatabaseMigrationSuccess")]
    public static partial void LogMigrationSuccess(ILogger<DataContext> logger);

    [LoggerMessage(Level = LogLevel.Error, Message = "An error occurred during database migration.", EventName = "DatabaseMigrationError")]
    public static partial void LogMigrationError(ILogger logger, Exception exception);
}
