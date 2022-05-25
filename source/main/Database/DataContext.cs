using Microsoft.EntityFrameworkCore;
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
        
        optionsBuilder.UseLoggerFactory(_consoleLogger);
        optionsBuilder.EnableSensitiveDataLogging(true);
#else
        serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
#endif

        optionsBuilder.UseMySql(_connectionString, serverVersion);
    }

#if DEBUG
    private static readonly ILoggerFactory _consoleLogger = LoggerFactory.Create(builder => builder.AddConsole());
#endif

    bool IDataContext.Ping()
        => Database.CanConnect();

    void IDataContext.SaveChanges()
        => base.SaveChanges();

    async Task IDataContext.SaveChangesAsync()
        => await base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Entities.Tournament.Configuration());
        modelBuilder.ApplyConfiguration(new Entities.Division.Configuration());
        modelBuilder.ApplyConfiguration(new Entities.Squad.Configuration());
        modelBuilder.ApplyConfiguration(new Entities.TournamentSquad.Configuration());
        modelBuilder.ApplyConfiguration(new Entities.SweeperSquad.Configuration());
        modelBuilder.ApplyConfiguration(new Entities.SweeperDivision.Configuration());        
    }
        

    public DbSet<Entities.Tournament> Tournaments { get; set; } = null!;

    public DbSet<Entities.Division> Divisions { get; set; } = null!;

    public DbSet<Entities.TournamentSquad> Squads { get; set; } = null!;
}

internal interface IDataContext
{
    bool Ping();

    DbSet<Entities.Tournament> Tournaments { get; }

    DbSet<Entities.Division> Divisions { get; }

    DbSet<Entities.TournamentSquad> Squads { get; }

    void SaveChanges();

    Task SaveChangesAsync();
}