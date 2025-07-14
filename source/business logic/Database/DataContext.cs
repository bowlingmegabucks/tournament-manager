using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartEnum.EFCore;

#if DEBUG
using Microsoft.Extensions.Logging;
#endif

namespace NortheastMegabuck.Database;

internal class DataContext 
    : DbContext, IDataContext
{

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    { }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ConfigureSmartEnum();

        base.ConfigureConventions(configurationBuilder);
    }

#if DEBUG
    private static readonly ILoggerFactory _consoleLogger = LoggerFactory.Create(builder => builder.AddConsole());

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_consoleLogger);
        optionsBuilder.EnableSensitiveDataLogging(true);
    }
#endif

    async Task<bool> IDataContext.PingAsync(CancellationToken cancellationToken)
        => await Database.CanConnectAsync(cancellationToken).ConfigureAwait(false);

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
    Task<bool> PingAsync(CancellationToken cancellationToken);

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    DbSet<Entities.Tournament> Tournaments { get; }

    DbSet<Entities.Division> Divisions { get; }

    DbSet<Entities.TournamentSquad> Squads { get; }

    DbSet<Entities.SweeperSquad> Sweepers { get; }

    DbSet<Entities.Bowler> Bowlers { get; }

    DbSet<Entities.Registration> Registrations { get; }

    DbSet<Entities.SquadScore> SquadScores { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken);
}