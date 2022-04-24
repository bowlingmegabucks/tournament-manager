namespace NewEnglandClassic.Tournaments.Retrieve;

partial class Form
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.ButtonOpenTournament = new System.Windows.Forms.Button();
            this.TournamentsGrid = new NewEnglandClassic.Tournaments.TournamentsGrid();
            this.SuspendLayout();
            // 
            // ButtonOpenTournament
            // 
            this.ButtonOpenTournament.Location = new System.Drawing.Point(595, 383);
            this.ButtonOpenTournament.Name = "ButtonOpenTournament";
            this.ButtonOpenTournament.Size = new System.Drawing.Size(75, 23);
            this.ButtonOpenTournament.TabIndex = 0;
            this.ButtonOpenTournament.Text = "Open";
            this.ButtonOpenTournament.UseVisualStyleBackColor = true;
            // 
            // TournamentsGrid
            // 
            this.TournamentsGrid.AllowRowSelection = true;
            this.TournamentsGrid.AlternateRowColors = true;
            this.TournamentsGrid.Location = new System.Drawing.Point(0, 0);
            this.TournamentsGrid.Name = "TournamentsGrid";
            this.TournamentsGrid.SelectedRowContextMenu = null;
            this.TournamentsGrid.Size = new System.Drawing.Size(670, 377);
            this.TournamentsGrid.TabIndex = 1;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 415);
            this.Controls.Add(this.TournamentsGrid);
            this.Controls.Add(this.ButtonOpenTournament);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open Tournament";
            this.ResumeLayout(false);

    }

    #endregion

    private Button ButtonOpenTournament;
    private TournamentsGrid TournamentsGrid;
}
