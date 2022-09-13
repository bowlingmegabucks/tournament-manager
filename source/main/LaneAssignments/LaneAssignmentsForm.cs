namespace NortheastMegabuck.LaneAssignments;
public partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;

    public TournamentId TournamentId { get; }
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

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void Disable()
    {
        laneAssignmentGroupbox.Enabled = false;
        unassignedRegistrationsGroupbox.Enabled = false;
        //newRegistrationButton.Enabled = false;
        addToRegistrationButton.Enabled = false;
    }

    public void BindRegistrations(IEnumerable<IViewModel> registrations)
        => unassignedRegistrationsFlowLayoutPanel.Controls.AddRange(registrations.Select(BuildLaneAssignmentControl).ToArray());

    public void BindLaneAssignments(IEnumerable<IViewModel> registrations)
    {
        foreach (var registration in registrations)
        {
            var openLane = laneAssignmentFlowLayoutPanel.Controls.OfType<Controls.LaneAssignmentControl>().Single(lane => lane.LaneAssignment == registration.LaneAssignment);

            openLane!.Bind(registration);
            openLane.MouseDown += UnassignedRegistration_MouseDown!;
            openLane.KeyUp += LaneAssignmentRegistered_KeyUp!;
            openLane.Enter += LaneAssignmentRegistered_Enter!;
            openLane.Leave += LaneAssignmentRegistered_Leave!;

            RemoveOpenLaneEventsFromAssignedLane(openLane);
        }   
    }

    public void AssignToLane(IViewModel registration, string position) 
    {
        var openLane = laneAssignmentFlowLayoutPanel.Controls.OfType<Controls.LaneAssignmentControl>().Single(control => control.LaneAssignment == position);

        openLane.Bind(registration);
        openLane.MouseDown += UnassignedRegistration_MouseDown!;
        openLane.KeyUp += LaneAssignmentRegistered_KeyUp!;
        openLane.Enter += LaneAssignmentRegistered_Enter!;
        openLane.Leave += LaneAssignmentRegistered_Leave!;

        if (string.IsNullOrEmpty(registration!.LaneAssignment))
        {
            unassignedRegistrationsFlowLayoutPanel.Controls.Remove(unassignedRegistrationsFlowLayoutPanel.Controls.OfType<Controls.LaneAssignmentControl>().Single(control=> control.BowlerId == registration.BowlerId));
        }
        else
        {
            var oldLane = laneAssignmentFlowLayoutPanel.Controls.OfType<Controls.LaneAssignmentControl>().Single(lane => lane.LaneAssignment == registration.LaneAssignment);
            oldLane.ClearRegistration();
            LaneAssignmentRegistered_Leave(oldLane, new EventArgs());
        }

        RemoveOpenLaneEventsFromAssignedLane(openLane);

        LaneAssignmentOpen_DragLeave(openLane, new EventArgs());
    }

    public void RemoveLaneAssignment(IViewModel registration) 
    {
        unassignedRegistrationsFlowLayoutPanel.Controls.Add(BuildLaneAssignmentControl(registration));

        var registeredLane = laneAssignmentFlowLayoutPanel.Controls.OfType<Controls.LaneAssignmentControl>().Single(control => control.LaneAssignment == registration.LaneAssignment);

        registeredLane!.ClearRegistration();
        registeredLane.KeyUp -= LaneAssignmentRegistered_KeyUp!;

        LaneAssignmentRegistered_Leave(registeredLane, new EventArgs());
    }

    public BowlerId? GetBowler(TournamentId tournamentId, SquadId squadId)
    {
        using var form = new Bowlers.Search.Dialog(_config, false, tournamentId, new[] { squadId });

        return form.ShowDialog(this) == DialogResult.OK ? form.SelectedBowlerId : null;
    }

    public IViewModel? NewRegistration(SquadId squadId)
    {
        using var form = new Registrations.Add.Form(_config, squadId);

        return form.ShowDialog(this) == DialogResult.Cancel ? null 
                : (IViewModel)new ViewModel(new Models.LaneAssignment());
    }
    public Form(IConfiguration config, TournamentId tournamentId, SquadId squadId, int startingLane, int numberOfLanes, int maxPerPair)
    {
        InitializeComponent();
        _config = config;

        TournamentId = tournamentId;
        SquadId = squadId;
        StartingLane = startingLane;
        NumberOfLanes = numberOfLanes;
        MaxPerPair = maxPerPair;

        new Presenter(_config, this).Load();
    }

    private Controls.LaneAssignmentControl BuildLaneAssignmentControl(IViewModel viewModel)
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

        new Presenter(_config, this).Update(SquadId, registration!, openLane!.LaneAssignment);  
    }

    private void LaneAssignmentRegistered_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Escape)
        {
            return;
        }

        var registeredLane = sender as IViewModel;

        new Presenter(_config, this).Update(SquadId, registeredLane!, string.Empty);
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

    private void NewRegistrationButton_Click(object sender, EventArgs e)
        => new Presenter(_config, this).NewRegistration();

    private void AddToRegistrationButton_Click(object sender, EventArgs e)
        => new Presenter(_config, this).AddToRegistration();
}

internal static class ExtensionMethods
{
    internal static bool GetDataPresent<T>(this DragEventArgs e) where T : class
        => e.Data!.GetDataPresent(typeof(T));

    internal static T? Data<T>(this DragEventArgs e) where T : class
        => e.Data!.GetData(typeof(T)) as T;
}