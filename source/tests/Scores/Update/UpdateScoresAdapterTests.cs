
namespace NortheastMegabuck.Tests.Scores.Update;

[TestFixture]
internal class Adapter
{
    private Mock<NortheastMegabuck.Scores.Update.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Scores.Update.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Scores.Update.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Scores.Update.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();

        var score1 = new NortheastMegabuck.Scores.Update.ViewModel
        {
            SquadId = squadId,
            BowlerId = BowlerId.New(),
            GameNumber = 1,
            Score = 200
        };

        var score2 = new NortheastMegabuck.Scores.Update.ViewModel
        {
            SquadId = squadId,
            BowlerId = score1.BowlerId,
            GameNumber = 2,
            Score = 201
        };

        var score3 = new NortheastMegabuck.Scores.Update.ViewModel
        {
            SquadId = squadId,
            BowlerId = BowlerId.New(),
            GameNumber = 1,
            Score = 202
        };

        var scores = new[] { score1, score2, score3 };

        _adapter.Execute(scores);

        Assert.Multiple(() =>
        {
            _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.Count() == 3)), Times.Once);

            _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.All(squadScore=> squadScore.SquadId == squadId))), Times.Once);
            _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.Count(squadScore=> squadScore.BowlerId == score1.BowlerId) == 2)), Times.Once);
            _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.Count(squadScore => squadScore.BowlerId == score3.BowlerId) == 1)), Times.Once);
        });
    }

    [Test]
    public void Execute_ErrorsSetToBusinessLogicErrors()
    {
        var errors = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("error"), 2);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        _adapter.Execute(Enumerable.Empty<NortheastMegabuck.Scores.Update.IViewModel>());

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public void Execute_InvalidScoresReturnedIfAny()
    {
        var invalidScore1 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = BowlerId.New(),
            GameNumber = 1,
            Score = 200
        };

        var invalidScore2 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = BowlerId.New(),
            GameNumber = 2,
            Score = 201
        };

        var invalidScores = new[] { invalidScore1, invalidScore2 };
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<IEnumerable<NortheastMegabuck.Models.SquadScore>>())).Returns(invalidScores);

        var result = _adapter.Execute(Enumerable.Empty<NortheastMegabuck.Scores.Update.IViewModel>()).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result.Count(score => score.BowlerId == invalidScore1.BowlerId), Is.EqualTo(1));
            Assert.That(result.Count(score => score.BowlerId == invalidScore2.BowlerId), Is.EqualTo(1));
        });
    }
}
