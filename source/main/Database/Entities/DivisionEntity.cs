using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace NewEnglandClassic.Database.Entities;
internal class Division
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public short Number { get; set; }

    [Required]
    public Guid TournamentId { get; set; }

    public Tournament Tournament { get; set; } = null!;

    public short? MinimumAge { get; set; }
    
    public short? MaximumAge { get; set; }
    
    public int? MinimumAverage { get; set; }

    public int? MaximumAverage { get; set; }

    [Precision(3,2)]
    public decimal? HandicapPercentage { get; set; }

    public int? HandicapBase { get; set; }

    public int? MaximumHandicapPerGame { get; set; }

    public Models.Gender? Gender { get; set; }

    public ICollection<SweeperDivision> Sweepers { get; set; } = null!;

    internal class Configuration : IEntityTypeConfiguration<Division>
    {
        public void Configure(EntityTypeBuilder<Division> builder)
            => builder.HasOne(division => division.Tournament)
                      .WithMany(tournament => tournament.Divisions)
                      .HasForeignKey(division => division.TournamentId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

    }
}