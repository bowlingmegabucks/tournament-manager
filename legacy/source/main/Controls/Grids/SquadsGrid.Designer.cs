namespace BowlingMegabucks.TournamentManager.Controls.Grids;

partial class SquadsGrid
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
        var dataGridViewCellStyle3 = new DataGridViewCellStyle();
        var dataGridViewCellStyle4 = new DataGridViewCellStyle();
        var dataGridViewCellStyle5 = new DataGridViewCellStyle();
        completeColumn = new DataGridViewCheckBoxColumn();
        squadDateColumn = new DataGridViewTextBoxColumn();
        squadTimeColumn = new DataGridViewTextBoxColumn();
        finalsRatioColumn = new DataGridViewTextBoxColumn();
        cashRatioColumn = new DataGridViewTextBoxColumn();
        maxPerPairColumn = new DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)GridView).BeginInit();
        SuspendLayout();
        // 
        // GridView
        // 
        GridView.Columns.AddRange(new DataGridViewColumn[] { completeColumn, squadDateColumn, squadTimeColumn, finalsRatioColumn, cashRatioColumn, maxPerPairColumn });
        GridView.Size = new Size(654, 377);
        // 
        // completeColumn
        // 
        completeColumn.DataPropertyName = "Complete";
        completeColumn.HeaderText = "Complete";
        completeColumn.Name = "completeColumn";
        completeColumn.ReadOnly = true;
        completeColumn.Resizable = DataGridViewTriState.True;
        completeColumn.SortMode = DataGridViewColumnSortMode.Automatic;
        completeColumn.Width = 80;
        // 
        // squadDateColumn
        // 
        squadDateColumn.DataPropertyName = "SquadDate";
        dataGridViewCellStyle1.Format = "d";
        dataGridViewCellStyle1.NullValue = null;
        squadDateColumn.DefaultCellStyle = dataGridViewCellStyle1;
        squadDateColumn.HeaderText = "Squad Date";
        squadDateColumn.Name = "squadDateColumn";
        squadDateColumn.ReadOnly = true;
        // 
        // squadTimeColumn
        // 
        squadTimeColumn.DataPropertyName = "SquadDate";
        dataGridViewCellStyle2.Format = "t";
        dataGridViewCellStyle2.NullValue = null;
        squadTimeColumn.DefaultCellStyle = dataGridViewCellStyle2;
        squadTimeColumn.HeaderText = "Squad Time";
        squadTimeColumn.Name = "squadTimeColumn";
        squadTimeColumn.ReadOnly = true;
        // 
        // finalsRatioColumn
        // 
        finalsRatioColumn.DataPropertyName = "FinalsRatio";
        dataGridViewCellStyle3.Format = "N1";
        dataGridViewCellStyle3.NullValue = "Default";
        finalsRatioColumn.DefaultCellStyle = dataGridViewCellStyle3;
        finalsRatioColumn.HeaderText = "Finals Ratio";
        finalsRatioColumn.Name = "finalsRatioColumn";
        finalsRatioColumn.ReadOnly = true;
        // 
        // cashRatioColumn
        // 
        cashRatioColumn.DataPropertyName = "CashRatio";
        dataGridViewCellStyle4.Format = "N1";
        dataGridViewCellStyle4.NullValue = "Default";
        cashRatioColumn.DefaultCellStyle = dataGridViewCellStyle4;
        cashRatioColumn.HeaderText = "Cash Ratio";
        cashRatioColumn.Name = "cashRatioColumn";
        cashRatioColumn.ReadOnly = true;
        // 
        // maxPerPairColumn
        // 
        maxPerPairColumn.DataPropertyName = "MaxPerPair";
        dataGridViewCellStyle5.Format = "N0";
        dataGridViewCellStyle5.NullValue = null;
        maxPerPairColumn.DefaultCellStyle = dataGridViewCellStyle5;
        maxPerPairColumn.HeaderText = "Max Per Pair";
        maxPerPairColumn.Name = "maxPerPairColumn";
        maxPerPairColumn.ReadOnly = true;
        // 
        // SquadsGrid
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Name = "SquadsGrid";
        Size = new Size(654, 377);
        ((System.ComponentModel.ISupportInitialize)GridView).EndInit();
        ResumeLayout(false);

    }

    #endregion

    private DataGridViewCheckBoxColumn completeColumn;
    private DataGridViewTextBoxColumn squadDateColumn;
    private DataGridViewTextBoxColumn squadTimeColumn;
    private DataGridViewTextBoxColumn finalsRatioColumn;
    private DataGridViewTextBoxColumn cashRatioColumn;
    private DataGridViewTextBoxColumn maxPerPairColumn;
}
