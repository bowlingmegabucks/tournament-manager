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
        this.mainContainer = new System.Windows.Forms.FlowLayoutPanel();
        this.overviewGroupBox = new System.Windows.Forms.GroupBox();
        this.overviewTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
        this.nameLabel = new System.Windows.Forms.Label();
        this.nameValueLabel = new System.Windows.Forms.Label();
        this.bowlingCenterLabel = new System.Windows.Forms.Label();
        this.bowlingCenterValueLabel = new System.Windows.Forms.Label();
        this.gamesLabel = new System.Windows.Forms.Label();
        this.gamesValueLabel = new System.Windows.Forms.Label();
        this.completedLabel = new System.Windows.Forms.Label();
        this.completedValueLabel = new System.Windows.Forms.Label();
        this.datesGroupBox = new System.Windows.Forms.GroupBox();
        this.datesTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
        this.startDateLabel = new System.Windows.Forms.Label();
        this.startDateValueLabel = new System.Windows.Forms.Label();
        this.endDateLabel = new System.Windows.Forms.Label();
        this.endDateValueLabel = new System.Windows.Forms.Label();
        this.financialGroupBox = new System.Windows.Forms.GroupBox();
        this.financialTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
        this.entryFeeLabel = new System.Windows.Forms.Label();
        this.entryFeeValueLabel = new System.Windows.Forms.Label();
        this.finalsRatioLabel = new System.Windows.Forms.Label();
        this.finalsRatioValueLabel = new System.Windows.Forms.Label();
        this.cashRatioLabel = new System.Windows.Forms.Label();
        this.cashRatioValueLabel = new System.Windows.Forms.Label();
        this.superSweeperCashRatioLabel = new System.Windows.Forms.Label();
        this.superSweeperCashRatioValueLabel = new System.Windows.Forms.Label();
        this.portalMenuStrip.SuspendLayout();
        this.overviewGroupBox.SuspendLayout();
        this.overviewTableLayoutPanel.SuspendLayout();
        this.datesGroupBox.SuspendLayout();
        this.datesTableLayoutPanel.SuspendLayout();
        this.financialGroupBox.SuspendLayout();
        this.financialTableLayoutPanel.SuspendLayout();
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
        // mainContainer
        //
        this.mainContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.mainContainer.AutoScroll = true;
        this.mainContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        this.mainContainer.Location = new System.Drawing.Point(12, 27);
        this.mainContainer.Name = "mainContainer";
        this.mainContainer.Size = new System.Drawing.Size(776, 411);
        this.mainContainer.TabIndex = 1;
        this.mainContainer.WrapContents = false;
        //
        // overviewGroupBox
        //
        this.overviewGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.overviewGroupBox.Controls.Add(this.overviewTableLayoutPanel);
        this.overviewGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.overviewGroupBox.Location = new System.Drawing.Point(3, 3);
        this.overviewGroupBox.Name = "overviewGroupBox";
        this.overviewGroupBox.Size = new System.Drawing.Size(770, 120);
        this.overviewGroupBox.TabIndex = 0;
        this.overviewGroupBox.TabStop = false;
        this.overviewGroupBox.Text = "Tournament Overview";
        //
        // overviewTableLayoutPanel
        //
        this.overviewTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.overviewTableLayoutPanel.ColumnCount = 2;
        this.overviewTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
        this.overviewTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
        this.overviewTableLayoutPanel.Controls.Add(this.nameLabel, 0, 0);
        this.overviewTableLayoutPanel.Controls.Add(this.nameValueLabel, 1, 0);
        this.overviewTableLayoutPanel.Controls.Add(this.bowlingCenterLabel, 0, 1);
        this.overviewTableLayoutPanel.Controls.Add(this.bowlingCenterValueLabel, 1, 1);
        this.overviewTableLayoutPanel.Controls.Add(this.gamesLabel, 0, 2);
        this.overviewTableLayoutPanel.Controls.Add(this.gamesValueLabel, 1, 2);
        this.overviewTableLayoutPanel.Controls.Add(this.completedLabel, 0, 3);
        this.overviewTableLayoutPanel.Controls.Add(this.completedValueLabel, 1, 3);
        this.overviewTableLayoutPanel.Location = new System.Drawing.Point(6, 19);
        this.overviewTableLayoutPanel.Name = "overviewTableLayoutPanel";
        this.overviewTableLayoutPanel.RowCount = 4;
        this.overviewTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
        this.overviewTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
        this.overviewTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
        this.overviewTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
        this.overviewTableLayoutPanel.Size = new System.Drawing.Size(758, 95);
        this.overviewTableLayoutPanel.TabIndex = 0;
        //
        // nameLabel
        //
        this.nameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.nameLabel.AutoSize = true;
        this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.nameLabel.Location = new System.Drawing.Point(3, 0);
        this.nameLabel.Name = "nameLabel";
        this.nameLabel.Size = new System.Drawing.Size(44, 15);
        this.nameLabel.TabIndex = 0;
        this.nameLabel.Text = "Name:";
        //
        // nameValueLabel
        //
        this.nameValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.nameValueLabel.AutoSize = true;
        this.nameValueLabel.Location = new System.Drawing.Point(227, 0);
        this.nameValueLabel.Name = "nameValueLabel";
        this.nameValueLabel.Size = new System.Drawing.Size(100, 15);
        this.nameValueLabel.TabIndex = 1;
        this.nameValueLabel.Text = "<name>";
        //
        // bowlingCenterLabel
        //
        this.bowlingCenterLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.bowlingCenterLabel.AutoSize = true;
        this.bowlingCenterLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.bowlingCenterLabel.Location = new System.Drawing.Point(3, 25);
        this.bowlingCenterLabel.Name = "bowlingCenterLabel";
        this.bowlingCenterLabel.Size = new System.Drawing.Size(95, 15);
        this.bowlingCenterLabel.TabIndex = 2;
        this.bowlingCenterLabel.Text = "Bowling Center:";
        //
        // bowlingCenterValueLabel
        //
        this.bowlingCenterValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.bowlingCenterValueLabel.AutoSize = true;
        this.bowlingCenterValueLabel.Location = new System.Drawing.Point(227, 25);
        this.bowlingCenterValueLabel.Name = "bowlingCenterValueLabel";
        this.bowlingCenterValueLabel.Size = new System.Drawing.Size(100, 15);
        this.bowlingCenterValueLabel.TabIndex = 3;
        this.bowlingCenterValueLabel.Text = "<bowling center>";
        //
        // gamesLabel
        //
        this.gamesLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.gamesLabel.AutoSize = true;
        this.gamesLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.gamesLabel.Location = new System.Drawing.Point(3, 50);
        this.gamesLabel.Name = "gamesLabel";
        this.gamesLabel.Size = new System.Drawing.Size(48, 15);
        this.gamesLabel.TabIndex = 4;
        this.gamesLabel.Text = "Games:";
        //
        // gamesValueLabel
        //
        this.gamesValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.gamesValueLabel.AutoSize = true;
        this.gamesValueLabel.Location = new System.Drawing.Point(227, 50);
        this.gamesValueLabel.Name = "gamesValueLabel";
        this.gamesValueLabel.Size = new System.Drawing.Size(100, 15);
        this.gamesValueLabel.TabIndex = 5;
        this.gamesValueLabel.Text = "<games>";
        //
        // completedLabel
        //
        this.completedLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.completedLabel.AutoSize = true;
        this.completedLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.completedLabel.Location = new System.Drawing.Point(3, 75);
        this.completedLabel.Name = "completedLabel";
        this.completedLabel.Size = new System.Drawing.Size(72, 15);
        this.completedLabel.TabIndex = 6;
        this.completedLabel.Text = "Completed:";
        //
        // completedValueLabel
        //
        this.completedValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.completedValueLabel.AutoSize = true;
        this.completedValueLabel.Location = new System.Drawing.Point(227, 75);
        this.completedValueLabel.Name = "completedValueLabel";
        this.completedValueLabel.Size = new System.Drawing.Size(100, 15);
        this.completedValueLabel.TabIndex = 7;
        this.completedValueLabel.Text = "<completed>";
        //
        // datesGroupBox
        //
        this.datesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.datesGroupBox.Controls.Add(this.datesTableLayoutPanel);
        this.datesGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.datesGroupBox.Location = new System.Drawing.Point(3, 129);
        this.datesGroupBox.Name = "datesGroupBox";
        this.datesGroupBox.Size = new System.Drawing.Size(770, 70);
        this.datesGroupBox.TabIndex = 1;
        this.datesGroupBox.TabStop = false;
        this.datesGroupBox.Text = "Dates";
        //
        // datesTableLayoutPanel
        //
        this.datesTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.datesTableLayoutPanel.ColumnCount = 2;
        this.datesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
        this.datesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
        this.datesTableLayoutPanel.Controls.Add(this.startDateLabel, 0, 0);
        this.datesTableLayoutPanel.Controls.Add(this.startDateValueLabel, 1, 0);
        this.datesTableLayoutPanel.Controls.Add(this.endDateLabel, 0, 1);
        this.datesTableLayoutPanel.Controls.Add(this.endDateValueLabel, 1, 1);
        this.datesTableLayoutPanel.Location = new System.Drawing.Point(6, 19);
        this.datesTableLayoutPanel.Name = "datesTableLayoutPanel";
        this.datesTableLayoutPanel.RowCount = 2;
        this.datesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
        this.datesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
        this.datesTableLayoutPanel.Size = new System.Drawing.Size(758, 45);
        this.datesTableLayoutPanel.TabIndex = 0;
        //
        // startDateLabel
        //
        this.startDateLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.startDateLabel.AutoSize = true;
        this.startDateLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.startDateLabel.Location = new System.Drawing.Point(3, 0);
        this.startDateLabel.Name = "startDateLabel";
        this.startDateLabel.Size = new System.Drawing.Size(67, 15);
        this.startDateLabel.TabIndex = 0;
        this.startDateLabel.Text = "Start Date:";
        //
        // startDateValueLabel
        //
        this.startDateValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.startDateValueLabel.AutoSize = true;
        this.startDateValueLabel.Location = new System.Drawing.Point(227, 0);
        this.startDateValueLabel.Name = "startDateValueLabel";
        this.startDateValueLabel.Size = new System.Drawing.Size(100, 15);
        this.startDateValueLabel.TabIndex = 1;
        this.startDateValueLabel.Text = "<start date>";
        //
        // endDateLabel
        //
        this.endDateLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.endDateLabel.AutoSize = true;
        this.endDateLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.endDateLabel.Location = new System.Drawing.Point(3, 25);
        this.endDateLabel.Name = "endDateLabel";
        this.endDateLabel.Size = new System.Drawing.Size(60, 15);
        this.endDateLabel.TabIndex = 2;
        this.endDateLabel.Text = "End Date:";
        //
        // endDateValueLabel
        //
        this.endDateValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.endDateValueLabel.AutoSize = true;
        this.endDateValueLabel.Location = new System.Drawing.Point(227, 25);
        this.endDateValueLabel.Name = "endDateValueLabel";
        this.endDateValueLabel.Size = new System.Drawing.Size(100, 15);
        this.endDateValueLabel.TabIndex = 3;
        this.endDateValueLabel.Text = "<end date>";
        //
        // financialGroupBox
        //
        this.financialGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.financialGroupBox.Controls.Add(this.financialTableLayoutPanel);
        this.financialGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.financialGroupBox.Location = new System.Drawing.Point(3, 205);
        this.financialGroupBox.Name = "financialGroupBox";
        this.financialGroupBox.Size = new System.Drawing.Size(770, 120);
        this.financialGroupBox.TabIndex = 2;
        this.financialGroupBox.TabStop = false;
        this.financialGroupBox.Text = "Financial Details";
        //
        // financialTableLayoutPanel
        //
        this.financialTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.financialTableLayoutPanel.ColumnCount = 2;
        this.financialTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
        this.financialTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
        this.financialTableLayoutPanel.Controls.Add(this.entryFeeLabel, 0, 0);
        this.financialTableLayoutPanel.Controls.Add(this.entryFeeValueLabel, 1, 0);
        this.financialTableLayoutPanel.Controls.Add(this.finalsRatioLabel, 0, 1);
        this.financialTableLayoutPanel.Controls.Add(this.finalsRatioValueLabel, 1, 1);
        this.financialTableLayoutPanel.Controls.Add(this.cashRatioLabel, 0, 2);
        this.financialTableLayoutPanel.Controls.Add(this.cashRatioValueLabel, 1, 2);
        this.financialTableLayoutPanel.Controls.Add(this.superSweeperCashRatioLabel, 0, 3);
        this.financialTableLayoutPanel.Controls.Add(this.superSweeperCashRatioValueLabel, 1, 3);
        this.financialTableLayoutPanel.Location = new System.Drawing.Point(6, 19);
        this.financialTableLayoutPanel.Name = "financialTableLayoutPanel";
        this.financialTableLayoutPanel.RowCount = 4;
        this.financialTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
        this.financialTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
        this.financialTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
        this.financialTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
        this.financialTableLayoutPanel.Size = new System.Drawing.Size(758, 95);
        this.financialTableLayoutPanel.TabIndex = 0;
        //
        // entryFeeLabel
        //
        this.entryFeeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.entryFeeLabel.AutoSize = true;
        this.entryFeeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.entryFeeLabel.Location = new System.Drawing.Point(3, 0);
        this.entryFeeLabel.Name = "entryFeeLabel";
        this.entryFeeLabel.Size = new System.Drawing.Size(61, 15);
        this.entryFeeLabel.TabIndex = 0;
        this.entryFeeLabel.Text = "Entry Fee:";
        //
        // entryFeeValueLabel
        //
        this.entryFeeValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.entryFeeValueLabel.AutoSize = true;
        this.entryFeeValueLabel.Location = new System.Drawing.Point(227, 0);
        this.entryFeeValueLabel.Name = "entryFeeValueLabel";
        this.entryFeeValueLabel.Size = new System.Drawing.Size(100, 15);
        this.entryFeeValueLabel.TabIndex = 1;
        this.entryFeeValueLabel.Text = "<entry fee>";
        //
        // finalsRatioLabel
        //
        this.finalsRatioLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.finalsRatioLabel.AutoSize = true;
        this.finalsRatioLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.finalsRatioLabel.Location = new System.Drawing.Point(3, 25);
        this.finalsRatioLabel.Name = "finalsRatioLabel";
        this.finalsRatioLabel.Size = new System.Drawing.Size(78, 15);
        this.finalsRatioLabel.TabIndex = 2;
        this.finalsRatioLabel.Text = "Finals Ratio:";
        //
        // finalsRatioValueLabel
        //
        this.finalsRatioValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.finalsRatioValueLabel.AutoSize = true;
        this.finalsRatioValueLabel.Location = new System.Drawing.Point(227, 25);
        this.finalsRatioValueLabel.Name = "finalsRatioValueLabel";
        this.finalsRatioValueLabel.Size = new System.Drawing.Size(100, 15);
        this.finalsRatioValueLabel.TabIndex = 3;
        this.finalsRatioValueLabel.Text = "<finals ratio>";
        //
        // cashRatioLabel
        //
        this.cashRatioLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.cashRatioLabel.AutoSize = true;
        this.cashRatioLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.cashRatioLabel.Location = new System.Drawing.Point(3, 50);
        this.cashRatioLabel.Name = "cashRatioLabel";
        this.cashRatioLabel.Size = new System.Drawing.Size(68, 15);
        this.cashRatioLabel.TabIndex = 4;
        this.cashRatioLabel.Text = "Cash Ratio:";
        //
        // cashRatioValueLabel
        //
        this.cashRatioValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.cashRatioValueLabel.AutoSize = true;
        this.cashRatioValueLabel.Location = new System.Drawing.Point(227, 50);
        this.cashRatioValueLabel.Name = "cashRatioValueLabel";
        this.cashRatioValueLabel.Size = new System.Drawing.Size(100, 15);
        this.cashRatioValueLabel.TabIndex = 5;
        this.cashRatioValueLabel.Text = "<cash ratio>";
        //
        // superSweeperCashRatioLabel
        //
        this.superSweeperCashRatioLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.superSweeperCashRatioLabel.AutoSize = true;
        this.superSweeperCashRatioLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.superSweeperCashRatioLabel.Location = new System.Drawing.Point(3, 75);
        this.superSweeperCashRatioLabel.Name = "superSweeperCashRatioLabel";
        this.superSweeperCashRatioLabel.Size = new System.Drawing.Size(138, 15);
        this.superSweeperCashRatioLabel.TabIndex = 6;
        this.superSweeperCashRatioLabel.Text = "Super Sweeper Ratio:";
        //
        // superSweeperCashRatioValueLabel
        //
        this.superSweeperCashRatioValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.superSweeperCashRatioValueLabel.AutoSize = true;
        this.superSweeperCashRatioValueLabel.Location = new System.Drawing.Point(227, 75);
        this.superSweeperCashRatioValueLabel.Name = "superSweeperCashRatioValueLabel";
        this.superSweeperCashRatioValueLabel.Size = new System.Drawing.Size(100, 15);
        this.superSweeperCashRatioValueLabel.TabIndex = 7;
        this.superSweeperCashRatioValueLabel.Text = "<super sweeper ratio>";
        //
        // Form
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.mainContainer);
        this.Controls.Add(this.portalMenuStrip);
        this.MainMenuStrip = this.portalMenuStrip;
        this.MaximizeBox = false;
        this.MinimumSize = new System.Drawing.Size(1024, 768);
        this.Name = "Form";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "TournamentPortal";
        this.mainContainer.Controls.Add(this.overviewGroupBox);
        this.mainContainer.Controls.Add(this.datesGroupBox);
        this.mainContainer.Controls.Add(this.financialGroupBox);
        this.portalMenuStrip.ResumeLayout(false);
        this.portalMenuStrip.PerformLayout();
        this.overviewGroupBox.ResumeLayout(false);
        this.overviewTableLayoutPanel.ResumeLayout(false);
        this.overviewTableLayoutPanel.PerformLayout();
        this.datesGroupBox.ResumeLayout(false);
        this.datesTableLayoutPanel.ResumeLayout(false);
        this.datesTableLayoutPanel.PerformLayout();
        this.financialGroupBox.ResumeLayout(false);
        this.financialTableLayoutPanel.ResumeLayout(false);
        this.financialTableLayoutPanel.PerformLayout();
        this.ResumeLayout(false);

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
    private FlowLayoutPanel mainContainer;
    private GroupBox overviewGroupBox;
    private TableLayoutPanel overviewTableLayoutPanel;
    private Label nameLabel;
    private Label nameValueLabel;
    private Label bowlingCenterLabel;
    private Label bowlingCenterValueLabel;
    private Label gamesLabel;
    private Label gamesValueLabel;
    private Label completedLabel;
    private Label completedValueLabel;
    private GroupBox datesGroupBox;
    private TableLayoutPanel datesTableLayoutPanel;
    private Label startDateLabel;
    private Label startDateValueLabel;
    private Label endDateLabel;
    private Label endDateValueLabel;
    private GroupBox financialGroupBox;
    private TableLayoutPanel financialTableLayoutPanel;
    private Label entryFeeLabel;
    private Label entryFeeValueLabel;
    private Label finalsRatioLabel;
    private Label finalsRatioValueLabel;
    private Label cashRatioLabel;
    private Label cashRatioValueLabel;
    private Label superSweeperCashRatioLabel;
    private Label superSweeperCashRatioValueLabel;
}
