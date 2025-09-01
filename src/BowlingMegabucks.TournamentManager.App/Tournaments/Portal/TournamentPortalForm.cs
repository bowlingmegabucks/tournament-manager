using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournamentById;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.App.Tournaments.Portal;

internal sealed partial class TournamentPortalForm
    : Form, IGetTournamentByIdView
{
    private readonly GetTournamentByIdPresenter _presenter;
    private CancellationTokenSource? _cancellationTokenSource;

    public TournamentPortalForm(GetTournamentByIdPresenter presenter, TournamentId id)
    {
        InitializeComponent();

        _presenter = presenter;
        _cancellationTokenSource = new();

        _cancellationTokenSource.Token.Register(Close);

        _ = _presenter.GetTournamentAsync(this, id, _cancellationTokenSource);
    }

    public void ShowProcessingMessage(string message, CancellationTokenSource cancellationTokenSource)
        => this.AddProcessingMessage(message, cancellationTokenSource);
    public void HideProcessingMessage()
        => this.RemoveProcessingMessage();
    public void BindTournament(TournamentDetailViewModel tournament)
    {

    }

    public void DisplayErrorMessage(IEnumerable<Error> errors)
        => errors.ShowMessageBoxWithErrors();

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

#pragma warning disable S125 // Sections of code should not be commented out

    private void AddDivisionMenuItem_Click(object sender, EventArgs e)
    {
        //using var form = new Divisions.Add.Form(_services, _id);

        //form.ShowDialog(this);
    }

    private void ViewDivisionsMenuItem_Click(object sender, EventArgs e)
    {
        //using var form = new Divisions.Retrieve.Form(_services, _id);

        //form.ShowDialog(this);
    }

    private void AddSquadMenuItem_Click(object sender, EventArgs e)
    {
        //using var form = new Squads.Add.Form(_services, _id);

        //form.ShowDialog(this);
    }

    private void OpenSquadMenuItem_Click(object sender, EventArgs e)
    {
        //using var form = new Squads.Retrieve.Form(_services, _id, _gamesPerSquad);

        //form.ShowDialog(this);
    }

    private void AddSweeperMenuItem_Click(object sender, EventArgs e)
    {
        //using var form = new Sweepers.Add.Form(_services, _id);

        //form.ShowDialog(this);
    }

    private void OpenSweeperMenuItem_Click(object sender, EventArgs e)
    {
        //using var form = new Sweepers.Retrieve.Form(_services, _id);

        //form.ShowDialog(this);
    }

    private void AddRegistrationMenuItem_Click(object sender, EventArgs e)
    {
        //using var form = new Registrations.Add.Form(_services, _id);

        //if (!form.IsDisposed)
        //{
        //    form.ShowDialog(this);
        //}
    }

    private void ViewTournamentRegistrationsMenuItem_Click(object sender, EventArgs e)
    {
        //using var form = new Registrations.Retrieve.RetrieveTournamentRegistrationsForm(_services, _id);

        //form.ShowDialog(this);
    }

    private void SuperSweeperResultsMenuItem_Click(object sender, EventArgs e)
    {
        //using var form = new Sweepers.Results.Form(_services, _id);

        //form.ShowDialog(this);
    }

    private void ExitMenuItem_Click(object sender, EventArgs e)
        => Close();

    private void AtLargeResultsMenuItem_Click(object sender, EventArgs e)
    {
        //using var form = new Results.AtLarge(_services, _id);

        //form.ShowDialog(this);
    }

    private void SeedingMenuItem_Click(object sender, EventArgs e)
    {
        //using var form = new Seeding.Form(_services, _id);

        //form.ShowDialog(this);
    }
}
