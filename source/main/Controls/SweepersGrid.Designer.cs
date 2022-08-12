namespace NewEnglandClassic.Contols;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.completeColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gamesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cashRatioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.completeColumn,
            this.dateColumn,
            this.timeColumn,
            this.gamesColumn,
            this.cashRatioColumn});
            this.GridView.RowTemplate.Height = 25;
            this.GridView.Size = new System.Drawing.Size(608, 377);
            // 
            // completeColumn
            // 
            this.completeColumn.DataPropertyName = "Complete";
            this.completeColumn.HeaderText = "Complete";
            this.completeColumn.Name = "completeColumn";
            this.completeColumn.ReadOnly = true;
            this.completeColumn.Width = 85;
            // 
            // dateColumn
            // 
            this.dateColumn.DataPropertyName = "Date";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.dateColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.dateColumn.HeaderText = "Sweeper Date";
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.ReadOnly = true;
            this.dateColumn.Width = 125;
            // 
            // timeColumn
            // 
            this.timeColumn.DataPropertyName = "Date";
            dataGridViewCellStyle4.Format = "t";
            dataGridViewCellStyle4.NullValue = null;
            this.timeColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.timeColumn.HeaderText = "Sweeper Time";
            this.timeColumn.Name = "timeColumn";
            this.timeColumn.ReadOnly = true;
            this.timeColumn.Width = 125;
            // 
            // gamesColumn
            // 
            this.gamesColumn.DataPropertyName = "Games";
            this.gamesColumn.HeaderText = "Games";
            this.gamesColumn.Name = "gamesColumn";
            this.gamesColumn.ReadOnly = true;
            this.gamesColumn.Width = 75;
            // 
            // cashRatioColumn
            // 
            this.cashRatioColumn.DataPropertyName = "CashRatio";
            this.cashRatioColumn.HeaderText = "Cashing Ratio";
            this.cashRatioColumn.Name = "cashRatioColumn";
            this.cashRatioColumn.ReadOnly = true;
            this.cashRatioColumn.Width = 125;
            // 
            // SweepersGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SweepersGrid";
            this.Size = new System.Drawing.Size(608, 377);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DataGridViewCheckBoxColumn completeColumn;
    private DataGridViewTextBoxColumn dateColumn;
    private DataGridViewTextBoxColumn timeColumn;
    private DataGridViewTextBoxColumn gamesColumn;
    private DataGridViewTextBoxColumn cashRatioColumn;
}
