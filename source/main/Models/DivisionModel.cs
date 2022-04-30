
namespace NewEnglandClassic.Models;
internal class Division
{
    public Guid Id { get; set; }

    public short Number { get; set; }

    public string Name { get; set; }

    public Guid TournamentId { get; set; }

    public short? MinimumAge { get; set; }

    public short? MaximumAge { get; set; }

    public int? MinimumAverage { get; set; }

    public int? MaximumAverage { get; set; }

    public decimal? HandicapPercentage { get; set; }

    public int? HandicapBase { get; set; }

    public int? MaximumHandicapPerGame { get; set; }

    public Gender? Gender { get; set; }
}
