using System.Text;
using NortheastMegabuck.Controls;

namespace NortheastMegabuck.LaneAssignments;
public partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;

    public TournamentId TournamentId { get; }
    public SquadId SquadId { get; }
    public int StartingLane { get; }

    public int NumberOfLanes { get; }

    public int MaxPerPair { get; }

    public short Games { get; }

    private readonly DateTime _squadDate;

    private readonly bool _complete;

    public void BuildLanes(IEnumerable<string> lanes)
    {
        foreach (var lane in lanes)
        {
            var control = new LaneAssignmentControl(lane)
            {
                Margin = new Padding(0, 0, 0, 0),
                AllowDrop = true
            };

            if (!_complete)
            {
                AddOpenLaneEventsToOpenLane(control);
            }

            laneAssignmentFlowLayoutPanel.Controls.Add(control);
        }
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public void Disable()
    {
        laneAssignmentGroupBox.Enabled = false;
        unassignedRegistrationsGroupBox.Enabled = false;
        newRegistrationButton.Enabled = false;
        addToRegistrationButton.Enabled = false;
        laneSkipGroupBox.Enabled = false;
        generateRecapSheetsButton.Enabled = false;
        copyAssignmentsToClipboardLinkLabel.Enabled = false;
    }

    public void BindRegistrations(IEnumerable<IViewModel> registrations)
        => unassignedRegistrationsFlowLayoutPanel.Controls.AddRange(registrations.Select(BuildLaneAssignmentControl).ToArray());

    public void AddToUnassigned(IViewModel registration)
        => unassignedRegistrationsFlowLayoutPanel.Controls.Add(BuildLaneAssignmentControl(registration));

    public void BindLaneAssignments(IEnumerable<IViewModel> registrations)
    {
        foreach (var registration in registrations)
        {
            var openLane = laneAssignmentFlowLayoutPanel.Controls.OfType<LaneAssignmentControl>().Single(lane => lane.LaneAssignment == registration.LaneAssignment);

            openLane!.Bind(registration);

            openLane.MouseDown += UnassignedRegistration_MouseDown!;
            openLane.KeyUp += LaneAssignmentRegistered_KeyUp!;
            openLane.Enter += LaneAssignmentRegistered_Enter!;
            openLane.Leave += LaneAssignmentRegistered_Leave!;
            openLane.ContextMenuStrip = laneAssignmentContextMenuStrip;

            RemoveOpenLaneEventsFromAssignedLane(openLane);
        }   
    }

    public void AssignToLane(IViewModel registration, string position) 
    {
        var openLane = laneAssignmentFlowLayoutPanel.Controls.OfType<LaneAssignmentControl>().Single(control => control.LaneAssignment == position);

        openLane.Bind(registration);

        openLane.MouseDown += UnassignedRegistration_MouseDown!;
        openLane.KeyUp += LaneAssignmentRegistered_KeyUp!;
        openLane.Enter += LaneAssignmentRegistered_Enter!;
        openLane.Leave += LaneAssignmentRegistered_Leave!;
        openLane.ContextMenuStrip = laneAssignmentContextMenuStrip;

        if (string.IsNullOrEmpty(registration!.LaneAssignment))
        {
            unassignedRegistrationsFlowLayoutPanel.Controls.Remove(unassignedRegistrationsFlowLayoutPanel.Controls.OfType<LaneAssignmentControl>().Single(control=> control.BowlerId == registration.BowlerId));
        }
        else
        {
            var oldLane = laneAssignmentFlowLayoutPanel.Controls.OfType<LaneAssignmentControl>().Single(lane => lane.LaneAssignment == registration.LaneAssignment);
            oldLane.ClearRegistration();
            oldLane.ContextMenuStrip = null;
            LaneAssignmentRegistered_Leave(oldLane, new EventArgs());
        }

        RemoveOpenLaneEventsFromAssignedLane(openLane);

        LaneAssignmentOpen_DragLeave(openLane, new EventArgs());
    }

    public void RemoveLaneAssignment(IViewModel registration) 
    {
        unassignedRegistrationsFlowLayoutPanel.Controls.Add(BuildLaneAssignmentControl(registration));

        var registeredLane = laneAssignmentFlowLayoutPanel.Controls.OfType<LaneAssignmentControl>().Single(control => control.LaneAssignment == registration.LaneAssignment);

        registeredLane!.ClearRegistration();
        registeredLane.KeyUp -= LaneAssignmentRegistered_KeyUp!;
        registeredLane.ContextMenuStrip = null;

        AddOpenLaneEventsToOpenLane(registeredLane);

        LaneAssignmentRegistered_Leave(registeredLane, new EventArgs());
    }

    public BowlerId? SelectBowler(TournamentId tournamentId, SquadId squadId)
    {
        using var form = new Bowlers.Search.Dialog(_config, false, tournamentId, new[] { squadId });

        return form.ShowDialog(this) == DialogResult.OK ? form.SelectedBowlerId : null;
    }

    public bool NewRegistration(TournamentId tournamentId, SquadId squadId)
    {
        using var form = new Registrations.Add.Form(_config, tournamentId, squadId);

        return form.ShowDialog(this) == DialogResult.OK;
    }

    public void ClearLanes()
        => laneAssignmentFlowLayoutPanel.Controls.Clear();

    public void ClearUnassigned()
        => unassignedRegistrationsFlowLayoutPanel.Controls.Clear();

    public void DeleteRegistration(BowlerId bowlerId)
    {
        var assigned = laneAssignmentFlowLayoutPanel.Controls.OfType<LaneAssignmentControl>().SingleOrDefault(assignment => assignment.BowlerId == bowlerId);

        if (assigned != null)
        {
            assigned.ClearRegistration();
            assigned.KeyUp -= LaneAssignmentRegistered_KeyUp!;

            AddOpenLaneEventsToOpenLane(assigned);

            LaneAssignmentRegistered_Leave(assigned, new EventArgs());

            return;
        }
        else
        {
            var unassigned = unassignedRegistrationsFlowLayoutPanel.Controls.OfType<LaneAssignmentControl>().Single(registration => registration.BowlerId == bowlerId);
            unassignedRegistrationsFlowLayoutPanel.Controls.Remove(unassigned);
        }
    }

    public bool Confirm(string message)
        => MessageBox.Show(message, "Confirm?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes;

    public Form(IConfiguration config, TournamentId tournamentId, SquadId squadId, int startingLane, int numberOfLanes, int maxPerPair, short gamesPerSquad, DateTime squadDate, bool complete)
    {
        InitializeComponent();
        _config = config;

        TournamentId = tournamentId;
        SquadId = squadId;
        StartingLane = startingLane;
        NumberOfLanes = numberOfLanes;
        MaxPerPair = maxPerPair;
        Games = gamesPerSquad;
        _squadDate = squadDate;
        _complete = complete;

        new Presenter(_config, this).Load();

        if (complete)
        {
            Disable();
        }
    }

    private LaneAssignmentControl BuildLaneAssignmentControl(IViewModel viewModel)
    {
        var control = new LaneAssignmentControl()
        {
            Margin = new Padding(3, 0, 0, 0),
            ContextMenuStrip = laneAssignmentContextMenuStrip
        };

        control.Bind(viewModel);

        control.MouseDown += UnassignedRegistration_MouseDown!;

        return control;
    }

    private void UnassignedRegistration_MouseDown(object sender, MouseEventArgs e)
    { 
        if (e.Button == MouseButtons.Right)
        {
            return;
        }

        (sender as Control)!.DoDragDrop(sender as IViewModel, DragDropEffects.Move);
    }

    private void LaneAssignmentOpen_DragOver(object sender, DragEventArgs e)
    { 
        if (e.GetDataPresent<LaneAssignmentControl>())
        {
            (sender as Control)!.BackColor = SystemColors.Highlight;
        }
    }

    private void LaneAssignmentOpen_DragEnter(object sender, DragEventArgs e)
    {
        if (e.GetDataPresent<LaneAssignmentControl>())
        {
            e.Effect = DragDropEffects.Move;
        }
    }
    private void LaneAssignmentOpen_DragLeave(object sender, EventArgs e)
        => (sender as Control)!.BackColor = SystemColors.Control;

    private void LaneAssignmentOpen_DragDrop(object sender, DragEventArgs e)
    {
        var registration = e.Data<LaneAssignmentControl>();

        var openLane = sender as LaneAssignmentControl;

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
    private void RemoveOpenLaneEventsFromAssignedLane(LaneAssignmentControl assignedLane)
    {
        assignedLane.DragEnter -= LaneAssignmentOpen_DragEnter!;
        assignedLane.DragOver -= LaneAssignmentOpen_DragOver!;
        assignedLane.DragLeave -= LaneAssignmentOpen_DragLeave!;
        assignedLane.DragDrop -= LaneAssignmentOpen_DragDrop!;
    }

    private void AddOpenLaneEventsToOpenLane(LaneAssignmentControl openLane)
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

    private void CopyAssignmentsToClipboardLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        var assignments = laneAssignmentFlowLayoutPanel.Controls.OfType<IViewModel>().Where(assignment => assignment.BowlerId != BowlerId.Empty).ToList();

        var text = new StringBuilder();

        assignments.ForEach(assignment => text.AppendLine(assignment.ToString()));

        Clipboard.SetText(text.ToString());
    }

    private void GenerateRecapSheetsButton_Click(object sender, EventArgs e)
    {
        var assignments = laneAssignmentFlowLayoutPanel.Controls.OfType<IViewModel>().Where(assignment => assignment.BowlerId != BowlerId.Empty).ToList();

        new Presenter(_config, this).GenerateRecaps(assignments);
    }

    public bool StaggeredSkipSelected
        => staggeredSkipRadioButton.Checked;

    void IView.GenerateRecaps(IEnumerable<Scores.IRecapSheetViewModel> recaps)
    {
        var form = new Scores.RecapSheetForm(_squadDate);

        form.Preview(recaps, Games);
    }

    private void DeleteLaneAssignmentMenuItem_Click(object sender, EventArgs e)
    {
        var menuItem = sender as ToolStripMenuItem;
        var contextMenu = menuItem?.Owner as ContextMenuStrip;
        var assignment = contextMenu?.SourceControl as LaneAssignmentControl;

        new Presenter(_config, this).Delete(assignment!.BowlerId);
    }
}

internal static class ExtensionMethods
{
    internal static bool GetDataPresent<T>(this DragEventArgs e) where T : class
        => e.Data!.GetDataPresent(typeof(T));

    internal static T? Data<T>(this DragEventArgs e) where T : class
        => e.Data!.GetData(typeof(T)) as T;
}