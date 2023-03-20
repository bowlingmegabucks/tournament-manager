
namespace NortheastMegabuck.Tests.Models;

[TestFixture]
internal class PersonName
{
    [Test]
    public void ToString_NoSuffix_ReturnsFirstLastName()
    {
        var name = new NortheastMegabuck.Models.PersonName
        {
            First = "first",
            MiddleInitial = "m",
            Last = "last"
        };

        var expected = "first last";
        var actual = name.ToString();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ToString_Suffix_ReturnsFirstLastCommaSuffix()
    {
        var name = new NortheastMegabuck.Models.PersonName       {
            First = "first",
            MiddleInitial = "m",
            Last = "last",
            Suffix = "suffix"
        };

        var expected = "first last, suffix";
        var actual = name.ToString();

        Assert.That(actual, Is.EqualTo(expected));
    }
}
