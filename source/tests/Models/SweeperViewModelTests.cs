
namespace NewEnglandClassic.Tests.Models;

[TestFixture]
internal class Sweeper
{
    [Test]
    public void Constructor_IViewModel_IdMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.Id).Returns(Guid.NewGuid());

        var model = new NewEnglandClassic.Models.Sweeper(viewModel.Object);

        Assert.That(model.Id, Is.EqualTo(viewModel.Object.Id));
    }

    [Test]
    public void Constructor_IViewModel_TournamentIdMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.TournamentId).Returns(Guid.NewGuid());

        var model = new NewEnglandClassic.Models.Sweeper(viewModel.Object);

        Assert.That(model.TournamentId, Is.EqualTo(viewModel.Object.TournamentId));
    }

    [Test]
    public void Constructor_IViewModel_EntryFeeMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.EntryFee).Returns(123.45m);

        var model = new NewEnglandClassic.Models.Sweeper(viewModel.Object);

        Assert.That(model.EntryFee, Is.EqualTo(123.45m));
    }

    [Test]
    public void Constructor_IViewModel_GamesMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.Games).Returns(5);

        var model = new NewEnglandClassic.Models.Sweeper(viewModel.Object);

        Assert.That(model.Games, Is.EqualTo(5));
    }

    [Test]
    public void Constructor_IViewModel_CashRatioMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.CashRatio).Returns(1.3m);

        var model = new NewEnglandClassic.Models.Sweeper(viewModel.Object);

        Assert.That(model.CashRatio, Is.EqualTo(1.3m));
    }

    [Test]
    public void Constructor_IViewModel_DateMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.Date).Returns(DateTime.Now);

        var model = new NewEnglandClassic.Models.Sweeper(viewModel.Object);

        Assert.That(model.Date, Is.EqualTo(viewModel.Object.Date));
    }

    [Test]
    public void Constructor_IViewModel_MaxPerPairMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.MaxPerPair).Returns(3);

        var model = new NewEnglandClassic.Models.Sweeper(viewModel.Object);

        Assert.That(model.MaxPerPair, Is.EqualTo(3));
    }

    [Test]
    public void Constructor_IViewModel_CompleteMapped([Values] bool complete)
    {
        var viewModel = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.Complete).Returns(complete);

        var model = new NewEnglandClassic.Models.Sweeper(viewModel.Object);

        Assert.That(model.Complete, Is.EqualTo(complete));
    }

    [Test]
    public void Constructor_IViewModel_DivisionsMapped()
    {
        var divisions = new Mock<IDictionary<Guid, int?>>();

        var viewModel = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.Divisions).Returns(divisions.Object);

        var model = new NewEnglandClassic.Models.Sweeper(viewModel.Object);

        Assert.That(model.Divisions, Is.EqualTo(divisions.Object));
    }
}
