namespace BowlingMegabucks.TournamentManager.Tournaments.Seeding;

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
        divisionsTabControl = new TabControl();
        statusStrip1 = new StatusStrip();
        copyToClipboardLabel = new ToolStripStatusLabel();
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        fileSaveAsPdfMenuItem = new ToolStripMenuItem();
        printToolStripMenuItem = new ToolStripMenuItem();
        statusStrip1.SuspendLayout();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // divisionsTabControl
        // 
        divisionsTabControl.Dock = DockStyle.Fill;
        divisionsTabControl.Location = new Point(0, 24);
        divisionsTabControl.Name = "divisionsTabControl";
        divisionsTabControl.SelectedIndex = 0;
        divisionsTabControl.Size = new Size(650, 752);
        divisionsTabControl.TabIndex = 0;
        // 
        // statusStrip1
        // 
        statusStrip1.Items.AddRange(new ToolStripItem[] { copyToClipboardLabel });
        statusStrip1.Location = new Point(0, 776);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(650, 22);
        statusStrip1.TabIndex = 2;
        statusStrip1.Text = "statusStrip1";
        // 
        // copyToClipboardLabel
        // 
        copyToClipboardLabel.IsLink = true;
        copyToClipboardLabel.Name = "copyToClipboardLabel";
        copyToClipboardLabel.Size = new Size(104, 17);
        copyToClipboardLabel.Text = "Copy to Clipboard";
        copyToClipboardLabel.Click += CopyToClipboardLabel_Click;
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(650, 24);
        menuStrip1.TabIndex = 3;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { fileSaveAsPdfMenuItem, printToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "File";
        // 
        // fileSaveAsPdfMenuItem
        // 
        fileSaveAsPdfMenuItem.Name = "fileSaveAsPdfMenuItem";
        fileSaveAsPdfMenuItem.Size = new Size(180, 22);
        fileSaveAsPdfMenuItem.Text = "Save as PDF";
        fileSaveAsPdfMenuItem.Click += FileSaveAsPdfMenuItem_Click;
        // 
        // printToolStripMenuItem
        // 
        printToolStripMenuItem.Name = "printToolStripMenuItem";
        printToolStripMenuItem.Size = new Size(180, 22);
        printToolStripMenuItem.Text = "Print";
        printToolStripMenuItem.Click += PrintToolStripMenuItem_Click;
        // 
        // Form
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(650, 798);
        Controls.Add(divisionsTabControl);
        Controls.Add(statusStrip1);
        Controls.Add(menuStrip1);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MainMenuStrip = menuStrip1;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "Form";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Seeding";
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TabControl divisionsTabControl;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel copyToClipboardLabel;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem fileSaveAsPdfMenuItem;
    private ToolStripMenuItem printToolStripMenuItem;
}