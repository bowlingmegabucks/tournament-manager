using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database;

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
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
            DatabaseExtensions.s_mariaDbServerVersion, mySqlOptions => mySqlOptions.EnableRetryOnFailure(3));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
