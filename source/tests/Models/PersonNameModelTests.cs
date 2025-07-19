
using BowlingMegabucks.TournamentManager.Bowlers.Update;

namespace BowlingMegabucks.TournamentManager.Tests.Models;

[TestFixture]
internal sealed class PersonName
{
    [Test]
    public void Constructor_UpdateBowlerNameViewModel_FirstMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Update.INameViewModel>();
        viewModel.SetupGet(v => v.FirstName).Returns("firstName");

        var model = viewModel.Object.ToPersonName();

        Assert.That(model.First, Is.EqualTo("firstName"));
    }

    [Test]
    public void Constructor_UpdateBowlerNameViewModel_MiddleInitialMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Update.INameViewModel>();
        viewModel.SetupGet(v => v.MiddleInitial).Returns("m");

        var model = viewModel.Object.ToPersonName();

        Assert.That(model.MiddleInitial, Is.EqualTo("m"));
    }

    [Test]
    public void Constructor_UpdateBowlerNameViewModel_LastMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Update.INameViewModel>();
        viewModel.SetupGet(v => v.LastName).Returns("lastName");

        var model = viewModel.Object.ToPersonName();

        Assert.That(model.Last, Is.EqualTo("lastName"));
    }

    [Test]
    public void Constructor_UpdateBowlerNameViewModel_SuffixMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Update.INameViewModel>();
        viewModel.SetupGet(v => v.Suffix).Returns("suffix");

        var model = viewModel.Object.ToPersonName();

        Assert.That(model.Suffix, Is.EqualTo("suffix"));
    }

    [Test]
    public void ToString_NoSuffix_ReturnsFirstLastName()
    {
        var name = new BowlingMegabucks.TournamentManager.Models.PersonName
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
        var name = new BowlingMegabucks.TournamentManager.Models.PersonName
        {
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
