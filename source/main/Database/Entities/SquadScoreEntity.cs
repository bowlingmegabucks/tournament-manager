using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NortheastMegabuck.Database.Entities;
internal class SquadScore
{
    public BowlerId BowlerId { get; set; }

    public Bowler Bowler { get; set; } = null!;

    public SquadId SquadId { get; set; }

    public Squad Squad { get; set; } = null!;

    public short Game { get; set; }

    public int Score { get; set; }

    internal class Configuration : IEntityTypeConfiguration<SquadScore>
    {
        public void Configure(EntityTypeBuilder<SquadScore> builder)
        {
            builder.HasKey(squadScore => new { squadScore.BowlerId, squadScore.SquadId, squadScore.Game });

            builder.Property(squadScore => squadScore.BowlerId).HasConversion<BowlerId.EfCoreValueConverter>();
            builder.Property(squadScore => squadScore.SquadId).HasConversion<SquadId.EfCoreValueConverter>();

            builder.HasOne(squadScore => squadScore.Bowler)
                    .WithMany(bowler => bowler.SquadScores)
                    .HasForeignKey(squadScore => squadScore.BowlerId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

            builder.HasOne(squadScore => squadScore.Squad)
                    .WithMany(squad => squad.Scores)
                    .HasForeignKey(squadScore => squadScore.SquadId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
        }
    }
}
