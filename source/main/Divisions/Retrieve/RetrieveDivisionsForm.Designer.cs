namespace BowlingMegabucks.TournamentManager.Divisions.Retrieve;

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
            this.divisionsGrid = new BowlingMegabucks.TournamentManager.Controls.Grids.DivisionsGrid();
            this.addButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // divisionsGrid
            // 
            this.divisionsGrid.AllowRowSelection = true;
            this.divisionsGrid.AlternateRowColors = true;
            this.divisionsGrid.Location = new System.Drawing.Point(12, 12);
            this.divisionsGrid.Name = "divisionsGrid";
            this.divisionsGrid.SelectedRowContextMenu = null;
            this.divisionsGrid.Size = new System.Drawing.Size(884, 377);
            this.divisionsGrid.TabIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(12, 395);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 443);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.divisionsGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Divisions";
            this.ResumeLayout(false);

    }

    #endregion

    private Controls.Grids.DivisionsGrid divisionsGrid;
    private Button addButton;
}