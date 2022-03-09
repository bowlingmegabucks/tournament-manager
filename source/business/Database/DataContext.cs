using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NewEnglandClassic.Database;

internal class DataContext : DbContext, IDataContext
{
    private readonly string _connectionString;
    internal DataContext(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("tournament-manager");
    }

#if DEBUG
    /// <summary>
    /// Migration Constructor
    /// </summary>
    public DataContext()
    {
        _connectionString = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddUserSecrets<DataContext>()
            .Build().GetConnectionString("tournament-manager");
    }
#endif

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString, options => options.EnableRetryOnFailure(3, new TimeSpan(0, 0, 1), new List<int>()));

#if DEBUG
        optionsBuilder.UseLoggerFactory(ConsoleLogger);
        optionsBuilder.EnableSensitiveDataLogging(true);
#endif
    }

#if DEBUG
    private static readonly ILoggerFactory ConsoleLogger = LoggerFactory.Create(builder => builder.AddConsole());
#endif

    bool IDataContext.Ping()
        => Database.CanConnect();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.Entity<Entities.Tournament>(builder =>
        {
            builder.Property(tournament => tournament.Start).HasConversion<DateOnlyConverter, DateOnlyComparer>();
            builder.Property(tournament => tournament.End).HasConversion<DateOnlyConverter, DateOnlyComparer>();
        });

    public DbSet<Entities.Tournament> Tournaments { get; set; } = null!;
}

internal interface IDataContext
{
    bool Ping();

    DbSet<Entities.Tournament> Tournaments { get; }
}