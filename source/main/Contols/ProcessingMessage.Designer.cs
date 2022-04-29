
namespace NewEnglandClassic.Controls
{
    /// <summary>
    /// 
    /// </summary>
    partial class ProcessingMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessingMessage));
            this.PictureBoxProcessing = new System.Windows.Forms.PictureBox();
            this.LabelMessage = new System.Windows.Forms.Label();
            this.ButtonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxProcessing)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxProcessing
            // 
            this.PictureBoxProcessing.Image = ((System.Drawing.Image)(resources.GetObject("PictureBoxProcessing.Image")));
            this.PictureBoxProcessing.Location = new System.Drawing.Point(200, 100);
            this.PictureBoxProcessing.Name = "PictureBoxProcessing";
            this.PictureBoxProcessing.Size = new System.Drawing.Size(200, 200);
            this.PictureBoxProcessing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxProcessing.TabIndex = 0;
            this.PictureBoxProcessing.TabStop = false;
            // 
            // LabelMessage
            // 
            this.LabelMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelMessage.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelMessage.Location = new System.Drawing.Point(0, 0);
            this.LabelMessage.Name = "LabelMessage";
            this.LabelMessage.Size = new System.Drawing.Size(600, 56);
            this.LabelMessage.TabIndex = 1;
            this.LabelMessage.Text = "Message";
            this.LabelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(500, 302);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(97, 23);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ProcessingMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.LabelMessage);
            this.Controls.Add(this.PictureBoxProcessing);
            this.Name = "ProcessingMessage";
            this.Size = new System.Drawing.Size(600, 328);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxProcessing)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxProcessing;
        private System.Windows.Forms.Label LabelMessage;
        private System.Windows.Forms.Button ButtonCancel;
    }
}
