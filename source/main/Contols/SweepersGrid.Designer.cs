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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ColumnComplete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCashRatio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnComplete,
            this.ColumnDate,
            this.ColumnTime,
            this.ColumnGames,
            this.ColumnCashRatio});
            this.GridView.RowTemplate.Height = 25;
            this.GridView.Size = new System.Drawing.Size(608, 377);
            // 
            // ColumnComplete
            // 
            this.ColumnComplete.DataPropertyName = "Complete";
            this.ColumnComplete.HeaderText = "Complete";
            this.ColumnComplete.Name = "ColumnComplete";
            this.ColumnComplete.ReadOnly = true;
            this.ColumnComplete.Width = 85;
            // 
            // ColumnDate
            // 
            this.ColumnDate.DataPropertyName = "Date";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.ColumnDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnDate.HeaderText = "Sweeper Date";
            this.ColumnDate.Name = "ColumnDate";
            this.ColumnDate.ReadOnly = true;
            this.ColumnDate.Width = 125;
            // 
            // ColumnTime
            // 
            this.ColumnTime.DataPropertyName = "Date";
            dataGridViewCellStyle2.Format = "t";
            dataGridViewCellStyle2.NullValue = null;
            this.ColumnTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnTime.HeaderText = "Sweeper Time";
            this.ColumnTime.Name = "ColumnTime";
            this.ColumnTime.ReadOnly = true;
            this.ColumnTime.Width = 125;
            // 
            // ColumnGames
            // 
            this.ColumnGames.DataPropertyName = "Games";
            this.ColumnGames.HeaderText = "Games";
            this.ColumnGames.Name = "ColumnGames";
            this.ColumnGames.ReadOnly = true;
            this.ColumnGames.Width = 75;
            // 
            // ColumnCashRatio
            // 
            this.ColumnCashRatio.DataPropertyName = "CashRatio";
            this.ColumnCashRatio.HeaderText = "Cashing Ratio";
            this.ColumnCashRatio.Name = "ColumnCashRatio";
            this.ColumnCashRatio.ReadOnly = true;
            this.ColumnCashRatio.Width = 125;
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

    private DataGridViewCheckBoxColumn ColumnComplete;
    private DataGridViewTextBoxColumn ColumnDate;
    private DataGridViewTextBoxColumn ColumnTime;
    private DataGridViewTextBoxColumn ColumnGames;
    private DataGridViewTextBoxColumn ColumnCashRatio;
}
