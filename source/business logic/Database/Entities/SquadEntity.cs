﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace BowlingMegabucks.TournamentManager.Database.Entities;
internal abstract class Squad
{
    [Key]
    public SquadId Id { get; set; }

    [Required]
    public TournamentId TournamentId { get; set; }

    public Tournament Tournament { get; set; } = null!;

    [Precision(3, 1)]
    public decimal? CashRatio { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public short MaxPerPair { get; set; }

    [Required]
    public bool Complete { get; set; }

    [Required]
    public short StartingLane { get; set; }

    [Required]
    public short NumberOfLanes { get; set; }

    public ICollection<SquadRegistration> Registrations { get; set; } = null!;

    public ICollection<SquadScore> Scores { get; set; } = null!;

    internal class Configuration : IEntityTypeConfiguration<Squad>
    {
        public void Configure(EntityTypeBuilder<Squad> builder)
        {
            builder.Property(squad => squad.Id)
                .HasConversion<SquadId.EfCoreValueConverter>()
                .HasValueGenerator<SquadIdValueGenerator>();

            builder.Property(squad => squad.TournamentId).HasConversion<TournamentId.EfCoreValueConverter>();

            builder.ToTable("Squads")
                      .HasDiscriminator<int>("SquadType")
                      .HasValue<TournamentSquad>(0)
                      .HasValue<SweeperSquad>(1);
        }
    }
}
