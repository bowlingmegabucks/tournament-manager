namespace NewEnglandClassic.Tests.Models;

[TestFixture]
internal class Gender
{
    [TestCase(NewEnglandClassic.Models.Gender.Male, 0)]
    [TestCase(NewEnglandClassic.Models.Gender.Female, 1)]
    public void Enum_MapsToCorrectBackingInteger(NewEnglandClassic.Models.Gender gender, int backingInt)
        => Assert.That((int)gender, Is.EqualTo(backingInt));
}
