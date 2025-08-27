using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database.Configurations;

internal static class AuditConfiguration
{
    internal const string CreatedAtColumnName = "CreatedAt";
    internal const string UpdatedAtColumnName = "UpdatedAt";

    public static EntityTypeBuilder<TEntity> HasAuditFields<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class
    {
        builder.Property<DateTime>(CreatedAtColumnName)
            .IsRequired()
            .HasColumnType("datetime(6)")
            .HasComment("UTC timestamp when entity was created");

        builder.Property<DateTime>(UpdatedAtColumnName)
            .IsRequired()
            .HasColumnType("datetime(6)")
            .HasComment("UTC timestamp when entity was last updated");

        return builder;
    }
}
