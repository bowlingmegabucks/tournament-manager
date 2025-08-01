﻿namespace BowlingMegabucks.TournamentManager.Tournaments.Add;
internal sealed partial class Form 
    : System.Windows.Forms.Form, IView
{
    private readonly Presenter _presenter;

    public Form(IServiceProvider services)
    {
        InitializeComponent();

        newTournament.StartDate = new DateOnly(DateTime.Now.Year, 1, 1);
        newTournament.EndDate = new DateOnly(DateTime.Now.Year, 12, 31);

        _presenter = new(this, services);
    }

    public bool IsValid()
        => ValidateChildren();

    public IViewModel Tournament
        => newTournament;

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    public void OkToClose()
        => DialogResult = DialogResult.OK;

    public void DisplayErrors(IEnumerable<string> errorMessages)
        => MessageBox.Show(string.Join(Environment.NewLine, errorMessages), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

    private async void SaveButton_Click(object sender, EventArgs e)
        => await _presenter.ExecuteAsync(default).ConfigureAwait(true);
}
