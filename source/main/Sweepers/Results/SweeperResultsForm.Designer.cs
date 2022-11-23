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
            this.resultsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.copyToClipboardLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // resultsFlowLayoutPanel
            // 
            this.resultsFlowLayoutPanel.AutoScroll = true;
            this.resultsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.resultsFlowLayoutPanel.Name = "resultsFlowLayoutPanel";
            this.resultsFlowLayoutPanel.Size = new System.Drawing.Size(579, 637);
            this.resultsFlowLayoutPanel.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 615);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(579, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // copyToClipboardLabel
            // 
            this.copyToClipboardLabel.IsLink = true;
            this.copyToClipboardLabel.Name = "copyToClipboardLabel";
            this.copyToClipboardLabel.Size = new System.Drawing.Size(104, 17);
            this.copyToClipboardLabel.Text = "Copy to Clipboard";
            this.copyToClipboardLabel.Click += new System.EventHandler(this.CopyToClipboardLabel_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 637);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.resultsFlowLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sweeper Results";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private FlowLayoutPanel resultsFlowLayoutPanel;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel copyToClipboardLabel;
}