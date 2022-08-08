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
        ComboBoxDivisions.DataSource = divisions.ToList();

        ComboBoxDivisions.ValueMember = nameof(Divisions.IViewModel.Id);
        ComboBoxDivisions.DisplayMember = nameof(Divisions.IViewModel.DivisionName);
    }

    public DivisionId DivisionId
        => (DivisionId)ComboBoxDivisions.SelectedValue;

    public Bowlers.Add.IViewModel Bowler
        => BowlerControl;

    public int? Average
        => NumericAverage.Value == 0 ? null : (int)NumericAverage.Value;

    public IEnumerable<SquadId> Squads
        => FlowLayoutPanelSquads.Controls.OfType<Controls.ISelectedIds>().Where(control=> control.Selected).Select(control => control.Id).AsEnumerable();

    public IEnumerable<SquadId> Sweepers
        => FlowLayoutPanelSweepers.Controls.OfType<Controls.ISelectedIds>().Where(control => control.Selected).Select(control => control.Id).AsEnumerable();

    private void ComboBoxDivisions_Validating(object sender, CancelEventArgs e)
    {
        if (ComboBoxDivisions.SelectedIndex == -1)
        {
            e.Cancel = true;
            ErrorProviderRegistration.SetError(ComboBoxDivisions, "Division is required");
        }
    }

    public void BindSquads(IEnumerable<Squads.IViewModel> squads)
    {
        foreach (var squad in squads)
        {
            FlowLayoutPanelSquads.Controls.Add(new Controls.SelectSquadControl(squad.Id, $"{squad.Date:d} ({squad.Date:t})", false));
        }
    }

    public void BindSweepers(IEnumerable<Sweepers.IViewModel> sweepers)
    {
        foreach (var sweeper in sweepers)
        {
            FlowLayoutPanelSweepers.Controls.Add(new Controls.SelectSquadControl(sweeper.Id, $"{sweeper.Date:d} ({sweeper.Date:t})", false));
        }
    }

    public void BindBowler(Bowlers.Retrieve.IViewModel bowler)
    { }

    public void Disable()
    {
        BowlerControl.Enabled = false;
        
        ComboBoxDivisions.Enabled = false;
        NumericAverage.Enabled = false;

        GroupboxSquads.Enabled = false;
        GroupboxSweepers.Enabled = false;
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

    private void ButtonSave_Click(object sender, EventArgs e)
        => new Presenter(_config, this).Execute();
}
