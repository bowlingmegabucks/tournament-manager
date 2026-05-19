using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BowlingMegabucks.TournamentManager.Database;

internal sealed class DataContextFactory
    : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        // Build configuration manually
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // EF runs this from the startup project dir
            .AddUserSecrets<DataContextFactory>() // Load user secrets
            .Build();

        var connectionString = configuration.GetConnectionString("Default");
        
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseMySql(connectionString,
            new MySqlServerVersion(new Version(11, 4, 7)), mySqlOptions => mySqlOptions.EnableRetryOnFailure(3));

        return new DataContext(optionsBuilder.Options);
    }
}