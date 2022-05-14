using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace NewEnglandClassic.Database.Entities;
internal class SweeperSquad : Squad
{
    [Required]
    public decimal EntryFee { get; set; }

    [Required]
    public short Games { get; set; }
    
    [Required]
    public decimal CashRatio { get; set; }

    internal class Configuration : IEntityTypeConfiguration<SweeperSquad>
    {
        public void Configure(EntityTypeBuilder<SweeperSquad> builder)
            => builder.HasOne(squad=> squad.Tournament)
                      .WithMany(tournament => tournament.Sweepers)
                      .HasForeignKey(squad => squad.TournamentId)
                      .IsRequired();

    }
}
