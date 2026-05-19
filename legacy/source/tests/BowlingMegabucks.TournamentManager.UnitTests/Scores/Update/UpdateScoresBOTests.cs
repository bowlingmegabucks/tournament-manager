using BowlingMegabucks.TournamentManager.UnitTests.Extensions;
using NUnit.Framework.Legacy;

namespace BowlingMegabucks.TournamentManager.UnitTests.Scores.Update;

[TestFixture]
internal sealed class BusinessLogic
{
    private BowlerId _validScoreBowlerId;
    private BowlerId _invalidScoreBowlerId;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _validScoreBowlerId = BowlerId.New();
        _invalidScoreBowlerId = BowlerId.New();
    }

    private Mock<FluentValidation.IValidator<IEnumerable<TournamentManager.Models.SquadScore>>> _validator;
    private Mock<TournamentManager.Scores.Update.IDataLayer> _dataLayer;

    private TournamentManager.Scores.Update.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _validator = new Mock<FluentValidation.IValidator<IEnumerable<TournamentManager.Models.SquadScore>>>();

        var validResult = new FluentValidation.Results.ValidationResult();

        _validator.Setup(validator => validator.ValidateAsync(It.Is<IEnumerable<TournamentManager.Models.SquadScore>>(squadScores => squadScores.Select(squadScore => squadScore.Bowler.Id).Contains(_validScoreBowlerId)), It.IsAny<CancellationToken>())).ReturnsAsync(validResult);

        var invalidResult = new FluentValidation.Results.ValidationResult();
        invalidResult.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "errorMessage"));

        _validator.Setup(validator => validator.ValidateAsync(It.Is<IEnumerable<TournamentManager.Models.SquadScore>>(squadScores => squadScores.Select(squadScore => squadScore.Bowler.Id).Contains(_invalidScoreBowlerId)), It.IsAny<CancellationToken>())).ReturnsAsync(invalidResult);

        _dataLayer = new Mock<TournamentManager.Scores.Update.IDataLayer>();

        _businessLogic = new TournamentManager.Scores.Update.BusinessLogic(_validator.Object, _dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_ValidatorValidate_CalledForEachBowlersScores()
    {
        var score1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _validScoreBowlerId },
            GameNumber = 1
        };

        var score2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _invalidScoreBowlerId },
            GameNumber = 2
        };

        var score3 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _validScoreBowlerId },
            GameNumber = 3
        };

        var score4 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _invalidScoreBowlerId },
            GameNumber = 4
        };

        var scores = new[] { score1, score2, score3, score4 };
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(scores, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.ValidateAsync(It.IsAny<IEnumerable<TournamentManager.Models.SquadScore>>(), cancellationToken), Times.Exactly(2));

            _validator.Verify(validator => validator.ValidateAsync(It.Is<IEnumerable<TournamentManager.Models.SquadScore>>(squadScores => squadScores.All(squadScore => squadScore.Bowler.Id == _validScoreBowlerId) && squadScores.Any(squadScore => squadScore.GameNumber == 1) && squadScores.Any(squadScore => squadScore.GameNumber == 3)), cancellationToken), Times.Once);
            _validator.Verify(validator => validator.ValidateAsync(It.Is<IEnumerable<TournamentManager.Models.SquadScore>>(squadScores => squadScores.All(squadScore => squadScore.Bowler.Id == _invalidScoreBowlerId) && squadScores.Any(squadScore => squadScore.GameNumber == 2) && squadScores.Any(squadScore => squadScore.GameNumber == 4)), cancellationToken), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecute_CalledWithValidScores()
    {
        var score1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _validScoreBowlerId },
            GameNumber = 1
        };

        var score2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _invalidScoreBowlerId },
            GameNumber = 2
        };

        var score3 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _validScoreBowlerId },
            GameNumber = 3
        };

        var score4 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _invalidScoreBowlerId },
            GameNumber = 4
        };

        var scores = new[] { score1, score2, score3, score4 };
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(scores, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.Is<IEnumerable<TournamentManager.Models.SquadScore>>(squadScores => squadScores.All(squadScore => squadScore.Bowler.Id == _validScoreBowlerId)), cancellationToken), Times.Once);
            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.Is<IEnumerable<TournamentManager.Models.SquadScore>>(squadScores => squadScores.Any(squadScore => squadScore.Bowler.Id == _invalidScoreBowlerId)), cancellationToken), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ReturnsInvalidScores()
    {
        var score1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _validScoreBowlerId },
            GameNumber = 1
        };

        var score2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _invalidScoreBowlerId },
            GameNumber = 2
        };

        var score3 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _validScoreBowlerId },
            GameNumber = 3
        };

        var score4 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _invalidScoreBowlerId },
            GameNumber = 4
        };

        var scores = new[] { score1, score2, score3, score4 };

        var invalidScores = await _businessLogic.ExecuteAsync(scores, default).ConfigureAwait(false);

        Assert.That(invalidScores.All(score => score.Bowler.Id == _invalidScoreBowlerId));
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<IEnumerable<TournamentManager.Models.SquadScore>>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var score1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _validScoreBowlerId },
            GameNumber = 1
        };

        var score2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _invalidScoreBowlerId },
            GameNumber = 2
        };

        var score3 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _validScoreBowlerId },
            GameNumber = 3
        };

        var score4 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = _invalidScoreBowlerId },
            GameNumber = 4
        };

        var scores = new[] { score1, score2, score3, score4 };

        var invalidScores = await _businessLogic.ExecuteAsync(scores, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("exception");

            CollectionAssert.AreEqual(scores, invalidScores);
        });
    }
}
