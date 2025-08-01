﻿namespace BowlingMegabucks.TournamentManager.Registrations.Retrieve;

partial class RetrieveTournamentRegistrationsForm
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
        components = new System.ComponentModel.Container();
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(RetrieveTournamentRegistrationsForm));
        tournamentRegistrationsGrid = new Controls.Grids.TournamentRegistrationGrid();
        registrationGridContextMenu = new ContextMenuStrip(components);
        updateBowlerNameMenuItem = new ToolStripMenuItem();
        addSuperSweeperMenuItem = new ToolStripMenuItem();
        changeAverageMenuItem = new ToolStripMenuItem();
        changeDivisionMenuItem = new ToolStripMenuItem();
        toolStripSeparator1 = new ToolStripSeparator();
        deleteMenuItem = new ToolStripMenuItem();
        divisionEntriesGroupBox = new GroupBox();
        divisionEntriesLabel = new Label();
        squadEntriesGroupBox = new GroupBox();
        squadEntriesLabel = new Label();
        sweeperEntriesGroupBox = new GroupBox();
        sweeperEntriesLabel = new Label();
        filterLabel = new Controls.LabelControl();
        filterText = new TextBox();
        updateBowlerInfoMenuItem = new ToolStripMenuItem();
        registrationGridContextMenu.SuspendLayout();
        divisionEntriesGroupBox.SuspendLayout();
        squadEntriesGroupBox.SuspendLayout();
        sweeperEntriesGroupBox.SuspendLayout();
        SuspendLayout();
        // 
        // tournamentRegistrationsGrid
        // 
        tournamentRegistrationsGrid.Location = new Point(12, 67);
        tournamentRegistrationsGrid.Name = "tournamentRegistrationsGrid";
        tournamentRegistrationsGrid.Size = new Size(751, 550);
        tournamentRegistrationsGrid.TabIndex = 0;
        // 
        // registrationGridContextMenu
        // 
        registrationGridContextMenu.Items.AddRange(new ToolStripItem[] { updateBowlerInfoMenuItem, updateBowlerNameMenuItem, addSuperSweeperMenuItem, changeAverageMenuItem, changeDivisionMenuItem, toolStripSeparator1, deleteMenuItem });
        registrationGridContextMenu.Name = "registrationGridContextMenu";
        registrationGridContextMenu.Size = new Size(181, 164);
        // 
        // updateBowlerNameMenuItem
        // 
        updateBowlerNameMenuItem.Name = "updateBowlerNameMenuItem";
        updateBowlerNameMenuItem.Size = new Size(180, 22);
        updateBowlerNameMenuItem.Text = "Update Name";
        updateBowlerNameMenuItem.Click += UpdateBowlerNameMenuItem_Click;
        // 
        // addSuperSweeperMenuItem
        // 
        addSuperSweeperMenuItem.Name = "addSuperSweeperMenuItem";
        addSuperSweeperMenuItem.Size = new Size(180, 22);
        addSuperSweeperMenuItem.Text = "Add Super Sweeper";
        addSuperSweeperMenuItem.Click += AddSuperSweeperMenuItem_Click;
        // 
        // changeAverageMenuItem
        // 
        changeAverageMenuItem.Name = "changeAverageMenuItem";
        changeAverageMenuItem.Size = new Size(180, 22);
        changeAverageMenuItem.Text = "Change Average";
        changeAverageMenuItem.Click += ChangeAverageMenuItem_Click;
        // 
        // changeDivisionMenuItem
        // 
        changeDivisionMenuItem.Name = "changeDivisionMenuItem";
        changeDivisionMenuItem.Size = new Size(180, 22);
        changeDivisionMenuItem.Text = "Change Division";
        changeDivisionMenuItem.Click += ChangeDivisionMenuItem_Click;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(177, 6);
        // 
        // deleteMenuItem
        // 
        deleteMenuItem.Name = "deleteMenuItem";
        deleteMenuItem.Size = new Size(180, 22);
        deleteMenuItem.Text = "Delete";
        deleteMenuItem.Click += DeleteMenuItem_Click;
        // 
        // divisionEntriesGroupBox
        // 
        divisionEntriesGroupBox.Controls.Add(divisionEntriesLabel);
        divisionEntriesGroupBox.Font = new Font("Segoe UI", 11F);
        divisionEntriesGroupBox.Location = new Point(778, 67);
        divisionEntriesGroupBox.Name = "divisionEntriesGroupBox";
        divisionEntriesGroupBox.Size = new Size(278, 131);
        divisionEntriesGroupBox.TabIndex = 1;
        divisionEntriesGroupBox.TabStop = false;
        divisionEntriesGroupBox.Text = "Division Entries";
        // 
        // divisionEntriesLabel
        // 
        divisionEntriesLabel.AutoSize = true;
        divisionEntriesLabel.Dock = DockStyle.Fill;
        divisionEntriesLabel.Location = new Point(3, 23);
        divisionEntriesLabel.Name = "divisionEntriesLabel";
        divisionEntriesLabel.Size = new Size(145, 80);
        divisionEntriesLabel.TabIndex = 0;
        divisionEntriesLabel.Text = "Division 1: 55 Entries\r\nDivision 1: 55 Entries\r\nDivision 1: 55 Entries\r\nDivision 1: 55 Entries";
        // 
        // squadEntriesGroupBox
        // 
        squadEntriesGroupBox.Controls.Add(squadEntriesLabel);
        squadEntriesGroupBox.Font = new Font("Segoe UI", 11F);
        squadEntriesGroupBox.Location = new Point(775, 204);
        squadEntriesGroupBox.Name = "squadEntriesGroupBox";
        squadEntriesGroupBox.Size = new Size(278, 276);
        squadEntriesGroupBox.TabIndex = 2;
        squadEntriesGroupBox.TabStop = false;
        squadEntriesGroupBox.Text = "Squad Entries";
        // 
        // squadEntriesLabel
        // 
        squadEntriesLabel.AutoSize = true;
        squadEntriesLabel.Dock = DockStyle.Fill;
        squadEntriesLabel.Location = new Point(3, 23);
        squadEntriesLabel.Name = "squadEntriesLabel";
        squadEntriesLabel.Size = new Size(183, 240);
        squadEntriesLabel.TabIndex = 0;
        squadEntriesLabel.Text = resources.GetString("squadEntriesLabel.Text");
        // 
        // sweeperEntriesGroupBox
        // 
        sweeperEntriesGroupBox.Controls.Add(sweeperEntriesLabel);
        sweeperEntriesGroupBox.Font = new Font("Segoe UI", 11F);
        sweeperEntriesGroupBox.Location = new Point(775, 486);
        sweeperEntriesGroupBox.Name = "sweeperEntriesGroupBox";
        sweeperEntriesGroupBox.Size = new Size(278, 131);
        sweeperEntriesGroupBox.TabIndex = 3;
        sweeperEntriesGroupBox.TabStop = false;
        sweeperEntriesGroupBox.Text = "Sweeper Entries";
        // 
        // sweeperEntriesLabel
        // 
        sweeperEntriesLabel.AutoSize = true;
        sweeperEntriesLabel.Dock = DockStyle.Fill;
        sweeperEntriesLabel.Location = new Point(3, 23);
        sweeperEntriesLabel.Name = "sweeperEntriesLabel";
        sweeperEntriesLabel.Size = new Size(183, 60);
        sweeperEntriesLabel.TabIndex = 0;
        sweeperEntriesLabel.Text = "01/01/00 11AM: 55 Entries\r\n01/01/00 11AM: 55 Entries\r\nSuper Sweeper: 55 Entries";
        // 
        // filterLabel
        // 
        filterLabel.AutoSize = true;
        filterLabel.Location = new Point(12, 12);
        filterLabel.Margin = new Padding(15, 3, 15, 3);
        filterLabel.Name = "filterLabel";
        filterLabel.Required = false;
        filterLabel.Size = new Size(117, 19);
        filterLabel.TabIndex = 31;
        filterLabel.TabStop = false;
        filterLabel.Text = "Filter:";
        // 
        // filterText
        // 
        filterText.Font = new Font("Calibri", 11.25F);
        filterText.Location = new Point(12, 34);
        filterText.Margin = new Padding(15, 3, 15, 9);
        filterText.MaxLength = 25;
        filterText.Name = "filterText";
        filterText.PlaceholderText = "Name Filter";
        filterText.Size = new Size(270, 26);
        filterText.TabIndex = 30;
        filterText.TextChanged += FilterText_TextChanged;
        // 
        // updateBowlerInfoMenuItem
        // 
        updateBowlerInfoMenuItem.Name = "updateBowlerInfoMenuItem";
        updateBowlerInfoMenuItem.Size = new Size(180, 22);
        updateBowlerInfoMenuItem.Text = "Update Bowler Info";
        updateBowlerInfoMenuItem.Click += UpdateBowlerInfoMenuItem_Click;
        // 
        // RetrieveTournamentRegistrationsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1081, 629);
        Controls.Add(filterLabel);
        Controls.Add(filterText);
        Controls.Add(sweeperEntriesGroupBox);
        Controls.Add(squadEntriesGroupBox);
        Controls.Add(divisionEntriesGroupBox);
        Controls.Add(tournamentRegistrationsGrid);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "RetrieveTournamentRegistrationsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Tournament Registrations";
        registrationGridContextMenu.ResumeLayout(false);
        divisionEntriesGroupBox.ResumeLayout(false);
        divisionEntriesGroupBox.PerformLayout();
        squadEntriesGroupBox.ResumeLayout(false);
        squadEntriesGroupBox.PerformLayout();
        sweeperEntriesGroupBox.ResumeLayout(false);
        sweeperEntriesGroupBox.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Controls.Grids.TournamentRegistrationGrid tournamentRegistrationsGrid;
    private GroupBox divisionEntriesGroupBox;
    private Label divisionEntriesLabel;
    private GroupBox squadEntriesGroupBox;
    private Label squadEntriesLabel;
    private GroupBox sweeperEntriesGroupBox;
    private Label sweeperEntriesLabel;
    private ContextMenuStrip registrationGridContextMenu;
    private ToolStripMenuItem deleteMenuItem;
    private ToolStripMenuItem updateBowlerNameMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem addSuperSweeperMenuItem;
    private ToolStripMenuItem changeDivisionMenuItem;
    private ToolStripMenuItem changeAverageMenuItem;
    private Controls.LabelControl filterLabel;
    private TextBox filterText;
    private ToolStripMenuItem updateBowlerInfoMenuItem;
}