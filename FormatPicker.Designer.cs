namespace Clock
{
    partial class FormatPicker
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
            this.tbFormat = new System.Windows.Forms.TextBox();
            this.rb12hr = new System.Windows.Forms.RadioButton();
            this.rb24hr = new System.Windows.Forms.RadioButton();
            this.cbSeconds = new System.Windows.Forms.CheckBox();
            this.cbAmPm = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbFormat
            // 
            this.tbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFormat.Location = new System.Drawing.Point(12, 12);
            this.tbFormat.Name = "tbFormat";
            this.tbFormat.Size = new System.Drawing.Size(207, 20);
            this.tbFormat.TabIndex = 0;
            this.tbFormat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rb12hr
            // 
            this.rb12hr.AutoSize = true;
            this.rb12hr.Checked = true;
            this.rb12hr.Location = new System.Drawing.Point(12, 56);
            this.rb12hr.Name = "rb12hr";
            this.rb12hr.Size = new System.Drawing.Size(61, 17);
            this.rb12hr.TabIndex = 1;
            this.rb12hr.TabStop = true;
            this.rb12hr.Text = "12 hour";
            this.rb12hr.UseVisualStyleBackColor = true;
            this.rb12hr.CheckedChanged += new System.EventHandler(this.rb12hr_CheckedChanged);
            // 
            // rb24hr
            // 
            this.rb24hr.AutoSize = true;
            this.rb24hr.Location = new System.Drawing.Point(12, 79);
            this.rb24hr.Name = "rb24hr";
            this.rb24hr.Size = new System.Drawing.Size(61, 17);
            this.rb24hr.TabIndex = 2;
            this.rb24hr.Text = "24 hour";
            this.rb24hr.UseVisualStyleBackColor = true;
            this.rb24hr.CheckedChanged += new System.EventHandler(this.rb24hr_CheckedChanged);
            // 
            // cbSeconds
            // 
            this.cbSeconds.AutoSize = true;
            this.cbSeconds.Checked = true;
            this.cbSeconds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSeconds.Location = new System.Drawing.Point(12, 102);
            this.cbSeconds.Name = "cbSeconds";
            this.cbSeconds.Size = new System.Drawing.Size(103, 17);
            this.cbSeconds.TabIndex = 3;
            this.cbSeconds.Text = "include seconds";
            this.cbSeconds.UseVisualStyleBackColor = true;
            this.cbSeconds.CheckedChanged += new System.EventHandler(this.cbSeconds_CheckedChanged);
            // 
            // cbAmPm
            // 
            this.cbAmPm.AutoSize = true;
            this.cbAmPm.Checked = true;
            this.cbAmPm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAmPm.Location = new System.Drawing.Point(89, 57);
            this.cbAmPm.Name = "cbAmPm";
            this.cbAmPm.Size = new System.Drawing.Size(106, 17);
            this.cbAmPm.TabIndex = 4;
            this.cbAmPm.Text = "include AM / PM";
            this.cbAmPm.UseVisualStyleBackColor = true;
            this.cbAmPm.CheckedChanged += new System.EventHandler(this.cbAmPm_CheckedChanged);
            // 
            // FormatPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 139);
            this.Controls.Add(this.cbAmPm);
            this.Controls.Add(this.cbSeconds);
            this.Controls.Add(this.rb24hr);
            this.Controls.Add(this.rb12hr);
            this.Controls.Add(this.tbFormat);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormatPicker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clock Format";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFormat;
        private System.Windows.Forms.RadioButton rb12hr;
        private System.Windows.Forms.RadioButton rb24hr;
        private System.Windows.Forms.CheckBox cbSeconds;
        private System.Windows.Forms.CheckBox cbAmPm;
    }
}