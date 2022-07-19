
namespace NewEnglandClassic.Tests.Bowlers.Retrieve;

[TestFixture]
internal class ViewModel
{
    [Test]
    public void Constructor_IdMapped()
    {
        var model = new NewEnglandClassic.Models.Bowler
        {
            Id = Guid.NewGuid()
        };

        var viewModel = new NewEnglandClassic.Bowlers.Retrieve.ViewModel(model);

        Assert.That(viewModel.Id, Is.EqualTo(model.Id));
    }
}
