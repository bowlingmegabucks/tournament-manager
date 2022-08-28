using System.ComponentModel;
using System.Data;

namespace NewEnglandClassic.Registrations.Add;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;

    public Form(IConfiguration config, TournamentId tournamentId)
    {
        InitializeComponent();

        TournamentId = tournamentId;
        _config = config;

        new Presenter(config, this).Load();
    }

    public TournamentId TournamentId { get; set; }

    public void BindDivisions(IEnumerable<Divisions.IViewModel> divisions)
    {
        divisionsDropdown.DataSource = divisions.ToList();

        divisionsDropdown.ValueMember = nameof(Divisions.IViewModel.Id);
        divisionsDropdown.DisplayMember = nameof(Divisions.IViewModel.DivisionName);
    }

    public NewEnglandClassic.Divisions.Id DivisionId
        => (NewEnglandClassic.Divisions.Id)divisionsDropdown.SelectedValue;

    public Bowlers.Add.IViewModel Bowler
        => bowlerControl;

    public int? Average
        => averageValue.Value == 0 ? null : (int)averageValue.Value;

    public IEnumerable<SquadId> Squads
        => squadsFlowPanelLayout.Controls.OfType<Controls.ISelectedIds>().Where(control=> control.Selected).Select(control => control.Id).AsEnumerable();

    public IEnumerable<SquadId> Sweepers
        => sweepersFlowLayoutPanel.Controls.OfType<Controls.ISelectedIds>().Where(control => control.Selected).Select(control => control.Id).AsEnumerable();

    public bool SuperSweeper
        => superSweeperCheckBox.Checked;

    private void DivisionsDropDown_Validating(object sender, CancelEventArgs e)
    {
        if (divisionsDropdown.SelectedIndex == -1)
        {
            e.Cancel = true;
            registrationErrorProvider.SetError(divisionsDropdown, "Division is required");
        }
    }

    public void BindSquads(IEnumerable<Squads.IViewModel> squads)
    {
        foreach (var squad in squads)
        {
            squadsFlowPanelLayout.Controls.Add(new Controls.SelectSquadControl(squad.Id, $"{squad.Date:d} ({squad.Date:t})", false));
        }
    }

    public void BindSweepers(IEnumerable<Sweepers.IViewModel> sweepers)
    {
        foreach (var sweeper in sweepers)
        {
            sweepersFlowLayoutPanel.Controls.Add(new Controls.SelectSquadControl(sweeper.Id, $"{sweeper.Date:d} ({sweeper.Date:t})", false));
        }
    }

    public void BindBowler(Bowlers.Retrieve.IViewModel bowler)
    { }

    public void Disable()
    {
        bowlerControl.Enabled = false;
        
        divisionsDropdown.Enabled = false;
        averageValue.Enabled = false;

        squadsGroupbox.Enabled = false;
        sweepersGroupbox.Enabled = false;
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public BowlerId? SelectBowler()
    {
        using var form = new Bowlers.Search.Dialog(_config, true);

        return form.ShowDialog(this) == DialogResult.OK ? form.SelectedBowlerId : null;
    }

    public bool IsValid()
        => ValidateChildren();

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    private void SaveButton_Click(object sender, EventArgs e)
        => new Presenter(_config, this).Execute();
}
