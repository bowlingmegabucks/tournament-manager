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
    /// Gets or sets the unique identifier for the division.
    /// </summary>
    DivisionId Id { get; set; }

    /// <summary>
    /// Gets or sets the division number.
    /// </summary>
    short Number { get; set; }

    /// <summary>
    /// Gets or sets the name of the division.
    /// </summary>
    string DivisionName { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the tournament to which the division belongs.
    /// </summary>
    TournamentId TournamentId { get; set; }

    /// <summary>
    /// Gets or sets the minimum age allowed in the division, if any.
    /// </summary>
    short? MinimumAge { get; set; }

    /// <summary>
    /// Gets or sets the maximum age allowed in the division, if any.
    /// </summary>
    short? MaximumAge { get; set; }

    /// <summary>
    /// Gets or sets the minimum average score required for the division, if any.
    /// </summary>
    int? MinimumAverage { get; set; }

    /// <summary>
    /// Gets or sets the maximum average score allowed for the division, if any.
    /// </summary>
    int? MaximumAverage { get; set; }

    /// <summary>
    /// Gets or sets the handicap percentage for the division, if any.
    /// </summary>
    decimal? HandicapPercentage { get; set; }

    /// <summary>
    /// Gets or sets the handicap base score for the division, if any.
    /// </summary>
    int? HandicapBase { get; set; }

    /// <summary>
    /// Gets or sets the maximum handicap per game for the division, if any.
    /// </summary>
    int? MaximumHandicapPerGame { get; set; }

    /// <summary>
    /// Gets or sets the gender restriction for the division, if any.
    /// </summary>
    Models.Gender? Gender { get; set; }
}

internal static class ViewModelExtensions
{
    /// <summary>
    /// Converts the division view model to a <see cref="Models.Division"/> domain model.
    /// </summary>
    /// <param name="viewModel">The division view model to convert.</param>
    /// <returns>A <see cref="Models.Division"/> instance with values mapped from the view model.</returns>
    /// <remarks>
    /// This method maps all properties from the view model to a new domain model instance, converting the handicap percentage from percent to decimal.
    /// </remarks>
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