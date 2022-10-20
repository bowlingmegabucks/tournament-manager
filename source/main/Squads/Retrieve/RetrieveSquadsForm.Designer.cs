namespace NortheastMegabuck.Squads.Retrieve;

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
            this.squadsGrid = new NortheastMegabuck.Controls.Grids.SquadsGrid();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // squadsGrid
            // 
            this.squadsGrid.AllowRowSelection = true;
            this.squadsGrid.AlternateRowColors = true;
            this.squadsGrid.Location = new System.Drawing.Point(12, 12);
            this.squadsGrid.Name = "squadsGrid";
            this.squadsGrid.SelectedRowContextMenu = null;
            this.squadsGrid.Size = new System.Drawing.Size(654, 377);
            this.squadsGrid.TabIndex = 0;
            this.squadsGrid.GridRowDoubleClicked += new System.EventHandler<NortheastMegabuck.Controls.Grids.GridRowDoubleClickEventArgs>(this.SquadsGrid_GridRowDoubleClicked);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(12, 395);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(591, 395);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 5;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 463);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.squadsGrid);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tournament Squads";
            this.ResumeLayout(false);

    }

    #endregion

    private Controls.Grids.SquadsGrid squadsGrid;
    private Button buttonAdd;
    private Button buttonOpen;
}