namespace NewEnglandClassic.Sweepers.Add;

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
            this.SweeperControl = new NewEnglandClassic.Contols.SweeperControl();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SweeperControl
            // 
            this.SweeperControl.AutoSize = true;
            this.SweeperControl.CashRatio = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SweeperControl.Complete = false;
            this.SweeperControl.Date = new System.DateTime(2022, 6, 10, 20, 43, 13, 850);
            this.SweeperControl.EntryFee = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SweeperControl.Games = ((short)(0));
            this.SweeperControl.Id = SquadId.Empty;
            this.SweeperControl.Location = new System.Drawing.Point(12, 12);
            this.SweeperControl.MaxPerPair = ((short)(0));
            this.SweeperControl.Name = "SweeperControl";
            this.SweeperControl.NumberOfLanes = ((short)(1));
            this.SweeperControl.Size = new System.Drawing.Size(546, 516);
            this.SweeperControl.StartingLane = ((short)(1));
            this.SweeperControl.TabIndex = 0;
            this.SweeperControl.TournamentId = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(12, 534);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 4;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // ButtonSave
            // 
            this.ButtonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonSave.Location = new System.Drawing.Point(483, 534);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 23);
            this.ButtonSave.TabIndex = 3;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // Form
            // 
            this.AcceptButton = this.ButtonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(578, 567);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.SweeperControl);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Sweeper";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Contols.SweeperControl SweeperControl;
    private Button ButtonCancel;
    private Button ButtonSave;
}