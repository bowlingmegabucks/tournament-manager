namespace NewEnglandClassic.Contols;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.completeColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.squadDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.squadTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finalsRatioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cashRatioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxPerPairColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.completeColumn,
            this.squadDateColumn,
            this.squadTimeColumn,
            this.finalsRatioColumn,
            this.cashRatioColumn,
            this.maxPerPairColumn});
            this.GridView.RowTemplate.Height = 25;
            this.GridView.Size = new System.Drawing.Size(654, 377);
            // 
            // completeColumn
            // 
            this.completeColumn.DataPropertyName = "Complete";
            this.completeColumn.HeaderText = "Complete";
            this.completeColumn.Name = "completeColumn";
            this.completeColumn.ReadOnly = true;
            this.completeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.completeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.completeColumn.Width = 80;
            // 
            // squadDateColumn
            // 
            this.squadDateColumn.DataPropertyName = "Date";
            dataGridViewCellStyle6.Format = "d";
            dataGridViewCellStyle6.NullValue = null;
            this.squadDateColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.squadDateColumn.HeaderText = "Squad Date";
            this.squadDateColumn.Name = "squadDateColumn";
            this.squadDateColumn.ReadOnly = true;
            // 
            // squadTimeColumn
            // 
            this.squadTimeColumn.DataPropertyName = "Date";
            dataGridViewCellStyle7.Format = "t";
            dataGridViewCellStyle7.NullValue = null;
            this.squadTimeColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.squadTimeColumn.HeaderText = "Squad Time";
            this.squadTimeColumn.Name = "squadTimeColumn";
            this.squadTimeColumn.ReadOnly = true;
            // 
            // finalsRatioColumn
            // 
            this.finalsRatioColumn.DataPropertyName = "FinalsRatio";
            dataGridViewCellStyle8.Format = "N1";
            dataGridViewCellStyle8.NullValue = "Default";
            this.finalsRatioColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.finalsRatioColumn.HeaderText = "Finals Ratio";
            this.finalsRatioColumn.Name = "finalsRatioColumn";
            this.finalsRatioColumn.ReadOnly = true;
            // 
            // cashRatioColumn
            // 
            this.cashRatioColumn.DataPropertyName = "CashRatio";
            dataGridViewCellStyle9.Format = "N1";
            dataGridViewCellStyle9.NullValue = "Default";
            this.cashRatioColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.cashRatioColumn.HeaderText = "Cash Ratio";
            this.cashRatioColumn.Name = "cashRatioColumn";
            this.cashRatioColumn.ReadOnly = true;
            // 
            // maxPerPairColumn
            // 
            this.maxPerPairColumn.DataPropertyName = "MaxPerPair";
            dataGridViewCellStyle10.Format = "N0";
            dataGridViewCellStyle10.NullValue = null;
            this.maxPerPairColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.maxPerPairColumn.HeaderText = "Max Per Pair";
            this.maxPerPairColumn.Name = "maxPerPairColumn";
            this.maxPerPairColumn.ReadOnly = true;
            // 
            // SquadsGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SquadsGrid";
            this.Size = new System.Drawing.Size(654, 377);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DataGridViewCheckBoxColumn completeColumn;
    private DataGridViewTextBoxColumn squadDateColumn;
    private DataGridViewTextBoxColumn squadTimeColumn;
    private DataGridViewTextBoxColumn finalsRatioColumn;
    private DataGridViewTextBoxColumn cashRatioColumn;
    private DataGridViewTextBoxColumn maxPerPairColumn;
}
