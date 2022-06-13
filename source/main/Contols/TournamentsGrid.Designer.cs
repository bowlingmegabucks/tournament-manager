namespace NewEnglandClassic.Controls;

partial class TournamentsGrid
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
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.ColumnStart,
            this.ColumnEnd,
            this.ColumnLocation});
            this.GridView.RowTemplate.Height = 25;
            // 
            // ColumnName
            // 
            this.ColumnName.DataPropertyName = "TournamentName";
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Width = 225;
            // 
            // ColumnStart
            // 
            this.ColumnStart.DataPropertyName = "Start";
            dataGridViewCellStyle1.Format = "MM/dd/yyyy";
            this.ColumnStart.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnStart.HeaderText = "Start Date";
            this.ColumnStart.Name = "ColumnStart";
            this.ColumnStart.ReadOnly = true;
            this.ColumnStart.Width = 90;
            // 
            // ColumnEnd
            // 
            this.ColumnEnd.DataPropertyName = "End";
            dataGridViewCellStyle2.Format = "MM/dd/yyyy";
            this.ColumnEnd.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnEnd.HeaderText = "End Date";
            this.ColumnEnd.Name = "ColumnEnd";
            this.ColumnEnd.ReadOnly = true;
            this.ColumnEnd.Width = 90;
            // 
            // ColumnLocation
            // 
            this.ColumnLocation.DataPropertyName = "BowlingCenter";
            this.ColumnLocation.HeaderText = "Location";
            this.ColumnLocation.Name = "ColumnLocation";
            this.ColumnLocation.ReadOnly = true;
            this.ColumnLocation.Width = 200;
            // 
            // TournamentsGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "TournamentsGrid";
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DataGridViewTextBoxColumn ColumnName;
    private DataGridViewTextBoxColumn ColumnStart;
    private DataGridViewTextBoxColumn ColumnEnd;
    private DataGridViewTextBoxColumn ColumnLocation;
}
