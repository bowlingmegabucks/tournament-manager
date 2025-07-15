using System.ComponentModel;
using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Registrations.Add;
internal partial class Form
    : System.Windows.Forms.Form, IView
{
    private readonly IServiceProvider _services;
    private readonly Presenter _presenter;

    /// <summary>
    /// Add Registration from Tournament Portal
    /// </summary>
    /// <param name="services"></param>
    /// <param name="tournamentId"></param>
    public Form(IServiceProvider services, TournamentId tournamentId)
    {
        InitializeComponent();

        _services = services;
        _presenter = new(this, services);

        _ = _presenter.LoadAsync(tournamentId, default);
    }

    /// <summary>
    /// Add Registration from Lane Assignment Screen
    /// </summary>
    /// <param name="services"></param>
    /// <param name="tournamentId"></param>
    /// <param name="squadId"></param>
    public Form(IServiceProvider services, TournamentId tournamentId, SquadId squadId)
    {
        InitializeComponent();

        _services = services;
        _presenter = _services.GetRequiredService<Presenter>();

        _ = _presenter.LoadAsync(tournamentId, squadId, default);
    }

    public void BindDivisions(IEnumerable<Divisions.IViewModel> divisions)
    {
        divisionsDropdown.DataSource = divisions.ToList();

        divisionsDropdown.ValueMember = nameof(Divisions.IViewModel.Id);
        divisionsDropdown.DisplayMember = nameof(Divisions.IViewModel.DivisionName);
    }

    public DivisionId DivisionId
        => (DivisionId)divisionsDropdown.SelectedValue!;

    public Bowlers.IViewModel Bowler
        => bowlerControl;

    public int? Average
        => averageValue.Value == 0 ? null : (int)averageValue.Value;

    public IEnumerable<SquadId> Squads
        => squadsFlowPanelLayout.Controls.OfType<Controls.ISelectedIds>().Where(control => control.Selected).Select(control => control.Id).AsEnumerable();

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

    public void BindSquads(IEnumerable<Squads.IViewModel> squads, SquadId squadId)
    {
        BindSquads(squads);

        var squad = squadsFlowPanelLayout.Controls.OfType<Controls.SelectSquadControl>().SingleOrDefault(control => control.Id == squadId);

        if (squad == null)
        {
            return;
        }

        squad.Selected = true;
        squad.Enabled = false;
    }

    public void BindSweepers(IEnumerable<Sweepers.IViewModel> sweepers)
    {
        foreach (var sweeper in sweepers)
        {
            sweepersFlowLayoutPanel.Controls.Add(new Controls.SelectSquadControl(sweeper.Id, $"{sweeper.Date:d} ({sweeper.Date:t})", false));
        }
    }

    public void BindSweepers(IEnumerable<Sweepers.IViewModel> sweepers, SquadId squadId)
    {
        BindSweepers(sweepers);

        var sweeper = sweepersFlowLayoutPanel.Controls.OfType<Controls.SelectSquadControl>().SingleOrDefault(control => control.Id == squadId);

        if (sweeper == null)
        {
            return;
        }

        sweeper.Selected = true;
        sweeper.Enabled = false;
    }

    public void BindBowler(Bowlers.Retrieve.IViewModel bowler)
    {
        bowlerControl.Id = bowler.Id;
        bowlerControl.FirstName = bowler.FirstName;
        bowlerControl.MiddleInitial = bowler.MiddleInitial;
        bowlerControl.LastName = bowler.LastName;
        bowlerControl.Suffix = bowler.Suffix;
        bowlerControl.StreetAddress = bowler.Street;
        bowlerControl.CityAddress = bowler.City;
        bowlerControl.StateAddress = bowler.State;
        bowlerControl.ZipCode = bowler.ZipCode;
        bowlerControl.EmailAddress = bowler.Email;
        bowlerControl.DateOfBirth = bowler.DateOfBirth;
        bowlerControl.PhoneNumber = bowler.PhoneNumber;
        bowlerControl.USBCId = bowler.USBCId;
        bowlerControl.Gender = bowler.Gender;
        bowlerControl.SocialSecurityNumber = bowler.SSN;
    }

    public void Disable()
    {
        bowlerControl.Enabled = false;

        divisionsDropdown.Enabled = false;
        averageValue.Enabled = false;

        squadsGroupBox.Enabled = false;
        sweepersGroupBox.Enabled = false;
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public BowlerId? SelectBowler()
    {
        using var form = new Bowlers.Search.Dialog(_services, true);

        return form.ShowDialog(this) == DialogResult.OK ? form.SelectedBowlerId : null;
    }

    public bool IsValid()
        => ValidateChildren();

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    private async void SaveButton_Click(object sender, EventArgs e)
        => await _presenter.ExecuteAsync(default).ConfigureAwait(true);
}
