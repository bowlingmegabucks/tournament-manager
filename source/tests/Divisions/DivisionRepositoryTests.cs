using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Divisions;

[TestFixture]
internal sealed class Repository
{

    private Mock<NortheastMegabuck.Database.IDataContext> _dataContext;

    private NortheastMegabuck.Divisions.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NortheastMegabuck.Database.IDataContext>();

        _repository = new NortheastMegabuck.Divisions.Repository(_dataContext.Object);
    }

    [Test]
    public async Task AddAsync_DivisionAddedWithId()
    {
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.Division>().SetUpDbContext());

        var division = new NortheastMegabuck.Database.Entities.Division();

        var id = await _repository.AddAsync(division, default).ConfigureAwait(false);

        Assert.That(division.Id, Is.EqualTo(id));
    }

    [Test]
    public async Task AddAsync_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.Division>().SetUpDbContext());

        var division = new NortheastMegabuck.Database.Entities.Division();
        CancellationToken cancellationToken = default;

        await _repository.AddAsync(division, cancellationToken).ConfigureAwait(false);

        _dataContext.Verify(dataContext => dataContext.SaveChangesAsync(cancellationToken), Times.Once);
    }

    [Test]
    public void Execute_ReturnsDivisionsForSelectedTournament()
    {
        var tournamentId = TournamentId.New();

        var division1 = new NortheastMegabuck.Database.Entities.Division
        {
            Id = DivisionId.New(),
            TournamentId = tournamentId,
            Name = "Yes"
        };

        var division2 = new NortheastMegabuck.Database.Entities.Division
        {
            Id = DivisionId.New(),
            TournamentId = tournamentId,
            Name = "Yes"
        };

        var division3 = new NortheastMegabuck.Database.Entities.Division
        {
            Id = DivisionId.New(),
            TournamentId = TournamentId.New(),
            Name = "No"
        };

        var divisions = new[] { division1, division2, division3 };
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(divisions.SetUpDbContext());

        var actual = _repository.Retrieve(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(2));
            Assert.That(actual.Count(division => division.Name == "Yes"), Is.EqualTo(2));
        });
    }

    [Test]
    public async Task RetrieveAsync_ReturnsDivision()
    {
        var divisionId = DivisionId.New();

        var division1 = new NortheastMegabuck.Database.Entities.Division
        {
            Id = divisionId,
            TournamentId = TournamentId.New(),
            Name = "Yes"
        };

        var division2 = new NortheastMegabuck.Database.Entities.Division
        {
            Id = DivisionId.New(),
            TournamentId = TournamentId.New(),
            Name = "No"
        };

        var division3 = new NortheastMegabuck.Database.Entities.Division
        {
            Id = DivisionId.New(),
            TournamentId = TournamentId.New(),
            Name = "No"
        };

        var divisions = new[] { division1, division2, division3 };
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(divisions.SetUpDbContext());

        var division = await _repository.RetrieveAsync(divisionId, default).ConfigureAwait(false);

        Assert.That(division.Id, Is.EqualTo(divisionId));
    }
}
