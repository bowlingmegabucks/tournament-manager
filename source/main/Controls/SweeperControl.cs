using System.ComponentModel;

namespace NewEnglandClassic.Contols;
public partial class SweeperControl : UserControl, Sweepers.IViewModel
{
    public SweeperControl()
    {
        InitializeComponent();
    }

    public SquadId Id { get; set; }

    public TournamentId TournamentId { get; set; }

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
            sweeperErrorProvider.SetError(entryFeeValue, "Entry fee must be greater than $0");
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
            sweeperErrorProvider.SetError(gamesValue, "Games must be greater than 0");
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
            sweeperErrorProvider.SetError(cashRatioValue, "Cash ratio must be greater than 1");
        }
    }

    public DateTime Date
    {
        get => squadDatePicker.Value;
        set => squadDatePicker.Value = value;
    }

    private void SquadDatePicker_Validating(object sender, CancelEventArgs e)
    {
        if (Date < DateTime.Now)
        {
            e.Cancel = true;
            sweeperErrorProvider.SetError(squadDatePicker, "Date cannot be in past");
        }
    }

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
            sweeperErrorProvider.SetError(maxPerPairValue, "Max per pair must be between 1 and 10");
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

    public IDictionary<DivisionId, int?> Divisions
        => sweeperDivisions.Divisions;

    public void BindDivisions(IEnumerable<Divisions.IViewModel> divisions) 
        => sweeperDivisions.BindDivisions(divisions);

    private void SweeperControl_Validated(object sender, EventArgs e)
        => sweeperErrorProvider.SetError((Control)sender, string.Empty);
}
