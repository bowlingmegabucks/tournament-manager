namespace NewEnglandClassic.Divisions.Add;

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
            this.DivisionNew = new NewEnglandClassic.Contols.DivisionControl();
            this.SuspendLayout();
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(12, 502);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 4;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // ButtonSave
            // 
            this.ButtonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonSave.Location = new System.Drawing.Point(357, 502);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 23);
            this.ButtonSave.TabIndex = 3;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // DivisionNew
            // 
            this.DivisionNew.DivisionName = "";
            this.DivisionNew.Gender = null;
            this.DivisionNew.HandicapBase = null;
            this.DivisionNew.HandicapPercentage = null;
            this.DivisionNew.Id = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.DivisionNew.Location = new System.Drawing.Point(1, 12);
            this.DivisionNew.MaximumAge = null;
            this.DivisionNew.MaximumAverage = null;
            this.DivisionNew.MaximumHandicapPerGame = null;
            this.DivisionNew.MinimumAge = null;
            this.DivisionNew.MinimumAverage = null;
            this.DivisionNew.Name = "DivisionNew";
            this.DivisionNew.Size = new System.Drawing.Size(463, 484);
            this.DivisionNew.TabIndex = 5;
            this.DivisionNew.TournamentId = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // Form
            // 
            this.AcceptButton = this.ButtonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(476, 542);
            this.Controls.Add(this.DivisionNew);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSave);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Division";
            this.ResumeLayout(false);

    }

    #endregion
    private Button ButtonCancel;
    private Button ButtonSave;
    private Contols.DivisionControl DivisionNew;
}
