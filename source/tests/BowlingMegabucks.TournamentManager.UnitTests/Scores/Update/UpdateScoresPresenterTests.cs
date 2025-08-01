﻿using BowlingMegabucks.TournamentManager.Scores;

namespace BowlingMegabucks.TournamentManager.UnitTests.Scores.Update;

[TestFixture]
internal sealed class Presenter
{
    private Mock<TournamentManager.Scores.Update.IView> _view;
    private Mock<TournamentManager.Scores.Update.IAdapter> _adapter;

    private TournamentManager.Scores.Update.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<TournamentManager.Scores.Update.IView>();
        _adapter = new Mock<TournamentManager.Scores.Update.IAdapter>();

        _presenter = new TournamentManager.Scores.Update.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public async Task ExecuteAsync_AdapterExecute_CalledCorrectly()
    {
        var scores = new Mock<IEnumerable<IViewModel>>();
        _view.SetupGet(view => view.Scores).Returns(scores.Object);

        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _adapter.Verify(adapter => adapter.ExecuteAsync(scores.Object, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_AdapterHasErrors_ErrorFlow()
    {
        var error1 = new TournamentManager.Models.ErrorDetail("error1");
        var error2 = new TournamentManager.Models.ErrorDetail("error2");

        var errors = new[] { error1, error2 };
        _adapter.SetupGet(adapter => adapter.Errors).Returns(errors);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError($"error1{Environment.NewLine}error2"), Times.Once);
            _view.Verify(view => view.KeepOpen(), Times.Once);

            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_AdapterHasNoErrors_SuccessFlow()
    {
        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("Scores updated"), Times.Once);

            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.KeepOpen(), Times.Never);
        });
    }
}
