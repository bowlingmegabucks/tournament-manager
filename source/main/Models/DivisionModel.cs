
namespace NortheastMegabuck.Models;
internal class Division : IEquatable<Division>
{
    public DivisionId Id { get; set; }

    public short Number { get; set; }

    public string Name { get; set; }

    public TournamentId TournamentId { get; set; }

    public short? MinimumAge { get; set; }

    public short? MaximumAge { get; set; }

    public int? MinimumAverage { get; set; }

    public int? MaximumAverage { get; set; }

    public decimal? HandicapPercentage { get; set; }

    public int? HandicapBase { get; set; }

    public int? MaximumHandicapPerGame { get; set; }

    public Gender? Gender { get; set; }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal Division()
    {
        Name = string.Empty;
        Id = DivisionId.New();
    }

    public Division(Divisions.IViewModel viewModel)
    {
        Id = viewModel.Id;
        Number = viewModel.Number;
        Name = viewModel.DivisionName;
        TournamentId = viewModel.TournamentId;
        MinimumAge = viewModel.MinimumAge;
        MaximumAge = viewModel.MaximumAge;
        MinimumAverage = viewModel.MinimumAverage;
        MaximumAverage = viewModel.MaximumAverage;
        HandicapPercentage = viewModel.HandicapPercentage / 100m;
        HandicapBase = viewModel.HandicapBase;
        MaximumHandicapPerGame = viewModel.MaximumHandicapPerGame;
        Gender = viewModel.Gender;
    }

    public Division(Database.Entities.Division entity)
    {
        Id = entity.Id;
        Number = entity.Number;
        Name = entity.Name;
        TournamentId = entity.TournamentId;
        MinimumAge = entity.MinimumAge;
        MaximumAge = entity.MaximumAge;
        MinimumAverage = entity.MinimumAverage;
        MaximumAverage = entity.MaximumAverage;
        HandicapPercentage = entity.HandicapPercentage;
        HandicapBase = entity.HandicapBase;
        MaximumHandicapPerGame = entity.MaximumHandicapPerGame;
        Gender = entity.Gender;
    }

    public bool Equals(Division? other)
        => other != null && Id == other.Id;

    public override bool Equals(object? obj)
        => Equals(obj as Division);

    public override int GetHashCode()
        => Id.GetHashCode();
}
