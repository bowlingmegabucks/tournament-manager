namespace NewEnglandClassic.Tournaments.Portal;

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
            this.MenuStripPortal = new System.Windows.Forms.MenuStrip();
            this.MenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDivision = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDivisionAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDivisionsView = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripPortal.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStripPortal
            // 
            this.MenuStripPortal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile,
            this.MenuItemDivision});
            this.MenuStripPortal.Location = new System.Drawing.Point(0, 0);
            this.MenuStripPortal.Name = "MenuStripPortal";
            this.MenuStripPortal.Size = new System.Drawing.Size(800, 24);
            this.MenuStripPortal.TabIndex = 0;
            this.MenuStripPortal.Text = "menuStrip1";
            // 
            // MenuItemFile
            // 
            this.MenuItemFile.Name = "MenuItemFile";
            this.MenuItemFile.Size = new System.Drawing.Size(37, 20);
            this.MenuItemFile.Text = "File";
            // 
            // MenuItemDivision
            // 
            this.MenuItemDivision.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemDivisionAdd,
            this.MenuItemDivisionsView});
            this.MenuItemDivision.Name = "MenuItemDivision";
            this.MenuItemDivision.Size = new System.Drawing.Size(66, 20);
            this.MenuItemDivision.Text = "Divisions";
            // 
            // MenuItemDivisionAdd
            // 
            this.MenuItemDivisionAdd.Name = "MenuItemDivisionAdd";
            this.MenuItemDivisionAdd.Size = new System.Drawing.Size(180, 22);
            this.MenuItemDivisionAdd.Text = "Add";
            this.MenuItemDivisionAdd.Click += new System.EventHandler(this.MenuItemDivisionAdd_Click);
            // 
            // MenuItemDivisionsView
            // 
            this.MenuItemDivisionsView.Name = "MenuItemDivisionsView";
            this.MenuItemDivisionsView.Size = new System.Drawing.Size(180, 22);
            this.MenuItemDivisionsView.Text = "View";
            this.MenuItemDivisionsView.Click += new System.EventHandler(this.MenuItemDivisionsView_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MenuStripPortal);
            this.MainMenuStrip = this.MenuStripPortal;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TournamentPortal";
            this.MenuStripPortal.ResumeLayout(false);
            this.MenuStripPortal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private MenuStrip MenuStripPortal;
    private ToolStripMenuItem MenuItemFile;
    private ToolStripMenuItem MenuItemDivision;
    private ToolStripMenuItem MenuItemDivisionAdd;
    private ToolStripMenuItem MenuItemDivisionsView;
}
