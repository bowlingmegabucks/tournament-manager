namespace NortheastMegabuck.Tests.Bowlers.Search;

[TestFixture]
internal sealed class ViewModel
{
    [Test]
    public void Constructor_Model_IdMapped()
    {
        var model = new NortheastMegabuck.Models.Bowler
        {
            Id = BowlerId.New()
        };

        var viewModel = new NortheastMegabuck.Bowlers.Search.ViewModel(model);

        Assert.That(viewModel.Id, Is.EqualTo(model.Id));
    }

    [Test]
    public void Constructor_Model_FirstNameMapped()
    {
        var model = new NortheastMegabuck.Models.Bowler
        {
            Name = new NortheastMegabuck.Models.PersonName { First = "first name" }
        };

        var viewModel = new NortheastMegabuck.Bowlers.Search.ViewModel(model);

        Assert.That(viewModel.FirstName, Is.EqualTo(model.Name.First));
    }

    [Test]
    public void Constructor_Model_LastNameMapped()
    {
        var model = new NortheastMegabuck.Models.Bowler
        {
            Name = new NortheastMegabuck.Models.PersonName { Last = "last Name" }
        };

        var viewModel = new NortheastMegabuck.Bowlers.Search.ViewModel(model);

        Assert.That(viewModel.LastName, Is.EqualTo(model.Name.Last));
    }

    [Test]
    public void Constructor_Model_EmailAddressMapped()
    {
        var model = new NortheastMegabuck.Models.Bowler
        {
            EmailAddress = "tetst@gmail.com"
        };

        var viewModel = new NortheastMegabuck.Bowlers.Search.ViewModel(model);

        Assert.That(viewModel.EmailAddress, Is.EqualTo(model.EmailAddress));
    }

    [Test]
    public void Constructor_Model_CityMapped()
    {
        var model = new NortheastMegabuck.Models.Bowler
        {
            CityAddress = "city"
        };

        var viewModel = new NortheastMegabuck.Bowlers.Search.ViewModel(model);

        Assert.That(viewModel.City, Is.EqualTo(model.CityAddress));
    }

    [Test]
    public void Constructor_Model_StateMapped()
    {
        var model = new NortheastMegabuck.Models.Bowler
        {
            StateAddress = "state"
        };

        var viewModel = new NortheastMegabuck.Bowlers.Search.ViewModel(model);

        Assert.That(viewModel.State, Is.EqualTo(model.StateAddress));
    }
}
