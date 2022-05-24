using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace NewEnglandClassic.Database.Entities;
internal class TournamentSquad : Squad
{
    [Precision(3, 1)]
    public decimal? FinalsRatio { get; set; }
    
    internal class Configuration : IEntityTypeConfiguration<TournamentSquad>
    {
        public void Configure(EntityTypeBuilder<TournamentSquad> builder)
            => builder.HasOne(squad => squad.Tournament)
                      .WithMany(tournament => tournament.Squads)
                      .HasForeignKey(squad => squad.TournamentId)
                      .IsRequired();

    }
}
