using System.ComponentModel;

namespace BowlingMegabucks.TournamentManager.Controls;
internal partial class SquadControl : UserControl, Squads.IViewModel
{
    public SquadControl()
    {
        InitializeComponent();
    }

    private void Controls_Validated(object sender, EventArgs e)
        => squadErrorProvider.SetError((Control)sender, string.Empty);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SquadId Id { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TournamentId TournamentId { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public decimal? EntryFee
    {
        get => entryFeeValue.Value == 0 ? null : entryFeeValue.Value;
        set => entryFeeValue.Value = value ?? 0;
    }

    private void EntryFeeValue_Validating(object sender, CancelEventArgs e)
    {
        if (EntryFee.HasValue && EntryFee <= 0)
        {
            e.Cancel = true;
            squadErrorProvider.SetError(entryFeeValue, "Entry fee must be greater than 0");
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public short MaxPerPair
    {
        get => (short)maxPerPairValue.Value;
        set => maxPerPairValue.Value = value;
    }

    private void MaxPerPairValue_Validating(object sender, CancelEventArgs e)
    {
        if (MaxPerPair is <= 0 or > 10)
        {
            e.Cancel = true;
            squadErrorProvider.SetError(maxPerPairValue, "Max per pair must be between 1 and 10");
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public short StartingLane
    {
        get => (short)startingLaneValue.Value;
        set => startingLaneValue.Value = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public short NumberOfLanes
    {
        get => (short)numberOfLanesValue.Value;
        set => numberOfLanesValue.Value = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Complete { get; set; }

    private void SquadControl_Validating(object sender, CancelEventArgs e)
        => e.Cancel = !ValidateChildren();
}
