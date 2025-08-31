namespace BowlingMegabucks.TournamentManager.App.Tournaments;

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
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        nameColumn = new DataGridViewTextBoxColumn();
        startDateColumn = new DataGridViewTextBoxColumn();
        endDateColumn = new DataGridViewTextBoxColumn();
        locationColumn = new DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)GridView).BeginInit();
        SuspendLayout();
        // 
        // GridView
        // 
        GridView.Columns.AddRange(new DataGridViewColumn[] { nameColumn, startDateColumn, endDateColumn, locationColumn });
        // 
        // nameColumn
        // 
        nameColumn.DataPropertyName = "Name";
        nameColumn.HeaderText = "Name";
        nameColumn.Name = "nameColumn";
        nameColumn.ReadOnly = true;
        nameColumn.Width = 225;
        // 
        // startDateColumn
        // 
        startDateColumn.DataPropertyName = "StartDate";
        dataGridViewCellStyle1.Format = "MM/dd/yyyy";
        startDateColumn.DefaultCellStyle = dataGridViewCellStyle1;
        startDateColumn.HeaderText = "Start Date";
        startDateColumn.Name = "startDateColumn";
        startDateColumn.ReadOnly = true;
        startDateColumn.Width = 90;
        // 
        // endDateColumn
        // 
        endDateColumn.DataPropertyName = "EndDate";
        dataGridViewCellStyle2.Format = "MM/dd/yyyy";
        endDateColumn.DefaultCellStyle = dataGridViewCellStyle2;
        endDateColumn.HeaderText = "End Date";
        endDateColumn.Name = "endDateColumn";
        endDateColumn.ReadOnly = true;
        endDateColumn.Width = 90;
        // 
        // locationColumn
        // 
        locationColumn.DataPropertyName = "BowlingCenter";
        locationColumn.HeaderText = "Location";
        locationColumn.Name = "locationColumn";
        locationColumn.ReadOnly = true;
        locationColumn.Width = 200;
        // 
        // TournamentsGrid
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Name = "TournamentsGrid";
        ((System.ComponentModel.ISupportInitialize)GridView).EndInit();
        ResumeLayout(false);

    }

    #endregion

    private DataGridViewTextBoxColumn nameColumn;
    private DataGridViewTextBoxColumn startDateColumn;
    private DataGridViewTextBoxColumn endDateColumn;
    private DataGridViewTextBoxColumn locationColumn;
}
