namespace NortheastMegabuck.Registrations.Retrieve;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RetrieveTournamentRegistrationsForm));
            this.tournamentRegistrationsGrid = new NortheastMegabuck.Controls.TournamentRegistrationGrid();
            this.divisionEntriesGroupBox = new System.Windows.Forms.GroupBox();
            this.divisionEntriesLabel = new System.Windows.Forms.Label();
            this.squadEntriesGroupBox = new System.Windows.Forms.GroupBox();
            this.squadEntriesLabel = new System.Windows.Forms.Label();
            this.sweeperEntriesGroupBox = new System.Windows.Forms.GroupBox();
            this.sweeperEntriesLabel = new System.Windows.Forms.Label();
            this.divisionEntriesGroupBox.SuspendLayout();
            this.squadEntriesGroupBox.SuspendLayout();
            this.sweeperEntriesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tournamentRegistrationsGrid
            // 
            this.tournamentRegistrationsGrid.AllowRowSelection = true;
            this.tournamentRegistrationsGrid.AlternateRowColors = true;
            this.tournamentRegistrationsGrid.Location = new System.Drawing.Point(12, 12);
            this.tournamentRegistrationsGrid.Name = "tournamentRegistrationsGrid";
            this.tournamentRegistrationsGrid.SelectedRowContextMenu = null;
            this.tournamentRegistrationsGrid.Size = new System.Drawing.Size(751, 550);
            this.tournamentRegistrationsGrid.TabIndex = 0;
            // 
            // divisionEntriesGroupBox
            // 
            this.divisionEntriesGroupBox.Controls.Add(this.divisionEntriesLabel);
            this.divisionEntriesGroupBox.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.divisionEntriesGroupBox.Location = new System.Drawing.Point(769, 12);
            this.divisionEntriesGroupBox.Name = "divisionEntriesGroupBox";
            this.divisionEntriesGroupBox.Size = new System.Drawing.Size(278, 131);
            this.divisionEntriesGroupBox.TabIndex = 1;
            this.divisionEntriesGroupBox.TabStop = false;
            this.divisionEntriesGroupBox.Text = "Division Entries";
            // 
            // divisionEntriesLabel
            // 
            this.divisionEntriesLabel.AutoSize = true;
            this.divisionEntriesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divisionEntriesLabel.Location = new System.Drawing.Point(3, 23);
            this.divisionEntriesLabel.Name = "divisionEntriesLabel";
            this.divisionEntriesLabel.Size = new System.Drawing.Size(145, 80);
            this.divisionEntriesLabel.TabIndex = 0;
            this.divisionEntriesLabel.Text = "Division 1: 55 Entries\r\nDivision 1: 55 Entries\r\nDivision 1: 55 Entries\r\nDivision " +
    "1: 55 Entries";
            // 
            // squadEntriesGroupBox
            // 
            this.squadEntriesGroupBox.Controls.Add(this.squadEntriesLabel);
            this.squadEntriesGroupBox.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.squadEntriesGroupBox.Location = new System.Drawing.Point(772, 149);
            this.squadEntriesGroupBox.Name = "squadEntriesGroupBox";
            this.squadEntriesGroupBox.Size = new System.Drawing.Size(278, 276);
            this.squadEntriesGroupBox.TabIndex = 2;
            this.squadEntriesGroupBox.TabStop = false;
            this.squadEntriesGroupBox.Text = "Squad Entries";
            // 
            // squadEntriesLabel
            // 
            this.squadEntriesLabel.AutoSize = true;
            this.squadEntriesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadEntriesLabel.Location = new System.Drawing.Point(3, 23);
            this.squadEntriesLabel.Name = "squadEntriesLabel";
            this.squadEntriesLabel.Size = new System.Drawing.Size(183, 240);
            this.squadEntriesLabel.TabIndex = 0;
            this.squadEntriesLabel.Text = resources.GetString("squadEntriesLabel.Text");
            // 
            // sweeperEntriesGroupBox
            // 
            this.sweeperEntriesGroupBox.Controls.Add(this.sweeperEntriesLabel);
            this.sweeperEntriesGroupBox.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sweeperEntriesGroupBox.Location = new System.Drawing.Point(775, 431);
            this.sweeperEntriesGroupBox.Name = "sweeperEntriesGroupBox";
            this.sweeperEntriesGroupBox.Size = new System.Drawing.Size(278, 131);
            this.sweeperEntriesGroupBox.TabIndex = 3;
            this.sweeperEntriesGroupBox.TabStop = false;
            this.sweeperEntriesGroupBox.Text = "Sweeper Entries";
            // 
            // sweeperEntriesLabel
            // 
            this.sweeperEntriesLabel.AutoSize = true;
            this.sweeperEntriesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sweeperEntriesLabel.Location = new System.Drawing.Point(3, 23);
            this.sweeperEntriesLabel.Name = "sweeperEntriesLabel";
            this.sweeperEntriesLabel.Size = new System.Drawing.Size(183, 60);
            this.sweeperEntriesLabel.TabIndex = 0;
            this.sweeperEntriesLabel.Text = "01/01/00 11AM: 55 Entries\r\n01/01/00 11AM: 55 Entries\r\nSuper Sweeper: 55 Entries";
            // 
            // RetrieveTournamentRegistrationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 580);
            this.Controls.Add(this.sweeperEntriesGroupBox);
            this.Controls.Add(this.squadEntriesGroupBox);
            this.Controls.Add(this.divisionEntriesGroupBox);
            this.Controls.Add(this.tournamentRegistrationsGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RetrieveTournamentRegistrationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tournament Registrations";
            this.divisionEntriesGroupBox.ResumeLayout(false);
            this.divisionEntriesGroupBox.PerformLayout();
            this.squadEntriesGroupBox.ResumeLayout(false);
            this.squadEntriesGroupBox.PerformLayout();
            this.sweeperEntriesGroupBox.ResumeLayout(false);
            this.sweeperEntriesGroupBox.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private Controls.TournamentRegistrationGrid tournamentRegistrationsGrid;
    private GroupBox divisionEntriesGroupBox;
    private Label divisionEntriesLabel;
    private GroupBox squadEntriesGroupBox;
    private Label squadEntriesLabel;
    private GroupBox sweeperEntriesGroupBox;
    private Label sweeperEntriesLabel;
}