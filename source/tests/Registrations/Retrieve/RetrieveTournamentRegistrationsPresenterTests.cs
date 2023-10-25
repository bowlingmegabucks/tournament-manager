namespace NortheastMegabuck.Tests.Registrations.Retrieve;

[TestFixture]
internal sealed class TournamentRegistrationsPresenter
{
    private Mock<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationsView> _view;
    private Mock<NortheastMegabuck.Registrations.Retrieve.IAdapter> _registrationsAdapter;
    private Mock<NortheastMegabuck.Squads.Retrieve.IAdapter> _squadsAdapter;
    private Mock<NortheastMegabuck.Sweepers.Retrieve.IAdapter> _sweepersAdapter;
    private Mock<NortheastMegabuck.Registrations.Delete.IAdapter> _deleteAdapter;

    private NortheastMegabuck.Registrations.Retrieve.TournamentRegistrationsPresenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationsView>();
        _registrationsAdapter = new Mock<NortheastMegabuck.Registrations.Retrieve.IAdapter>();
        _squadsAdapter = new Mock<NortheastMegabuck.Squads.Retrieve.IAdapter>();
        _sweepersAdapter = new Mock<NortheastMegabuck.Sweepers.Retrieve.IAdapter>();
        _deleteAdapter = new Mock<NortheastMegabuck.Registrations.Delete.IAdapter>();

        _presenter = new NortheastMegabuck.Registrations.Retrieve.TournamentRegistrationsPresenter(_view.Object, _registrationsAdapter.Object, _squadsAdapter.Object, _sweepersAdapter.Object, _deleteAdapter.Object);
    }

    [Test]
    public async Task ExecuteAsync_RegistrationAdapterExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _registrationsAdapter.Verify(registrationsAdapter => registrationsAdapter.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadAdapterExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _squadsAdapter.Verify(squadsAdapter => squadsAdapter.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SweepersAdapterExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        CancellationToken cancellationToken = default;
        await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _sweepersAdapter.Verify(sweepersAdapter => sweepersAdapter.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TwoAdaptersHasSameError_ErrorDisplayedCorrectly()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _registrationsAdapter.SetupGet(registrationAdapter => registrationAdapter.Error).Returns(error);
        _squadsAdapter.SetupGet(squadsAdapter => squadsAdapter.Error).Returns(error);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.DisplayError("error"), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_AllAdaptersHaveDifferentErrors_ErrorDisplayedCorrectly()
    {
        var error1 = new NortheastMegabuck.Models.ErrorDetail("error1");
        _registrationsAdapter.SetupGet(registrationsAdapter => registrationsAdapter.Error).Returns(error1);

        var error2 = new NortheastMegabuck.Models.ErrorDetail("error2");
        _squadsAdapter.SetupGet(squadsAdapter => squadsAdapter.Error).Returns(error2);

        var error3 = new NortheastMegabuck.Models.ErrorDetail("error3");
        _sweepersAdapter.SetupGet(sweepersAdapter => sweepersAdapter.Error).Returns(error3);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.DisplayError($"error1{Environment.NewLine}error2{Environment.NewLine}error3"), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_AdaperCallHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _registrationsAdapter.SetupGet(registrationAdapter => registrationAdapter.Error).Returns(error);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.BindRegistrations(It.IsAny<IEnumerable<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>>()), Times.Never);
            _view.Verify(view => view.SetDivisionEntries(It.IsAny<IDictionary<string, int>>()), Times.Never);
            _view.Verify(view => view.SetSquadEntries(It.IsAny<IDictionary<string, int>>()), Times.Never);
            _view.Verify(view => view.SetSweeperEntries(It.IsAny<IDictionary<string, int>>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_AdapterCallsHaveNoErrors_ViewBindRegistrationsCalledCorrectly()
    {
        var registrations = new List<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>
        {
            new NortheastMegabuck.Registrations.Retrieve.TournamentRegistrationViewModel
            { 
                FirstName = "Joe",
                LastName = "Bowler"
            },
            new NortheastMegabuck.Registrations.Retrieve.TournamentRegistrationViewModel
            { 
                FirstName = "John",
                LastName = "Apples"
            },
            new NortheastMegabuck.Registrations.Retrieve.TournamentRegistrationViewModel
            { 
                FirstName = "Jane",
                LastName = "Bowler"
            }
        };

        _registrationsAdapter.Setup(registrationAdapter => registrationAdapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(registrations);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindRegistrations(It.Is<IEnumerable<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>>(r => r.First().FirstName == "John")), Times.Once);
            _view.Verify(view => view.BindRegistrations(It.Is<IEnumerable<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>>(r => r.Last().FirstName == "Joe")), Times.Once);
            _view.Verify(view => view.BindRegistrations(It.Is<IEnumerable<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>>(r => r.ToList()[1].FirstName == "Jane")), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_AdapterCallsHaveNoErrors_ViewSetDivisionEntries_CalledCorrectly()
    {
        var registration1 = new Mock<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>();
        registration1.SetupGet(registration => registration.DivisionName).Returns("division1");
        registration1.SetupGet(registration=> registration.SquadsEnteredCount).Returns(1);

        var registration2 = new Mock<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>();
        registration2.SetupGet(registration => registration.DivisionName).Returns("division2");
        registration2.SetupGet(registration => registration.SquadsEnteredCount).Returns(2);

        var registration3 = new Mock<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>();
        registration3.SetupGet(registration => registration.DivisionName).Returns("division1");
        registration3.SetupGet(registration => registration.SquadsEnteredCount).Returns(3);

        var registrations = new[] { registration1.Object, registration2.Object, registration3.Object };
        _registrationsAdapter.Setup(registrationAdapter => registrationAdapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(registrations);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.SetDivisionEntries(It.Is<IDictionary<string, int>>(entries => entries.Count == 2)), Times.Once);

            _view.Verify(view => view.SetDivisionEntries(It.Is<IDictionary<string, int>>(entries => entries["division1"] == 4)), Times.Once);
            _view.Verify(view => view.SetDivisionEntries(It.Is<IDictionary<string, int>>(entries => entries["division2"] == 2)), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_AdapterCallsHaveNoErors_ViewSetSquadEntries_CalledCorrectly()
    {
        var squad1 = new Mock<NortheastMegabuck.Squads.IViewModel>();
        squad1.SetupGet(squad => squad.Id).Returns(SquadId.New());
        squad1.SetupGet(squad => squad.Date).Returns(new DateTime(2000, 1, 1, 9, 0, 0, DateTimeKind.Unspecified));

        var squad2 = new Mock<NortheastMegabuck.Squads.IViewModel>();
        squad2.SetupGet(squad => squad.Id).Returns(SquadId.New());
        squad2.SetupGet(squad => squad.Date).Returns(new DateTime(2000, 1, 1, 11, 0, 0, DateTimeKind.Unspecified));

        var squads = new[] { squad1.Object, squad2.Object };
        _squadsAdapter.Setup(squadsAdapter => squadsAdapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squads);

        var registration1 = new Mock<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>();
        registration1.SetupGet(registration => registration.DivisionName).Returns("division1");
        registration1.SetupGet(registration => registration.SquadsEntered).Returns(new[] { squad1.Object.Id});

        var registration2 = new Mock<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>();
        registration2.SetupGet(registration => registration.DivisionName).Returns("division2");
        registration2.SetupGet(registration => registration.SquadsEntered).Returns(new[] { squad1.Object.Id});

        var registration3 = new Mock<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>();
        registration3.SetupGet(registration => registration.DivisionName).Returns("division1");
        registration3.SetupGet(registration => registration.SquadsEntered).Returns(new[] { squad1.Object.Id, squad2.Object.Id});

        var registrations = new[] { registration1.Object, registration2.Object, registration3.Object };
        _registrationsAdapter.Setup(registrationAdapter => registrationAdapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(registrations);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.SetSquadEntries(It.Is<IDictionary<string, int>>(entries => entries.Count == 2)), Times.Once);

            _view.Verify(view => view.SetSquadEntries(It.Is<IDictionary<string, int>>(entries => entries["01/01/00 9AM"] == 3)), Times.Once);
            _view.Verify(view => view.SetSquadEntries(It.Is<IDictionary<string, int>>(entries => entries["01/01/00 11AM"] == 1)), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_AdapterCallsHaveNoErors_ViewSetSweeperdEntries_CalledCorrectly()
    {
        var sweeper1 = new Mock<NortheastMegabuck.Sweepers.IViewModel>();
        sweeper1.SetupGet(squad => squad.Id).Returns(NortheastMegabuck.SquadId.New());
        sweeper1.SetupGet(squad => squad.Date).Returns(new DateTime(2000, 1, 1, 9, 0, 0, DateTimeKind.Unspecified));

        var sweeper2 = new Mock<NortheastMegabuck.Sweepers.IViewModel>();
        sweeper2.SetupGet(squad => squad.Id).Returns(NortheastMegabuck.SquadId.New());
        sweeper2.SetupGet(squad => squad.Date).Returns(new DateTime(2000, 1, 1, 11, 0, 0, DateTimeKind.Unspecified));

        var sweepers = new[] { sweeper1.Object, sweeper2.Object };
        _sweepersAdapter.Setup(sweepersAdapter => sweepersAdapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweepers);

        var registration1 = new Mock<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>();
        registration1.SetupGet(registration => registration.DivisionName).Returns("division1");
        registration1.SetupGet(registration => registration.SweepersEntered).Returns(new[] { sweeper1.Object.Id });
        registration1.SetupGet(registration => registration.SuperSweeperEntered).Returns(true);

        var registration2 = new Mock<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>();
        registration2.SetupGet(registration => registration.DivisionName).Returns("division2");
        registration2.SetupGet(registration => registration.SweepersEntered).Returns(new[] { sweeper1.Object.Id });
        registration2.SetupGet(registration => registration.SuperSweeperEntered).Returns(false);

        var registration3 = new Mock<NortheastMegabuck.Registrations.Retrieve.ITournamentRegistrationViewModel>();
        registration3.SetupGet(registration => registration.DivisionName).Returns("division1");
        registration3.SetupGet(registration => registration.SweepersEntered).Returns(new[] { sweeper1.Object.Id, sweeper2.Object.Id });
        registration3.SetupGet(registration => registration.SuperSweeperEntered).Returns(true);

        var registrations = new[] { registration1.Object, registration2.Object, registration3.Object };
        _registrationsAdapter.Setup(registrationAdapter => registrationAdapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(registrations);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.SetSweeperEntries(It.Is<IDictionary<string, int>>(entries => entries.Count == 3)), Times.Once);

            _view.Verify(view => view.SetSweeperEntries(It.Is<IDictionary<string, int>>(entries => entries["01/01/00 9AM"] == 3)), Times.Once);
            _view.Verify(view => view.SetSweeperEntries(It.Is<IDictionary<string, int>>(entries => entries["01/01/00 11AM"] == 1)), Times.Once);
            _view.Verify(view => view.SetSweeperEntries(It.Is<IDictionary<string, int>>(entries => entries["Super Sweeper"] == 2)), Times.Once);
        });
    }

    [Test]
    public async Task DeleteAsync_ViewConfirm_CalledCorrectly()
    {
        await _presenter.DeleteAsync(RegistrationId.New(), default).ConfigureAwait(false);

        _view.Verify(view => view.Confirm("Are you sure you want to delete this bowler's entire registration?"), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_ViewConfirmFalse_NothingElseHappens()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(false);

        await _presenter.DeleteAsync(RegistrationId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _deleteAdapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<RegistrationId>(), It.IsAny<CancellationToken>()), Times.Never);

            _view.Verify(view => view.RemoveRegistration(It.IsAny<RegistrationId>()), Times.Never);
        });
    }

    [Test]
    public async Task DeleteAsync_ViewConfirmTrue_DeleteAdapterExecute_CalledCorrectly()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(true);

        var registrationId = RegistrationId.New();
        CancellationToken cancellationToken = default;

        await _presenter.DeleteAsync(registrationId, cancellationToken).ConfigureAwait(false);

        _deleteAdapter.Verify(adapter => adapter.ExecuteAsync(registrationId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_ViewConfirmTrue_DeleteAdapterHasError_ErrorFlow()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(true);

        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _deleteAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _presenter.DeleteAsync(RegistrationId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.RemoveRegistration(It.IsAny<RegistrationId>()), Times.Never);
        });
    }

    [Test]
    public async Task DeleteAsync_ViewConfirmTrue_DeleteAdapterSuccess_ViewRemoveRegistration_CalledCorrectly()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(true);

        var registrationId = RegistrationId.New();

        await _presenter.DeleteAsync(registrationId, default).ConfigureAwait(false);

        _view.Verify(view=> view.RemoveRegistration(registrationId), Times.Once);
    }

    [Test]
    public void UpdateBowlerName_ViewUpdateBowlerName_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();

        _presenter.UpdateBowlerName(bowlerId);

        _view.Verify(view => view.UpdateBowlerName(bowlerId), Times.Once);
    }

    [Test]
    public void UpdateBowlerName_ViewUpdateBowlerNameReturnsNull_CancelFlow()
    {
        var bowlerId = BowlerId.New();

        _presenter.UpdateBowlerName(bowlerId);

        _view.Verify(view => view.UpdateBowlerName(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void UpdateBowlerName_ViewUpdateBowlerNameReturnsValue_ViewUpdateBowlerName_CalledCorrectly()
    {
        var bowlerName = "bowlerName";
        _view.Setup(view => view.UpdateBowlerName(It.IsAny<BowlerId>())).Returns(bowlerName);

        var bowlerId = BowlerId.New();

        _presenter.UpdateBowlerName(bowlerId);

        _view.Verify(view => view.UpdateBowlerName("bowlerName"), Times.Once);
    }
}
