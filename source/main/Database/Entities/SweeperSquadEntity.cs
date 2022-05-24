using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace NewEnglandClassic.Database.Entities;
internal class SweeperSquad : Squad
{
    [Required]
    [Precision(5,2)]
    public decimal EntryFee { get; set; }

    [Required]
    public short Games { get; set; }

    public ICollection<SweeperDivision> Divisions { get; set; } = null!;

    internal class Configuration : IEntityTypeConfiguration<SweeperSquad>
    {
        public void Configure(EntityTypeBuilder<SweeperSquad> builder)
            => builder.HasOne(squad=> squad.Tournament)
                      .WithMany(tournament => tournament.Sweepers)
                      .HasForeignKey(squad => squad.TournamentId)
                      .IsRequired();

    }
}
