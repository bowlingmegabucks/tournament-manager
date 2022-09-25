using System.ComponentModel;

namespace NortheastMegabuck.Controls;
public partial class SquadControl : UserControl, Squads.IViewModel
{
    public SquadControl()
    {
        InitializeComponent();
    }

    private void Controls_Validated(object sender, EventArgs e)
        => squadErrorProvider.SetError((Control)sender, string.Empty);

    public SquadId Id { get; set; }
    
    public TournamentId TournamentId { get; set; }
    
    public decimal? CashRatio
    {
        get => cashRatioValue.Value == 0 ? null : cashRatioValue.Value;
        set => cashRatioValue.Value = value ?? 0;
    }

    private void CashRatioValue_Validating(object sender, CancelEventArgs e)
    {
        if (CashRatio.HasValue && CashRatio <= 1)
        {
            e.Cancel = true;
            squadErrorProvider.SetError(cashRatioValue, "Cash ratio must be greater than 1");
        }
    }

    public decimal? FinalsRatio
    {
        get => finalsRatioValue.Value == 0 ? null : finalsRatioValue.Value;
        set => finalsRatioValue.Value = value ?? 0;
    }

    private void FinalsRatioValue_Validating(object sender, CancelEventArgs e)
    {
        if (FinalsRatio.HasValue && FinalsRatio <= 1)
        {
            e.Cancel = true;
            squadErrorProvider.SetError(finalsRatioValue, "Finals ratio must be greater than 1");
        }
    }

    public DateTime Date 
    { 
        get => datePicker.Value;
        set => datePicker.Value = value; 
    }

    private void DatePicker_Validating(object sender, CancelEventArgs e)
    {
        if (Date < DateTime.Now)
        {
            e.Cancel = true;
            squadErrorProvider.SetError(datePicker, "Date cannot be in past");
        }
    }

    public short MaxPerPair 
    {
        get => (short)masPerPairValue.Value;
        set => masPerPairValue.Value = value; 
    }

    private void MaxPerPairValue_Validating(object sender, CancelEventArgs e)
    {
        if (MaxPerPair is <= 0 or > 10)
        {
            e.Cancel = true;
            squadErrorProvider.SetError(masPerPairValue, "Max per pair must be between 1 and 10");
        }
    }

    public short StartingLane
    {
        get => (short)startingLaneValue.Value;
        set => startingLaneValue.Value = value;
    }

    public short NumberOfLanes
    {
        get => (short)numberOfLanesValue.Value;
        set => numberOfLanesValue.Value = value;
    }

    public bool Complete { get; set; }

    private void SquadControl_Validating(object sender, CancelEventArgs e)
        => e.Cancel = !ValidateChildren();
}
