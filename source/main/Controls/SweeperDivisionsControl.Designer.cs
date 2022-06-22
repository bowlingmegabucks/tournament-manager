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
            this.FlowLayoutPanelSweeperDivisions = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // FlowLayoutPanelSweeperDivisions
            // 
            this.FlowLayoutPanelSweeperDivisions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowLayoutPanelSweeperDivisions.Location = new System.Drawing.Point(0, 0);
            this.FlowLayoutPanelSweeperDivisions.Name = "FlowLayoutPanelSweeperDivisions";
            this.FlowLayoutPanelSweeperDivisions.Size = new System.Drawing.Size(535, 369);
            this.FlowLayoutPanelSweeperDivisions.TabIndex = 0;
            // 
            // SweeperDivisionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FlowLayoutPanelSweeperDivisions);
            this.Name = "SweeperDivisionsControl";
            this.Size = new System.Drawing.Size(535, 369);
            this.ResumeLayout(false);

    }

    #endregion

    private FlowLayoutPanel FlowLayoutPanelSweeperDivisions;
}
