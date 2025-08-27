using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database.Configurations;

internal sealed class TournamentConfiguration
    : IEntityTypeConfiguration<Tournament>
{
    public void Configure(EntityTypeBuilder<Tournament> builder)
    {
        builder.HasKey(tournament => tournament.Id);
        builder.Property(tournament => tournament.Id)
            .ValueGeneratedNever()
            .HasConversion<TournamentId.EfCoreValueConverter>();

        builder.Property(tournament => tournament.Name)
            .IsRequired()
            .HasMaxLength(Tournament.MaxNameLength);

        builder.OwnsOne(tournament => tournament.TournamentDates, dateRangeBuilder =>
        {
            dateRangeBuilder.Property(dateRange => dateRange.StartDate)
                .HasColumnName("StartDate")
                .IsRequired();

            dateRangeBuilder.Property(dateRange => dateRange.EndDate)
                .HasColumnName("EndDate")
                .IsRequired();
        });

        builder.Property(tournament => tournament.EntryFee)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(tournament => tournament.Games)
            .IsRequired();

        builder.OwnsOne(tournament => tournament.FinalsRatio, ratioBuilder =>
        {
            ratioBuilder.Property(ratio => ratio.Value)
                .HasColumnName("FinalsRatio")
                .IsRequired()
                .HasPrecision(3, 1);
        });

        builder.OwnsOne(tournament => tournament.CashRatio, ratioBuilder =>
        {
            ratioBuilder.Property(ratio => ratio.Value)
                .HasColumnName("CashRatio")
                .IsRequired()
                .HasPrecision(3, 1);
        });

        builder.Property(tournament => tournament.BowlingCenter)
            .IsRequired()
            .HasMaxLength(Tournament.MaxBowlingCenterLength);

        builder.Property(tournament => tournament.Completed)
            .IsRequired();
    }
}
