using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BowlingMegabucks.TournamentManager.Database;

/// <summary>
/// A factory class used by Entity Framework Core at design time to create instances of <see cref="DataContext"/>.
/// This is primarily used for migrations and other design-time operations.
/// </summary>
internal class DataContextFactory
    : IDesignTimeDbContextFactory<DataContext>
{
    internal static MySqlServerVersion Version
        => new(new Version(11, 4, 7));

    internal static Action<MySqlDbContextOptionsBuilder> MySqlOptions
        => mySqlOptions => mySqlOptions.EnableRetryOnFailure(3);

    /// <summary>
    /// Creates a new instance of <see cref="DataContext"/> using the specified arguments.
    /// This method is used by Entity Framework Core at design time.
    /// </summary>
    /// <param name="args">An array of arguments passed by the design-time tools. Not used in this implementation.</param>
    /// <returns>A new instance of <see cref="DataContext"/> configured with the connection string and MySQL options.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the "DB_CONNECTION_STRING" environment variable is not set.</exception>
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