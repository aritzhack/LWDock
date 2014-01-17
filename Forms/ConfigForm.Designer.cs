namespace LWDock.Forms
{
    partial class ConfigForm
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
            this.maxPopupsLabel = new System.Windows.Forms.Label();
            this.maxPopupsNumber = new System.Windows.Forms.NumericUpDown();
            this.keepOnTopCheckBox = new System.Windows.Forms.CheckBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.noPopupsCheckBox = new System.Windows.Forms.CheckBox();
            this.foldersFirstCheck = new System.Windows.Forms.CheckBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.iconQualityLabel = new System.Windows.Forms.Label();
            this.iconQuality = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.maxPopupsNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // maxPopupsLabel
            // 
            this.maxPopupsLabel.AutoSize = true;
            this.maxPopupsLabel.Location = new System.Drawing.Point(12, 62);
            this.maxPopupsLabel.Name = "maxPopupsLabel";
            this.maxPopupsLabel.Size = new System.Drawing.Size(185, 13);
            this.maxPopupsLabel.TabIndex = 5;
            this.maxPopupsLabel.Text = "Max nested popups (-1 means infinite)";
            // 
            // maxPopupsNumber
            // 
            this.maxPopupsNumber.Location = new System.Drawing.Point(229, 60);
            this.maxPopupsNumber.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxPopupsNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.maxPopupsNumber.Name = "maxPopupsNumber";
            this.maxPopupsNumber.Size = new System.Drawing.Size(43, 20);
            this.maxPopupsNumber.TabIndex = 4;
            // 
            // keepOnTopCheckBox
            // 
            this.keepOnTopCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.keepOnTopCheckBox.Location = new System.Drawing.Point(12, 12);
            this.keepOnTopCheckBox.Name = "keepOnTopCheckBox";
            this.keepOnTopCheckBox.Size = new System.Drawing.Size(259, 18);
            this.keepOnTopCheckBox.TabIndex = 3;
            this.keepOnTopCheckBox.Text = "Keep always on top";
            this.keepOnTopCheckBox.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(197, 144);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(116, 144);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // noPopupsCheckBox
            // 
            this.noPopupsCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.noPopupsCheckBox.Location = new System.Drawing.Point(12, 36);
            this.noPopupsCheckBox.Name = "noPopupsCheckBox";
            this.noPopupsCheckBox.Size = new System.Drawing.Size(259, 18);
            this.noPopupsCheckBox.TabIndex = 8;
            this.noPopupsCheckBox.Text = "Do not open popups";
            this.noPopupsCheckBox.UseVisualStyleBackColor = true;
            this.noPopupsCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // foldersFirstCheck
            // 
            this.foldersFirstCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.foldersFirstCheck.Location = new System.Drawing.Point(12, 86);
            this.foldersFirstCheck.Name = "foldersFirstCheck";
            this.foldersFirstCheck.Size = new System.Drawing.Size(259, 17);
            this.foldersFirstCheck.TabIndex = 9;
            this.foldersFirstCheck.Text = "Folders first";
            this.foldersFirstCheck.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(35, 144);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 10;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // iconQualityLabel
            // 
            this.iconQualityLabel.AutoSize = true;
            this.iconQualityLabel.Location = new System.Drawing.Point(13, 112);
            this.iconQualityLabel.Name = "iconQualityLabel";
            this.iconQualityLabel.Size = new System.Drawing.Size(61, 13);
            this.iconQualityLabel.TabIndex = 12;
            this.iconQualityLabel.Text = "Icon quality";
            // 
            // iconQuality
            // 
            this.iconQuality.FormattingEnabled = true;
            this.iconQuality.Items.AddRange(new object[] {
            "Minimum",
            "Small",
            "Medium",
            "Big"});
            this.iconQuality.Location = new System.Drawing.Point(150, 109);
            this.iconQuality.Name = "iconQuality";
            this.iconQuality.Size = new System.Drawing.Size(121, 21);
            this.iconQuality.TabIndex = 13;
            this.iconQuality.SelectedIndexChanged += new System.EventHandler(this.iconQuality_SelectedIndexChanged);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 173);
            this.Controls.Add(this.iconQuality);
            this.Controls.Add(this.iconQualityLabel);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.foldersFirstCheck);
            this.Controls.Add(this.noPopupsCheckBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.maxPopupsLabel);
            this.Controls.Add(this.maxPopupsNumber);
            this.Controls.Add(this.keepOnTopCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.maxPopupsNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label maxPopupsLabel;
        private System.Windows.Forms.NumericUpDown maxPopupsNumber;
        private System.Windows.Forms.CheckBox keepOnTopCheckBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.CheckBox noPopupsCheckBox;
        private System.Windows.Forms.CheckBox foldersFirstCheck;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label iconQualityLabel;
        private System.Windows.Forms.ComboBox iconQuality;

    }
}