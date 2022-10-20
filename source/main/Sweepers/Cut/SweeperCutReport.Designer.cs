namespace NortheastMegabuck.Sweepers.Cut;

partial class Report
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.sweeperCutReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // sweeperCutReportViewer
            // 
            this.sweeperCutReportViewer.LocalReport.ReportEmbeddedResource = "NortheastMegabuck.Sweepers.Cut.SweeperCut.rdlc";
            this.sweeperCutReportViewer.Location = new System.Drawing.Point(0, 0);
            this.sweeperCutReportViewer.Name = "ReportViewer";
            this.sweeperCutReportViewer.ServerReport.BearerToken = null;
            this.sweeperCutReportViewer.Size = new System.Drawing.Size(396, 246);
            this.sweeperCutReportViewer.TabIndex = 0;
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 686);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sweeper Cut";
            this.ResumeLayout(false);

    }

    #endregion

    private Microsoft.Reporting.WinForms.ReportViewer sweeperCutReportViewer;
}