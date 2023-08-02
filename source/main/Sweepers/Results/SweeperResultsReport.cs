
namespace NortheastMegabuck.Sweepers.Results;
public partial class Report : System.Windows.Forms.Form
{
    //this will take in the collection of viewmodels since the form will already have the data
    public Report()
    {
        InitializeComponent();

        sweeperCutReportViewer.Dock = DockStyle.Fill;
        Controls.Add(sweeperCutReportViewer);

        //this is done in the load, maybe we do it on the bind?
        //Report.Load - this does all the bindings and such
        //sweeperCutReportViewer.RefreshReport()
    }
}
