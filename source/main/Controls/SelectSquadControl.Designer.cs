namespace NewEnglandClassic.Controls;

partial class SelectSquadControl
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
            this.CheckboxName = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CheckboxName
            // 
            this.CheckboxName.AutoSize = true;
            this.CheckboxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CheckboxName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckboxName.Location = new System.Drawing.Point(0, 0);
            this.CheckboxName.Name = "CheckboxName";
            this.CheckboxName.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.CheckboxName.Size = new System.Drawing.Size(237, 45);
            this.CheckboxName.TabIndex = 0;
            this.CheckboxName.Text = "DisplayText";
            this.CheckboxName.UseVisualStyleBackColor = true;
            // 
            // SelectIdControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CheckboxName);
            this.Name = "SelectIdControl";
            this.Size = new System.Drawing.Size(237, 45);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private CheckBox CheckboxName;
}
