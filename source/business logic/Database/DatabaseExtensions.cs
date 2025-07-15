using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Database;
internal static class DatabaseExtensions
{
    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseMySql(config.GetConnectionString("tournament-manager-db") ?? throw new InvalidOperationException("Cannot get connection string tournament-manager-db"),
            new MySqlServerVersion(new Version(10, 3, 35)), mySqlOptions => mySqlOptions.EnableRetryOnFailure(3)));

        services.AddTransient<IDataContext, DataContext>();

        return services;
    }
}
