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
            .Build().GetConnectionString("tournament-manager-migration");
    }
#endif

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        MySqlServerVersion serverVersion;

#if DEBUG
        serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
        
        optionsBuilder.UseLoggerFactory(ConsoleLogger);
        optionsBuilder.EnableSensitiveDataLogging(true);
#else
        serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
#endif

        optionsBuilder.UseMySql(_connectionString, serverVersion);
    }

#if DEBUG
    private static readonly ILoggerFactory ConsoleLogger = LoggerFactory.Create(builder => builder.AddConsole());
#endif

    bool IDataContext.Ping()
        => Database.CanConnect();

    void IDataContext.SaveChanges()
        => base.SaveChanges();

    async Task IDataContext.SaveChangesAsync()
        => await base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.Tournament>(builder =>
        {
            builder.Property(tournament => tournament.Start).HasConversion<DateOnlyConverter, DateOnlyComparer>();
            builder.Property(tournament => tournament.End).HasConversion<DateOnlyConverter, DateOnlyComparer>();
        });

        modelBuilder.ApplyConfiguration(new Entities.Division.Configuration());
    }
        

    public DbSet<Entities.Tournament> Tournaments { get; set; } = null!;

    public DbSet<Entities.Division> Divisions { get; set; } = null!;    
}

internal interface IDataContext
{
    bool Ping();

    DbSet<Entities.Tournament> Tournaments { get; }

    void SaveChanges();

    Task SaveChangesAsync();
}