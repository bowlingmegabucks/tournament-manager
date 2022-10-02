namespace NortheastMegabuck.Controls;

partial class ScoresGrid
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
            this.laneAssignmentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bowlerNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.laneAssignmentColumn,
            this.bowlerNameColumn});
            this.GridView.RowTemplate.Height = 25;
            // 
            // laneAssignmentColumn
            // 
            this.laneAssignmentColumn.DataPropertyName = "LaneAssignment";
            this.laneAssignmentColumn.Frozen = true;
            this.laneAssignmentColumn.HeaderText = "Lane";
            this.laneAssignmentColumn.Name = "laneAssignmentColumn";
            this.laneAssignmentColumn.ReadOnly = true;
            this.laneAssignmentColumn.Width = 50;
            // 
            // bowlerNameColumn
            // 
            this.bowlerNameColumn.DataPropertyName = "BowlerName";
            this.bowlerNameColumn.Frozen = true;
            this.bowlerNameColumn.HeaderText = "Bowler";
            this.bowlerNameColumn.Name = "bowlerNameColumn";
            this.bowlerNameColumn.ReadOnly = true;
            this.bowlerNameColumn.Width = 250;
            // 
            // ScoresGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ScoresGrid";
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DataGridViewTextBoxColumn laneAssignmentColumn;
    private DataGridViewTextBoxColumn bowlerNameColumn;
}
