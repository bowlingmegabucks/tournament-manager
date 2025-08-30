namespace BowlingMegabucks.TournamentManager.App.Tournaments;

partial class GetTournamentsForm
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
            this.openButton = new System.Windows.Forms.Button();
            this.tournamentsGrid = new BowlingMegabucks.TournamentManager.App.Tournaments.TournamentsGrid();
            this.newButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(595, 383);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // tournamentsGrid
            // 
            this.tournamentsGrid.AllowRowSelection = true;
            this.tournamentsGrid.AlternateRowColors = true;
            this.tournamentsGrid.Location = new System.Drawing.Point(0, 0);
            this.tournamentsGrid.Name = "tournamentsGrid";
            this.tournamentsGrid.SelectedRowContextMenu = null;
            this.tournamentsGrid.Size = new System.Drawing.Size(670, 377);
            this.tournamentsGrid.TabIndex = 1;
            this.tournamentsGrid.GridRowDoubleClicked += new System.EventHandler<BowlingMegabucks.TournamentManager.App.Controls.Grids.GridRowDoubleClickEventArgs>(this.TournamentsGrid_GridRowDoubleClicked);
            // 
            // newButton
            // 
            this.newButton.Location = new System.Drawing.Point(12, 383);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(75, 23);
            this.newButton.TabIndex = 2;
            this.newButton.Text = "New";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 415);
            this.Controls.Add(this.newButton);
            this.Controls.Add(this.tournamentsGrid);
            this.Controls.Add(this.openButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open Tournament";
            this.ResumeLayout(false);

    }

    #endregion

    private Button openButton;
    private TournamentsGrid tournamentsGrid;
    private Button newButton;
}
