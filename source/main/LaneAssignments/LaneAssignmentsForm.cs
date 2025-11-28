using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Versioning;
using System.Text;
using BowlingMegabucks.TournamentManager.Controls;

namespace BowlingMegabucks.TournamentManager.LaneAssignments;

[SupportedOSPlatform("windows")]
internal sealed partial class Form 
    : System.Windows.Forms.Form, IView
{
    private Scores.RecapSheetForm? _recapSheetForm;

    private readonly Presenter _presenter;
    private readonly IServiceProvider _services;

    public TournamentId TournamentId { get; }
    public SquadId SquadId { get; }
    public int StartingLane { get; }

    public int NumberOfLanes { get; }

    public int MaxPerPair { get; }

    public short Games { get; }

    private readonly DateTime _squadDate;

    private readonly bool _complete;

    public void BuildLanes([NotNull] IEnumerable<string> lanes)
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
        refreshAssignmentsLinkLabel.Enabled = false;
    }

    public void BindRegistrations(IEnumerable<IViewModel> registrations)
        => unassignedRegistrationsFlowLayoutPanel.Controls.AddRange(registrations.Select(BuildLaneAssignmentControl).ToArray());

    public void AddToUnassigned([NotNull] IViewModel laneAssignment)
    {
        unassignedRegistrationsFlowLayoutPanel.Controls.Add(BuildLaneAssignmentControl(laneAssignment));

        if (_divisionEntries.TryGetValue(laneAssignment.DivisionName, out var entries))
        {
            _divisionEntries[laneAssignment.DivisionName] = entries + 1;
        }
        else
        {
            _divisionEntries.Add(laneAssignment.DivisionName, 1);
        }

        BindEntriesPerDivision();
    }

    public void BindLaneAssignments([NotNull] IEnumerable<IViewModel> assignments)
    {
        foreach (var registration in assignments)
        {
            var openLane = laneAssignmentFlowLayoutPanel.Controls.OfType<LaneAssignmentControl>().Single(lane => lane.LaneAssignment == registration.LaneAssignment);

            openLane!.Bind(registration);

            openLane.MouseDown -= UnassignedRegistration_MouseDown!;
            openLane.Enter -= LaneAssignmentRegistered_Enter!;
            openLane.Leave -= LaneAssignmentRegistered_Leave!;
            openLane.MouseDown += UnassignedRegistration_MouseDown!;
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

        openLane.MouseDown -= UnassignedRegistration_MouseDown!;
        openLane.Enter -= LaneAssignmentRegistered_Enter!;
        openLane.Leave -= LaneAssignmentRegistered_Leave!;
        openLane.MouseDown += UnassignedRegistration_MouseDown!;
        openLane.Enter += LaneAssignmentRegistered_Enter!;
        openLane.Leave += LaneAssignmentRegistered_Leave!;
        openLane.ContextMenuStrip = laneAssignmentContextMenuStrip;

        if (string.IsNullOrEmpty(registration!.LaneAssignment))
        {
            var unassignedLane = unassignedRegistrationsFlowLayoutPanel.Controls.OfType<LaneAssignmentControl>().Single(control => control.BowlerId == registration.BowlerId);
            unassignedRegistrationsFlowLayoutPanel.Controls.Remove(unassignedLane);
            unassignedLane.Dispose();
            unassignedRegistrationsFlowLayoutPanel.PerformLayout();
        }
        else
        {
            var oldLane = laneAssignmentFlowLayoutPanel.Controls.OfType<LaneAssignmentControl>().Single(lane => lane.LaneAssignment == registration.LaneAssignment);
            oldLane.ClearRegistration();
            oldLane.ContextMenuStrip = null;

            LaneAssignmentRegistered_Leave(oldLane, new EventArgs());
            AddOpenLaneEventsToOpenLane(oldLane);
        }

        RemoveOpenLaneEventsFromAssignedLane(openLane);

        LaneAssignmentOpen_DragLeave(openLane, new EventArgs());
    }

    public void RemoveLaneAssignment(IViewModel registration)
    {
        unassignedRegistrationsFlowLayoutPanel.Controls.Add(BuildLaneAssignmentControl(registration));

        var registeredLane = laneAssignmentFlowLayoutPanel.Controls.OfType<LaneAssignmentControl>().Single(control => control.LaneAssignment == registration.LaneAssignment);

        registeredLane!.ClearRegistration();
        registeredLane.ContextMenuStrip = null;

        AddOpenLaneEventsToOpenLane(registeredLane);

        LaneAssignmentRegistered_Leave(registeredLane, new EventArgs());
    }

    public BowlerId? SelectBowler(TournamentId tournamentId, SquadId squadId)
    {
        using var form = new Bowlers.Search.Dialog(_services, false, tournamentId, [squadId]);

        return form.ShowDialog(this) == DialogResult.OK ? form.SelectedBowlerId : null;
    }

    public bool NewRegistration(TournamentId tournamentId, SquadId squadId)
    {
        using var form = new Registrations.Add.Form(_services, tournamentId, squadId);

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
            _divisionEntries[assigned.DivisionName]--;

            assigned.ClearRegistration();

            AddOpenLaneEventsToOpenLane(assigned);

            LaneAssignmentRegistered_Leave(assigned, new EventArgs());
        }
        else
        {
            var unassigned = unassignedRegistrationsFlowLayoutPanel.Controls.OfType<LaneAssignmentControl>().Single(registration => registration.BowlerId == bowlerId);

            _divisionEntries[unassigned.DivisionName]--;
            unassignedRegistrationsFlowLayoutPanel.Controls.Remove(unassigned);
        }

        BindEntriesPerDivision();
    }

    public bool Confirm(string message)
        => MessageBox.Show(message, "Confirm?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes;

    public Form(IServiceProvider services, TournamentId tournamentId, SquadId squadId, int startingLane, int numberOfLanes, int maxPerPair, short gamesPerSquad, DateTime squadDate, bool complete)
    {
        InitializeComponent();
        _services = services;

        _presenter = new(this, services);

        TournamentId = tournamentId;
        SquadId = squadId;
        StartingLane = startingLane;
        NumberOfLanes = numberOfLanes;
        MaxPerPair = maxPerPair;
        Games = gamesPerSquad;
        _squadDate = squadDate;
        _complete = complete;

        _ = _presenter.LoadAsync(default);

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

        (sender as Control)!.DoDragDrop((sender as IViewModel)!, DragDropEffects.Move);
    }

    private void LaneAssignmentOpen_DragOver(object sender, DragEventArgs e)
    {
        if (e.GetDataPresent<LaneAssignmentControl>())
        {
            (sender as Control)!.BackColor = SystemColors.Highlight;
        }
    }

    public void ClearHighlights()
    {
        foreach (var lane in laneAssignmentFlowLayoutPanel.Controls.OfType<LaneAssignmentControl>())
        {
            LaneAssignmentOpen_DragLeave(lane, new EventArgs());
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

        if (registration!.BowlerId == BowlerId.Empty)
        {
            return;
        }

        var openLane = sender as LaneAssignmentControl;

        _ = _presenter.UpdateAsync(SquadId, registration!, openLane!.LaneAssignment, default).ConfigureAwait(true);
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
        => _ = _presenter.NewRegistrationAsync(default).ConfigureAwait(true);

    private void AddToRegistrationButton_Click(object sender, EventArgs e)
        => _ = _presenter.AddToRegistrationAsync(default).ConfigureAwait(true);

    private void CopyAssignmentsToClipboardLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        var assignments = laneAssignmentFlowLayoutPanel.Controls.OfType<IViewModel>().Where(assignment => assignment.BowlerId != BowlerId.Empty).ToList();

        if (assignments.Count == 0)
        {
            MessageBox.Show("No lanes have been assigned", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

        var text = new StringBuilder();

        assignments.ForEach(assignment => text.AppendLine(assignment.ToString()));

        Clipboard.SetText(text.ToString());
    }

    private void GenerateRecapSheetsButton_Click(object sender, EventArgs e)
    {
        var assignments = laneAssignmentFlowLayoutPanel.Controls.OfType<IViewModel>().Where(assignment => assignment.BowlerId != BowlerId.Empty).ToList();

        _presenter.GenerateRecaps(assignments);
    }

    public bool StaggeredSkipSelected
        => staggeredSkipRadioButton.Checked;

    void IView.GenerateRecaps(IEnumerable<Scores.IRecapSheetViewModel> recaps)
    {
        _recapSheetForm = new Scores.RecapSheetForm(_squadDate);

        _recapSheetForm.Preview(recaps, Games);
    }

    private void DeleteLaneAssignmentMenuItem_Click(object sender, EventArgs e)
    {
        var menuItem = sender as ToolStripMenuItem;
        var contextMenu = menuItem?.Owner as ContextMenuStrip;
        var assignment = contextMenu?.SourceControl as LaneAssignmentControl;

        _ = _presenter.DeleteAsync(assignment!.BowlerId, default).ConfigureAwait(true);
    }

    private async void RefreshAssignmentsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        refreshAssignmentsLinkLabel.Enabled = false;

        laneAssignmentFlowLayoutPanel.Visible = false;
        unassignedRegistrationsFlowLayoutPanel.Visible = false;

        //look into why we can't just clear the controls
        var laneAssignments = laneAssignmentFlowLayoutPanel.Controls.OfType<IViewModel>().ToList();

        foreach (var laneAssignment in laneAssignments)
        {
            var control = laneAssignment as Control;
            control!.Dispose();
        }

        var unassignedRegistrations = unassignedRegistrationsFlowLayoutPanel.Controls.OfType<IViewModel>().ToList();

        foreach (var unassignedRegistration in unassignedRegistrations)
        {
            var control = unassignedRegistration as Control;
            control!.Dispose();
        }

        await _presenter.LoadAsync(default).ConfigureAwait(true);

        laneAssignmentFlowLayoutPanel.Visible = true;
        unassignedRegistrationsFlowLayoutPanel.Visible = true;

        refreshAssignmentsLinkLabel.Enabled = true;
    }

    private IDictionary<string, int> _divisionEntries = new Dictionary<string, int>();

    public void BindEntriesPerDivision(IDictionary<string, int> entriesPerDivision)
    {
        _divisionEntries = entriesPerDivision;

        BindEntriesPerDivision();
    }

    private void BindEntriesPerDivision()
    {
        var entriesPerDivision = new StringBuilder(Environment.NewLine);

        foreach (var entry in _divisionEntries.Where(entries => entries.Value > 0))
        {
            entriesPerDivision.AppendLine(CultureInfo.CurrentCulture, $"{entry.Key}: {entry.Value}");
            entriesPerDivision.AppendLine();
        }

        entriesPerDivisionLabel.Text = entriesPerDivision.ToString();
    }

    private void RemoveFromLaneAssignmentToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var menuItem = sender as ToolStripMenuItem;
        var contextMenu = menuItem?.Owner as ContextMenuStrip;
        var assignment = contextMenu?.SourceControl as LaneAssignmentControl;

        _ = _presenter.UpdateAsync(SquadId, assignment!, string.Empty, default).ConfigureAwait(true);
    }
}

[SupportedOSPlatform("windows")]
internal static class ExtensionMethods
{
    internal static bool GetDataPresent<T>(this DragEventArgs e) where T : class
        => e.Data!.GetDataPresent(typeof(T));

    internal static T? Data<T>(this DragEventArgs e) where T : class
        => e.Data!.GetData(typeof(T)) as T;
}