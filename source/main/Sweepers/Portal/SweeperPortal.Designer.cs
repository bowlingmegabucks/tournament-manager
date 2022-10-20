namespace NortheastMegabuck.Sweepers.Portal;

partial class Form
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
            this.portalMenuStrip = new System.Windows.Forms.MenuStrip();
            this.laneAssignmentsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scoresMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portalMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // portalMenuStrip
            // 
            this.portalMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.laneAssignmentsMenuItem,
            this.scoresMenuItem,
            this.cutMenuItem});
            this.portalMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.portalMenuStrip.Name = "portalMenuStrip";
            this.portalMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.portalMenuStrip.TabIndex = 0;
            this.portalMenuStrip.Text = "menuStrip1";
            // 
            // laneAssignmentsMenuItem
            // 
            this.laneAssignmentsMenuItem.Name = "laneAssignmentsMenuItem";
            this.laneAssignmentsMenuItem.Size = new System.Drawing.Size(115, 20);
            this.laneAssignmentsMenuItem.Text = "Lane Assignments";
            this.laneAssignmentsMenuItem.Click += new System.EventHandler(this.LaneAssignmentsMenuItem_Click);
            // 
            // scoresMenuItem
            // 
            this.scoresMenuItem.Name = "scoresMenuItem";
            this.scoresMenuItem.Size = new System.Drawing.Size(53, 20);
            this.scoresMenuItem.Text = "Scores";
            this.scoresMenuItem.Click += new System.EventHandler(this.ScoresMenuItem_Click);
            // 
            // cutMenuItem
            // 
            this.cutMenuItem.Name = "cutMenuItem";
            this.cutMenuItem.Size = new System.Drawing.Size(38, 20);
            this.cutMenuItem.Text = "Cut";
            this.cutMenuItem.Click += new System.EventHandler(this.CutMenuItem_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.portalMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.portalMenuStrip;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SquadPortal";
            this.portalMenuStrip.ResumeLayout(false);
            this.portalMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private MenuStrip portalMenuStrip;
    private ToolStripMenuItem laneAssignmentsMenuItem;
    private ToolStripMenuItem scoresMenuItem;
    private ToolStripMenuItem cutMenuItem;
}