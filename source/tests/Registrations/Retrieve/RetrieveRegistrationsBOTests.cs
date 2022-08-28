using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Tests.Registrations.Retrieve;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NewEnglandClassic.Registrations.Retrieve.IDataLayer> _dataLayer;

    private NewEnglandClassic.Registrations.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NewEnglandClassic.Registrations.Retrieve.IDataLayer>();

        _businessLogic = new NewEnglandClassic.Registrations.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public void Execute_TournamentId_DataLayerExecute_CalledCorrectly()
    {
        var tournamentId = NewEnglandClassic.TournamentId.New();

        _businessLogic.Execute(tournamentId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_TournamentId_ReturnsDataLayerExecute()
    {
        var registrations = Enumerable.Repeat(new NewEnglandClassic.Models.Registration(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.TournamentId>())).Returns(registrations);

        var tournamentId = NewEnglandClassic.TournamentId.New();

        var actual = _businessLogic.Execute(tournamentId);

        Assert.That(actual, Is.EqualTo(registrations));
    }

    [Test]
    public void Execute_TournamentId_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.TournamentId>())).Throws(new Exception("exception"));

        var tournamentId = NewEnglandClassic.TournamentId.New();

        var actual = _businessLogic.Execute(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Empty);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }
}
