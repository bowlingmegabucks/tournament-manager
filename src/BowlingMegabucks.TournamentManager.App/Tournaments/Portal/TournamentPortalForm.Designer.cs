namespace BowlingMegabucks.TournamentManager.App.Tournaments.Portal;

partial class TournamentPortalForm
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
            this.fieMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.divisionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDivisionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDivisionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.squadsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSquadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSquadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sweepersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSweeperMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSweeperMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sweeperSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.superSweeperResultsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRegistrationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTournamentRegistrationsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atLargeResultsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seedingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portalMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // portalMenuStrip
            // 
            this.portalMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fieMenuItem,
            this.divisionMenuItem,
            this.squadsMenuItem,
            this.sweepersMenuItem,
            this.registrationMenuItem,
            this.resultsMenuItem});
            this.portalMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.portalMenuStrip.Name = "portalMenuStrip";
            this.portalMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.portalMenuStrip.TabIndex = 0;
            this.portalMenuStrip.Text = "menuStrip1";
            // 
            // fieMenuItem
            // 
            this.fieMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
            this.fieMenuItem.Name = "fieMenuItem";
            this.fieMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fieMenuItem.Text = "File";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // divisionMenuItem
            // 
            this.divisionMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDivisionMenuItem,
            this.viewDivisionMenuItem});
            this.divisionMenuItem.Name = "divisionMenuItem";
            this.divisionMenuItem.Size = new System.Drawing.Size(66, 20);
            this.divisionMenuItem.Text = "Divisions";
            // 
            // addDivisionMenuItem
            // 
            this.addDivisionMenuItem.Name = "addDivisionMenuItem";
            this.addDivisionMenuItem.Size = new System.Drawing.Size(99, 22);
            this.addDivisionMenuItem.Text = "Add";
            this.addDivisionMenuItem.Click += new System.EventHandler(this.AddDivisionMenuItem_Click);
            // 
            // viewDivisionMenuItem
            // 
            this.viewDivisionMenuItem.Name = "viewDivisionMenuItem";
            this.viewDivisionMenuItem.Size = new System.Drawing.Size(99, 22);
            this.viewDivisionMenuItem.Text = "View";
            this.viewDivisionMenuItem.Click += new System.EventHandler(this.ViewDivisionsMenuItem_Click);
            // 
            // squadsMenuItem
            // 
            this.squadsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSquadMenuItem,
            this.openSquadMenuItem});
            this.squadsMenuItem.Name = "squadsMenuItem";
            this.squadsMenuItem.Size = new System.Drawing.Size(57, 20);
            this.squadsMenuItem.Text = "Squads";
            // 
            // addSquadMenuItem
            // 
            this.addSquadMenuItem.Name = "addSquadMenuItem";
            this.addSquadMenuItem.Size = new System.Drawing.Size(103, 22);
            this.addSquadMenuItem.Text = "Add";
            this.addSquadMenuItem.Click += new System.EventHandler(this.AddSquadMenuItem_Click);
            // 
            // openSquadMenuItem
            // 
            this.openSquadMenuItem.Name = "openSquadMenuItem";
            this.openSquadMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openSquadMenuItem.Text = "Open";
            this.openSquadMenuItem.Click += new System.EventHandler(this.OpenSquadMenuItem_Click);
            // 
            // sweepersMenuItem
            // 
            this.sweepersMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSweeperMenuItem,
            this.openSweeperMenuItem,
            this.sweeperSeparator,
            this.superSweeperResultsMenuItem});
            this.sweepersMenuItem.Name = "sweepersMenuItem";
            this.sweepersMenuItem.Size = new System.Drawing.Size(68, 20);
            this.sweepersMenuItem.Text = "Sweepers";
            // 
            // addSweeperMenuItem
            // 
            this.addSweeperMenuItem.Name = "addSweeperMenuItem";
            this.addSweeperMenuItem.Size = new System.Drawing.Size(191, 22);
            this.addSweeperMenuItem.Text = "Add";
            this.addSweeperMenuItem.Click += new System.EventHandler(this.AddSweeperMenuItem_Click);
            // 
            // openSweeperMenuItem
            // 
            this.openSweeperMenuItem.Name = "openSweeperMenuItem";
            this.openSweeperMenuItem.Size = new System.Drawing.Size(191, 22);
            this.openSweeperMenuItem.Text = "Open";
            this.openSweeperMenuItem.Click += new System.EventHandler(this.OpenSweeperMenuItem_Click);
            // 
            // sweeperSeparator
            // 
            this.sweeperSeparator.Name = "sweeperSeparator";
            this.sweeperSeparator.Size = new System.Drawing.Size(188, 6);
            // 
            // superSweeperResultsMenuItem
            // 
            this.superSweeperResultsMenuItem.Name = "superSweeperResultsMenuItem";
            this.superSweeperResultsMenuItem.Size = new System.Drawing.Size(191, 22);
            this.superSweeperResultsMenuItem.Text = "Super Sweeper Results";
            this.superSweeperResultsMenuItem.Click += new System.EventHandler(this.SuperSweeperResultsMenuItem_Click);
            // 
            // registrationMenuItem
            // 
            this.registrationMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRegistrationMenuItem,
            this.viewTournamentRegistrationsMenuItem});
            this.registrationMenuItem.Name = "registrationMenuItem";
            this.registrationMenuItem.Size = new System.Drawing.Size(82, 20);
            this.registrationMenuItem.Text = "Registration";
            // 
            // addRegistrationMenuItem
            // 
            this.addRegistrationMenuItem.Name = "addRegistrationMenuItem";
            this.addRegistrationMenuItem.Size = new System.Drawing.Size(99, 22);
            this.addRegistrationMenuItem.Text = "Add";
            this.addRegistrationMenuItem.Click += new System.EventHandler(this.AddRegistrationMenuItem_Click);
            // 
            // viewTournamentRegistrationsMenuItem
            // 
            this.viewTournamentRegistrationsMenuItem.Name = "viewTournamentRegistrationsMenuItem";
            this.viewTournamentRegistrationsMenuItem.Size = new System.Drawing.Size(99, 22);
            this.viewTournamentRegistrationsMenuItem.Text = "View";
            this.viewTournamentRegistrationsMenuItem.Click += new System.EventHandler(this.ViewTournamentRegistrationsMenuItem_Click);
            // 
            // resultsMenuItem
            // 
            this.resultsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.atLargeResultsMenuItem,
            this.seedingMenuItem});
            this.resultsMenuItem.Name = "resultsMenuItem";
            this.resultsMenuItem.Size = new System.Drawing.Size(56, 20);
            this.resultsMenuItem.Text = "Results";
            // 
            // atLargeResultsMenuItem
            // 
            this.atLargeResultsMenuItem.Name = "atLargeResultsMenuItem";
            this.atLargeResultsMenuItem.Size = new System.Drawing.Size(180, 22);
            this.atLargeResultsMenuItem.Text = "At Large";
            this.atLargeResultsMenuItem.Click += new System.EventHandler(this.AtLargeResultsMenuItem_Click);
            // 
            // seedingMenuItem
            // 
            this.seedingMenuItem.Name = "seedingMenuItem";
            this.seedingMenuItem.Size = new System.Drawing.Size(180, 22);
            this.seedingMenuItem.Text = "Seeding";
            this.seedingMenuItem.Click += new System.EventHandler(this.SeedingMenuItem_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.portalMenuStrip);
            this.MainMenuStrip = this.portalMenuStrip;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TournamentPortal";
            this.portalMenuStrip.ResumeLayout(false);
            this.portalMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private MenuStrip portalMenuStrip;
    private ToolStripMenuItem fieMenuItem;
    private ToolStripMenuItem divisionMenuItem;
    private ToolStripMenuItem addDivisionMenuItem;
    private ToolStripMenuItem viewDivisionMenuItem;
    private ToolStripMenuItem squadsMenuItem;
    private ToolStripMenuItem addSquadMenuItem;
    private ToolStripMenuItem openSquadMenuItem;
    private ToolStripMenuItem sweepersMenuItem;
    private ToolStripMenuItem addSweeperMenuItem;
    private ToolStripMenuItem openSweeperMenuItem;
    private ToolStripMenuItem registrationMenuItem;
    private ToolStripMenuItem addRegistrationMenuItem;
    private ToolStripMenuItem viewTournamentRegistrationsMenuItem;
    private ToolStripSeparator sweeperSeparator;
    private ToolStripMenuItem superSweeperResultsMenuItem;
    private ToolStripMenuItem exitMenuItem;
    private ToolStripMenuItem resultsMenuItem;
    private ToolStripMenuItem atLargeResultsMenuItem;
    private ToolStripMenuItem seedingMenuItem;
}
