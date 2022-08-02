using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NewEnglandClassic.Database.Entities;
internal class SquadRegistration
{
    [Required]
    public Guid RegistrationId { get; set; }

    public Registration Registration { get; set; } = null!;

    [Required]
    public Guid SquadId { get; set; }

    public Squad Squad { get; set; } = null!;

    internal class Configuration : IEntityTypeConfiguration<SquadRegistration>
    {
        public void Configure(EntityTypeBuilder<SquadRegistration> builder)
        {
            builder.HasKey(squadRegistration => new { squadRegistration.RegistrationId, squadRegistration.SquadId });

            builder.HasOne(squadRegistration => squadRegistration.Squad)
                   .WithMany(squad => squad.Registrations)
                   .HasForeignKey(squadRegistration => squadRegistration.SquadId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(squadRegistration => squadRegistration.Registration)
                   .WithMany(registration => registration.Squads)
                   .HasForeignKey(squadRegistration => squadRegistration.RegistrationId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
