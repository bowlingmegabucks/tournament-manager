using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests.LaneAssignments.Retrieve;

[TestFixture]
internal class DataLayer
{
    private Mock<NortheastMegabuck.LaneAssignments.IRepository> _repository;

    private NortheastMegabuck.LaneAssignments.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.LaneAssignments.IRepository>();

        _dataLayer = new NortheastMegabuck.LaneAssignments.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public void Execute_RepositoryRetrieve_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _dataLayer.Execute(squadId);

        _repository.Verify(repository => repository.Retrieve(squadId), Times.Once);
    }

    [Test]
    public void Execute_ReturnsRepositoryRetrieve()
    {
        var laneAssignments = Enumerable.Repeat(new NortheastMegabuck.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            Squad = new NortheastMegabuck.Database.Entities.TournamentSquad(),
            RegistrationId = RegistrationId.New(),
            Registration = new NortheastMegabuck.Database.Entities.Registration
            {
                Bowler = new NortheastMegabuck.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new NortheastMegabuck.Database.Entities.Division
                {
                    Id = DivisionId.New()
                },
                Average = 200
            },
            LaneAssignment = "12C"
        }, 3);

        _repository.Setup(repository => repository.Retrieve(It.IsAny<SquadId>())).Returns(laneAssignments);

        var actual = _dataLayer.Execute(SquadId.New()).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));
            Assert.That(actual.All(laneAssignment => laneAssignment.Position == "12C"));
        });
    }
}
