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
        DisposeFields(disposing);

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
        portalMenuStrip = new MenuStrip();
        fieMenuItem = new ToolStripMenuItem();
        exitMenuItem = new ToolStripMenuItem();
        divisionMenuItem = new ToolStripMenuItem();
        addDivisionMenuItem = new ToolStripMenuItem();
        viewDivisionMenuItem = new ToolStripMenuItem();
        squadsMenuItem = new ToolStripMenuItem();
        addSquadMenuItem = new ToolStripMenuItem();
        openSquadMenuItem = new ToolStripMenuItem();
        sweepersMenuItem = new ToolStripMenuItem();
        addSweeperMenuItem = new ToolStripMenuItem();
        openSweeperMenuItem = new ToolStripMenuItem();
        sweeperSeparator = new ToolStripSeparator();
        superSweeperResultsMenuItem = new ToolStripMenuItem();
        registrationMenuItem = new ToolStripMenuItem();
        addRegistrationMenuItem = new ToolStripMenuItem();
        viewTournamentRegistrationsMenuItem = new ToolStripMenuItem();
        resultsMenuItem = new ToolStripMenuItem();
        atLargeResultsMenuItem = new ToolStripMenuItem();
        seedingMenuItem = new ToolStripMenuItem();
        portalMenuStrip.SuspendLayout();
        SuspendLayout();
        // 
        // portalMenuStrip
        // 
        portalMenuStrip.Items.AddRange(new ToolStripItem[] { fieMenuItem, divisionMenuItem, squadsMenuItem, sweepersMenuItem, registrationMenuItem, resultsMenuItem });
        portalMenuStrip.Location = new Point(0, 0);
        portalMenuStrip.Name = "portalMenuStrip";
        portalMenuStrip.Size = new Size(1008, 24);
        portalMenuStrip.TabIndex = 0;
        portalMenuStrip.Text = "menuStrip1";
        // 
        // fieMenuItem
        // 
        fieMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitMenuItem });
        fieMenuItem.Name = "fieMenuItem";
        fieMenuItem.Size = new Size(37, 20);
        fieMenuItem.Text = "File";
        // 
        // exitMenuItem
        // 
        exitMenuItem.Name = "exitMenuItem";
        exitMenuItem.Size = new Size(92, 22);
        exitMenuItem.Text = "E&xit";
        exitMenuItem.Click += ExitMenuItem_Click;
        // 
        // divisionMenuItem
        // 
        divisionMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addDivisionMenuItem, viewDivisionMenuItem });
        divisionMenuItem.Name = "divisionMenuItem";
        divisionMenuItem.Size = new Size(66, 20);
        divisionMenuItem.Text = "Divisions";
        // 
        // addDivisionMenuItem
        // 
        addDivisionMenuItem.Name = "addDivisionMenuItem";
        addDivisionMenuItem.Size = new Size(99, 22);
        addDivisionMenuItem.Text = "Add";
        addDivisionMenuItem.Click += AddDivisionMenuItem_Click;
        // 
        // viewDivisionMenuItem
        // 
        viewDivisionMenuItem.Name = "viewDivisionMenuItem";
        viewDivisionMenuItem.Size = new Size(99, 22);
        viewDivisionMenuItem.Text = "View";
        viewDivisionMenuItem.Click += ViewDivisionsMenuItem_Click;
        // 
        // squadsMenuItem
        // 
        squadsMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addSquadMenuItem, openSquadMenuItem });
        squadsMenuItem.Name = "squadsMenuItem";
        squadsMenuItem.Size = new Size(57, 20);
        squadsMenuItem.Text = "Squads";
        // 
        // addSquadMenuItem
        // 
        addSquadMenuItem.Name = "addSquadMenuItem";
        addSquadMenuItem.Size = new Size(103, 22);
        addSquadMenuItem.Text = "Add";
        addSquadMenuItem.Click += AddSquadMenuItem_Click;
        // 
        // openSquadMenuItem
        // 
        openSquadMenuItem.Name = "openSquadMenuItem";
        openSquadMenuItem.Size = new Size(103, 22);
        openSquadMenuItem.Text = "Open";
        openSquadMenuItem.Click += OpenSquadMenuItem_Click;
        // 
        // sweepersMenuItem
        // 
        sweepersMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addSweeperMenuItem, openSweeperMenuItem, sweeperSeparator, superSweeperResultsMenuItem });
        sweepersMenuItem.Name = "sweepersMenuItem";
        sweepersMenuItem.Size = new Size(68, 20);
        sweepersMenuItem.Text = "Sweepers";
        // 
        // addSweeperMenuItem
        // 
        addSweeperMenuItem.Name = "addSweeperMenuItem";
        addSweeperMenuItem.Size = new Size(191, 22);
        addSweeperMenuItem.Text = "Add";
        addSweeperMenuItem.Click += AddSweeperMenuItem_Click;
        // 
        // openSweeperMenuItem
        // 
        openSweeperMenuItem.Name = "openSweeperMenuItem";
        openSweeperMenuItem.Size = new Size(191, 22);
        openSweeperMenuItem.Text = "Open";
        openSweeperMenuItem.Click += OpenSweeperMenuItem_Click;
        // 
        // sweeperSeparator
        // 
        sweeperSeparator.Name = "sweeperSeparator";
        sweeperSeparator.Size = new Size(188, 6);
        // 
        // superSweeperResultsMenuItem
        // 
        superSweeperResultsMenuItem.Name = "superSweeperResultsMenuItem";
        superSweeperResultsMenuItem.Size = new Size(191, 22);
        superSweeperResultsMenuItem.Text = "Super Sweeper Results";
        superSweeperResultsMenuItem.Click += SuperSweeperResultsMenuItem_Click;
        // 
        // registrationMenuItem
        // 
        registrationMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addRegistrationMenuItem, viewTournamentRegistrationsMenuItem });
        registrationMenuItem.Name = "registrationMenuItem";
        registrationMenuItem.Size = new Size(82, 20);
        registrationMenuItem.Text = "Registration";
        // 
        // addRegistrationMenuItem
        // 
        addRegistrationMenuItem.Name = "addRegistrationMenuItem";
        addRegistrationMenuItem.Size = new Size(99, 22);
        addRegistrationMenuItem.Text = "Add";
        addRegistrationMenuItem.Click += AddRegistrationMenuItem_Click;
        // 
        // viewTournamentRegistrationsMenuItem
        // 
        viewTournamentRegistrationsMenuItem.Name = "viewTournamentRegistrationsMenuItem";
        viewTournamentRegistrationsMenuItem.Size = new Size(99, 22);
        viewTournamentRegistrationsMenuItem.Text = "View";
        viewTournamentRegistrationsMenuItem.Click += ViewTournamentRegistrationsMenuItem_Click;
        // 
        // resultsMenuItem
        // 
        resultsMenuItem.DropDownItems.AddRange(new ToolStripItem[] { atLargeResultsMenuItem, seedingMenuItem });
        resultsMenuItem.Name = "resultsMenuItem";
        resultsMenuItem.Size = new Size(56, 20);
        resultsMenuItem.Text = "Results";
        // 
        // atLargeResultsMenuItem
        // 
        atLargeResultsMenuItem.Name = "atLargeResultsMenuItem";
        atLargeResultsMenuItem.Size = new Size(118, 22);
        atLargeResultsMenuItem.Text = "At Large";
        atLargeResultsMenuItem.Click += AtLargeResultsMenuItem_Click;
        // 
        // seedingMenuItem
        // 
        seedingMenuItem.Name = "seedingMenuItem";
        seedingMenuItem.Size = new Size(118, 22);
        seedingMenuItem.Text = "Seeding";
        seedingMenuItem.Click += SeedingMenuItem_Click;
        // 
        // TournamentPortalForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1008, 729);
        Controls.Add(portalMenuStrip);
        MainMenuStrip = portalMenuStrip;
        MaximizeBox = false;
        MinimumSize = new Size(1024, 768);
        Name = "TournamentPortalForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "TournamentPortal";
        portalMenuStrip.ResumeLayout(false);
        portalMenuStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();

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
