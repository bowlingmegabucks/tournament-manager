namespace BowlingMegabucks.TournamentManager.Controls.Grids;

partial class SearchBowlersGrid
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
            this.firstNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.firstNameColumn,
            this.lastNameColumn,
            this.emailColumn,
            this.cityColumn,
            this.stateColumn});
            this.GridView.RowTemplate.Height = 25;
            this.GridView.Size = new System.Drawing.Size(938, 377);
            // 
            // firstNameColumn
            // 
            this.firstNameColumn.DataPropertyName = "FirstName";
            this.firstNameColumn.HeaderText = "First Name";
            this.firstNameColumn.Name = "firstNameColumn";
            this.firstNameColumn.ReadOnly = true;
            this.firstNameColumn.Width = 125;
            // 
            // lastNameColumn
            // 
            this.lastNameColumn.DataPropertyName = "LastName";
            this.lastNameColumn.HeaderText = "Last Name";
            this.lastNameColumn.Name = "lastNameColumn";
            this.lastNameColumn.ReadOnly = true;
            this.lastNameColumn.Width = 175;
            // 
            // emailColumn
            // 
            this.emailColumn.DataPropertyName = "EmailAddress";
            this.emailColumn.HeaderText = "Email Address";
            this.emailColumn.Name = "emailColumn";
            this.emailColumn.ReadOnly = true;
            this.emailColumn.Width = 175;
            // 
            // cityColumn
            // 
            this.cityColumn.DataPropertyName = "City";
            this.cityColumn.HeaderText = "City";
            this.cityColumn.Name = "cityColumn";
            this.cityColumn.ReadOnly = true;
            this.cityColumn.Width = 135;
            // 
            // stateColumn
            // 
            this.stateColumn.DataPropertyName = "State";
            this.stateColumn.HeaderText = "State";
            this.stateColumn.Name = "stateColumn";
            this.stateColumn.ReadOnly = true;
            this.stateColumn.Width = 50;
            // 
            // SearchBowlersGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SearchBowlersGrid";
            this.Size = new System.Drawing.Size(938, 377);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DataGridViewTextBoxColumn firstNameColumn;
    private DataGridViewTextBoxColumn lastNameColumn;
    private DataGridViewTextBoxColumn emailColumn;
    private DataGridViewTextBoxColumn cityColumn;
    private DataGridViewTextBoxColumn stateColumn;
}
