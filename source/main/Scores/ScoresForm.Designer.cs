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
        scoresGrid = new Controls.Grids.ScoresGrid();
        pasteScoresFromClipboardLinkLabel = new LinkLabel();
        saveButton = new Button();
        SuspendLayout();
        // 
        // scoresGrid
        // 
        scoresGrid.AllowRowSelection = true;
        scoresGrid.AlternateRowColors = true;
        scoresGrid.Location = new Point(12, 12);
        scoresGrid.Name = "scoresGrid";
        scoresGrid.SelectedRowContextMenu = null;
        scoresGrid.Size = new Size(773, 588);
        scoresGrid.TabIndex = 0;
        // 
        // pasteScoresFromClipboardLinkLabel
        // 
        pasteScoresFromClipboardLinkLabel.AutoSize = true;
        pasteScoresFromClipboardLinkLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
        pasteScoresFromClipboardLinkLabel.Location = new Point(589, 609);
        pasteScoresFromClipboardLinkLabel.Name = "pasteScoresFromClipboardLinkLabel";
        pasteScoresFromClipboardLinkLabel.Size = new Size(196, 20);
        pasteScoresFromClipboardLinkLabel.TabIndex = 8;
        pasteScoresFromClipboardLinkLabel.TabStop = true;
        pasteScoresFromClipboardLinkLabel.Text = "Paste Scores from Clipboard";
        pasteScoresFromClipboardLinkLabel.LinkClicked += PasteScoresFromClipboardLinkLabel_LinkClicked;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(12, 606);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(94, 29);
        saveButton.TabIndex = 9;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        // 
        // Form
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(805, 648);
        Controls.Add(saveButton);
        Controls.Add(pasteScoresFromClipboardLinkLabel);
        Controls.Add(scoresGrid);
        MaximizeBox = false;
        Name = "Form";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Scores";
        Load += Form_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Controls.Grids.ScoresGrid scoresGrid;
    private LinkLabel pasteScoresFromClipboardLinkLabel;
    private Button saveButton;
}