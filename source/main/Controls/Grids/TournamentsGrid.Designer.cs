namespace BowlingMegabucks.TournamentManager.Controls.Grids;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameColumn,
            this.startDateColumn,
            this.endDateColumn,
            this.locationColumn});
            this.GridView.RowTemplate.Height = 25;
            // 
            // nameColumn
            // 
            this.nameColumn.DataPropertyName = "TournamentName";
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            this.nameColumn.Width = 225;
            // 
            // startDateColumn
            // 
            this.startDateColumn.DataPropertyName = "Start";
            dataGridViewCellStyle3.Format = "MM/dd/yyyy";
            this.startDateColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.startDateColumn.HeaderText = "Start Date";
            this.startDateColumn.Name = "startDateColumn";
            this.startDateColumn.ReadOnly = true;
            this.startDateColumn.Width = 90;
            this.startDateColumn.DataPropertyName = "StartDate";
            // 
            // endDateColumn
            // 
            this.endDateColumn.DataPropertyName = "End";
            dataGridViewCellStyle4.Format = "MM/dd/yyyy";
            this.endDateColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.endDateColumn.HeaderText = "End Date";
            this.endDateColumn.Name = "endDateColumn";
            this.endDateColumn.ReadOnly = true;
            this.endDateColumn.Width = 90;
            this.endDateColumn.DataPropertyName = "EndDate";
        // 
        // locationColumn
        // 
        this.locationColumn.DataPropertyName = "BowlingCenter";
            this.locationColumn.HeaderText = "Location";
            this.locationColumn.Name = "locationColumn";
            this.locationColumn.ReadOnly = true;
            this.locationColumn.Width = 200;
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

    private DataGridViewTextBoxColumn nameColumn;
    private DataGridViewTextBoxColumn startDateColumn;
    private DataGridViewTextBoxColumn endDateColumn;
    private DataGridViewTextBoxColumn locationColumn;
}
