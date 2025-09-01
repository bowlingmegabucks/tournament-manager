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
        mainContainer = new TableLayoutPanel();
        overviewGroupBox = new GroupBox();
        overviewTableLayoutPanel = new TableLayoutPanel();
        nameLabel = new Label();
        nameValueLabel = new Label();
        bowlingCenterLabel = new Label();
        bowlingCenterValueLabel = new Label();
        completedLabel = new Label();
        completedValueLabel = new Label();
        datesGroupBox = new GroupBox();
        datesTableLayoutPanel = new TableLayoutPanel();
        startDateLabel = new Label();
        startDateValueLabel = new Label();
        endDateLabel = new Label();
        endDateValueLabel = new Label();
        financialGroupBox = new GroupBox();
        financialTableLayoutPanel = new TableLayoutPanel();
        entryFeeLabel = new Label();
        entryFeeValueLabel = new Label();
        finalsRatioLabel = new Label();
        finalsRatioValueLabel = new Label();
        cashRatioLabel = new Label();
        cashRatioValueLabel = new Label();
        divisionsGroupBox = new GroupBox();
        divisionsTableLayoutPanel = new TableLayoutPanel();
        squadsGroupBox = new GroupBox();
        squadsTableLayoutPanel = new TableLayoutPanel();
        gamesPerSquadLabel = new Label();
        gamesPerSquadValueLabel = new Label();
        sweepersGroupBox = new GroupBox();
        sweepersTableLayoutPanel = new TableLayoutPanel();
        superSweeperCashRatioLabel2 = new Label();
        superSweeperCashRatioValueLabel2 = new Label();
        portalMenuStrip.SuspendLayout();
        mainContainer.SuspendLayout();
        overviewGroupBox.SuspendLayout();
        overviewTableLayoutPanel.SuspendLayout();
        datesGroupBox.SuspendLayout();
        datesTableLayoutPanel.SuspendLayout();
        financialGroupBox.SuspendLayout();
        financialTableLayoutPanel.SuspendLayout();
        divisionsGroupBox.SuspendLayout();
        divisionsTableLayoutPanel.SuspendLayout();
        squadsGroupBox.SuspendLayout();
        squadsTableLayoutPanel.SuspendLayout();
        sweepersGroupBox.SuspendLayout();
        sweepersTableLayoutPanel.SuspendLayout();
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
        // mainContainer
        // 
        mainContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        mainContainer.ColumnCount = 3;
        mainContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        mainContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        mainContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
        mainContainer.Controls.Add(overviewGroupBox, 0, 0);
        mainContainer.Controls.Add(datesGroupBox, 1, 0);
        mainContainer.Controls.Add(financialGroupBox, 2, 0);
        mainContainer.Controls.Add(divisionsGroupBox, 0, 1);
        mainContainer.Controls.Add(squadsGroupBox, 1, 1);
        mainContainer.Controls.Add(sweepersGroupBox, 2, 1);
        mainContainer.Location = new Point(12, 36);
        mainContainer.Name = "mainContainer";
        mainContainer.RowCount = 4;
        mainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        mainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        mainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        mainContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        mainContainer.Size = new Size(984, 681);
        mainContainer.TabIndex = 1;
        // 
        // overviewGroupBox
        // 
        overviewGroupBox.Controls.Add(overviewTableLayoutPanel);
        overviewGroupBox.Dock = DockStyle.Fill;
        overviewGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        overviewGroupBox.Location = new Point(3, 3);
        overviewGroupBox.Name = "overviewGroupBox";
        overviewGroupBox.Padding = new Padding(10);
        overviewGroupBox.Size = new Size(322, 194);
        overviewGroupBox.TabIndex = 0;
        overviewGroupBox.TabStop = false;
        overviewGroupBox.Text = "Tournament Overview";
        // 
        // overviewTableLayoutPanel
        // 
        overviewTableLayoutPanel.ColumnCount = 1;
        overviewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        overviewTableLayoutPanel.Controls.Add(nameLabel, 0, 0);
        overviewTableLayoutPanel.Controls.Add(nameValueLabel, 0, 1);
        overviewTableLayoutPanel.Controls.Add(bowlingCenterLabel, 0, 2);
        overviewTableLayoutPanel.Controls.Add(bowlingCenterValueLabel, 0, 3);
        overviewTableLayoutPanel.Controls.Add(completedLabel, 0, 4);
        overviewTableLayoutPanel.Controls.Add(completedValueLabel, 0, 5);
        overviewTableLayoutPanel.Dock = DockStyle.Fill;
        overviewTableLayoutPanel.Location = new Point(10, 26);
        overviewTableLayoutPanel.Name = "overviewTableLayoutPanel";
        overviewTableLayoutPanel.RowCount = 6;
        overviewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        overviewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        overviewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        overviewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        overviewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        overviewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        overviewTableLayoutPanel.Size = new Size(302, 158);
        overviewTableLayoutPanel.TabIndex = 0;
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        nameLabel.Location = new Point(3, 0);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(108, 15);
        nameLabel.TabIndex = 0;
        nameLabel.Text = "Tournament Name";
        // 
        // nameValueLabel
        // 
        nameValueLabel.AutoSize = true;
        nameValueLabel.Font = new Font("Segoe UI", 9F);
        nameValueLabel.Location = new Point(3, 15);
        nameValueLabel.Margin = new Padding(3, 0, 3, 8);
        nameValueLabel.Name = "nameValueLabel";
        nameValueLabel.Size = new Size(12, 15);
        nameValueLabel.TabIndex = 1;
        nameValueLabel.Text = "-";
        // 
        // bowlingCenterLabel
        // 
        bowlingCenterLabel.AutoSize = true;
        bowlingCenterLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        bowlingCenterLabel.Location = new Point(3, 38);
        bowlingCenterLabel.Name = "bowlingCenterLabel";
        bowlingCenterLabel.Size = new Size(93, 15);
        bowlingCenterLabel.TabIndex = 2;
        bowlingCenterLabel.Text = "Bowling Center";
        // 
        // bowlingCenterValueLabel
        // 
        bowlingCenterValueLabel.AutoSize = true;
        bowlingCenterValueLabel.Font = new Font("Segoe UI", 9F);
        bowlingCenterValueLabel.Location = new Point(3, 53);
        bowlingCenterValueLabel.Margin = new Padding(3, 0, 3, 8);
        bowlingCenterValueLabel.Name = "bowlingCenterValueLabel";
        bowlingCenterValueLabel.Size = new Size(12, 15);
        bowlingCenterValueLabel.TabIndex = 3;
        bowlingCenterValueLabel.Text = "-";
        // 
        // completedLabel
        // 
        completedLabel.AutoSize = true;
        completedLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        completedLabel.Location = new Point(3, 76);
        completedLabel.Name = "completedLabel";
        completedLabel.Size = new Size(68, 15);
        completedLabel.TabIndex = 4;
        completedLabel.Text = "Completed";
        // 
        // completedValueLabel
        // 
        completedValueLabel.AutoSize = true;
        completedValueLabel.Font = new Font("Segoe UI", 9F);
        completedValueLabel.Location = new Point(3, 91);
        completedValueLabel.Margin = new Padding(3, 0, 3, 8);
        completedValueLabel.Name = "completedValueLabel";
        completedValueLabel.Size = new Size(12, 15);
        completedValueLabel.TabIndex = 5;
        completedValueLabel.Text = "-";
        // 
        // datesGroupBox
        // 
        datesGroupBox.Controls.Add(datesTableLayoutPanel);
        datesGroupBox.Dock = DockStyle.Fill;
        datesGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        datesGroupBox.Location = new Point(331, 3);
        datesGroupBox.Name = "datesGroupBox";
        datesGroupBox.Padding = new Padding(10);
        datesGroupBox.Size = new Size(322, 194);
        datesGroupBox.TabIndex = 1;
        datesGroupBox.TabStop = false;
        datesGroupBox.Text = "Tournament Dates";
        // 
        // datesTableLayoutPanel
        // 
        datesTableLayoutPanel.ColumnCount = 1;
        datesTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        datesTableLayoutPanel.Controls.Add(startDateLabel, 0, 0);
        datesTableLayoutPanel.Controls.Add(startDateValueLabel, 0, 1);
        datesTableLayoutPanel.Controls.Add(endDateLabel, 0, 2);
        datesTableLayoutPanel.Controls.Add(endDateValueLabel, 0, 3);
        datesTableLayoutPanel.Dock = DockStyle.Fill;
        datesTableLayoutPanel.Location = new Point(10, 26);
        datesTableLayoutPanel.Name = "datesTableLayoutPanel";
        datesTableLayoutPanel.RowCount = 4;
        datesTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        datesTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        datesTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        datesTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        datesTableLayoutPanel.Size = new Size(302, 158);
        datesTableLayoutPanel.TabIndex = 0;
        // 
        // startDateLabel
        // 
        startDateLabel.AutoSize = true;
        startDateLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        startDateLabel.Location = new Point(3, 0);
        startDateLabel.Name = "startDateLabel";
        startDateLabel.Size = new Size(65, 15);
        startDateLabel.TabIndex = 0;
        startDateLabel.Text = "Start Date";
        // 
        // startDateValueLabel
        // 
        startDateValueLabel.AutoSize = true;
        startDateValueLabel.Font = new Font("Segoe UI", 9F);
        startDateValueLabel.Location = new Point(3, 15);
        startDateValueLabel.Margin = new Padding(3, 0, 3, 8);
        startDateValueLabel.Name = "startDateValueLabel";
        startDateValueLabel.Size = new Size(12, 15);
        startDateValueLabel.TabIndex = 1;
        startDateValueLabel.Text = "-";
        // 
        // endDateLabel
        // 
        endDateLabel.AutoSize = true;
        endDateLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        endDateLabel.Location = new Point(3, 38);
        endDateLabel.Name = "endDateLabel";
        endDateLabel.Size = new Size(59, 15);
        endDateLabel.TabIndex = 2;
        endDateLabel.Text = "End Date";
        // 
        // endDateValueLabel
        // 
        endDateValueLabel.AutoSize = true;
        endDateValueLabel.Font = new Font("Segoe UI", 9F);
        endDateValueLabel.Location = new Point(3, 53);
        endDateValueLabel.Margin = new Padding(3, 0, 3, 8);
        endDateValueLabel.Name = "endDateValueLabel";
        endDateValueLabel.Size = new Size(12, 15);
        endDateValueLabel.TabIndex = 3;
        endDateValueLabel.Text = "-";
        // 
        // financialGroupBox
        // 
        financialGroupBox.Controls.Add(financialTableLayoutPanel);
        financialGroupBox.Dock = DockStyle.Fill;
        financialGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        financialGroupBox.Location = new Point(659, 3);
        financialGroupBox.Name = "financialGroupBox";
        financialGroupBox.Padding = new Padding(10);
        financialGroupBox.Size = new Size(322, 194);
        financialGroupBox.TabIndex = 2;
        financialGroupBox.TabStop = false;
        financialGroupBox.Text = "Financial Details";
        // 
        // financialTableLayoutPanel
        // 
        financialTableLayoutPanel.ColumnCount = 1;
        financialTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        financialTableLayoutPanel.Controls.Add(entryFeeLabel, 0, 0);
        financialTableLayoutPanel.Controls.Add(entryFeeValueLabel, 0, 1);
        financialTableLayoutPanel.Controls.Add(finalsRatioLabel, 0, 2);
        financialTableLayoutPanel.Controls.Add(finalsRatioValueLabel, 0, 3);
        financialTableLayoutPanel.Controls.Add(cashRatioLabel, 0, 4);
        financialTableLayoutPanel.Controls.Add(cashRatioValueLabel, 0, 5);
        financialTableLayoutPanel.Dock = DockStyle.Fill;
        financialTableLayoutPanel.Location = new Point(10, 26);
        financialTableLayoutPanel.Name = "financialTableLayoutPanel";
        financialTableLayoutPanel.RowCount = 6;
        financialTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        financialTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        financialTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        financialTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        financialTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        financialTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        financialTableLayoutPanel.Size = new Size(302, 158);
        financialTableLayoutPanel.TabIndex = 0;
        // 
        // entryFeeLabel
        // 
        entryFeeLabel.AutoSize = true;
        entryFeeLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        entryFeeLabel.Location = new Point(3, 0);
        entryFeeLabel.Name = "entryFeeLabel";
        entryFeeLabel.Size = new Size(61, 15);
        entryFeeLabel.TabIndex = 0;
        entryFeeLabel.Text = "Entry Fee";
        // 
        // entryFeeValueLabel
        // 
        entryFeeValueLabel.AutoSize = true;
        entryFeeValueLabel.Font = new Font("Segoe UI", 9F);
        entryFeeValueLabel.Location = new Point(3, 15);
        entryFeeValueLabel.Margin = new Padding(3, 0, 3, 8);
        entryFeeValueLabel.Name = "entryFeeValueLabel";
        entryFeeValueLabel.Size = new Size(12, 15);
        entryFeeValueLabel.TabIndex = 1;
        entryFeeValueLabel.Text = "-";
        // 
        // finalsRatioLabel
        // 
        finalsRatioLabel.AutoSize = true;
        finalsRatioLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        finalsRatioLabel.Location = new Point(3, 38);
        finalsRatioLabel.Name = "finalsRatioLabel";
        finalsRatioLabel.Size = new Size(71, 15);
        finalsRatioLabel.TabIndex = 2;
        finalsRatioLabel.Text = "Finals Ratio";
        // 
        // finalsRatioValueLabel
        // 
        finalsRatioValueLabel.AutoSize = true;
        finalsRatioValueLabel.Font = new Font("Segoe UI", 9F);
        finalsRatioValueLabel.Location = new Point(3, 53);
        finalsRatioValueLabel.Margin = new Padding(3, 0, 3, 8);
        finalsRatioValueLabel.Name = "finalsRatioValueLabel";
        finalsRatioValueLabel.Size = new Size(12, 15);
        finalsRatioValueLabel.TabIndex = 3;
        finalsRatioValueLabel.Text = "-";
        // 
        // cashRatioLabel
        // 
        cashRatioLabel.AutoSize = true;
        cashRatioLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        cashRatioLabel.Location = new Point(3, 76);
        cashRatioLabel.Name = "cashRatioLabel";
        cashRatioLabel.Size = new Size(67, 15);
        cashRatioLabel.TabIndex = 4;
        cashRatioLabel.Text = "Cash Ratio";
        // 
        // cashRatioValueLabel
        // 
        cashRatioValueLabel.AutoSize = true;
        cashRatioValueLabel.Font = new Font("Segoe UI", 9F);
        cashRatioValueLabel.Location = new Point(3, 91);
        cashRatioValueLabel.Margin = new Padding(3, 0, 3, 8);
        cashRatioValueLabel.Name = "cashRatioValueLabel";
        cashRatioValueLabel.Size = new Size(12, 15);
        cashRatioValueLabel.TabIndex = 5;
        cashRatioValueLabel.Text = "-";
        // 
        // divisionsGroupBox
        // 
        divisionsGroupBox.Controls.Add(divisionsTableLayoutPanel);
        divisionsGroupBox.Dock = DockStyle.Fill;
        divisionsGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        divisionsGroupBox.Location = new Point(3, 203);
        divisionsGroupBox.Name = "divisionsGroupBox";
        divisionsGroupBox.Padding = new Padding(10);
        divisionsGroupBox.Size = new Size(322, 194);
        divisionsGroupBox.TabIndex = 3;
        divisionsGroupBox.TabStop = false;
        divisionsGroupBox.Text = "Divisions";
        // 
        // divisionsTableLayoutPanel
        // 
        divisionsTableLayoutPanel.ColumnCount = 1;
        divisionsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        divisionsTableLayoutPanel.Dock = DockStyle.Fill;
        divisionsTableLayoutPanel.Location = new Point(10, 26);
        divisionsTableLayoutPanel.Name = "divisionsTableLayoutPanel";
        divisionsTableLayoutPanel.RowCount = 1;
        divisionsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        divisionsTableLayoutPanel.Size = new Size(302, 158);
        divisionsTableLayoutPanel.TabIndex = 0;
        // 
        // squadsGroupBox
        // 
        squadsGroupBox.Controls.Add(squadsTableLayoutPanel);
        squadsGroupBox.Dock = DockStyle.Fill;
        squadsGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        squadsGroupBox.Location = new Point(331, 203);
        squadsGroupBox.Name = "squadsGroupBox";
        squadsGroupBox.Padding = new Padding(10);
        squadsGroupBox.Size = new Size(322, 194);
        squadsGroupBox.TabIndex = 4;
        squadsGroupBox.TabStop = false;
        squadsGroupBox.Text = "Squads";
        // 
        // squadsTableLayoutPanel
        // 
        squadsTableLayoutPanel.ColumnCount = 1;
        squadsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        squadsTableLayoutPanel.Controls.Add(gamesPerSquadLabel, 0, 0);
        squadsTableLayoutPanel.Controls.Add(gamesPerSquadValueLabel, 0, 1);
        squadsTableLayoutPanel.Dock = DockStyle.Fill;
        squadsTableLayoutPanel.Location = new Point(10, 26);
        squadsTableLayoutPanel.Name = "squadsTableLayoutPanel";
        squadsTableLayoutPanel.RowCount = 2;
        squadsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        squadsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        squadsTableLayoutPanel.Size = new Size(302, 158);
        squadsTableLayoutPanel.TabIndex = 0;
        // 
        // gamesPerSquadLabel
        // 
        gamesPerSquadLabel.AutoSize = true;
        gamesPerSquadLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        gamesPerSquadLabel.Location = new Point(3, 0);
        gamesPerSquadLabel.Name = "gamesPerSquadLabel";
        gamesPerSquadLabel.Size = new Size(107, 15);
        gamesPerSquadLabel.TabIndex = 0;
        gamesPerSquadLabel.Text = "Games per Squad";
        // 
        // gamesPerSquadValueLabel
        // 
        gamesPerSquadValueLabel.AutoSize = true;
        gamesPerSquadValueLabel.Font = new Font("Segoe UI", 9F);
        gamesPerSquadValueLabel.Location = new Point(3, 15);
        gamesPerSquadValueLabel.Margin = new Padding(3, 0, 3, 8);
        gamesPerSquadValueLabel.Name = "gamesPerSquadValueLabel";
        gamesPerSquadValueLabel.Size = new Size(12, 15);
        gamesPerSquadValueLabel.TabIndex = 1;
        gamesPerSquadValueLabel.Text = "-";
        // 
        // sweepersGroupBox
        // 
        sweepersGroupBox.Controls.Add(sweepersTableLayoutPanel);
        sweepersGroupBox.Dock = DockStyle.Fill;
        sweepersGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        sweepersGroupBox.Location = new Point(659, 203);
        sweepersGroupBox.Name = "sweepersGroupBox";
        sweepersGroupBox.Padding = new Padding(10);
        sweepersGroupBox.Size = new Size(322, 194);
        sweepersGroupBox.TabIndex = 5;
        sweepersGroupBox.TabStop = false;
        sweepersGroupBox.Text = "Sweepers";
        // 
        // sweepersTableLayoutPanel
        // 
        sweepersTableLayoutPanel.ColumnCount = 1;
        sweepersTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        sweepersTableLayoutPanel.Controls.Add(superSweeperCashRatioLabel2, 0, 0);
        sweepersTableLayoutPanel.Controls.Add(superSweeperCashRatioValueLabel2, 0, 1);
        sweepersTableLayoutPanel.Dock = DockStyle.Fill;
        sweepersTableLayoutPanel.Location = new Point(10, 26);
        sweepersTableLayoutPanel.Name = "sweepersTableLayoutPanel";
        sweepersTableLayoutPanel.RowCount = 2;
        sweepersTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        sweepersTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        sweepersTableLayoutPanel.Size = new Size(302, 158);
        sweepersTableLayoutPanel.TabIndex = 0;
        // 
        // superSweeperCashRatioLabel2
        // 
        superSweeperCashRatioLabel2.AutoSize = true;
        superSweeperCashRatioLabel2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        superSweeperCashRatioLabel2.Location = new Point(3, 0);
        superSweeperCashRatioLabel2.Name = "superSweeperCashRatioLabel2";
        superSweeperCashRatioLabel2.Size = new Size(146, 15);
        superSweeperCashRatioLabel2.TabIndex = 0;
        superSweeperCashRatioLabel2.Text = "Super Sweeper Cash Ratio";
        // 
        // superSweeperCashRatioValueLabel2
        // 
        superSweeperCashRatioValueLabel2.AutoSize = true;
        superSweeperCashRatioValueLabel2.Font = new Font("Segoe UI", 9F);
        superSweeperCashRatioValueLabel2.Location = new Point(3, 15);
        superSweeperCashRatioValueLabel2.Margin = new Padding(3, 0, 3, 8);
        superSweeperCashRatioValueLabel2.Name = "superSweeperCashRatioValueLabel2";
        superSweeperCashRatioValueLabel2.Size = new Size(12, 15);
        superSweeperCashRatioValueLabel2.TabIndex = 1;
        superSweeperCashRatioValueLabel2.Text = "-";
        // 
        // TournamentPortalForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1008, 729);
        Controls.Add(mainContainer);
        Controls.Add(portalMenuStrip);
        MainMenuStrip = portalMenuStrip;
        MaximizeBox = false;
        MinimumSize = new Size(1024, 768);
        Name = "TournamentPortalForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "TournamentPortal";
        portalMenuStrip.ResumeLayout(false);
        portalMenuStrip.PerformLayout();
        mainContainer.ResumeLayout(false);
        overviewGroupBox.ResumeLayout(false);
        overviewTableLayoutPanel.ResumeLayout(false);
        overviewTableLayoutPanel.PerformLayout();
        datesGroupBox.ResumeLayout(false);
        datesTableLayoutPanel.ResumeLayout(false);
        datesTableLayoutPanel.PerformLayout();
        financialGroupBox.ResumeLayout(false);
        financialTableLayoutPanel.ResumeLayout(false);
        financialTableLayoutPanel.PerformLayout();
        divisionsGroupBox.ResumeLayout(false);
        divisionsTableLayoutPanel.ResumeLayout(false);
        divisionsTableLayoutPanel.PerformLayout();
        squadsGroupBox.ResumeLayout(false);
        squadsTableLayoutPanel.ResumeLayout(false);
        squadsTableLayoutPanel.PerformLayout();
        sweepersGroupBox.ResumeLayout(false);
        sweepersTableLayoutPanel.ResumeLayout(false);
        sweepersTableLayoutPanel.PerformLayout();
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
    private ToolStripSeparator sweeperSeparator;
    private ToolStripMenuItem superSweeperResultsMenuItem;
    private ToolStripMenuItem registrationMenuItem;
    private ToolStripMenuItem addRegistrationMenuItem;
    private ToolStripMenuItem viewTournamentRegistrationsMenuItem;
    private ToolStripMenuItem exitMenuItem;
    private ToolStripMenuItem resultsMenuItem;
    private ToolStripMenuItem atLargeResultsMenuItem;
    private ToolStripMenuItem seedingMenuItem;
    private TableLayoutPanel mainContainer;
    private GroupBox overviewGroupBox;
    private TableLayoutPanel overviewTableLayoutPanel;
    private Label nameLabel;
    private Label nameValueLabel;
    private Label bowlingCenterLabel;
    private Label bowlingCenterValueLabel;
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
    private GroupBox divisionsGroupBox;
    private TableLayoutPanel divisionsTableLayoutPanel;
    private GroupBox squadsGroupBox;
    private TableLayoutPanel squadsTableLayoutPanel;
    private Label gamesPerSquadLabel;
    private Label gamesPerSquadValueLabel;
    private GroupBox sweepersGroupBox;
    private TableLayoutPanel sweepersTableLayoutPanel;
    private Label superSweeperCashRatioLabel2;
    private Label superSweeperCashRatioValueLabel2;
}
