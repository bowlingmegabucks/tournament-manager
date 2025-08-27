using System.Diagnostics.CodeAnalysis;
using BowlingMegabucks.TournamentManager.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database;

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
internal sealed class ApplicationDbContext
    : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TournamentConfiguration());
    }
}
