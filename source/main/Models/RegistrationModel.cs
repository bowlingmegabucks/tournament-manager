
namespace NewEnglandClassic.Models;
internal class Registration
{
    public Guid Id { get; set; }

    public Bowler Bowler { get; set; }

    public Division Division { get; set; }

    public int? Average { get; set; }

    public IEnumerable<Guid> Squads { get; set; }

    public IEnumerable<Guid> Sweepers { get; set; }

    internal DateOnly TournamentStartDate { get; set; }

    public Registration(BowlerId bowlerId, DivisionId divisionId, IEnumerable<Guid> squads, IEnumerable<Guid> sweepers, int? average)
        : this(new Bowler { Id = bowlerId }, divisionId, squads, sweepers, average)
    { }

    public Registration(Bowler bowler, DivisionId divisionId, IEnumerable<Guid> squads, IEnumerable<Guid> sweepers, int? average)
    {
        Bowler = bowler;
        Division = new Division { Id = divisionId };

        Squads = squads;
        Sweepers = sweepers;

        Average = average;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal Registration()
    {
        Bowler = new Bowler();
        Division = new Division();

        Squads = Enumerable.Empty<Guid>();
        Sweepers = Enumerable.Empty<Guid>();
    }
}
