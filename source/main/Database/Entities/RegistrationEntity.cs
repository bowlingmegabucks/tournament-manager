using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NewEnglandClassic.Database.Entities;
internal class Registration
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid BowlerId { get; set; }

    public Bowler Bowler { get; set; } = null!;

    [Required]
    public Guid DivisionId { get; set; }

    public Division Division { get; set; } = null!;

    public int? Average { get; set; }

    public ICollection<SquadRegistration> Sqauds { get; set; } = null!;

    internal class Configuration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
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
