using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BowlingMegabucks.TournamentManager.Database.Entities;
internal class TournamentSquad : Squad
{
    [Precision(3, 1)]
    public decimal? FinalsRatio { get; set; }

    [Precision(5, 2)]
    public decimal? EntryFee { get; set; }

    internal new class Configuration : IEntityTypeConfiguration<TournamentSquad>
    {
        public void Configure(EntityTypeBuilder<TournamentSquad> builder)
        {
            builder.Property(squad => squad.EntryFee).HasColumnName("SquadEntryFee");

            builder.HasOne(squad => squad.Tournament)
                      .WithMany(tournament => tournament.Squads)
                      .HasForeignKey(squad => squad.TournamentId)
                      .IsRequired();
        }
    }
}
