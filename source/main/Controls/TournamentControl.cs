using System.ComponentModel;

namespace NewEnglandClassic.Contols;
internal partial class TournamentControl : UserControl, Tournaments.IViewModel
{
    public TournamentControl()
    {
        InitializeComponent();
    }

    private void Controls_Validated(object sender, EventArgs e)
        => ErrorProviderTournament.SetError((Control)sender, string.Empty);

    public TournamentId Id { get; set; }

    public string TournamentName
    {
        get => TextboxTournament.Text;
        set => TextboxTournament.Text = value;
    }

    private void TextboxTournament_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TournamentName))
        {
            e.Cancel = true;
            ErrorProviderTournament.SetError(TextboxTournament, "Tournament name is required");
        }
    }

    public DateOnly Start
    {
        get => DateOnly.FromDateTime(DatePickerStart.Value);
        set => DatePickerStart.Value = new DateTime(value.Year, value.Month, value.Day);
    }

    public DateOnly End
    {
        get => DateOnly.FromDateTime(DatePickerEnd.Value);
        set => DatePickerEnd.Value = new DateTime(value.Year, value.Month, value.Day);
    }

    private void TournamentDates_Validating(object sender, CancelEventArgs e)
    {
        if (Start > End)
        {
            e.Cancel = true;
            ErrorProviderTournament.SetError((Control)sender, "Start date must be before end date");
        }
    }

    public decimal EntryFee
    {
        get => NumericEntryFee.Value;
        set => NumericEntryFee.Value = value;
    }

    private void NumericEntryFee_Validating(object sender, CancelEventArgs e)
    {
        if (EntryFee <= 0)
        {
            e.Cancel = true;
            ErrorProviderTournament.SetError(NumericEntryFee, "Entry fee must be greater than $0");
        }
    }

    public short Games
    {
        get => (short)NumericGames.Value;
        set => NumericGames.Value = value;
    }

    private void NumericGames_Validating(object sender, CancelEventArgs e)
    {
        if (Games <= 0)
        {
            e.Cancel = true;
            ErrorProviderTournament.SetError(NumericGames, "Games must be greater than 0");
        }
    }

    public decimal FinalsRatio
    {
        get => NumericFinalsRatio.Value;
        set => NumericFinalsRatio.Value = value;
    }

    private void NumericFinalsRatio_Validating(object sender, CancelEventArgs e)
    {
        if (FinalsRatio <= 1)
        {
            e.Cancel = true;
            ErrorProviderTournament.SetError(NumericFinalsRatio, "Finals ratio must be greater than 1");
        }
    }

    public decimal CashRatio
    {
        get => NumericCashRatio.Value;
        set => NumericCashRatio.Value = value;
    }

    private void NumericCashRatio_Validating(object sender, CancelEventArgs e)
    {
        if (CashRatio <= 1)
        {
            e.Cancel = true;
            ErrorProviderTournament.SetError(NumericCashRatio, "Cash ratio must be greater than 1");
        }
    }

    public string BowlingCenter
    {
        get => TextboxBowlingCenter.Text;
        set => TextboxBowlingCenter.Text = value;
    }

    private void TextboxBowlingCenter_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(BowlingCenter))
        {
            e.Cancel = true;
            ErrorProviderTournament.SetError(TextboxBowlingCenter, "Bowling center is required");
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
