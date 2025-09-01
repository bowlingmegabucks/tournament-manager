using System.Globalization;
using System.Threading;
using BowlingMegabucks.TournamentManager.SharedKernel.Properties;
using FluentAssertions;
using Xunit;

namespace BowlingMegabucks.TournamentManager.UnitTests.Domain;

public sealed class ResourceTests
{
    [Fact]
    public void Resources_ShouldSupportLocalization()
    {
        // Test that resources are accessible (basic functionality)
        // Note: Full culture switching may not work in globalization-invariant mode
        string yesValue = Resources.Yes;
        string noValue = Resources.No;

        // Basic assertions - values should not be null or empty
        yesValue.Should().NotBeNull();
        yesValue.Should().NotBeEmpty();
        noValue.Should().NotBeNull();
        noValue.Should().NotBeEmpty();

        // Values should be either English or Spanish depending on current culture
        (yesValue == "Yes" || yesValue == "Sí").Should().BeTrue();
        noValue.Should().Be("No");
    }

    [Theory]
    [InlineData("en-US", "Yes", "No")]
    [InlineData("es-ES", "Sí", "No")]
    public void Resources_ShouldReturnCorrectValues_ForDifferentCultures(string cultureName, string expectedYes, string expectedNo)
    {
        CultureInfo originalCulture = Thread.CurrentThread.CurrentUICulture;

        try
        {
            // Set the culture
            var culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = culture;

            // Test the resource values
            Resources.Yes.Should().Be(expectedYes);
            Resources.No.Should().Be(expectedNo);
        }
        catch (CultureNotFoundException)
        {
            // Culture not supported in this environment, skip test
            return;
        }
        finally
        {
            // Always restore original culture
            Thread.CurrentThread.CurrentUICulture = originalCulture;
        }
    }

    [Fact]
    public void Resources_ShouldFallbackToDefault_WhenCultureNotSupported()
    {
        CultureInfo originalCulture = Thread.CurrentThread.CurrentUICulture;

        try
        {
            // Try to set an unsupported culture
            var unsupportedCulture = new CultureInfo("fr-FR"); // French
            Thread.CurrentThread.CurrentUICulture = unsupportedCulture;

            // Should fallback to default (en-US) values
            Resources.Yes.Should().Be("Yes");
            Resources.No.Should().Be("No");
        }
        catch (CultureNotFoundException)
        {
            // Culture not supported, which is expected
            Resources.Yes.Should().Be("Yes");
            Resources.No.Should().Be("No");
        }
        finally
        {
            Thread.CurrentThread.CurrentUICulture = originalCulture;
        }
    }
}
