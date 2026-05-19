using NUnit.Framework;

namespace BowlingMegabucks.TournamentManager.UnitTests.Models;

[TestFixture]
internal sealed class Gender
{
    [Test]
    public void Male_Enum_MapsToCorrectBackingInteger()
        => Assert.That(TournamentManager.Models.Gender.Male.Value, Is.EqualTo(0));

    [Test]
    public void Female_Enum_MapsToCorrectBackingInteger()
        => Assert.That(TournamentManager.Models.Gender.Female.Value, Is.EqualTo(1));
}
