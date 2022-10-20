
namespace NortheastMegabuck.Sweepers.Cut;
internal partial class Form : System.Windows.Forms.Form, IView
{
    public Form(IConfiguration config, SquadId squadId) : this()
    {
        new Presenter(config, this).Execute(squadId);
    }

    private Form()
    {
        InitializeComponent();
    }

    public void DisplayError(string message)
        => MessageBox.Show(message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindResults(IEnumerable<IViewModel> results)
    {
        var cashers = results.Where(result => result.Casher);
        var nonCashers = results.Where(result => !result.Casher);

        resultsFlowLayoutPanel.Controls.AddRange(cashers.Select(casher => new Controls.SweeperCutControl(casher)).ToArray());
        resultsFlowLayoutPanel.Controls.Add(new Controls.SweeperCutControl());
        resultsFlowLayoutPanel.Controls.AddRange(nonCashers.Select(nonCasher => new Controls.SweeperCutControl(nonCasher)).ToArray());
    }
}
