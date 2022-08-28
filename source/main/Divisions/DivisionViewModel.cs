
namespace NewEnglandClassic.Divisions;
internal class ViewModel : IViewModel
{
    public Id Id { get; set; }

    public short Number { get; set; }

    public string DivisionName { get; set; }

    public TournamentId TournamentId { get; set; }

    public short? MinimumAge { get; set; }

    public short? MaximumAge { get; set; }

    public int? MinimumAverage { get; set; }

    public int? MaximumAverage { get; set; }

    public decimal? HandicapPercentage { get; set; }

    public int? HandicapBase { get; set; }

    public int? MaximumHandicapPerGame { get; set; }

    public Models.Gender? Gender { get; set; }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal ViewModel()
    {
        DivisionName = string.Empty;
    }

    public ViewModel(Models.Division division)
    {
        Id = division.Id;
        Number = division.Number;
        DivisionName = division.Name;
        TournamentId = division.TournamentId;
        MinimumAge = division.MinimumAge;
        MaximumAge = division.MaximumAge;
        MinimumAverage = division.MinimumAverage;
        MaximumAverage = division.MaximumAverage;
        HandicapPercentage = division.HandicapPercentage * 100;
        HandicapBase = division.HandicapBase;
        MaximumHandicapPerGame = division.MaximumHandicapPerGame;
        Gender = division.Gender;
    }
}

public interface IViewModel
{
    Id Id { get; set; }

    short Number { get; set; }
    
    string DivisionName { get; set; }

    TournamentId TournamentId { get; set; }

    short? MinimumAge { get; set; }

    short? MaximumAge { get; set; }

    int? MinimumAverage { get; set; }

    int? MaximumAverage { get; set; }

    decimal? HandicapPercentage { get; set; }

    int? HandicapBase { get; set; }

    int? MaximumHandicapPerGame { get; set; }

    Models.Gender? Gender { get; set; }
}