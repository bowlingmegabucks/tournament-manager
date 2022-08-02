namespace NewEnglandClassic.Controls;

partial class DivisionsGrid
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ColumnNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMinimumAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMaximumAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMinimumAverage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMaximumAverage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnHandicap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNumber,
            this.ColumnName,
            this.ColumnMinimumAge,
            this.ColumnMaximumAge,
            this.ColumnMinimumAverage,
            this.ColumnMaximumAverage,
            this.ColumnGender,
            this.ColumnHandicap});
            this.GridView.RowTemplate.Height = 25;
            this.GridView.Size = new System.Drawing.Size(907, 377);
            this.GridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridView_CellFormatting);
            // 
            // ColumnNumber
            // 
            this.ColumnNumber.DataPropertyName = "Number";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.ColumnNumber.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnNumber.HeaderText = "Number";
            this.ColumnNumber.Name = "ColumnNumber";
            this.ColumnNumber.ReadOnly = true;
            this.ColumnNumber.Width = 60;
            // 
            // ColumnName
            // 
            this.ColumnName.DataPropertyName = "DivisionName";
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Width = 175;
            // 
            // ColumnMinimumAge
            // 
            this.ColumnMinimumAge.DataPropertyName = "MinimumAge";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "-";
            this.ColumnMinimumAge.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnMinimumAge.HeaderText = "Min. Age";
            this.ColumnMinimumAge.Name = "ColumnMinimumAge";
            this.ColumnMinimumAge.ReadOnly = true;
            this.ColumnMinimumAge.Width = 80;
            // 
            // ColumnMaximumAge
            // 
            this.ColumnMaximumAge.DataPropertyName = "MaximumAge";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "-";
            this.ColumnMaximumAge.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnMaximumAge.HeaderText = "Max. Age";
            this.ColumnMaximumAge.Name = "ColumnMaximumAge";
            this.ColumnMaximumAge.ReadOnly = true;
            this.ColumnMaximumAge.Width = 80;
            // 
            // ColumnMinimumAverage
            // 
            this.ColumnMinimumAverage.DataPropertyName = "MinimumAverage";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = "-";
            this.ColumnMinimumAverage.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnMinimumAverage.HeaderText = "Min. Avg.";
            this.ColumnMinimumAverage.Name = "ColumnMinimumAverage";
            this.ColumnMinimumAverage.ReadOnly = true;
            this.ColumnMinimumAverage.Width = 85;
            // 
            // ColumnMaximumAverage
            // 
            this.ColumnMaximumAverage.DataPropertyName = "MaximumAverage";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.NullValue = "-";
            this.ColumnMaximumAverage.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnMaximumAverage.HeaderText = "Max. Avg.";
            this.ColumnMaximumAverage.Name = "ColumnMaximumAverage";
            this.ColumnMaximumAverage.ReadOnly = true;
            this.ColumnMaximumAverage.Width = 85;
            // 
            // ColumnGender
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.NullValue = "-";
            this.ColumnGender.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColumnGender.HeaderText = "Gender";
            this.ColumnGender.Name = "ColumnGender";
            this.ColumnGender.ReadOnly = true;
            this.ColumnGender.Width = 65;
            // 
            // ColumnHandicap
            // 
            this.ColumnHandicap.HeaderText = "Handicap";
            this.ColumnHandicap.Name = "ColumnHandicap";
            this.ColumnHandicap.ReadOnly = true;
            this.ColumnHandicap.Width = 185;
            // 
            // DivisionsGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "DivisionsGrid";
            this.Size = new System.Drawing.Size(907, 377);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DataGridViewTextBoxColumn ColumnNumber;
    private DataGridViewTextBoxColumn ColumnName;
    private DataGridViewTextBoxColumn ColumnMinimumAge;
    private DataGridViewTextBoxColumn ColumnMaximumAge;
    private DataGridViewTextBoxColumn ColumnMinimumAverage;
    private DataGridViewTextBoxColumn ColumnMaximumAverage;
    private DataGridViewTextBoxColumn ColumnGender;
    private DataGridViewTextBoxColumn ColumnHandicap;
}
