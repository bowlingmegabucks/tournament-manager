
namespace BowlingMegabucks.TournamentManager.App.Controls
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
            processingPicture = new PictureBox();
            messageLabel = new Label();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)processingPicture).BeginInit();
            SuspendLayout();
            // 
            // processingPicture
            // 
            processingPicture.Image = (Image)resources.GetObject("processingPicture.Image");
            processingPicture.Location = new Point(200, 100);
            processingPicture.Name = "processingPicture";
            processingPicture.Size = new Size(200, 200);
            processingPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            processingPicture.TabIndex = 0;
            processingPicture.TabStop = false;
            // 
            // messageLabel
            // 
            messageLabel.Dock = DockStyle.Top;
            messageLabel.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            messageLabel.Location = new Point(0, 0);
            messageLabel.Name = "messageLabel";
            messageLabel.Size = new Size(600, 56);
            messageLabel.TabIndex = 1;
            messageLabel.Text = "Message";
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(500, 302);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(97, 23);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += CancelButton_Click;
            // 
            // ProcessingMessage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(cancelButton);
            Controls.Add(messageLabel);
            Controls.Add(processingPicture);
            Name = "ProcessingMessage";
            Size = new Size(600, 328);
            SizeChanged += ProcessingMessage_SizeChanged;
            ((System.ComponentModel.ISupportInitialize)processingPicture).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox processingPicture;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Button cancelButton;
    }
}
