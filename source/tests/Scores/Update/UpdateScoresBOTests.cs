
using NortheastMegabuck.Database.Entities;
using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Scores.Update;

[TestFixture]
internal class BusinessLogic
{
    private BowlerId _validScoreBowlerId;
    private BowlerId _invalidScoreBowlerId;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _validScoreBowlerId = BowlerId.New();
        _invalidScoreBowlerId = BowlerId.New();
    }

    private Mock<FluentValidation.IValidator<IEnumerable<NortheastMegabuck.Models.SquadScore>>> _validator;
    private Mock<NortheastMegabuck.Scores.Update.IDataLayer> _dataLayer;

    private NortheastMegabuck.Scores.Update.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _validator = new Mock<FluentValidation.IValidator<IEnumerable<NortheastMegabuck.Models.SquadScore>>>();

        var validResult = new FluentValidation.Results.ValidationResult();

        _validator.Setup(validator => validator.Validate(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.Select(squadScore => squadScore.BowlerId).Contains(_validScoreBowlerId)))).Returns(validResult);

        var invalidResult = new FluentValidation.Results.ValidationResult();
        invalidResult.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "errorMessage"));

        _validator.Setup(validator => validator.Validate(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.Select(squadScore => squadScore.BowlerId).Contains(_invalidScoreBowlerId)))).Returns(invalidResult);

        _dataLayer = new Mock<NortheastMegabuck.Scores.Update.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Scores.Update.BusinessLogic(_validator.Object, _dataLayer.Object);
    }

    [Test]
    public void Execute_ValidatorValidate_CalledForEachBowlersScores()
    {
        var score1 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _validScoreBowlerId,
            GameNumber = 1
        };

        var score2 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _invalidScoreBowlerId,
            GameNumber = 2
        };

        var score3 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _validScoreBowlerId,
            GameNumber = 3
        };

        var score4 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _invalidScoreBowlerId,
            GameNumber = 4
        };

        var scores = new[] { score1, score2, score3, score4 };

        _businessLogic.Execute(scores);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.Validate(It.IsAny<IEnumerable<NortheastMegabuck.Models.SquadScore>>()), Times.Exactly(2));

            _validator.Verify(validator => validator.Validate(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.All(squadScore => squadScore.BowlerId == _validScoreBowlerId) && squadScores.Any(squadScore => squadScore.GameNumber == 1) && squadScores.Any(squadScore => squadScore.GameNumber == 3))), Times.Once);
            _validator.Verify(validator => validator.Validate(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.All(squadScore => squadScore.BowlerId == _invalidScoreBowlerId) && squadScores.Any(squadScore => squadScore.GameNumber == 2) && squadScores.Any(squadScore => squadScore.GameNumber == 4))), Times.Once);
        });
    }

    [Test]
    public void Execute_DataLayerExecute_CalledWithValidScores()
    {
        var score1 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _validScoreBowlerId,
            GameNumber = 1
        };

        var score2 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _invalidScoreBowlerId,
            GameNumber = 2
        };

        var score3 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _validScoreBowlerId,
            GameNumber = 3
        };

        var score4 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _invalidScoreBowlerId,
            GameNumber = 4
        };

        var scores = new[] { score1, score2, score3, score4 };

        _businessLogic.Execute(scores);

        Assert.Multiple(() =>
        {
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.All(squadScore => squadScore.BowlerId == _validScoreBowlerId))), Times.Once);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.Is<IEnumerable<NortheastMegabuck.Models.SquadScore>>(squadScores => squadScores.Any(squadScore => squadScore.BowlerId == _invalidScoreBowlerId))), Times.Never);
        });
    }

    [Test]
    public void Execute_ReturnsInvalidScores()
    {
        var score1 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _validScoreBowlerId,
            GameNumber = 1
        };

        var score2 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _invalidScoreBowlerId,
            GameNumber = 2
        };

        var score3 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _validScoreBowlerId,
            GameNumber = 3
        };

        var score4 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _invalidScoreBowlerId,
            GameNumber = 4
        };

        var scores = new[] { score1, score2, score3, score4 };

        var invalidScores = _businessLogic.Execute(scores);

        Assert.That(invalidScores.All(score => score.BowlerId == _invalidScoreBowlerId));
    }

    [Test]
    public void Execute_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<IEnumerable<NortheastMegabuck.Models.SquadScore>>())).Throws(ex);

        var score1 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _validScoreBowlerId,
            GameNumber = 1
        };

        var score2 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _invalidScoreBowlerId,
            GameNumber = 2
        };

        var score3 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _validScoreBowlerId,
            GameNumber = 3
        };

        var score4 = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = _invalidScoreBowlerId,
            GameNumber = 4
        };

        var scores = new[] { score1, score2, score3, score4 };

        var invalidScores = _businessLogic.Execute(scores);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("exception");

            CollectionAssert.AreEqual(scores, invalidScores);
        });
    }
}
