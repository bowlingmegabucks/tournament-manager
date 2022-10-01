namespace NortheastMegabuck.Scores;

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
            this.scoresGrid = new NortheastMegabuck.Controls.ScoresGrid();
            this.SuspendLayout();
            // 
            // scoresGrid
            // 
            this.scoresGrid.AllowRowSelection = true;
            this.scoresGrid.AlternateRowColors = true;
            this.scoresGrid.Location = new System.Drawing.Point(12, 66);
            this.scoresGrid.Name = "scoresGrid";
            this.scoresGrid.SelectedRowContextMenu = null;
            this.scoresGrid.Size = new System.Drawing.Size(811, 377);
            this.scoresGrid.TabIndex = 0;
            // 
            // ScoresForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 746);
            this.Controls.Add(this.scoresGrid);
            this.MaximizeBox = false;
            this.Name = "ScoresForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scores";
            this.ResumeLayout(false);

    }

    #endregion

    private Controls.ScoresGrid scoresGrid;
}