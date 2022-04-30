namespace NewEnglandClassic.Tests.Tournaments.Retrieve;

[TestFixture]
internal class Adapter
{
    private Mock<NewEnglandClassic.Tournaments.Retrieve.IBusinessLogic> _businessLogic;

    private NewEnglandClassic.Tournaments.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NewEnglandClassic.Tournaments.Retrieve.IBusinessLogic>();

        _adapter = new NewEnglandClassic.Tournaments.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_Called()
    {
        _adapter.Execute();

        _businessLogic.Verify(businessLogic => businessLogic.Execute(), Times.Once);
    }

    [Test]
    public void Execute_BusinessLogicErrorDetailNull_AdapterErrorDetailNull()
    {
        _businessLogic.Setup(businessLogic => businessLogic.ErrorDetail).Returns((NewEnglandClassic.Models.ErrorDetail)null);

        _adapter.Execute();

        Assert.That(_adapter.ErrorDetail, Is.Null);
    }

    [Test]
    public void Execute_BusinessLogicErrorDetailNotNull_AdapterErrorDetailEqualToBusinessLogicErrorDetail()
    {
        var errorDetail = new NewEnglandClassic.Models.ErrorDetail("message");
        _businessLogic.Setup(businessLogic => businessLogic.ErrorDetail).Returns(errorDetail);

        _adapter.Execute();

        Assert.That(_adapter.ErrorDetail, Is.EqualTo(errorDetail));
    }

    [Test]
    public void Execute_ReturnsBusinessLogicResponse()
    {
        var tournament1 = new NewEnglandClassic.Models.Tournament { EntryFee = 1 };
        var tournament2 = new NewEnglandClassic.Models.Tournament { EntryFee = 2 };
        var tournament3 = new NewEnglandClassic.Models.Tournament { EntryFee = 3 };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _businessLogic.Setup(businessLogic => businessLogic.Execute()).Returns(tournaments);

        var actual = _adapter.Execute();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.TypeOf<List<NewEnglandClassic.Tournaments.ViewModel>>());
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Any(tournament => tournament.EntryFee == 1), Is.True, "tournament1 Missing");
            Assert.That(actual.Any(tournament => tournament.EntryFee == 2), Is.True, "tournament2 Missing");
            Assert.That(actual.Any(tournament => tournament.EntryFee == 3), Is.True, "tournament3 Missing");
        });
    }
}
