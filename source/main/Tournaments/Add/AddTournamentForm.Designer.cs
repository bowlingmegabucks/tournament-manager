namespace NewEnglandClassic.Tournaments.Add;

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
            this.TournamentControlNew = new NewEnglandClassic.Contols.TournamentControl();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TournamentControlNew
            // 
            this.TournamentControlNew.BowlingCenter = "";
            this.TournamentControlNew.CashRatio = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TournamentControlNew.Completed = false;
            this.TournamentControlNew.EntryFee = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TournamentControlNew.FinalsRatio = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TournamentControlNew.Games = ((short)(0));
            this.TournamentControlNew.Id = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.TournamentControlNew.Location = new System.Drawing.Point(12, 12);
            this.TournamentControlNew.Name = "TournamentControlNew";
            this.TournamentControlNew.Size = new System.Drawing.Size(354, 330);
            this.TournamentControlNew.TabIndex = 0;
            this.TournamentControlNew.TournamentName = "";
            // 
            // ButtonSave
            // 
            this.ButtonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonSave.Location = new System.Drawing.Point(282, 348);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 23);
            this.ButtonSave.TabIndex = 1;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(12, 348);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(369, 387);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.TournamentControlNew);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Tournament";
            this.ResumeLayout(false);

    }

    #endregion

    private Contols.TournamentControl TournamentControlNew;
    private Button ButtonSave;
    private Button ButtonCancel;
}
