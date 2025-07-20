using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartEnum.EFCore;

#if DEBUG
using Microsoft.Extensions.Logging;
#endif

namespace BowlingMegabucks.TournamentManager.Database;

/// <summary>
/// Represents the database context for the Tournament Manager application.
/// </summary>
internal sealed class DataContext
    : DbContext, IDataContext
{

    /// <summary>
    /// Initializes a new instance of the <see cref="DataContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to configure the database context.</param>
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    { }

    /// <summary>
    /// Configures conventions for the database context, including support for SmartEnum.
    /// </summary>
    /// <param name="configurationBuilder">The builder used to configure model conventions.</param>
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ConfigureSmartEnum();

        base.ConfigureConventions(configurationBuilder);
    }

#if DEBUG
    private static readonly ILoggerFactory _consoleLogger = LoggerFactory.Create(builder => builder.AddConsole());

    /// <summary>
    /// Configures the database context to use a console logger and enables sensitive data logging.
    /// This method is only executed in the DEBUG build configuration.
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ArgumentNullException.ThrowIfNull(optionsBuilder);

        optionsBuilder.UseLoggerFactory(_consoleLogger);
        optionsBuilder.EnableSensitiveDataLogging(true);
    }
#endif

    async Task<bool> IDataContext.PingAsync(CancellationToken cancellationToken)
        => await Database.CanConnectAsync(cancellationToken).ConfigureAwait(false);

    async Task IDataContext.SaveChangesAsync(CancellationToken cancellationToken)
        => await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// Configures the entity mappings and relationships for the database context.
    /// This method is called by the Entity Framework runtime when the model is being created.
    /// </summary>
    /// <param name="modelBuilder">The builder used to define the model for the database context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

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

    DbSet<Entities.Tournament> IDataContext.Tournaments
        => Set<Entities.Tournament>();

    DbSet<Entities.Division> IDataContext.Divisions
        => Set<Entities.Division>();

    DbSet<Entities.TournamentSquad> IDataContext.Squads
        => Set<Entities.TournamentSquad>();

    DbSet<Entities.SweeperSquad> IDataContext.Sweepers
        => Set<Entities.SweeperSquad>();

    DbSet<Entities.Bowler> IDataContext.Bowlers
        => Set<Entities.Bowler>();

    DbSet<Entities.Registration> IDataContext.Registrations
        => Set<Entities.Registration>();

    DbSet<Entities.SquadScore> IDataContext.SquadScores
        => Set<Entities.SquadScore>();
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