using System.ComponentModel;

namespace NortheastMegabuck.Controls;
internal partial class TournamentControl : UserControl, Tournaments.IViewModel
{
    public TournamentControl()
    {
        InitializeComponent();
    }

    private void Controls_Validated(object sender, EventArgs e)
        => tournamentErrorProvider.SetError((Control)sender, string.Empty);

    public TournamentId Id { get; set; }

    public string TournamentName
    {
        get => nameText.Text;
        set => nameText.Text = value;
    }

    private void TournamentText_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TournamentName))
        {
            e.Cancel = true;
            tournamentErrorProvider.SetError(nameText, "Tournament name is required");
        }
    }

    public DateOnly Start
    {
        get => DateOnly.FromDateTime(startDatePicker.Value);
        set => startDatePicker.Value = new DateTime(value.Year, value.Month, value.Day);
    }

    public DateOnly End
    {
        get => DateOnly.FromDateTime(endDatePicker.Value);
        set => endDatePicker.Value = new DateTime(value.Year, value.Month, value.Day);
    }

    private void TournamentDates_Validating(object sender, CancelEventArgs e)
    {
        if (Start > End)
        {
            e.Cancel = true;
            tournamentErrorProvider.SetError((Control)sender, "Start date must be before end date");
        }
    }

    public decimal EntryFee
    {
        get => entryFeeValue.Value;
        set => entryFeeValue.Value = value;
    }

    private void EntryFeeValue_Validating(object sender, CancelEventArgs e)
    {
        if (EntryFee <= 0)
        {
            e.Cancel = true;
            tournamentErrorProvider.SetError(entryFeeValue, "Entry fee must be greater than $0");
        }
    }

    public short Games
    {
        get => (short)gamesValue.Value;
        set => gamesValue.Value = value;
    }

    private void GamesValue_Validating(object sender, CancelEventArgs e)
    {
        if (Games <= 0)
        {
            e.Cancel = true;
            tournamentErrorProvider.SetError(gamesValue, "Games must be greater than 0");
        }
    }

    public decimal FinalsRatio
    {
        get => finalsRatioValue.Value;
        set => finalsRatioValue.Value = value;
    }

    private void FinalsRatioValue_Validating(object sender, CancelEventArgs e)
    {
        if (FinalsRatio <= 1)
        {
            e.Cancel = true;
            tournamentErrorProvider.SetError(finalsRatioValue, "Finals ratio must be greater than 1");
        }
    }

    public decimal CashRatio
    {
        get => cashRatioValue.Value;
        set => cashRatioValue.Value = value;
    }

    private void CashRatioValue_Validating(object sender, CancelEventArgs e)
    {
        if (CashRatio <= 1)
        {
            e.Cancel = true;
            tournamentErrorProvider.SetError(cashRatioValue, "Cash ratio must be greater than 1");
        }
    }

    public string BowlingCenter
    {
        get => bowlingCenterValue.Text;
        set => bowlingCenterValue.Text = value;
    }

    private void BowlingCenterText_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(BowlingCenter))
        {
            e.Cancel = true;
            tournamentErrorProvider.SetError(bowlingCenterValue, "Bowling center is required");
        }
    }

    public bool Completed
    {
        get => CheckboxComplete.Checked;
        set => CheckboxComplete.Checked = value;
    }

    private void TournamentControl_Validating(object sender, CancelEventArgs e)
        => e.Cancel = !ValidateChildren();
}
