using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;

namespace NortheastMegabuck.Database;

internal class DataContext : DbContext, IDataContext
{
    private readonly string _connectionString;
    internal DataContext(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("tournament-manager-db") ?? throw new ConfigurationErrorsException("Cannot get connection string");
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
            .Build().GetConnectionString("tournament-manager-migration") ?? throw new ConfigurationErrorsException("Cannot get connection string");
    }
#endif

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serverVersion = new MySqlServerVersion(new Version(10, 3, 35));

#if DEBUG
        optionsBuilder.UseLoggerFactory(_consoleLogger);
        optionsBuilder.EnableSensitiveDataLogging(true);
#endif

        optionsBuilder.UseMySql(_connectionString, serverVersion, options => options.EnableRetryOnFailure(3));
    }

#if DEBUG
    private static readonly ILoggerFactory _consoleLogger = LoggerFactory.Create(builder => builder.AddConsole());
#endif

    bool IDataContext.Ping()
        => Database.CanConnect();

    void IDataContext.SaveChanges()
        => base.SaveChanges();

    async Task IDataContext.SaveChangesAsync(CancellationToken cancellationToken)
        => await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Entities.Tournament.Configuration());
        modelBuilder.ApplyConfiguration(new Entities.Division.Configuration());
        modelBuilder.ApplyConfiguration(new Entities.Squad.Configuration());
        modelBuilder.ApplyConfiguration(new Entities.TournamentSquad.Configuration());
        modelBuilder.ApplyConfiguration(new Entities.SweeperSquad.Configuration());
        modelBuilder.ApplyConfiguration(new Entities.SweeperDivision.Configuration());
        modelBuilder.ApplyConfiguration(new Entities.Registration.Configuration());
        modelBuilder.ApplyConfiguration(new Entities.SquadRegistration.Configuration());

        modelBuilder.ApplyConfiguration(new Entities.Bowler.Configuration());

        modelBuilder.ApplyConfiguration(new Entities.SquadScore.Configuration());
    }
        

    public DbSet<Entities.Tournament> Tournaments { get; set; } = null!;

    public DbSet<Entities.Division> Divisions { get; set; } = null!;

    public DbSet<Entities.TournamentSquad> Squads { get; set; } = null!;

    public DbSet<Entities.SweeperSquad> Sweepers { get; set; } = null!;

    public DbSet<Entities.Bowler> Bowlers { get; set; } = null!;

    public DbSet<Entities.Registration> Registrations { get; set; } = null!;

    public DbSet<Entities.SquadScore> SquadScores { get; set; } = null!;
}

internal interface IDataContext
{
    bool Ping();

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    DbSet<Entities.Tournament> Tournaments { get; }

    DbSet<Entities.Division> Divisions { get; }

    DbSet<Entities.TournamentSquad> Squads { get; }

    DbSet<Entities.SweeperSquad> Sweepers { get; }

    DbSet<Entities.Bowler> Bowlers { get; }

    DbSet<Entities.Registration> Registrations { get; }

    DbSet<Entities.SquadScore> SquadScores { get; }

    void SaveChanges();

    Task SaveChangesAsync(CancellationToken cancellationToken);
}