using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database.Configurations;

internal sealed class TournamentConfiguration
    : IEntityTypeConfiguration<Tournament>
{
    public void Configure(EntityTypeBuilder<Tournament> builder)
    {
        builder.ToTable("Tournaments");

        builder.HasKey(tournament => tournament.Id);
        builder.Property(tournament => tournament.Id)
            .ValueGeneratedNever()
            .HasConversion<TournamentId.EfCoreValueConverter>()
            .HasComment("Unique identifier for the tournament");

        builder.Property(tournament => tournament.Name)
            .IsRequired()
            .HasMaxLength(Tournament.MaxNameLength)
            .HasComment("Name of the tournament");

        builder.OwnsOne(tournament => tournament.TournamentDates, dateRangeBuilder =>
        {
            dateRangeBuilder.Property(dateRange => dateRange.StartDate)
                .HasColumnName("StartDate")
                .IsRequired()
                .HasComment("Start date of the tournament");

            dateRangeBuilder.Property(dateRange => dateRange.EndDate)
                .HasColumnName("EndDate")
                .IsRequired()
                .HasComment("End date of the tournament");
        });

        builder.Property(tournament => tournament.EntryFee)
            .IsRequired()
            .HasPrecision(5, 2)
            .HasComment("Entry fee for the tournament");

        builder.Property(tournament => tournament.Games)
            .IsRequired()
            .HasComment("Number of games during qualifying in the tournament");

        builder.OwnsOne(tournament => tournament.FinalsRatio, ratioBuilder =>
        {
            ratioBuilder.Property(ratio => ratio.Value)
                .HasColumnName("FinalsRatio")
                .IsRequired()
                .HasPrecision(3, 1)
                .HasComment("Finals ratio for the tournament");
        });

        builder.OwnsOne(tournament => tournament.CashRatio, ratioBuilder =>
        {
            ratioBuilder.Property(ratio => ratio.Value)
                .HasColumnName("CashRatio")
                .IsRequired()
                .HasPrecision(3, 1)
                .HasComment("Cash ratio for the tournament");
        });

        builder.OwnsOne(tournament => tournament.SuperSweeperCashRatio, ratioBuilder =>
        {
            ratioBuilder.Property(ratio => ratio.Value)
                .HasColumnName("SuperSweeperCashRatio")
                .IsRequired()
                .HasPrecision(3, 1)
                .HasComment("Super Sweeper cash ratio for the tournament");
        });

        builder.Property(tournament => tournament.BowlingCenter)
            .IsRequired()
            .HasMaxLength(Tournament.MaxBowlingCenterLength)
            .HasComment("Bowling center hosting the tournament");

        builder.Property(tournament => tournament.Completed)
            .IsRequired()
            .HasComment("Indicates if the tournament is completed");

        builder.HasAuditFields();
    }
}
