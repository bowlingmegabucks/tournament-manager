namespace NortheastMegabuck.LaneAssignments;
public partial class Form : System.Windows.Forms.Form, IView
{
    public SquadId SquadId { get; }
    public int StartingLane { get; }

    public int NumberOfLanes { get; }

    public int MaxPerPair { get; }

    public void BuildLanes(IEnumerable<string> lanes)
    {
        foreach (var lane in lanes)
        {
            var control = new Controls.LaneAssignmentControl(lane)
            {
                Margin = new Padding(0, 0, 0, 0),
                AllowDrop = true
            };

            AddOpenLaneEventsToOpenLane(control);

            laneAssignmentFlowLayoutPanel.Controls.Add(control);
        }
    }
    public Form(SquadId squadId, int startingLane, int numberOfLanes, int maxPerPair)
    {
        InitializeComponent();

        SquadId = squadId;
        StartingLane = startingLane;
        NumberOfLanes = numberOfLanes;
        MaxPerPair = maxPerPair;

        new Presenter(this).Load();

        var bowler1 = new ViewModel("Dave Kipperman", "36 - 54", 0, 0);
        var bowler2 = new ViewModel("Ashlie Kipperman", "Over 55 / Women", 0, 0);
        var bowler3 = new ViewModel("Joe Bowler", "Under 215 Average", 200, 12);

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        unassignedRegistrationsFlowLayoutPanel.Controls.AddRange(bowlers.Select(bowler => Add(bowler)).ToArray());
    }

    private Controls.LaneAssignmentControl Add(string laneAssignment)
    { 
        var control = new Controls.LaneAssignmentControl(laneAssignment)
        {
            Margin = new Padding(0, 0, 0, 0),
            AllowDrop = true
        };

        AddOpenLaneEventsToOpenLane(control);

        return control;
    }

    private Controls.LaneAssignmentControl Add(IViewModel viewModel)
    {
        var control = new Controls.LaneAssignmentControl()
        {
            Margin = new Padding(3, 0, 0, 0),
        };

        control.Bind(viewModel);

        control.MouseDown += UnassignedRegistration_MouseDown!;

        return control;
    }

    private void UnassignedRegistration_MouseDown(object sender, MouseEventArgs e)
        => (sender as Control)!.DoDragDrop(sender as IViewModel, DragDropEffects.Move);

    private void LaneAssignmentOpen_DragOver(object sender, DragEventArgs e)
    { 
        if (e.GetDataPresent<Controls.LaneAssignmentControl>())
        {
            (sender as Control)!.BackColor = SystemColors.Highlight;
        }
    }

    private void LaneAssignmentOpen_DragEnter(object sender, DragEventArgs e)
    {
        if (e.GetDataPresent<Controls.LaneAssignmentControl>())
        {
            e.Effect = DragDropEffects.Move;
        }
    }
    private void LaneAssignmentOpen_DragLeave(object sender, EventArgs e)
        => (sender as Control)!.BackColor = SystemColors.Control;

    private void LaneAssignmentOpen_DragDrop(object sender, DragEventArgs e)
    {
        var registration = e.Data<Controls.LaneAssignmentControl>();

        var openLane = sender as Controls.LaneAssignmentControl;

        openLane!.Bind(registration!);
        openLane.MouseDown += UnassignedRegistration_MouseDown!;
        openLane.KeyUp += LaneAssignmentRegistered_KeyUp!;
        openLane.Enter += LaneAssignmentRegistered_Enter!;
        openLane.Leave += LaneAssignmentRegistered_Leave!;

        if (string.IsNullOrEmpty(registration!.LaneAssignment))
        {
            unassignedRegistrationsFlowLayoutPanel.Controls.Remove(registration);
        }
        else
        {
            var oldLane = laneAssignmentFlowLayoutPanel.Controls.OfType<Controls.LaneAssignmentControl>().Single(lane => lane.LaneAssignment == registration.LaneAssignment);
            oldLane.ClearRegistration();
            LaneAssignmentRegistered_Leave(oldLane, e);
        } 

        RemoveOpenLaneEventsFromAssignedLane(openLane);

        LaneAssignmentOpen_DragLeave(sender, e);
    }

    private void LaneAssignmentRegistered_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Escape)
        {
            return;
        }

        var registeredLane = sender as Controls.LaneAssignmentControl;

        var unassignedRegistration = Add(registeredLane!);

        unassignedRegistrationsFlowLayoutPanel.Controls.Add(unassignedRegistration);

        registeredLane!.ClearRegistration();

        LaneAssignmentRegistered_Leave(sender, e);
    }

    private void LaneAssignmentRegistered_Enter(object sender, EventArgs e)
        => (sender as Control)!.BackColor = SystemColors.Highlight;

    private void LaneAssignmentRegistered_Leave(object sender, EventArgs e)
        => (sender as Control)!.BackColor = SystemColors.Control;
    private void RemoveOpenLaneEventsFromAssignedLane(Controls.LaneAssignmentControl assignedLane)
    {
        assignedLane.DragEnter -= LaneAssignmentOpen_DragEnter!;
        assignedLane.DragOver -= LaneAssignmentOpen_DragOver!;
        assignedLane.DragLeave -= LaneAssignmentOpen_DragLeave!;
        assignedLane.DragDrop -= LaneAssignmentOpen_DragDrop!;
    }

    private void AddOpenLaneEventsToOpenLane(Controls.LaneAssignmentControl openLane)
    {
        openLane.DragOver += LaneAssignmentOpen_DragOver!;
        openLane.DragLeave += LaneAssignmentOpen_DragLeave!;
        openLane.DragEnter += LaneAssignmentOpen_DragEnter!;
        openLane.DragDrop += LaneAssignmentOpen_DragDrop!;
    }
}

internal static class ExtensionMethods
{
    internal static bool GetDataPresent<T>(this DragEventArgs e) where T : class
        => e.Data!.GetDataPresent(typeof(T));

    internal static T? Data<T>(this DragEventArgs e) where T : class
        => e.Data!.GetData(typeof(T)) as T;
}