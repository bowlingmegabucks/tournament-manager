using System.ComponentModel;

namespace NewEnglandClassic.Contols;
internal partial class SweeperControl : UserControl, Sweepers.IViewModel
{
    public SweeperControl()
    {
        InitializeComponent();
    }

    public Guid Id { get; set; }

    public Guid TournamentId { get; set; }

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
            ErrorProviderSweeper.SetError(NumericEntryFee, "Entry fee must be greater than $0");
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
            ErrorProviderSweeper.SetError(NumericGames, "Games must be greater than 0");
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
            ErrorProviderSweeper.SetError(NumericCashRatio, "Cash ratio must be greater than 1");
        }
    }

    public DateTime Date
    {
        get => DatePickerSweeperDate.Value;
        set => DatePickerSweeperDate.Value = value;
    }

    private void DatePickerSquadDate_Validating(object sender, CancelEventArgs e)
    {
        if (Date < DateTime.Now)
        {
            e.Cancel = true;
            ErrorProviderSweeper.SetError(DatePickerSweeperDate, "Date cannot be in past");
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
            ErrorProviderSweeper.SetError(NumericMaxPerPair, "Max per pair must be between 1 and 10");
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

    public IDictionary<DivisionId, int?> Divisions
        => SweeperDivisions.Divisions;

    public void BindDivisions(IEnumerable<Divisions.IViewModel> divisions) 
        => SweeperDivisions.BindDivisions(divisions);

    private void SweeperControl_Validated(object sender, EventArgs e)
        => ErrorProviderSweeper.SetError((Control)sender, string.Empty);
}
