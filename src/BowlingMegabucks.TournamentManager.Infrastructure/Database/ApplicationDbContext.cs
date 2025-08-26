using Microsoft.EntityFrameworkCore;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database;

internal sealed class ApplicationDbContext
    : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }
}
