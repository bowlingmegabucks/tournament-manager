﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BowlingMegabucks.TournamentManager.Database.Entities;
internal class SquadRegistration
{
    [Required]
    public RegistrationId RegistrationId { get; set; }

    public Registration Registration { get; set; } = null!;

    [Required]
    public SquadId SquadId { get; set; }

    public Squad Squad { get; set; } = null!;

    [MaxLength(3)]
    public string LaneAssignment { get; set; } = string.Empty;

    internal class Configuration : IEntityTypeConfiguration<SquadRegistration>
    {
        public void Configure(EntityTypeBuilder<SquadRegistration> builder)
        {
            builder.Property(squadRegistration => squadRegistration.RegistrationId).HasConversion<RegistrationId.EfCoreValueConverter>();

            builder.Property(squadRegistration => squadRegistration.SquadId).HasConversion<SquadId.EfCoreValueConverter>();

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
