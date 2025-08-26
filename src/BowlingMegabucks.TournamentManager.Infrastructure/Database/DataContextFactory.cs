using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database;

internal sealed class DataContextFactory
    : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<DataContextFactory>()
            .Build();

        string connectionString = configuration.GetConnectionString("TournamentManager")
            ?? throw new InvalidOperationException("Cannot get connection string TournamentManager");

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseMySql(connectionString,
            new MySqlServerVersion(new Version(11, 4, 7)), mySqlOptions => mySqlOptions.EnableRetryOnFailure(3));


        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
