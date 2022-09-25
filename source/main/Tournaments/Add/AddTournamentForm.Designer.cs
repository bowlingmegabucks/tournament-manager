namespace NortheastMegabuck.Tournaments.Add;

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
            this.newTournament = new NortheastMegabuck.Controls.TournamentControl();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newTournament
            // 
            this.newTournament.BowlingCenter = "";
            this.newTournament.CashRatio = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.newTournament.Completed = false;
            this.newTournament.EntryFee = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.newTournament.FinalsRatio = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.newTournament.Games = ((short)(0));
            this.newTournament.Location = new System.Drawing.Point(12, 12);
            this.newTournament.Name = "newTournament";
            this.newTournament.Size = new System.Drawing.Size(354, 330);
            this.newTournament.TabIndex = 0;
            this.newTournament.TournamentName = "";
            // 
            // saveButton
            // 
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(282, 348);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(12, 348);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // Form
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(369, 387);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.newTournament);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Tournament";
            this.ResumeLayout(false);

    }

    #endregion

    private Controls.TournamentControl newTournament;
    private Button saveButton;
    private Button cancelButton;
}
