namespace NortheastMegabuck.Tests.Bowlers.Search;

[TestFixture]
internal class ViewModel
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
            FirstName = "first name"
        };

        var viewModel = new NortheastMegabuck.Bowlers.Search.ViewModel(model);

        Assert.That(viewModel.FirstName, Is.EqualTo(model.FirstName));
    }

    [Test]
    public void Constructor_Model_LastNameMapped()
    {
        var model = new NortheastMegabuck.Models.Bowler
        {
            LastName = "last name"
        };

        var viewModel = new NortheastMegabuck.Bowlers.Search.ViewModel(model);

        Assert.That(viewModel.LastName, Is.EqualTo(model.LastName));
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
