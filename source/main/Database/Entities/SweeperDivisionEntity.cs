using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Database.Entities;
internal class SweeperDivision
{
    public SquadId SweeperId { get; set; }

    public SweeperSquad Sweeper { get; set; } = null!;

    public DivisionId DivisionId { get; set; }

    public Division Division { get; set; } = null!;

    public int? BonusPinsPerGame { get; set; }

    internal class Configuration : IEntityTypeConfiguration<SweeperDivision>
    {
        public void Configure(EntityTypeBuilder<SweeperDivision> builder)
        {
            builder.Property(sweeperDivision => sweeperDivision.SweeperId).HasConversion<SquadId.EfCoreValueConverter>();

            builder.Property(builder => builder.DivisionId).HasConversion<DivisionId.EfCoreValueConverter>();

            builder.HasKey(e => new { e.SweeperId, e.DivisionId });
            
            builder.HasOne(squad => squad.Sweeper)
                   .WithMany(sweeper => sweeper.Divisions)
                   .HasForeignKey(squad => squad.SweeperId)
                   .IsRequired();

            builder.HasOne(division => division.Division)
                   .WithMany(division => division.Sweepers)
                   .HasForeignKey(division => division.DivisionId)
                   .IsRequired();
        }
    }
}
