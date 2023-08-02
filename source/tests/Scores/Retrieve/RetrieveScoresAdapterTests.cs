
namespace NortheastMegabuck.Tests.Scores.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Scores.Retrieve.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Scores.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Scores.Retrieve.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Scores.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _adapter.Execute(squadId);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(squadId), Times.Once);
    }

    [Test]
    public void Execute_AdapterError_SetToBusinessLogicError()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        _adapter.Execute(SquadId.New());

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public void Execute_ReturnsAdapterExecute()
    {
        var score1 = new NortheastMegabuck.Models.SquadScore
        {
            GameNumber = 1,
            Score = 200
        };

        var score2 = new NortheastMegabuck.Models.SquadScore
        {
            GameNumber = 2,
            Score = 201
        };

        var scores = new[] { score1, score2 };
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<SquadId>())).Returns(scores);

        var actual = _adapter.Execute(SquadId.New()).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(2));

            Assert.That(actual[0].GameNumber, Is.EqualTo(1));
            Assert.That(actual[1].Score, Is.EqualTo(201));
        });
    }
}
