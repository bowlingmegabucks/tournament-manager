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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ColumnComplete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnSquadDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSquadTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFinalsRatio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCashRatio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMaxPerPair = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnComplete,
            this.ColumnSquadDate,
            this.ColumnSquadTime,
            this.ColumnFinalsRatio,
            this.ColumnCashRatio,
            this.ColumnMaxPerPair});
            this.GridView.RowTemplate.Height = 25;
            this.GridView.Size = new System.Drawing.Size(654, 377);
            // 
            // ColumnComplete
            // 
            this.ColumnComplete.DataPropertyName = "Complete";
            this.ColumnComplete.HeaderText = "Complete";
            this.ColumnComplete.Name = "ColumnComplete";
            this.ColumnComplete.ReadOnly = true;
            this.ColumnComplete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnComplete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnComplete.Width = 80;
            // 
            // ColumnSquadDate
            // 
            this.ColumnSquadDate.DataPropertyName = "Date";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.ColumnSquadDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnSquadDate.HeaderText = "Squad Date";
            this.ColumnSquadDate.Name = "ColumnSquadDate";
            this.ColumnSquadDate.ReadOnly = true;
            // 
            // ColumnSquadTime
            // 
            this.ColumnSquadTime.DataPropertyName = "Date";
            dataGridViewCellStyle2.Format = "t";
            dataGridViewCellStyle2.NullValue = null;
            this.ColumnSquadTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnSquadTime.HeaderText = "Squad Time";
            this.ColumnSquadTime.Name = "ColumnSquadTime";
            this.ColumnSquadTime.ReadOnly = true;
            // 
            // ColumnFinalsRatio
            // 
            this.ColumnFinalsRatio.DataPropertyName = "FinalsRatio";
            dataGridViewCellStyle3.Format = "N1";
            dataGridViewCellStyle3.NullValue = "Default";
            this.ColumnFinalsRatio.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnFinalsRatio.HeaderText = "Finals Ratio";
            this.ColumnFinalsRatio.Name = "ColumnFinalsRatio";
            this.ColumnFinalsRatio.ReadOnly = true;
            // 
            // ColumnCashRatio
            // 
            this.ColumnCashRatio.DataPropertyName = "CashRatio";
            dataGridViewCellStyle4.Format = "N1";
            dataGridViewCellStyle4.NullValue = "Default";
            this.ColumnCashRatio.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnCashRatio.HeaderText = "Cash Ratio";
            this.ColumnCashRatio.Name = "ColumnCashRatio";
            this.ColumnCashRatio.ReadOnly = true;
            // 
            // ColumnMaxPerPair
            // 
            this.ColumnMaxPerPair.DataPropertyName = "MaxPerPair";
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.ColumnMaxPerPair.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnMaxPerPair.HeaderText = "Max Per Pair";
            this.ColumnMaxPerPair.Name = "ColumnMaxPerPair";
            this.ColumnMaxPerPair.ReadOnly = true;
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

    private DataGridViewCheckBoxColumn ColumnComplete;
    private DataGridViewTextBoxColumn ColumnSquadDate;
    private DataGridViewTextBoxColumn ColumnSquadTime;
    private DataGridViewTextBoxColumn ColumnFinalsRatio;
    private DataGridViewTextBoxColumn ColumnCashRatio;
    private DataGridViewTextBoxColumn ColumnMaxPerPair;
}
