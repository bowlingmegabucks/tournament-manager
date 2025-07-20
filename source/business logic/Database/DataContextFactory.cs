using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BowlingMegabucks.TournamentManager.Database;

/// <summary>
/// A factory class used by Entity Framework Core at design time to create instances of <see cref="DataContext"/>.
/// This is primarily used for migrations and other design-time operations.
/// </summary>
public class DataContextFactory
    : IDesignTimeDbContextFactory<DataContext>
{
    internal static MySqlServerVersion Version
        => new(new Version(11, 4, 7));

    internal static Action<MySqlDbContextOptionsBuilder> MySqlOptions
        => mySqlOptions => mySqlOptions.EnableRetryOnFailure(3);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
            ?? throw new InvalidOperationException("DB_CONNECTION_STRING environment variable is not set.");

        optionsBuilder.UseMySql(connectionString,
                Version,
                MySqlOptions);
        return new DataContext(optionsBuilder.Options);
    }
}