namespace OlegZhukov.CAR.Paint.NET
{
    partial class CARProgressDialog
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.resizingLabel = new System.Windows.Forms.Label();
            this.resizeProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(189, 78);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 24);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // resizingLabel
            // 
            this.resizingLabel.AutoSize = true;
            this.resizingLabel.Location = new System.Drawing.Point(12, 15);
            this.resizingLabel.Name = "resizingLabel";
            this.resizingLabel.Size = new System.Drawing.Size(73, 13);
            this.resizingLabel.TabIndex = 4;
            this.resizingLabel.Text = "Resizing... 0%";
            // 
            // resizeProgressBar
            // 
            this.resizeProgressBar.Location = new System.Drawing.Point(15, 39);
            this.resizeProgressBar.Name = "resizeProgressBar";
            this.resizeProgressBar.Size = new System.Drawing.Size(248, 17);
            this.resizeProgressBar.TabIndex = 11;
            // 
            // CARDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(275, 109);
            this.Controls.Add(this.resizeProgressBar);
            this.Controls.Add(this.resizingLabel);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CARDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Content-Aware Resize";
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.resizingLabel, 0);
            this.Controls.SetChildIndex(this.resizeProgressBar, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label resizingLabel;
        private System.Windows.Forms.ProgressBar resizeProgressBar;
    }
}
