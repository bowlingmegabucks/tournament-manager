using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.App.Tournaments;

internal sealed partial class GetTournamentsForm
    : Form, IGetTournamentsView
{
    private readonly GetTournamentsPresenter _presenter;
    private CancellationTokenSource? _cancellationTokenSource;

    public GetTournamentsForm(GetTournamentsPresenter presenter)
    {
        InitializeComponent();

        _presenter = presenter;
        _cancellationTokenSource = new();

        _cancellationTokenSource.Token.Register(CancelLoadingTournaments);

        _ = _presenter.GetTournamentsAsync(this, page: null, pageSize: null, _cancellationTokenSource);
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

    public void BindTournaments(IReadOnlyCollection<TournamentSummaryViewModel> tournaments)
        => tournamentsGrid.Bind(tournaments);

    public void DisableOpenTournament()
        => openButton.Enabled = false;

    public void DisplayErrorMessage(IEnumerable<Error> errors)
        => MessageBox.Show(
            string.Join(
                Environment.NewLine,
                errors.Select(error => $"{error.Code}: {error.Description}")),
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

    public Guid? CreateNewTournament()
#pragma warning disable S125 // Sections of code should not be commented out
    {
        //using var form = new Add.Form(_services);

        //return form.ShowDialog() == DialogResult.OK ? form.Tournament.Id : null;

        return null;
    }
#pragma warning restore S125 // Sections of code should not be commented out

#pragma warning disable S125 // Sections of code should not be commented out
#pragma warning disable IDE0060 // Remove unused parameter
#pragma warning disable RCS1163 // Unused parameter
    public void OpenTournament(TournamentId id)
#pragma warning restore RCS1163 // Unused parameter
#pragma warning restore IDE0060 // Remove unused parameter
    {
        //using var portal = new Portal.Form(_services, id, tournamentName);

        //Hide();

        //portal.ShowDialog();

        Close();
#pragma warning restore S125 // Sections of code should not be commented out
    }

    private void TournamentsGrid_GridRowDoubleClicked(object sender, Controls.Grids.GridRowDoubleClickEventArgs e)
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
