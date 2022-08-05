using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace NewEnglandClassic.Database.Entities;
internal class SweeperDivision
{
    public Guid SweeperId { get; set; }

    public SweeperSquad Sweeper { get; set; } = null!;

    public DivisionId DivisionId { get; set; }

    public Division Division { get; set; } = null!;

    public int? BonusPinsPerGame { get; set; }

    internal class Configuration : IEntityTypeConfiguration<SweeperDivision>
    {
        public void Configure(EntityTypeBuilder<SweeperDivision> builder)
        {
            builder.Property(builder => builder.DivisionId).HasConversion(new DivisionIdConverter());

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
