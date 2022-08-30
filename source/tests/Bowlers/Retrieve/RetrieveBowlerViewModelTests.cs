
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
}
