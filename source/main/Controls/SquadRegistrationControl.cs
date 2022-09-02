
namespace NortheastMegabuck.Controls;
public partial class SquadRegistrationControl : UserControl, Registrations.Retrieve.ISquadRegistrationViewModel
{    
    /// <summary>
    /// Used for created Empty Lane Assignment Grid
    /// </summary>
    /// <param name="laneAssignment"></param>
    public SquadRegistrationControl(string laneAssignment) : this()
    {
        LaneAssignment = laneAssignment;
    }

    public SquadRegistrationControl()
    {
        InitializeComponent();
        ClearRegistration();
        LaneAssignment = string.Empty;
        BorderStyle = BorderStyle.FixedSingle;
    }

    public void Bind(Registrations.Retrieve.ISquadRegistrationViewModel viewModel)
    {
        BowlerId = viewModel.BowlerId;
        BowlerName = viewModel.BowlerName;
        DivisionNumber = viewModel.DivisionNumber;
        DivisionName = viewModel.DivisionName;
        Average = viewModel.Average;
        Handicap = viewModel.Handicap;

        divisionPanel.Visible = true;
        averagePanel.Visible = true;
        handicapPanel.Visible = true;
        laneAssignmentLabel.Visible = !string.IsNullOrEmpty(LaneAssignment);

        _stringifyViewModel = viewModel?.ToString() ?? string.Empty;
    }

    public void ClearRegistration()
    {
        BowlerId = BowlerId.Empty;
        
        BowlerName = string.Empty;
        DivisionNumber = 0;
        DivisionName = string.Empty;
        Average = 0;
        Handicap = 0;

        divisionPanel.Visible = false;
        averagePanel.Visible = false;
        handicapPanel.Visible = false;
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

    private string _stringifyViewModel = string.Empty;
    public override string ToString()
        => _stringifyViewModel;
    private void Controls_MouseDown(object sender, MouseEventArgs e)
        => OnMouseDown(e);

    private void Controls_DragEnter(object sender, DragEventArgs e)
        => OnDragEnter(e);

    private void Controls_DragOver(object sender, DragEventArgs e)
        => OnDragOver(e);

    private void Controls_DragLeave(object sender, EventArgs e)
        => OnDragLeave(e);
}
