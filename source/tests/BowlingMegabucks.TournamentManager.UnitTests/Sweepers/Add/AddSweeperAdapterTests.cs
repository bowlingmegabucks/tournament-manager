﻿namespace BowlingMegabucks.TournamentManager.UnitTests.Sweepers.Add;

[TestFixture]
internal sealed class Adapter
{
    private Mock<TournamentManager.Sweepers.Add.IBusinessLogic> _businessLogic;

    private TournamentManager.Sweepers.Add.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<TournamentManager.Sweepers.Add.IBusinessLogic>();

        _adapter = new TournamentManager.Sweepers.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var id = SquadId.New();

        var viewModel = new Mock<TournamentManager.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.Id).Returns(id);

        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(viewModel.Object, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(It.Is<TournamentManager.Models.Sweeper>(sweeper => sweeper.Id == id), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ErrorsSetToBusinessLogicErrors([Range(0, 2)] int errorCount)
    {
        var errors = Enumerable.Repeat(new TournamentManager.Models.ErrorDetail("error"), errorCount);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var viewModel = new Mock<TournamentManager.Sweepers.IViewModel>();

        await _adapter.ExecuteAsync(viewModel.Object, default).ConfigureAwait(false);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecuteReturnsNull_NullReturned()
    {
        SquadId? noId = null;
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<TournamentManager.Models.Sweeper>(), It.IsAny<CancellationToken>())).ReturnsAsync(noId);

        var viewModel = new Mock<TournamentManager.Sweepers.IViewModel>();

        var actual = await _adapter.ExecuteAsync(viewModel.Object, default).ConfigureAwait(false);

        Assert.That(actual, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecuteReturnsId_IdReturned()
    {
        var id = SquadId.New();
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<TournamentManager.Models.Sweeper>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var viewModel = new Mock<TournamentManager.Sweepers.IViewModel>();

        var actual = await _adapter.ExecuteAsync(viewModel.Object, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(id));
    }
}
