using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BowlingMegabucks.TournamentManager.Database;

/// <summary>
/// 
/// </summary>
public sealed class DataContextFactory
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
    DataContext IDesignTimeDbContextFactory<DataContext>.CreateDbContext(string[] args)
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