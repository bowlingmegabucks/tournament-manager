using System.Globalization;

namespace BowlingMegabucks.TournamentManager.Domain.UnitTests;

public sealed class DateOnlyRangeTests
{
    [Fact]
    public void Ctor_ShouldInitializeProperties_WhenEndDateIsAfterStartDate()
    {
        // Arrange
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 1, 2);

        // Act
        var dateRange = new DateOnlyRange(startDate, endDate);

        // Assert
        dateRange.StartDate.Should().Be(startDate);
        dateRange.EndDate.Should().Be(endDate);
    }

    [Fact]
    public void Ctor_ShouldInitializeProperties_WhenStartDateIsEqualToEndDate()
    {
        // Arrange
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 1, 1);

        // Act
        var dateRange = new DateOnlyRange(startDate, endDate);

        // Assert
        dateRange.StartDate.Should().Be(startDate);
        dateRange.EndDate.Should().Be(endDate);
    }

    [Fact]
    public void Ctor_ShouldThrowInvalidDateRangeException_WhenStartDateIsAfterEndDate()
    {
        // Arrange
        var startDate = new DateOnly(2024, 1, 2);
        var endDate = new DateOnly(2024, 1, 1);

        // Act
        Action act = () => _ = new DateOnlyRange(startDate, endDate);

        // Assert
        act.Should().Throw<InvalidDateRangeException<DateOnly>>()
            .Where(ex => ex.StartDate == startDate && ex.EndDate == endDate)
            .WithMessage($"Invalid date range: start date '{startDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}' "
                + $"must be before or equal to end date '{endDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}'.");
    }
}
