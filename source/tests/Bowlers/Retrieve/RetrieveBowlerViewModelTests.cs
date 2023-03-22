
namespace NortheastMegabuck.Tests.Bowlers.Retrieve;

[TestFixture]
internal class ViewModel
{
    [Test]
    public void Constructor_IdMapped()
    {
        var model = new NortheastMegabuck.Models.Bowler
        {
            Id = BowlerId.New()
        };

        var viewModel = new NortheastMegabuck.Bowlers.Retrieve.ViewModel(model);

        Assert.That(viewModel.Id, Is.EqualTo(model.Id));
    }

    [Test]
    public void Constructor_FirstNameMapped()
    {
        var model = new NortheastMegabuck.Models.Bowler
        {
            Name = new NortheastMegabuck.Models.PersonName { First = "first"}
        };

        var viewModel = new NortheastMegabuck.Bowlers.Retrieve.ViewModel(model);

        Assert.That(viewModel.FirstName, Is.EqualTo(model.Name.First));
    }

    [Test]
    public void Constructor_MiddleInitialMapped()
    {
        var model = new NortheastMegabuck.Models.Bowler
        {
            Name = new NortheastMegabuck.Models.PersonName { MiddleInitial = "middle"}
        };

        var viewModel = new NortheastMegabuck.Bowlers.Retrieve.ViewModel(model);

        Assert.That(viewModel.MiddleInitial, Is.EqualTo(model.Name.MiddleInitial));
    }

    [Test]
    public void Constructor_LastNameMapped()
    {
        var model = new NortheastMegabuck.Models.Bowler
        {
            Name = new NortheastMegabuck.Models.PersonName { Last = "last"}
        };

        var viewModel = new NortheastMegabuck.Bowlers.Retrieve.ViewModel(model);

        Assert.That(viewModel.LastName, Is.EqualTo(model.Name.Last));
    }

    [Test]
    public void Constructor_SuffixMapped()
    {
        var model = new NortheastMegabuck.Models.Bowler
        {
            Name = new NortheastMegabuck.Models.PersonName { Suffix = "suffix"}
        };

        var viewModel = new NortheastMegabuck.Bowlers.Retrieve.ViewModel(model);

        Assert.That(viewModel.Suffix, Is.EqualTo(model.Name.Suffix));
    }
}
