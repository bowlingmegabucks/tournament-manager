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
            this.pasteScoresFromClipboardLinkLabel = new System.Windows.Forms.LinkLabel();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // scoresGrid
            // 
            this.scoresGrid.AllowRowSelection = true;
            this.scoresGrid.AlternateRowColors = true;
            this.scoresGrid.Location = new System.Drawing.Point(12, 12);
            this.scoresGrid.Name = "scoresGrid";
            this.scoresGrid.SelectedRowContextMenu = null;
            this.scoresGrid.Size = new System.Drawing.Size(773, 588);
            this.scoresGrid.TabIndex = 0;
            // 
            // pasteScoresFromClipboardLinkLabel
            // 
            this.pasteScoresFromClipboardLinkLabel.AutoSize = true;
            this.pasteScoresFromClipboardLinkLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pasteScoresFromClipboardLinkLabel.Location = new System.Drawing.Point(589, 609);
            this.pasteScoresFromClipboardLinkLabel.Name = "pasteScoresFromClipboardLinkLabel";
            this.pasteScoresFromClipboardLinkLabel.Size = new System.Drawing.Size(196, 20);
            this.pasteScoresFromClipboardLinkLabel.TabIndex = 8;
            this.pasteScoresFromClipboardLinkLabel.TabStop = true;
            this.pasteScoresFromClipboardLinkLabel.Text = "Paste Scores from Clipboard";
            this.pasteScoresFromClipboardLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PasteScoresFromClipboardLinkLabel_LinkClicked);
            // 
            // saveButton
            // 
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(12, 606);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(94, 29);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 648);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.pasteScoresFromClipboardLinkLabel);
            this.Controls.Add(this.scoresGrid);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scores";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Controls.ScoresGrid scoresGrid;
    private LinkLabel pasteScoresFromClipboardLinkLabel;
    private Button saveButton;
}