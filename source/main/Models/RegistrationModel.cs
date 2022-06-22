
namespace NewEnglandClassic.Models;
internal class Registration
{
    public Bowler Bowler { get; set; }

    public Division Division { get; set; }

    public int? Average { get; set; }

    public IEnumerable<Guid> Squads { get; set; }

    public IEnumerable<Guid> Sweepers { get; set; }

    public Registration(Guid bowlerId, Guid divisionId, IEnumerable<Guid> squads, IEnumerable<Guid> sweepers, int? average)
    {
        Bowler = new Bowler { Id = bowlerId };
        Division = new Division { Id = divisionId };

        Squads = squads;
        Sweepers = sweepers;

        Average = average;
    }
}
