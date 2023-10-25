namespace NortheastMegabuck.Tests.Models;

[TestFixture]
internal sealed class Gender
{
    [TestCase(NortheastMegabuck.Models.Gender.Male, 0)]
    [TestCase(NortheastMegabuck.Models.Gender.Female, 1)]
    public void Enum_MapsToCorrectBackingInteger(NortheastMegabuck.Models.Gender gender, int backingInt)
        => Assert.That((int)gender, Is.EqualTo(backingInt));
}
