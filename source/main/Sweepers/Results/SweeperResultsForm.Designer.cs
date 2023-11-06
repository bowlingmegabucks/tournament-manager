namespace NortheastMegabuck.Sweepers.Results;

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
        resultsFlowLayoutPanel = new FlowLayoutPanel();
        statusStrip1 = new StatusStrip();
        copyToClipboardLabel = new ToolStripStatusLabel();
        menuStripMain = new MenuStrip();
        fileMenuItem = new ToolStripMenuItem();
        fileSaveAsPDFMenuItem = new ToolStripMenuItem();
        statusStrip1.SuspendLayout();
        menuStripMain.SuspendLayout();
        SuspendLayout();
        // 
        // resultsFlowLayoutPanel
        // 
        resultsFlowLayoutPanel.AutoScroll = true;
        resultsFlowLayoutPanel.Dock = DockStyle.Fill;
        resultsFlowLayoutPanel.Location = new Point(0, 24);
        resultsFlowLayoutPanel.Name = "resultsFlowLayoutPanel";
        resultsFlowLayoutPanel.Size = new Size(579, 613);
        resultsFlowLayoutPanel.TabIndex = 0;
        // 
        // statusStrip1
        // 
        statusStrip1.Items.AddRange(new ToolStripItem[] { copyToClipboardLabel });
        statusStrip1.Location = new Point(0, 615);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(579, 22);
        statusStrip1.TabIndex = 0;
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
        // menuStripMain
        // 
        menuStripMain.Items.AddRange(new ToolStripItem[] { fileMenuItem });
        menuStripMain.Location = new Point(0, 0);
        menuStripMain.Name = "menuStripMain";
        menuStripMain.Size = new Size(579, 24);
        menuStripMain.TabIndex = 1;
        menuStripMain.Text = "menuStrip1";
        // 
        // fileMenuItem
        // 
        fileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { fileSaveAsPDFMenuItem });
        fileMenuItem.Name = "fileMenuItem";
        fileMenuItem.Size = new Size(37, 20);
        fileMenuItem.Text = "File";
        // 
        // fileSaveAsPDFMenuItem
        // 
        fileSaveAsPDFMenuItem.Name = "fileSaveAsPDFMenuItem";
        fileSaveAsPDFMenuItem.Size = new Size(136, 22);
        fileSaveAsPDFMenuItem.Text = "Save as PDF";
        fileSaveAsPDFMenuItem.Click += FileSaveAsPDFMenuItem_Click;
        // 
        // Form
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(579, 637);
        Controls.Add(statusStrip1);
        Controls.Add(resultsFlowLayoutPanel);
        Controls.Add(menuStripMain);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MainMenuStrip = menuStripMain;
        MaximizeBox = false;
        Name = "Form";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Sweeper Results";
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        menuStripMain.ResumeLayout(false);
        menuStripMain.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private FlowLayoutPanel resultsFlowLayoutPanel;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel copyToClipboardLabel;
    private MenuStrip menuStripMain;
    private ToolStripMenuItem fileMenuItem;
    private ToolStripMenuItem fileSaveAsPDFMenuItem;
}