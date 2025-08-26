using BowlingMegabucks.TournamentManager.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database.Interceptors;

internal sealed class AuditInterceptor
    : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateAuditFields(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateAuditFields(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateAuditFields(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        DateTime utcNow = DateTime.UtcNow;

        foreach (EntityEntry entry in context.ChangeTracker.Entries().Where(HasAuditFields))
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(AuditConfiguration.CreatedAtColumnName).CurrentValue = utcNow;
                    entry.Property(AuditConfiguration.ModifiedAtColumnName).CurrentValue = utcNow;
                    break;

                case EntityState.Modified:
                    entry.Property(AuditConfiguration.ModifiedAtColumnName).CurrentValue = utcNow;
                    entry.Property(AuditConfiguration.CreatedAtColumnName).IsModified = false;
                    break;
            }
        }
    }

    private static bool HasAuditFields(EntityEntry entry)
        => entry.Metadata.FindProperty(AuditConfiguration.CreatedAtColumnName) is not null;
}
