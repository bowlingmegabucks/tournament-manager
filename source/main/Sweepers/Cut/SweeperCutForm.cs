
namespace NortheastMegabuck.Sweepers.Cut;
public partial class Form : System.Windows.Forms.Form
{
    public Form(SquadId squadId)
    {
        InitializeComponent();

        sweeperCutReportViewer.Dock = DockStyle.Fill;
        Controls.Add(sweeperCutReportViewer);

        if (squadId.Value == Guid.NewGuid())
        {
            
        }

        //this is done in the load, maybe we do it on the bind?
        //Report.Load - this does all the bindings and such
        //sweeperCutReportViewer.RefreshReport()
    }
}
