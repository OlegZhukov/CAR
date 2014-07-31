namespace OlegZhukov.CAR.Paint.NET
{
    partial class CARDialog
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
            this.okButton = new System.Windows.Forms.Button();
            this.contentEnergyFunctionComboBox = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.widthOrHeightComboBox = new System.Windows.Forms.ComboBox();
            this.contentEnergyFunctionLabel = new System.Windows.Forms.Label();
            this.targetSizeUpDownControl = new System.Windows.Forms.NumericUpDown();
            this.toLabel = new System.Windows.Forms.Label();
            this.percentOrPixelComboBox = new System.Windows.Forms.ComboBox();
            this.changeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.targetSizeUpDownControl)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(108, 78);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 24);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // contentEnergyFunctionComboBox
            // 
            this.contentEnergyFunctionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentEnergyFunctionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.contentEnergyFunctionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.contentEnergyFunctionComboBox.FormattingEnabled = true;
            this.contentEnergyFunctionComboBox.Items.AddRange(new object[] {
            "Brightness Gradient (norm)",
            "Brightness Gradient (X component)",
            "RGB Gradient (norm with const borders)"});
            this.contentEnergyFunctionComboBox.Location = new System.Drawing.Point(109, 39);
            this.contentEnergyFunctionComboBox.Name = "contentEnergyFunctionComboBox";
            this.contentEnergyFunctionComboBox.Size = new System.Drawing.Size(154, 21);
            this.contentEnergyFunctionComboBox.TabIndex = 2;
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
            // widthOrHeightComboBox
            // 
            this.widthOrHeightComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.widthOrHeightComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.widthOrHeightComboBox.FormattingEnabled = true;
            this.widthOrHeightComboBox.Items.AddRange(new object[] {
            "width",
            "height"});
            this.widthOrHeightComboBox.Location = new System.Drawing.Point(62, 12);
            this.widthOrHeightComboBox.Name = "widthOrHeightComboBox";
            this.widthOrHeightComboBox.Size = new System.Drawing.Size(57, 21);
            this.widthOrHeightComboBox.TabIndex = 5;
            // 
            // contentEnergyFunctionLabel
            // 
            this.contentEnergyFunctionLabel.AutoSize = true;
            this.contentEnergyFunctionLabel.Location = new System.Drawing.Point(12, 42);
            this.contentEnergyFunctionLabel.Name = "contentEnergyFunctionLabel";
            this.contentEnergyFunctionLabel.Size = new System.Drawing.Size(91, 13);
            this.contentEnergyFunctionLabel.TabIndex = 6;
            this.contentEnergyFunctionLabel.Text = "Content detection";
            // 
            // targetSizeUpDownControl
            // 
            this.targetSizeUpDownControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.targetSizeUpDownControl.Location = new System.Drawing.Point(159, 13);
            this.targetSizeUpDownControl.Name = "targetSizeUpDownControl";
            this.targetSizeUpDownControl.Size = new System.Drawing.Size(56, 20);
            this.targetSizeUpDownControl.TabIndex = 8;
            this.targetSizeUpDownControl.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // toLabel
            // 
            this.toLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(132, 15);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(16, 13);
            this.toLabel.TabIndex = 9;
            this.toLabel.Text = "to";
            // 
            // percentOrPixelComboBox
            // 
            this.percentOrPixelComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.percentOrPixelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.percentOrPixelComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.percentOrPixelComboBox.FormattingEnabled = true;
            this.percentOrPixelComboBox.Items.AddRange(new object[] {
            "%",
            "px"});
            this.percentOrPixelComboBox.Location = new System.Drawing.Point(221, 12);
            this.percentOrPixelComboBox.Name = "percentOrPixelComboBox";
            this.percentOrPixelComboBox.Size = new System.Drawing.Size(42, 21);
            this.percentOrPixelComboBox.TabIndex = 10;
            // 
            // changeLabel
            // 
            this.changeLabel.AutoSize = true;
            this.changeLabel.Location = new System.Drawing.Point(12, 15);
            this.changeLabel.Name = "changeLabel";
            this.changeLabel.Size = new System.Drawing.Size(44, 13);
            this.changeLabel.TabIndex = 11;
            this.changeLabel.Text = "Change";
            // 
            // CARDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(275, 109);
            this.Controls.Add(this.changeLabel);
            this.Controls.Add(this.percentOrPixelComboBox);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.targetSizeUpDownControl);
            this.Controls.Add(this.contentEnergyFunctionLabel);
            this.Controls.Add(this.widthOrHeightComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.contentEnergyFunctionComboBox);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CARDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Content-Aware Resize";
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.contentEnergyFunctionComboBox, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.widthOrHeightComboBox, 0);
            this.Controls.SetChildIndex(this.contentEnergyFunctionLabel, 0);
            this.Controls.SetChildIndex(this.targetSizeUpDownControl, 0);
            this.Controls.SetChildIndex(this.toLabel, 0);
            this.Controls.SetChildIndex(this.percentOrPixelComboBox, 0);
            this.Controls.SetChildIndex(this.changeLabel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.targetSizeUpDownControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ComboBox contentEnergyFunctionComboBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox widthOrHeightComboBox;
        private System.Windows.Forms.Label contentEnergyFunctionLabel;
        private System.Windows.Forms.NumericUpDown targetSizeUpDownControl;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.ComboBox percentOrPixelComboBox;
        private System.Windows.Forms.Label changeLabel;
    }
}
