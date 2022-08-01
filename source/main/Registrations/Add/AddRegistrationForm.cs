using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewEnglandClassic.Registrations.Add;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;

    public Form(IConfiguration config, Guid tournamentId)
    {
        InitializeComponent();

        TournamentId = tournamentId;
        _config = config;

        new Presenter(config, this).Load();
    }

    public Guid TournamentId { get; set; }

    public void BindDivisions(IEnumerable<Divisions.IViewModel> divisions)
    {
        ComboBoxDivisions.DataSource = divisions.ToList();

        ComboBoxDivisions.ValueMember = nameof(Divisions.IViewModel.Id);
        ComboBoxDivisions.DisplayMember = nameof(Divisions.IViewModel.DivisionName);
    }

    public Guid Division 
        => (Guid)ComboBoxDivisions.SelectedValue;

    public Bowlers.Add.IViewModel Bowler
        => BowlerControl;

    public int? Average
        => NumericAverage.Value == 0 ? (int?)null : (int)NumericAverage.Value;

    public IEnumerable<Controls.ISelectedIds> Squads
        => FlowLayoutPanelSquads.Controls.OfType<Controls.ISelectedIds>().AsEnumerable();

    public IEnumerable<Controls.ISelectedIds> Sweepers
        => FlowLayoutPanelSweepers.Controls.OfType<Controls.ISelectedIds>().AsEnumerable();

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

    public void DisplayError(string message) => throw new NotImplementedException();

    public Guid? SelectBowler()
    {
        using var form = new Bowlers.Search.Dialog(_config, true);

        return form.ShowDialog(this) == DialogResult.OK ? form.SelectedBowlerId : null;
    }

    private void ButtonSave_Click(object sender, EventArgs e)
        => new Presenter(_config, this).Execute();
}
