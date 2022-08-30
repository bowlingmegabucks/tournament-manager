namespace NortheastMegabuck.Controls;

partial class TournamentRegistrationGrid
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.bowlerNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.divisionNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.squadsEnteredColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sweepersEnteredColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.superSweeperColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bowlerNameColumn,
            this.divisionNameColumn,
            this.squadsEnteredColumn,
            this.sweepersEnteredColumn,
            this.superSweeperColumn});
            this.GridView.RowTemplate.Height = 25;
            this.GridView.Size = new System.Drawing.Size(853, 377);
            // 
            // bowlerNameColumn
            // 
            this.bowlerNameColumn.DataPropertyName = "BowlerName";
            this.bowlerNameColumn.HeaderText = "Bowler";
            this.bowlerNameColumn.Name = "bowlerNameColumn";
            this.bowlerNameColumn.ReadOnly = true;
            this.bowlerNameColumn.Width = 150;
            // 
            // divisionNameColumn
            // 
            this.divisionNameColumn.DataPropertyName = "DivisionName";
            this.divisionNameColumn.HeaderText = "Division";
            this.divisionNameColumn.Name = "divisionNameColumn";
            this.divisionNameColumn.ReadOnly = true;
            this.divisionNameColumn.Width = 180;
            // 
            // squadsEnteredColumn
            // 
            this.squadsEnteredColumn.DataPropertyName = "SquadsEnteredCount";
            this.squadsEnteredColumn.HeaderText = "Squads Entered";
            this.squadsEnteredColumn.Name = "squadsEnteredColumn";
            this.squadsEnteredColumn.ReadOnly = true;
            this.squadsEnteredColumn.Width = 125;
            // 
            // sweepersEnteredColumn
            // 
            this.sweepersEnteredColumn.DataPropertyName = "SweepersEnteredCount";
            this.sweepersEnteredColumn.HeaderText = "Sweepers Entered";
            this.sweepersEnteredColumn.Name = "sweepersEnteredColumn";
            this.sweepersEnteredColumn.ReadOnly = true;
            this.sweepersEnteredColumn.Width = 125;
            // 
            // superSweeperColumn
            // 
            this.superSweeperColumn.DataPropertyName = "SuperSweeperEntered";
            this.superSweeperColumn.HeaderText = "Super Sweeper";
            this.superSweeperColumn.Name = "superSweeperColumn";
            this.superSweeperColumn.ReadOnly = true;
            // 
            // TournamentRegistrationGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "TournamentRegistrationGrid";
            this.Size = new System.Drawing.Size(853, 377);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DataGridViewTextBoxColumn bowlerNameColumn;
    private DataGridViewTextBoxColumn divisionNameColumn;
    private DataGridViewTextBoxColumn squadsEnteredColumn;
    private DataGridViewTextBoxColumn sweepersEnteredColumn;
    private DataGridViewCheckBoxColumn superSweeperColumn;
}
