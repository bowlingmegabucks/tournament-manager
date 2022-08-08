using System.ComponentModel;

namespace NewEnglandClassic.Contols;
internal partial class SquadControl : UserControl, Squads.IViewModel
{
    public SquadControl()
    {
        InitializeComponent();
    }

    private void Controls_Validated(object sender, EventArgs e)
        => ErrorProviderSquad.SetError((Control)sender, string.Empty);

    public SquadId Id { get; set; }
    
    public TournamentId TournamentId { get; set; }
    
    public decimal? CashRatio
    {
        get => NumericCashRatio.Value == 0 ? null : NumericCashRatio.Value;
        set => NumericCashRatio.Value = value ?? 0;
    }

    private void NumericCashRatio_Validating(object sender, CancelEventArgs e)
    {
        if (CashRatio.HasValue && CashRatio <= 1)
        {
            e.Cancel = true;
            ErrorProviderSquad.SetError(NumericCashRatio, "Cash ratio must be greater than 1");
        }
    }

    public decimal? FinalsRatio
    {
        get => NumericFinalsRatio.Value == 0 ? null : NumericFinalsRatio.Value;
        set => NumericFinalsRatio.Value = value ?? 0;
    }

    private void NumericFinalsRatio_Validating(object sender, CancelEventArgs e)
    {
        if (FinalsRatio.HasValue && FinalsRatio <= 1)
        {
            e.Cancel = true;
            ErrorProviderSquad.SetError(NumericFinalsRatio, "Finals ratio must be greater than 1");
        }
    }

    public DateTime Date 
    { 
        get => DatePickerSquadDate.Value;
        set => DatePickerSquadDate.Value = value; 
    }

    private void DatePickerSquadDate_Validating(object sender, CancelEventArgs e)
    {
        if (Date < DateTime.Now)
        {
            e.Cancel = true;
            ErrorProviderSquad.SetError(DatePickerSquadDate, "Date cannot be in past");
        }
    }

    public short MaxPerPair 
    {
        get => (short)NumericMaxPerPair.Value;
        set => NumericMaxPerPair.Value = value; 
    }

    private void NumericMaxPerPair_Validating(object sender, CancelEventArgs e)
    {
        if (MaxPerPair is <= 0 or > 10)
        {
            e.Cancel = true;
            ErrorProviderSquad.SetError(NumericMaxPerPair, "Max per pair must be between 1 and 10");
        }
    }

    public short StartingLane
    {
        get => (short)NumericStartingLane.Value;
        set => NumericStartingLane.Value = value;
    }

    public short NumberOfLanes
    {
        get => (short)NumericNumberOfLanes.Value;
        set => NumericNumberOfLanes.Value = value;
    }

    public bool Complete { get; set; }

    private void SquadControl_Validating(object sender, CancelEventArgs e)
        => e.Cancel = !ValidateChildren();
}
