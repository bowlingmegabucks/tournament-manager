namespace NewEnglandClassic.Contols;

partial class SweeperDivisionsControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.sweeperDivisionsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // sweeperDivisionsFlowLayoutPanel
            // 
            this.sweeperDivisionsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sweeperDivisionsFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.sweeperDivisionsFlowLayoutPanel.Name = "sweeperDivisionsFlowLayoutPanel";
            this.sweeperDivisionsFlowLayoutPanel.Size = new System.Drawing.Size(535, 369);
            this.sweeperDivisionsFlowLayoutPanel.TabIndex = 0;
            // 
            // SweeperDivisionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sweeperDivisionsFlowLayoutPanel);
            this.Name = "SweeperDivisionsControl";
            this.Size = new System.Drawing.Size(535, 369);
            this.ResumeLayout(false);

    }

    #endregion

    private FlowLayoutPanel sweeperDivisionsFlowLayoutPanel;
}
