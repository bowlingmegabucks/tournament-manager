
namespace NortheastMegabuck.Controls;
public partial class SquadRegistrationControl : UserControl, Registrations.Retrieve.ISquadRegistrationViewModel
{    
    public SquadRegistrationControl(string laneAssignment)
    {
        InitializeComponent();

        BowlerId = BowlerId.Empty;
        LaneAssignment = laneAssignment;
        BowlerName = string.Empty;
        DivisionNumber = 0;
        DivisionName = string.Empty;
        Average = 0;
        Handicap = 0;

        divisionPanel.Visible = false;
        averagePanel.Visible = false;
        handicapPanel.Visible = false;

        _stringifyViewModel = string.Empty;
    }

    public void Bind(Registrations.Retrieve.ISquadRegistrationViewModel viewModel)
    {
        BowlerId = viewModel.BowlerId;
        LaneAssignment = viewModel.LaneAssignment;
        BowlerName = viewModel.BowlerName;
        DivisionNumber = viewModel.DivisionNumber;
        DivisionName = viewModel.DivisionName;
        Average = viewModel.Average;
        Handicap = viewModel.Handicap;

        divisionPanel.Visible = true;
        averagePanel.Visible = true;
        handicapPanel.Visible = true;

        _stringifyViewModel = viewModel?.ToString() ?? string.Empty;
    }

    public BowlerId BowlerId { get; set; }

    public string LaneAssignment
    {
        get => laneAssignmentLabel.Text;
        set => laneAssignmentLabel.Text = value;
    }

    public string BowlerName
    {
        get => bowlerNameLabel.Text;
        set => bowlerNameLabel.Text = value;
    }

    public int DivisionNumber { get; set; }

    public string DivisionName
    {
        get => divisionLabelValue.Text;
        set => divisionLabelValue.Text = value;
    }

    public int Average
    {
        get => averageLabelValue.Text == "-" ? 0 : Convert.ToInt32(averageLabelValue.Text);
        set => averageLabelValue.Text = value == 0 ? "-" : value.ToString();
    }

    public int Handicap
    { 
        get => handicapLabelValue.Text == "-" ? 0 : Convert.ToInt32(handicapLabelValue.Text);
        set => handicapLabelValue.Text = value == 0 ? "-" : value.ToString();
    }

    private string _stringifyViewModel;
    public override string ToString()
        => _stringifyViewModel;
}
