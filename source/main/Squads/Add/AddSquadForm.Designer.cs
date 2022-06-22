namespace NewEnglandClassic.Squads.Add;

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
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.SquadNew = new NewEnglandClassic.Contols.SquadControl();
            this.LabelDisclaimer = new System.Windows.Forms.Label();
            this.LabelTournamentFinalsRatio = new System.Windows.Forms.Label();
            this.LabelTournamentCashRatio = new System.Windows.Forms.Label();
            this.TextboxTournamentFinalsRatio = new System.Windows.Forms.TextBox();
            this.TextboxTournamentCashRatio = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(12, 352);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // ButtonSave
            // 
            this.ButtonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonSave.Location = new System.Drawing.Point(430, 352);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 23);
            this.ButtonSave.TabIndex = 1;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // SquadNew
            // 
            this.SquadNew.CashRatio = null;
            this.SquadNew.Complete = false;
            this.SquadNew.Date = new System.DateTime(2022, 6, 8, 10, 30, 39, 618);
            this.SquadNew.FinalsRatio = null;
            this.SquadNew.Id = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.SquadNew.Location = new System.Drawing.Point(12, 139);
            this.SquadNew.MaxPerPair = ((short)(0));
            this.SquadNew.Name = "SquadNew";
            this.SquadNew.NumberOfLanes = ((short)(1));
            this.SquadNew.Size = new System.Drawing.Size(519, 207);
            this.SquadNew.StartingLane = ((short)(1));
            this.SquadNew.TabIndex = 0;
            this.SquadNew.TournamentId = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // LabelDisclaimer
            // 
            this.LabelDisclaimer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelDisclaimer.Location = new System.Drawing.Point(12, 9);
            this.LabelDisclaimer.Name = "LabelDisclaimer";
            this.LabelDisclaimer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.LabelDisclaimer.Size = new System.Drawing.Size(519, 77);
            this.LabelDisclaimer.TabIndex = 8;
            this.LabelDisclaimer.Text = "If the desire is to keep the Finals and/or Cash Ratio for this squad the same as " +
    "the tournament default, set the vaules for these fields to zero.";
            // 
            // LabelTournamentFinalsRatio
            // 
            this.LabelTournamentFinalsRatio.AutoSize = true;
            this.LabelTournamentFinalsRatio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelTournamentFinalsRatio.Location = new System.Drawing.Point(12, 86);
            this.LabelTournamentFinalsRatio.Name = "LabelTournamentFinalsRatio";
            this.LabelTournamentFinalsRatio.Size = new System.Drawing.Size(163, 17);
            this.LabelTournamentFinalsRatio.TabIndex = 9;
            this.LabelTournamentFinalsRatio.Text = "Tournament Finals Ratio:";
            // 
            // LabelTournamentCashRatio
            // 
            this.LabelTournamentCashRatio.AutoSize = true;
            this.LabelTournamentCashRatio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelTournamentCashRatio.Location = new System.Drawing.Point(349, 86);
            this.LabelTournamentCashRatio.Name = "LabelTournamentCashRatio";
            this.LabelTournamentCashRatio.Size = new System.Drawing.Size(156, 17);
            this.LabelTournamentCashRatio.TabIndex = 10;
            this.LabelTournamentCashRatio.Text = "Tournament Cash Ratio:";
            // 
            // TextboxTournamentFinalsRatio
            // 
            this.TextboxTournamentFinalsRatio.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxTournamentFinalsRatio.Location = new System.Drawing.Point(12, 106);
            this.TextboxTournamentFinalsRatio.Name = "TextboxTournamentFinalsRatio";
            this.TextboxTournamentFinalsRatio.ReadOnly = true;
            this.TextboxTournamentFinalsRatio.Size = new System.Drawing.Size(163, 27);
            this.TextboxTournamentFinalsRatio.TabIndex = 11;
            this.TextboxTournamentFinalsRatio.TabStop = false;
            this.TextboxTournamentFinalsRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TextboxTournamentCashRatio
            // 
            this.TextboxTournamentCashRatio.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxTournamentCashRatio.Location = new System.Drawing.Point(349, 106);
            this.TextboxTournamentCashRatio.Name = "TextboxTournamentCashRatio";
            this.TextboxTournamentCashRatio.ReadOnly = true;
            this.TextboxTournamentCashRatio.Size = new System.Drawing.Size(156, 27);
            this.TextboxTournamentCashRatio.TabIndex = 12;
            this.TextboxTournamentCashRatio.TabStop = false;
            this.TextboxTournamentCashRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form
            // 
            this.AcceptButton = this.ButtonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(533, 390);
            this.Controls.Add(this.TextboxTournamentCashRatio);
            this.Controls.Add(this.TextboxTournamentFinalsRatio);
            this.Controls.Add(this.LabelTournamentCashRatio);
            this.Controls.Add(this.LabelTournamentFinalsRatio);
            this.Controls.Add(this.LabelDisclaimer);
            this.Controls.Add(this.SquadNew);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Squad";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Button ButtonCancel;
    private Button ButtonSave;
    private Contols.SquadControl SquadNew;
    private Label LabelDisclaimer;
    private Label LabelTournamentFinalsRatio;
    private Label LabelTournamentCashRatio;
    private TextBox TextboxTournamentFinalsRatio;
    private TextBox TextboxTournamentCashRatio;
}