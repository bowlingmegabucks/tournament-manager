using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewEnglandClassic.Tests.Extensions;

namespace NewEnglandClassic.Tests.Tournaments;

[TestFixture]
internal class TournamentsRepositoryTests
{
    private Mock<NewEnglandClassic.Database.IDataContext> _dataContext;

    private NewEnglandClassic.Tournaments.IRepository _tournamentsRepository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NewEnglandClassic.Database.IDataContext>();

        _tournamentsRepository = new NewEnglandClassic.Tournaments.Repository(_dataContext.Object);
    }

    [Test]
    public void RetrieveAll_ReturnsAllTournaments()
    {
        var tournament1 = new NewEnglandClassic.Database.Entities.Tournament { Id = Guid.NewGuid() };
        var tournament2 = new NewEnglandClassic.Database.Entities.Tournament { Id = Guid.NewGuid() };
        var tournament3 = new NewEnglandClassic.Database.Entities.Tournament { Id = Guid.NewGuid() };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(tournaments.SetUpDbContext());

        var actual = _tournamentsRepository.RetrieveAll();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Any(tournament => tournament.Id == tournament1.Id), Is.True, "tournament1 Id Not Found");
            Assert.That(actual.Any(tournament => tournament.Id == tournament2.Id), Is.True, "tournament2 Id Not Found");
            Assert.That(actual.Any(tournament => tournament.Id == tournament3.Id), Is.True, "tournament3 Id Not Found");
        });
    }
}
