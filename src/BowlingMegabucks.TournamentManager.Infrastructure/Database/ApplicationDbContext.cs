using System.Diagnostics.CodeAnalysis;
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
}
