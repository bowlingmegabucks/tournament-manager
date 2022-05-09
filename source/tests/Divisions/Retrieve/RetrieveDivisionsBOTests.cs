using NewEnglandClassic.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Tests.Divisions.Retrieve;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NewEnglandClassic.Divisions.Retrieve.IDataLayer> _dataLayer;

    private NewEnglandClassic.Divisions.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NewEnglandClassic.Divisions.Retrieve.IDataLayer>();

        _businessLogic = new NewEnglandClassic.Divisions.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public void ForTournament_DataLayerForTournament_CalledCorrectly()
    {
        var tournamentId = Guid.NewGuid();

        _businessLogic.ForTournament(tournamentId);

        _dataLayer.Verify(dataLayer => dataLayer.ForTournament(tournamentId), Times.Once);
    }

    [Test]
    public void ForTournament_ReturnsDataLayerForTournamentResults()
    {
        var divisions = Enumerable.Repeat(new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ForTournament(It.IsAny<Guid>())).Returns(divisions);

        var tournamentId = Guid.NewGuid();

        var actual = _businessLogic.ForTournament(tournamentId);

        Assert.That(actual, Is.EqualTo(divisions));
    }

    [Test]
    public void ForTournament_DataLayerForTournamentNoException_ErrorsEmpty()
    {
        var divisions = Enumerable.Repeat(new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ForTournament(It.IsAny<Guid>())).Returns(divisions);

        var tournamentId = Guid.NewGuid();

         _businessLogic.ForTournament(tournamentId);

        Assert.That(_businessLogic.Errors, Is.Empty);
    }

    [Test]
    public void ForTournament_DataLayerForTournamentThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ForTournament(It.IsAny<Guid>())).Throws(ex);

        var tournamentId = Guid.NewGuid();

        var actual = _businessLogic.ForTournament(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Empty);
            _businessLogic.Errors.HasErrorMessage("exception");
        });
    }
}