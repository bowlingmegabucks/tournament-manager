namespace NortheastMegabuck.Controls;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.numberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minimumAgeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maximumAgeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minimumAverageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maximumAverageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genderColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.handicapColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numberColumn,
            this.nameColumn,
            this.minimumAgeColumn,
            this.maximumAgeColumn,
            this.minimumAverageColumn,
            this.maximumAverageColumn,
            this.genderColumn,
            this.handicapColumn});
            this.GridView.RowTemplate.Height = 25;
            this.GridView.Size = new System.Drawing.Size(907, 377);
            this.GridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridView_CellFormatting);
            // 
            // numberColumn
            // 
            this.numberColumn.DataPropertyName = "Number";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.NullValue = null;
            this.numberColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.numberColumn.HeaderText = "Number";
            this.numberColumn.Name = "numberColumn";
            this.numberColumn.ReadOnly = true;
            this.numberColumn.Width = 60;
            // 
            // nameColumn
            // 
            this.nameColumn.DataPropertyName = "DivisionName";
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            this.nameColumn.Width = 175;
            // 
            // minimumAgeColumn
            // 
            this.minimumAgeColumn.DataPropertyName = "MinimumAge";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = "-";
            this.minimumAgeColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.minimumAgeColumn.HeaderText = "Min. Age";
            this.minimumAgeColumn.Name = "minimumAgeColumn";
            this.minimumAgeColumn.ReadOnly = true;
            this.minimumAgeColumn.Width = 80;
            // 
            // maximumAgeColumn
            // 
            this.maximumAgeColumn.DataPropertyName = "MaximumAge";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.NullValue = "-";
            this.maximumAgeColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.maximumAgeColumn.HeaderText = "Max. Age";
            this.maximumAgeColumn.Name = "maximumAgeColumn";
            this.maximumAgeColumn.ReadOnly = true;
            this.maximumAgeColumn.Width = 80;
            // 
            // minimumAverageColumn
            // 
            this.minimumAverageColumn.DataPropertyName = "MinimumAverage";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.NullValue = "-";
            this.minimumAverageColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.minimumAverageColumn.HeaderText = "Min. Avg.";
            this.minimumAverageColumn.Name = "minimumAverageColumn";
            this.minimumAverageColumn.ReadOnly = true;
            this.minimumAverageColumn.Width = 85;
            // 
            // maximumAverageColumn
            // 
            this.maximumAverageColumn.DataPropertyName = "MaximumAverage";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.NullValue = "-";
            this.maximumAverageColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.maximumAverageColumn.HeaderText = "Max. Avg.";
            this.maximumAverageColumn.Name = "maximumAverageColumn";
            this.maximumAverageColumn.ReadOnly = true;
            this.maximumAverageColumn.Width = 85;
            // 
            // genderColumn
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.NullValue = "-";
            this.genderColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.genderColumn.HeaderText = "Gender";
            this.genderColumn.Name = "genderColumn";
            this.genderColumn.ReadOnly = true;
            this.genderColumn.Width = 65;
            // 
            // handicapColumn
            // 
            this.handicapColumn.HeaderText = "Handicap";
            this.handicapColumn.Name = "handicapColumn";
            this.handicapColumn.ReadOnly = true;
            this.handicapColumn.Width = 185;
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

    private DataGridViewTextBoxColumn numberColumn;
    private DataGridViewTextBoxColumn nameColumn;
    private DataGridViewTextBoxColumn minimumAgeColumn;
    private DataGridViewTextBoxColumn maximumAgeColumn;
    private DataGridViewTextBoxColumn minimumAverageColumn;
    private DataGridViewTextBoxColumn maximumAverageColumn;
    private DataGridViewTextBoxColumn genderColumn;
    private DataGridViewTextBoxColumn handicapColumn;
}
