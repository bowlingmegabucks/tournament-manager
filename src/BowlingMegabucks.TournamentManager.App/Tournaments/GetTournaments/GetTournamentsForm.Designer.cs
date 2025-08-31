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
        DisposeFields(disposing);

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
        openButton = new Button();
        tournamentsGrid = new TournamentsGrid();
        newButton = new Button();
        SuspendLayout();
        // 
        // openButton
        // 
        openButton.Location = new Point(595, 383);
        openButton.Name = "openButton";
        openButton.Size = new Size(75, 23);
        openButton.TabIndex = 0;
        openButton.Text = "Open";
        openButton.UseVisualStyleBackColor = true;
        openButton.Click += OpenButton_Click;
        // 
        // tournamentsGrid
        // 
        tournamentsGrid.Location = new Point(0, 0);
        tournamentsGrid.Name = "tournamentsGrid";
        tournamentsGrid.PageSize = 2;
        tournamentsGrid.PageSizeOptions.Add(2);
        tournamentsGrid.PageSizeOptions.Add(10);
        tournamentsGrid.PageSizeOptions.Add(25);
        tournamentsGrid.PageSizeOptions.Add(50);
        tournamentsGrid.PageSizeOptions.Add(100);
        tournamentsGrid.PageSizeOptions.Add(2);
        tournamentsGrid.PageSizeOptions.Add(10);
        tournamentsGrid.PageSizeOptions.Add(25);
        tournamentsGrid.PageSizeOptions.Add(50);
        tournamentsGrid.PageSizeOptions.Add(100);
        tournamentsGrid.PageSizeOptions.Add(2);
        tournamentsGrid.PageSizeOptions.Add(10);
        tournamentsGrid.PageSizeOptions.Add(25);
        tournamentsGrid.PageSizeOptions.Add(50);
        tournamentsGrid.PageSizeOptions.Add(100);
        tournamentsGrid.PaginationEnabled = true;
        tournamentsGrid.Size = new Size(670, 377);
        tournamentsGrid.TabIndex = 1;
        tournamentsGrid.GridRowDoubleClicked += TournamentsGrid_GridRowDoubleClicked;
        tournamentsGrid.PagingChanged += TournamentsGrid_PagingChanged;
        // 
        // newButton
        // 
        newButton.Location = new Point(12, 383);
        newButton.Name = "newButton";
        newButton.Size = new Size(75, 23);
        newButton.TabIndex = 2;
        newButton.Text = "New";
        newButton.UseVisualStyleBackColor = true;
        newButton.Click += NewButton_Click;
        // 
        // GetTournamentsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(674, 415);
        Controls.Add(newButton);
        Controls.Add(tournamentsGrid);
        Controls.Add(openButton);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "GetTournamentsForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Open Tournament";
        ResumeLayout(false);

    }

    #endregion

    private Button openButton;
    private TournamentsGrid tournamentsGrid;
    private Button newButton;
}
