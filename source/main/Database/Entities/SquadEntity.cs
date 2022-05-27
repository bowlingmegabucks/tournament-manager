using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace NewEnglandClassic.Database.Entities;
internal abstract class Squad
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid TournamentId { get; set; }

    public Tournament Tournament { get; set; } = null!;

    [Precision(3, 1)]
    public decimal? CashRatio { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public short MaxPerPair { get; set; }

    [Required]
    public bool Complete { get; set; }

    internal class Configuration : IEntityTypeConfiguration<Squad>
    {
        public void Configure(EntityTypeBuilder<Squad> builder) 
            => builder.ToTable("Squads")
                      .HasDiscriminator<int>("SquadType")
                      .HasValue<TournamentSquad>(0)
                      .HasValue<SweeperSquad>(1);
    }
}
