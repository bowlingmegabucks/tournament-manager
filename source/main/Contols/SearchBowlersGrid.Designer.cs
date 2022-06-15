namespace NewEnglandClassic.Contols;

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
            this.ColumnFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFirstName,
            this.ColumnLastName,
            this.ColumnEmail,
            this.ColumnCity,
            this.ColumnState});
            this.GridView.RowTemplate.Height = 25;
            this.GridView.Size = new System.Drawing.Size(599, 377);
            // 
            // ColumnFirstName
            // 
            this.ColumnFirstName.DataPropertyName = "FirstName";
            this.ColumnFirstName.HeaderText = "First Name";
            this.ColumnFirstName.Name = "ColumnFirstName";
            this.ColumnFirstName.ReadOnly = true;
            // 
            // ColumnLastName
            // 
            this.ColumnLastName.DataPropertyName = "LastName";
            this.ColumnLastName.HeaderText = "Last Name";
            this.ColumnLastName.Name = "ColumnLastName";
            this.ColumnLastName.ReadOnly = true;
            this.ColumnLastName.Width = 150;
            // 
            // ColumnEmail
            // 
            this.ColumnEmail.DataPropertyName = "EmailAddress";
            this.ColumnEmail.HeaderText = "Email Address";
            this.ColumnEmail.Name = "ColumnEmail";
            this.ColumnEmail.ReadOnly = true;
            this.ColumnEmail.Width = 125;
            // 
            // ColumnCity
            // 
            this.ColumnCity.DataPropertyName = "City";
            this.ColumnCity.HeaderText = "City";
            this.ColumnCity.Name = "ColumnCity";
            this.ColumnCity.ReadOnly = true;
            // 
            // ColumnState
            // 
            this.ColumnState.DataPropertyName = "State";
            this.ColumnState.HeaderText = "State";
            this.ColumnState.Name = "ColumnState";
            this.ColumnState.ReadOnly = true;
            this.ColumnState.Width = 50;
            // 
            // SearchBowlersGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SearchBowlersGrid";
            this.Size = new System.Drawing.Size(599, 377);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DataGridViewTextBoxColumn ColumnFirstName;
    private DataGridViewTextBoxColumn ColumnLastName;
    private DataGridViewTextBoxColumn ColumnEmail;
    private DataGridViewTextBoxColumn ColumnCity;
    private DataGridViewTextBoxColumn ColumnState;
}
