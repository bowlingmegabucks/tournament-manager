using BowlingMegabucks.TournamentManager.Domain.Tournaments;

namespace BowlingMegabucks.TournamentManager.Domain.UnitTests.Tournaments;

public sealed class TournamentTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Create_ShouldReturnAnError_WhenTournamentNameIsMissing(string? name)
    {
        // Arrange
        var tournamentDates = new DateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 2));
        decimal entryFee = 10m;
        short games = 3;
        Ratio finalsRatio = Ratio.Create(7m).Value;
        Ratio cashRatio = Ratio.Create(4m).Value;
        string bowlingCenter = "Bowling Center";

        // Act
        ErrorOr.ErrorOr<Tournament> result = Tournament.Create(
            name!,
            tournamentDates,
            entryFee,
            games,
            finalsRatio,
            cashRatio,
            bowlingCenter);

        // Assert
        result.IsError.Should().BeTrue();

        result.Errors.Should().ContainSingle();
        result.FirstError.Code.Should().Be("Tournament.TournamentNameIsRequired");
        result.FirstError.Description.Should().Be("Tournament name is required.");
    }

    [Fact]
    public void Create_ShouldReturnAnError_WhenTournamentNameIsTooLong()
    {
        // Arrange
        string name = new('A', Tournament.MaxNameLength + 1);
        var tournamentDates = new DateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 2));
        decimal entryFee = 10m;
        short games = 3;
        Ratio finalsRatio = Ratio.Create(7m).Value;
        Ratio cashRatio = Ratio.Create(4m).Value;
        string bowlingCenter = "Bowling Center";

        // Act
        ErrorOr.ErrorOr<Tournament> result = Tournament.Create(
            name,
            tournamentDates,
            entryFee,
            games,
            finalsRatio,
            cashRatio,
            bowlingCenter);

        // Assert
        result.IsError.Should().BeTrue();

        result.Errors.Should().ContainSingle();
        result.FirstError.Code.Should().Be("Tournament.TournamentNameIsTooLong");
        result.FirstError.Description.Should().Be("Tournament name exceeds maximum length.");
        result.FirstError.Metadata.Should().ContainKey("MaxLength").WhoseValue.Should().Be(Tournament.MaxNameLength);
        result.FirstError.Metadata.Should().ContainKey("ActualLength").WhoseValue.Should().Be(name.Length);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Create_ShouldReturnAnError_WhenGamesIsLessThanOrEqualToZero(short games)
    {
        // Arrange
        string name = "Tournament Name";
        var tournamentDates = new DateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 2));
        decimal entryFee = 10m;
        Ratio finalsRatio = Ratio.Create(7m).Value;
        Ratio cashRatio = Ratio.Create(4m).Value;
        string bowlingCenter = "Bowling Center";

        // Act
        ErrorOr.ErrorOr<Tournament> result = Tournament.Create(
            name,
            tournamentDates,
            entryFee,
            games,
            finalsRatio,
            cashRatio,
            bowlingCenter);

        // Assert
        result.IsError.Should().BeTrue();

        result.Errors.Should().ContainSingle();
        result.FirstError.Code.Should().Be("Tournament.TournamentGamesMustBeGreaterThanZero");
        result.FirstError.Description.Should().Be("Tournament games must be greater than zero.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Create_ShouldReturnAnError_WhenBowlingCenterIsMissing(string? bowlingCenter)
    {
        // Arrange
        string name = "Tournament Name";
        var tournamentDates = new DateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 2));
        decimal entryFee = 10m;
        short games = 3;
        Ratio finalsRatio = Ratio.Create(7m).Value;
        Ratio cashRatio = Ratio.Create(4m).Value;

        // Act
        ErrorOr.ErrorOr<Tournament> result = Tournament.Create(
            name,
            tournamentDates,
            entryFee,
            games,
            finalsRatio,
            cashRatio,
            bowlingCenter!);

        // Assert
        result.IsError.Should().BeTrue();

        result.Errors.Should().ContainSingle();
        result.FirstError.Code.Should().Be("Tournament.TournamentBowlingCenterIsRequired");
        result.FirstError.Description.Should().Be("Tournament bowling center is required.");
    }

    [Fact]
    public void Create_ShouldReturnAnError_WhenBowlingCenterNameIsTooLong()
    {
        // Arrange
        string name = "Tournament Name";
        var tournamentDates = new DateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 2));
        decimal entryFee = 10m;
        short games = 3;
        Ratio finalsRatio = Ratio.Create(7m).Value;
        Ratio cashRatio = Ratio.Create(4m).Value;
        string bowlingCenter = new('A', Tournament.BowlingCenterMaxLength + 1);

        // Act
        ErrorOr.ErrorOr<Tournament> result = Tournament.Create(
            name,
            tournamentDates,
            entryFee,
            games,
            finalsRatio,
            cashRatio,
            bowlingCenter);

        // Assert
        result.IsError.Should().BeTrue();

        result.Errors.Should().ContainSingle();
        result.FirstError.Code.Should().Be("Tournament.TournamentBowlingCenterIsTooLong");
        result.FirstError.Description.Should().Be("Tournament bowling center exceeds maximum length.");
        result.FirstError.Metadata.Should().ContainKey("MaxLength").WhoseValue.Should().Be(Tournament.BowlingCenterMaxLength);
        result.FirstError.Metadata.Should().ContainKey("ActualLength").WhoseValue.Should().Be(bowlingCenter.Length);
    }

    [Fact]
    public void Create_ShouldReturnTournament_WhenAllFieldsAreValid()
    {
        // Arrange
        string name = "Tournament Name";
        var tournamentDates = new DateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 2));
        decimal entryFee = 10m;
        short games = 3;
        Ratio finalsRatio = Ratio.Create(7m).Value;
        Ratio cashRatio = Ratio.Create(4m).Value;
        string bowlingCenter = "Bowling Center";

        // Act
        ErrorOr.ErrorOr<Tournament> result = Tournament.Create(
            name,
            tournamentDates,
            entryFee,
            games,
            finalsRatio,
            cashRatio,
            bowlingCenter);

        // Assert
        result.IsError.Should().BeFalse();

        Tournament tournament = result.Value;

        tournament.Name.Should().Be(name);
        tournament.TournamentDates.Should().Be(tournamentDates);
        tournament.EntryFee.Should().Be(entryFee);
        tournament.Games.Should().Be(games);
        tournament.FinalsRatio.Should().Be(finalsRatio);
        tournament.CashRatio.Should().Be(cashRatio);
        tournament.BowlingCenter.Should().Be(bowlingCenter);
        tournament.Completed.Should().BeFalse();
    }
}
