using BowlingMegabucks.TournamentManager.App.Controls.Grids;
using BowlingMegabucks.TournamentManager.App.Tournaments.Portal;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.App.Tournaments;

internal sealed partial class GetTournamentsForm
    : Form, IGetTournamentsView
{
    private readonly GetTournamentsPresenter _presenter;
    private readonly ITournamentPortalFormFactory _tournamentPortalFormFactory;

    private CancellationTokenSource? _cancellationTokenSource;

    public GetTournamentsForm(GetTournamentsPresenter presenter, ITournamentPortalFormFactory portalFormFactory)
    {
        InitializeComponent();

        _presenter = presenter;
        _tournamentPortalFormFactory = portalFormFactory;
        _cancellationTokenSource = new();

        _cancellationTokenSource.Token.Register(CancelLoadingTournaments);

        // Subscribe to the new event for paging
        tournamentsGrid.PageChangeCommitted += (s, e)
            => _ = _presenter.GetTournamentsAsync(this, e.NewPage, e.NewPageSize, e.CancellationTokenSource);

        _ = _presenter.GetTournamentsAsync(this, page: tournamentsGrid.CurrentPage, pageSize: tournamentsGrid.PageSize, _cancellationTokenSource);
    }

    private void CancelLoadingTournaments()
    {
        DisableOpenTournament();
        HideProcessingMessage();
    }

    public void ShowProcessingMessage(string message, CancellationTokenSource cancellationTokenSource)
        => this.AddProcessingMessage(message, cancellationTokenSource);

    public void HideProcessingMessage()
        => this.RemoveProcessingMessage();

    public void BindTournaments(IReadOnlyCollection<TournamentSummaryViewModel> tournaments, int totalRecords)
        => tournamentsGrid.Bind(tournaments, totalRecords);

    public void DisableOpenTournament()
        => openButton.Enabled = false;

    public void DisplayErrorMessage(IEnumerable<Error> errors)
        => errors.ShowMessageBoxWithErrors();

    public Guid? CreateNewTournament()
#pragma warning disable S125 // Sections of code should not be commented out
    {
        //using var form = new Add.Form(_services);

        //return form.ShowDialog() == DialogResult.OK ? form.Tournament.Id : null;

        return null;
    }
#pragma warning restore S125 // Sections of code should not be commented out

    public void OpenTournament(TournamentId id)
    {
        using TournamentPortalForm portal = _tournamentPortalFormFactory.Create(id);

        Hide();

        portal.ShowDialog();

        Close();
    }

    private void TournamentsGrid_GridRowDoubleClicked(object sender, GridRowDoubleClickEventArgs e)
        => OpenButton_Click(sender, e);

    private void NewButton_Click(object sender, EventArgs e)

#pragma warning disable S125 // Sections of code should not be commented out
        //=> _presenter.NewTournament();
        => MessageBox.Show("Not implemented", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
#pragma warning restore S125 // Sections of code should not be commented out

    private void OpenButton_Click(object sender, EventArgs e)
        => OpenTournament(tournamentsGrid.SelectedTournament!.Id);

    private void DisposeFields(bool disposing)
    {
        if (disposing)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        base.Dispose(disposing);
    }
}
