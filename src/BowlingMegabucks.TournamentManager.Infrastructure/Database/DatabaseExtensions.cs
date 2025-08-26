using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database;

internal static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(config.GetConnectionString("TournamentManager") ?? throw new InvalidOperationException("Cannot get connection string TournamentManager"),
            new MySqlServerVersion(new Version(11, 4, 7)), mySqlOptions => mySqlOptions.EnableRetryOnFailure(3)));

        return services;
    }
}
