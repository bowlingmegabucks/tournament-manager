
using System.Windows.Forms;

namespace NortheastMegabuck.Squads.CheckIn;
public partial class Form : System.Windows.Forms.Form
{
    public Form()
    {
        InitializeComponent();

        //Temp testing code
        for (var i = 1; i <= 40; i+= 2)
        {
            var bowlerA = Add($"{i}A");
            var bowlerB = Add($"{i}B");
            var bowlerC = Add($"{i+1}C");
            var bowlerD = Add($"{i+1}D");

            laneAssignmentFlowLayoutPanel.Controls.AddRange(new[] { bowlerA, bowlerB, bowlerC, bowlerD });
        }

        var bowler1 = new Registrations.Retrieve.SquadRegistrationViewModel("Dave Kipperman", "36 - 54", 0, 0);
        var bowler2 = new Registrations.Retrieve.SquadRegistrationViewModel("Ashlie Kipperman", "Over 55 / Women", 0, 0);
        var bowler3 = new Registrations.Retrieve.SquadRegistrationViewModel("Joe Bowler", "Under 215 Average", 200, 12);

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        unassignedRegistrationsFlowLayoutPanel.Controls.AddRange(bowlers.Select(bowler => Add(bowler)).ToArray());
    }

    private Controls.SquadRegistrationControl Add(string laneAssignment)
    { 
        var control = new Controls.SquadRegistrationControl(laneAssignment)
        {
            Margin = new Padding(0, 0, 0, 0),
            AllowDrop = true
        };

        AddOpenLaneEventsToOpenLane(control);

        return control;
    }

    private Controls.SquadRegistrationControl Add(Registrations.Retrieve.ISquadRegistrationViewModel viewModel)
    {
        var control = new Controls.SquadRegistrationControl()
        {
            Margin = new Padding(3, 0, 0, 0),
        };

        control.Bind(viewModel);

        control.MouseDown += UnassignedRegistration_MouseDown!;

        return control;
    }

    private void UnassignedRegistration_MouseDown(object sender, MouseEventArgs e)
        => (sender as Control)!.DoDragDrop(sender as Registrations.Retrieve.ISquadRegistrationViewModel, DragDropEffects.Move);

    private void LaneAssignmentOpen_DragOver(object sender, DragEventArgs e)
    { 
        if (e.GetDataPresent<Controls.SquadRegistrationControl>())
        {
            (sender as Control)!.BackColor = SystemColors.Highlight;
        }
    }

    private void LaneAssignmentOpen_DragEnter(object sender, DragEventArgs e)
    {
        if (e.GetDataPresent<Controls.SquadRegistrationControl>())
        {
            e.Effect = DragDropEffects.Move;
        }
    }
    private void LaneAssignmentOpen_DragLeave(object sender, EventArgs e)
        => (sender as Control)!.BackColor = SystemColors.Control;

    private void LaneAssignmentOpen_DragDrop(object sender, DragEventArgs e)
    {
        var registration = e.Data<Controls.SquadRegistrationControl>();

        var openLane = sender as Controls.SquadRegistrationControl;

        openLane!.Bind(registration!);
        openLane.MouseDown += UnassignedRegistration_MouseDown!;
        openLane.KeyUp += LaneAssignmentRegistered_KeyUp!;


        if (string.IsNullOrEmpty(registration!.LaneAssignment))
        {
            unassignedRegistrationsFlowLayoutPanel.Controls.Remove(registration);
        }
        else
        {
            var oldLane = laneAssignmentFlowLayoutPanel.Controls.OfType<Controls.SquadRegistrationControl>().Single(lane => lane.LaneAssignment == registration.LaneAssignment);
            oldLane.ClearRegistration();
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

        var registeredLane = sender as Controls.SquadRegistrationControl;

        var unassignedRegistration = Add(registeredLane!);

        unassignedRegistrationsFlowLayoutPanel.Controls.Add(unassignedRegistration);

        registeredLane!.ClearRegistration();
    }
    private void RemoveOpenLaneEventsFromAssignedLane(Controls.SquadRegistrationControl assignedLane)
    {
        assignedLane.DragEnter -= LaneAssignmentOpen_DragEnter!;
        assignedLane.DragOver -= LaneAssignmentOpen_DragOver!;
        assignedLane.DragLeave -= LaneAssignmentOpen_DragLeave!;
        assignedLane.DragDrop -= LaneAssignmentOpen_DragDrop!;
    }

    private void AddOpenLaneEventsToOpenLane(Controls.SquadRegistrationControl openLane)
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