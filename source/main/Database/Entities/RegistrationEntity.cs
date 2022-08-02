using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NewEnglandClassic.Database.Entities;
internal class Registration
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public BowlerId BowlerId { get; set; }

    public Bowler Bowler { get; set; } = null!;

    [Required]
    public Guid DivisionId { get; set; }

    public Division Division { get; set; } = null!;

    public int? Average { get; set; }

    public ICollection<SquadRegistration> Squads { get; set; } = null!;

    internal class Configuration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.Property(registration => registration.BowlerId).HasConversion(new BowlerIdConverter());

            builder.HasAlternateKey(registration => new { registration.BowlerId, registration.DivisionId });

            builder.HasOne(registration => registration.Bowler)
                   .WithMany(bowler => bowler.Registrations)
                   .HasForeignKey(registration => registration.BowlerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(registration => registration.Division)
                    .WithMany(division => division.Registrations)
                    .HasForeignKey(registration => registration.DivisionId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
