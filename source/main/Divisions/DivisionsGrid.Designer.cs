namespace NewEnglandClassic.Divisions;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ColumnNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.Column,
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
            this.ColumnNumber.HeaderText = "Number";
            this.ColumnNumber.Name = "ColumnNumber";
            this.ColumnNumber.ReadOnly = true;
            // 
            // Column
            // 
            this.Column.DataPropertyName = "DivisionName";
            this.Column.HeaderText = "Name";
            this.Column.Name = "Column";
            this.Column.ReadOnly = true;
            // 
            // ColumnMinimumAge
            // 
            this.ColumnMinimumAge.DataPropertyName = "MinimumAge";
            dataGridViewCellStyle5.NullValue = "-";
            this.ColumnMinimumAge.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnMinimumAge.HeaderText = "Min. Age";
            this.ColumnMinimumAge.Name = "ColumnMinimumAge";
            this.ColumnMinimumAge.ReadOnly = true;
            // 
            // ColumnMaximumAge
            // 
            this.ColumnMaximumAge.DataPropertyName = "MaximumAge";
            dataGridViewCellStyle6.NullValue = "-";
            this.ColumnMaximumAge.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColumnMaximumAge.HeaderText = "Max. Age";
            this.ColumnMaximumAge.Name = "ColumnMaximumAge";
            this.ColumnMaximumAge.ReadOnly = true;
            // 
            // ColumnMinimumAverage
            // 
            this.ColumnMinimumAverage.DataPropertyName = "MinimumAverage";
            dataGridViewCellStyle7.NullValue = "-";
            this.ColumnMinimumAverage.DefaultCellStyle = dataGridViewCellStyle7;
            this.ColumnMinimumAverage.HeaderText = "Min. Avg.";
            this.ColumnMinimumAverage.Name = "ColumnMinimumAverage";
            this.ColumnMinimumAverage.ReadOnly = true;
            // 
            // ColumnMaximumAverage
            // 
            this.ColumnMaximumAverage.DataPropertyName = "MaximumAverage";
            dataGridViewCellStyle8.NullValue = "-";
            this.ColumnMaximumAverage.DefaultCellStyle = dataGridViewCellStyle8;
            this.ColumnMaximumAverage.HeaderText = "Max. Avg.";
            this.ColumnMaximumAverage.Name = "ColumnMaximumAverage";
            this.ColumnMaximumAverage.ReadOnly = true;
            // 
            // ColumnGender
            // 
            this.ColumnGender.HeaderText = "Gender";
            this.ColumnGender.Name = "ColumnGender";
            this.ColumnGender.ReadOnly = true;
            // 
            // ColumnHandicap
            // 
            this.ColumnHandicap.HeaderText = "Handicap";
            this.ColumnHandicap.Name = "ColumnHandicap";
            this.ColumnHandicap.ReadOnly = true;
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
    private DataGridViewTextBoxColumn Column;
    private DataGridViewTextBoxColumn ColumnMinimumAge;
    private DataGridViewTextBoxColumn ColumnMaximumAge;
    private DataGridViewTextBoxColumn ColumnMinimumAverage;
    private DataGridViewTextBoxColumn ColumnMaximumAverage;
    private DataGridViewTextBoxColumn ColumnGender;
    private DataGridViewTextBoxColumn ColumnHandicap;
}
