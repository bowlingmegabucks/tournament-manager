namespace NewEnglandClassic.Sweepers.Retrieve;

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
            this.sweepersGrid = new NewEnglandClassic.Contols.SweepersGrid();
            this.addButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sweepersGrid
            // 
            this.sweepersGrid.AllowRowSelection = true;
            this.sweepersGrid.AlternateRowColors = true;
            this.sweepersGrid.Location = new System.Drawing.Point(12, 12);
            this.sweepersGrid.Name = "sweepersGrid";
            this.sweepersGrid.SelectedRowContextMenu = null;
            this.sweepersGrid.Size = new System.Drawing.Size(654, 377);
            this.sweepersGrid.TabIndex = 0;
            this.sweepersGrid.GridRowDoubleClicked += new System.EventHandler<NewEnglandClassic.Controls.GridRowDoubleClickEventArgs>(this.SweepersGrid_GridRowDoubleClicked);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(12, 395);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(591, 395);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 5;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 463);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.sweepersGrid);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tournament Sweepers";
            this.ResumeLayout(false);

    }

    #endregion

    private Contols.SweepersGrid sweepersGrid;
    private Button addButton;
    private Button openButton;
}