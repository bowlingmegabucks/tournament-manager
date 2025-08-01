﻿using BowlingMegabucks.TournamentManager.UnitTests.Extensions;

namespace BowlingMegabucks.TournamentManager.UnitTests.Divisions;

[TestFixture]
internal sealed class Repository
{

    private Mock<TournamentManager.Database.IDataContext> _dataContext;

    private TournamentManager.Divisions.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<TournamentManager.Database.IDataContext>();

        _repository = new TournamentManager.Divisions.Repository(_dataContext.Object);
    }

    [Test]
    public async Task AddAsync_DivisionAddedWithId()
    {
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(Enumerable.Empty<TournamentManager.Database.Entities.Division>().SetUpDbContext());

        var division = new TournamentManager.Database.Entities.Division();

        var id = await _repository.AddAsync(division, default).ConfigureAwait(false);

        Assert.That(division.Id, Is.EqualTo(id));
    }

    [Test]
    public async Task AddAsync_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(Enumerable.Empty<TournamentManager.Database.Entities.Division>().SetUpDbContext());

        var division = new TournamentManager.Database.Entities.Division();
        CancellationToken cancellationToken = default;

        await _repository.AddAsync(division, cancellationToken).ConfigureAwait(false);

        _dataContext.Verify(dataContext => dataContext.SaveChangesAsync(cancellationToken), Times.Once);
    }

    [Test]
    public void Execute_ReturnsDivisionsForSelectedTournament()
    {
        var tournamentId = TournamentId.New();

        var division1 = new TournamentManager.Database.Entities.Division
        {
            Id = DivisionId.New(),
            TournamentId = tournamentId,
            Name = "Yes"
        };

        var division2 = new TournamentManager.Database.Entities.Division
        {
            Id = DivisionId.New(),
            TournamentId = tournamentId,
            Name = "Yes"
        };

        var division3 = new TournamentManager.Database.Entities.Division
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

        var division1 = new TournamentManager.Database.Entities.Division
        {
            Id = divisionId,
            TournamentId = TournamentId.New(),
            Name = "Yes"
        };

        var division2 = new TournamentManager.Database.Entities.Division
        {
            Id = DivisionId.New(),
            TournamentId = TournamentId.New(),
            Name = "No"
        };

        var division3 = new TournamentManager.Database.Entities.Division
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
