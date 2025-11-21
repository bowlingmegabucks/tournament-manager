namespace BowlingMegabucks.TournamentManager.Controls.Grids;

partial class SweepersGrid
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
        var dataGridViewCellStyle1 = new DataGridViewCellStyle();
        var dataGridViewCellStyle2 = new DataGridViewCellStyle();
        completeColumn = new DataGridViewCheckBoxColumn();
        dateColumn = new DataGridViewTextBoxColumn();
        timeColumn = new DataGridViewTextBoxColumn();
        gamesColumn = new DataGridViewTextBoxColumn();
        cashRatioColumn = new DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)GridView).BeginInit();
        SuspendLayout();
        // 
        // GridView
        // 
        GridView.Columns.AddRange(new DataGridViewColumn[] { completeColumn, dateColumn, timeColumn, gamesColumn, cashRatioColumn });
        GridView.Size = new Size(608, 377);
        // 
        // completeColumn
        // 
        completeColumn.DataPropertyName = "Complete";
        completeColumn.HeaderText = "Complete";
        completeColumn.Name = "completeColumn";
        completeColumn.ReadOnly = true;
        completeColumn.Width = 85;
        // 
        // dateColumn
        // 
        dateColumn.DataPropertyName = "SweeperDate";
        dataGridViewCellStyle1.Format = "d";
        dataGridViewCellStyle1.NullValue = null;
        dateColumn.DefaultCellStyle = dataGridViewCellStyle1;
        dateColumn.HeaderText = "Sweeper Date";
        dateColumn.Name = "dateColumn";
        dateColumn.ReadOnly = true;
        dateColumn.Width = 125;
        // 
        // timeColumn
        // 
        timeColumn.DataPropertyName = "SweeperDate";
        dataGridViewCellStyle2.Format = "t";
        dataGridViewCellStyle2.NullValue = null;
        timeColumn.DefaultCellStyle = dataGridViewCellStyle2;
        timeColumn.HeaderText = "Sweeper Time";
        timeColumn.Name = "timeColumn";
        timeColumn.ReadOnly = true;
        timeColumn.Width = 125;
        // 
        // gamesColumn
        // 
        gamesColumn.DataPropertyName = "Games";
        gamesColumn.HeaderText = "Games";
        gamesColumn.Name = "gamesColumn";
        gamesColumn.ReadOnly = true;
        gamesColumn.Width = 75;
        // 
        // cashRatioColumn
        // 
        cashRatioColumn.DataPropertyName = "CashRatio";
        cashRatioColumn.HeaderText = "Cashing Ratio";
        cashRatioColumn.Name = "cashRatioColumn";
        cashRatioColumn.ReadOnly = true;
        cashRatioColumn.Width = 125;
        // 
        // SweepersGrid
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Name = "SweepersGrid";
        Size = new Size(608, 377);
        ((System.ComponentModel.ISupportInitialize)GridView).EndInit();
        ResumeLayout(false);

    }

    #endregion

    private DataGridViewCheckBoxColumn completeColumn;
    private DataGridViewTextBoxColumn dateColumn;
    private DataGridViewTextBoxColumn timeColumn;
    private DataGridViewTextBoxColumn gamesColumn;
    private DataGridViewTextBoxColumn cashRatioColumn;
}
