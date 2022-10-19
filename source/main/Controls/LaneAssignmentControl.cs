using System.Globalization;
using NortheastMegabuck.Bowlers.Add;

namespace NortheastMegabuck.Controls;
public partial class LaneAssignmentControl : UserControl, LaneAssignments.IViewModel
{
    private readonly CultureInfo _culture;

    /// <summary>
    /// Used for created Empty Lane Assignment Grid
    /// </summary>
    /// <param name="laneAssignment"></param>
    public LaneAssignmentControl(string laneAssignment) : this()
    {
        LaneAssignment = laneAssignment;
    }

    public LaneAssignmentControl()
    {
        InitializeComponent();
        ClearRegistration();
        _culture = new CultureInfo("en-US");

        LaneAssignment = string.Empty;
        BorderStyle = BorderStyle.FixedSingle;
    }

    public void Bind(LaneAssignments.IViewModel viewModel)
    {
        BowlerId = viewModel.BowlerId;
        BowlerName = viewModel.BowlerName;
        DivisionNumber = viewModel.DivisionNumber;
        DivisionName = viewModel.DivisionName;
        Average = viewModel.Average;
        Handicap = viewModel.Handicap;

        divisionPanel.Visible = true;
        averagePanel.Visible = Average > 0;
        handicapPanel.Visible = Handicap > 0;
        laneAssignmentLabel.Visible = !string.IsNullOrEmpty(LaneAssignment);

        _viewModel = viewModel;
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
        get => averageLabelValue.Text == "-" ? 0 : Convert.ToInt32(averageLabelValue.Text, _culture);
        set => averageLabelValue.Text = value == 0 ? "-" : value.ToString(_culture);
    }

    public int Handicap
    { 
        get => handicapLabelValue.Text == "-" ? 0 : Convert.ToInt32(handicapLabelValue.Text, _culture);
        set => handicapLabelValue.Text = value == 0 ? "-" : value.ToString(_culture);
    }

    private LaneAssignments.IViewModel? _viewModel;
    public override string ToString()
        => _viewModel?.ToString(LaneAssignment) ?? string.Empty;

    public string ToString(string laneAssignment)
        => _viewModel?.ToString(laneAssignment) ?? string.Empty;

    private void Controls_MouseDown(object sender, MouseEventArgs e)
        => OnMouseDown(e);

    private void Controls_DragEnter(object sender, DragEventArgs e)
        => OnDragEnter(e);

    private void Controls_DragOver(object sender, DragEventArgs e)
        => OnDragOver(e);

    private void Controls_DragLeave(object sender, EventArgs e)
        => OnDragLeave(e);
    public int CompareTo(LaneAssignments.IViewModel? other)
        => 0;
}
