using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.UnitTests.Domain.Tournaments;

public sealed class RatioTests
{
    [Theory]
    [InlineData(.9)]
    [InlineData(1.0)]
    public void Create_ShouldReturnAnError_WhenRatioIsLessThanOrEqualToOne(decimal ratio)
    {
        // Act
        ErrorOr<Ratio> result = Ratio.Create(ratio);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainSingle();

        result.FirstError.Code.Should().Be("Ratio.RatioMustBeGreaterThanOne");
        result.FirstError.Description.Should().Be("Ratio must be greater than one.");
    }

    [Fact]
    public void Create_ShouldReturnRatio_WhenRatioIsGreaterThanOne()
    {
        // Act
        ErrorOr<Ratio> result = Ratio.Create(1.1m);

        // Assert
        result.IsError.Should().BeFalse();

        result.Value.Should().NotBeNull();
        result.Value.Value.Should().Be(1.1m);
    }
}
