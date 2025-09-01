using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournamentById;
using BowlingMegabucks.TournamentManager.SharedKernel.Properties;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.App.Tournaments.Portal;

internal sealed partial class TournamentPortalForm
    : Form, IGetTournamentByIdView
{
    private readonly GetTournamentByIdPresenter _presenter;
    private CancellationTokenSource? _cancellationTokenSource;

    public TournamentPortalForm(GetTournamentByIdPresenter presenter, TournamentId id)
    {
        System.Diagnostics.Debug.WriteLine($"TournamentPortalForm constructor called with ID: {id}");

        InitializeComponent();

        System.Diagnostics.Debug.WriteLine($"Form initialized. Visible: {this.Visible}, Size: {this.Size}");

        _presenter = presenter;
        _cancellationTokenSource = new();

        _cancellationTokenSource.Token.Register(Close);

        System.Diagnostics.Debug.WriteLine("Calling GetTournamentAsync...");
        _ = _presenter.GetTournamentAsync(this, id, _cancellationTokenSource);
    }

    public void ShowProcessingMessage(string message, CancellationTokenSource cancellationTokenSource)
        => this.AddProcessingMessage(message, cancellationTokenSource);

    public void HideProcessingMessage()
    {
        System.Diagnostics.Debug.WriteLine("HideProcessingMessage called");
        this.RemoveProcessingMessage();
        System.Diagnostics.Debug.WriteLine("HideProcessingMessage completed");
    }
    public void BindTournament(TournamentDetailViewModel tournament)
    {
        // Debug: Show that BindTournament is being called
        System.Diagnostics.Debug.WriteLine($"BindTournament called with tournament: {tournament?.Name ?? "NULL"}");

        if (tournament is null)
        {
            System.Diagnostics.Debug.WriteLine("Tournament is null - cannot bind data");
            return;
        }

        // Check if we're on the UI thread
        if (this.InvokeRequired)
        {
            System.Diagnostics.Debug.WriteLine("BindTournament called from background thread - invoking on UI thread");
            this.Invoke(() => BindTournament(tournament));
            return;
        }

        System.Diagnostics.Debug.WriteLine("BindTournament executing on UI thread");

        // Set tournament data
        SetTournamentData(tournament);

        // Debug visibility and refresh
        DebugVisibilityAndRefresh();

        System.Diagnostics.Debug.WriteLine("BindTournament completed successfully");
    }

    private void SetTournamentData(TournamentDetailViewModel tournament)
    {
        // Set overview data
        SetOverviewData(tournament);

        // Set dates data
        SetDatesData(tournament);

        // Set financial data
        SetFinancialData(tournament);

        // Force layout updates
        PerformLayoutUpdates();

        System.Diagnostics.Debug.WriteLine("All tournament data set and layout updated");
    }

    private void SetOverviewData(TournamentDetailViewModel tournament)
    {
        // Tournament Overview section
        nameValueLabel.Text = tournament.Name;
        bowlingCenterValueLabel.Text = tournament.BowlingCenter;
        gamesValueLabel.Text = tournament.Games.ToString(System.Globalization.CultureInfo.InvariantCulture);
        completedValueLabel.Text = tournament.Completed ? Resources.Yes : Resources.No;

        // Force layout update for overview labels
        nameValueLabel.PerformLayout();
        bowlingCenterValueLabel.PerformLayout();
        gamesValueLabel.PerformLayout();
        completedValueLabel.PerformLayout();

        // Debug: Verify overview labels
        System.Diagnostics.Debug.WriteLine($"Name label set to: {nameValueLabel.Text}");
        System.Diagnostics.Debug.WriteLine($"Name label properties - Visible: {nameValueLabel.Visible}, Location: {nameValueLabel.Location}, Size: {nameValueLabel.Size}");
        System.Diagnostics.Debug.WriteLine($"Bowling Center label set to: {bowlingCenterValueLabel.Text}");
        System.Diagnostics.Debug.WriteLine($"Bowling Center label properties - Visible: {bowlingCenterValueLabel.Visible}, Location: {bowlingCenterValueLabel.Location}, Size: {bowlingCenterValueLabel.Size}");
    }

    private void SetDatesData(TournamentDetailViewModel tournament)
    {
        // Dates section
        startDateValueLabel.Text = tournament.StartDate.ToString("MMMM dd, yyyy", System.Globalization.CultureInfo.InvariantCulture);
        endDateValueLabel.Text = tournament.EndDate.ToString("MMMM dd, yyyy", System.Globalization.CultureInfo.InvariantCulture);

        // Force layout update for date labels
        startDateValueLabel.PerformLayout();
        endDateValueLabel.PerformLayout();

        System.Diagnostics.Debug.WriteLine($"Start Date: '{startDateValueLabel.Text}', End Date: '{endDateValueLabel.Text}'");
    }

    private void SetFinancialData(TournamentDetailViewModel tournament)
    {
        // Financial Details section
        entryFeeValueLabel.Text = tournament.EntryFee.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
        finalsRatioValueLabel.Text = tournament.FinalsRatio.ToString("F1", System.Globalization.CultureInfo.InvariantCulture);
        cashRatioValueLabel.Text = tournament.CashRatio.ToString("F1", System.Globalization.CultureInfo.InvariantCulture);
        superSweeperCashRatioValueLabel.Text = tournament.SuperSweeperCashRatio.ToString("F1", System.Globalization.CultureInfo.InvariantCulture);

        // Force layout update for financial labels
        entryFeeValueLabel.PerformLayout();
        finalsRatioValueLabel.PerformLayout();
        cashRatioValueLabel.PerformLayout();
        superSweeperCashRatioValueLabel.PerformLayout();

        System.Diagnostics.Debug.WriteLine($"Entry Fee: '{entryFeeValueLabel.Text}'");
    }

    private void PerformLayoutUpdates()
    {
        // Force the table layout panels to recalculate their layout
        overviewTableLayoutPanel.PerformLayout();
        datesTableLayoutPanel.PerformLayout();
        financialTableLayoutPanel.PerformLayout();

        // Force invalidate to ensure labels redraw
        nameValueLabel.Invalidate();
        bowlingCenterValueLabel.Invalidate();
        gamesValueLabel.Invalidate();
        completedValueLabel.Invalidate();
        startDateValueLabel.Invalidate();
        endDateValueLabel.Invalidate();
        entryFeeValueLabel.Invalidate();
        finalsRatioValueLabel.Invalidate();
        cashRatioValueLabel.Invalidate();
        superSweeperCashRatioValueLabel.Invalidate();

        System.Diagnostics.Debug.WriteLine("Layout updates and invalidates performed on all controls");
    }

    private void DebugVisibilityAndRefresh()
    {
        // Debug: Check form and control visibility
        System.Diagnostics.Debug.WriteLine($"Form Visible: {this.Visible}");
        System.Diagnostics.Debug.WriteLine($"Form Size: {this.Size}");
        System.Diagnostics.Debug.WriteLine($"Form Location: {this.Location}");
        System.Diagnostics.Debug.WriteLine($"Form WindowState: {this.WindowState}");
        System.Diagnostics.Debug.WriteLine($"Form TopMost: {this.TopMost}");
        System.Diagnostics.Debug.WriteLine($"MainContainer Visible: {mainContainer.Visible}");
        System.Diagnostics.Debug.WriteLine($"MainContainer Size: {mainContainer.Size}");
        System.Diagnostics.Debug.WriteLine($"OverviewGroupBox Visible: {overviewGroupBox.Visible}");
        System.Diagnostics.Debug.WriteLine($"NameValueLabel Visible: {nameValueLabel.Visible}");
        System.Diagnostics.Debug.WriteLine($"NameValueLabel Location: {nameValueLabel.Location}");
        System.Diagnostics.Debug.WriteLine($"NameValueLabel Size: {nameValueLabel.Size}");
        System.Diagnostics.Debug.WriteLine($"NameValueLabel Parent: {nameValueLabel.Parent?.Name ?? "null"}");

        // Force refresh to ensure controls are displayed
        this.Refresh();
        System.Diagnostics.Debug.WriteLine("Form refreshed");

        // Ensure form is visible and bring to front
        if (!this.Visible)
        {
            System.Diagnostics.Debug.WriteLine("Form was not visible, showing it now");
            this.Show();
        }

        // Bring form to front and activate it
        this.BringToFront();
        this.Activate();
        System.Diagnostics.Debug.WriteLine("Form brought to front and activated");

        // Force another refresh after bringing to front
        this.Refresh();
        System.Diagnostics.Debug.WriteLine("Form refreshed again after bringing to front");

        // Use a timer for delayed check
        using var timer = new System.Windows.Forms.Timer();
        timer.Interval = 1000;
        timer.Tick += (s, e) =>
        {
            timer.Stop();
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                System.Diagnostics.Debug.WriteLine("Delayed check - Form still visible: " + this.Visible);
                System.Diagnostics.Debug.WriteLine("Delayed check - Name label text: '" + nameValueLabel.Text + "'");
                System.Diagnostics.Debug.WriteLine("Delayed check - Name label visible: " + nameValueLabel.Visible);
            }
            timer.Dispose();
        };
        timer.Start();
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
