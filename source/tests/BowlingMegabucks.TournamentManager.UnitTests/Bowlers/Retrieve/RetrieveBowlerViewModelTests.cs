
namespace BowlingMegabucks.TournamentManager.UnitTests.Bowlers.Retrieve;

[TestFixture]
internal sealed class ViewModel
{
    [Test]
    public void Constructor_IdMapped()
    {
        var model = new TournamentManager.Models.Bowler
        {
            Id = BowlerId.New()
        };

        var viewModel = new TournamentManager.Bowlers.Retrieve.ViewModel(model);

        Assert.That(viewModel.Id, Is.EqualTo(model.Id));
    }

    [Test]
    public void Constructor_FirstNameMapped()
    {
        var model = new TournamentManager.Models.Bowler
        {
            Name = new TournamentManager.Models.PersonName { First = "first" }
        };

        var viewModel = new TournamentManager.Bowlers.Retrieve.ViewModel(model);

        Assert.That(viewModel.FirstName, Is.EqualTo(model.Name.First));
    }

    [Test]
    public void Constructor_MiddleInitialMapped()
    {
        var model = new TournamentManager.Models.Bowler
        {
            Name = new TournamentManager.Models.PersonName { MiddleInitial = "middle" }
        };

        var viewModel = new TournamentManager.Bowlers.Retrieve.ViewModel(model);

        Assert.That(viewModel.MiddleInitial, Is.EqualTo(model.Name.MiddleInitial));
    }

    [Test]
    public void Constructor_LastNameMapped()
    {
        var model = new TournamentManager.Models.Bowler
        {
            Name = new TournamentManager.Models.PersonName { Last = "last" }
        };

        var viewModel = new TournamentManager.Bowlers.Retrieve.ViewModel(model);

        Assert.That(viewModel.LastName, Is.EqualTo(model.Name.Last));
    }

    [Test]
    public void Constructor_SuffixMapped()
    {
        var model = new TournamentManager.Models.Bowler
        {
            Name = new TournamentManager.Models.PersonName { Suffix = "suffix" }
        };

        var viewModel = new TournamentManager.Bowlers.Retrieve.ViewModel(model);

        Assert.That(viewModel.Suffix, Is.EqualTo(model.Name.Suffix));
    }
}
