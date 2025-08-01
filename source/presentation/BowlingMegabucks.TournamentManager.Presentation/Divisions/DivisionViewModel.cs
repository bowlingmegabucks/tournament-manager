
namespace BowlingMegabucks.TournamentManager.Divisions;

internal class ViewModel : IViewModel
{
    public DivisionId Id { get; set; }

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

/// <summary>
/// 
/// </summary>
public interface IViewModel
{
    /// <summary>
    /// 
    /// </summary>
    DivisionId Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    short Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string DivisionName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    TournamentId TournamentId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    short? MinimumAge { get; set; }

    /// <summary>
    /// 
    /// </summary>
    short? MaximumAge { get; set; }

    /// <summary>
    /// 
    /// </summary>
    int? MinimumAverage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    int? MaximumAverage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    decimal? HandicapPercentage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    int? HandicapBase { get; set; }

    /// <summary>
    /// 
    /// </summary>
    int? MaximumHandicapPerGame { get; set; }

    /// <summary>
    /// 
    /// </summary>
    Models.Gender? Gender { get; set; }
}

internal static class ViewModelExtensions
{
    public static Models.Division ToModel(this IViewModel viewModel)
    {
        return new Models.Division
        {
            Id = viewModel.Id,
            Number = viewModel.Number,
            Name = viewModel.DivisionName,
            TournamentId = viewModel.TournamentId,
            MinimumAge = viewModel.MinimumAge,
            MaximumAge = viewModel.MaximumAge,
            MinimumAverage = viewModel.MinimumAverage,
            MaximumAverage = viewModel.MaximumAverage,
            HandicapPercentage = viewModel.HandicapPercentage / 100m,
            HandicapBase = viewModel.HandicapBase,
            MaximumHandicapPerGame = viewModel.MaximumHandicapPerGame,
            Gender = viewModel.Gender
        };
    } 
}