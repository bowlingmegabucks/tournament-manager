using NUnit.Framework;

namespace BowlingMegabucks.TournamentManager.Tests.Models;

[TestFixture]
internal sealed class Gender
{
    [Test]
    public void Male_Enum_MapsToCorrectBackingInteger()
        => Assert.That(BowlingMegabucks.TournamentManager.Models.Gender.Male.Value, Is.EqualTo(0));

    [Test]
    public void Female_Enum_MapsToCorrectBackingInteger()
        => Assert.That(BowlingMegabucks.TournamentManager.Models.Gender.Female.Value, Is.EqualTo(1));
}
