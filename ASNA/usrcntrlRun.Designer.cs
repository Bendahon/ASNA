namespace ASNA
{
    partial class usrcntrlRun
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
            this.btnBackitup = new System.Windows.Forms.Button();
            this.lstboxSites = new System.Windows.Forms.ListBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmboSaveLocation = new System.Windows.Forms.ComboBox();
            this.lblSaveLocation = new System.Windows.Forms.Label();
            this.txtActualSaveLocation = new System.Windows.Forms.TextBox();
            this.txtLogBox = new System.Windows.Forms.TextBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.cmboSavedSite = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnBackitup
            // 
            this.btnBackitup.Location = new System.Drawing.Point(256, 652);
            this.btnBackitup.Name = "btnBackitup";
            this.btnBackitup.Size = new System.Drawing.Size(136, 51);
            this.btnBackitup.TabIndex = 7;
            this.btnBackitup.Text = "Backup";
            this.btnBackitup.UseVisualStyleBackColor = true;
            this.btnBackitup.Click += new System.EventHandler(this.btnBackitup_Click);
            // 
            // lstboxSites
            // 
            this.lstboxSites.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstboxSites.FormattingEnabled = true;
            this.lstboxSites.Location = new System.Drawing.Point(0, 0);
            this.lstboxSites.Name = "lstboxSites";
            this.lstboxSites.Size = new System.Drawing.Size(250, 706);
            this.lstboxSites.TabIndex = 7;
            this.lstboxSites.TabStop = false;
            this.lstboxSites.SelectedIndexChanged += new System.EventHandler(this.lstboxSites_SelectedIndexChanged);
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(345, 135);
            this.txtServerName.MaxLength = 64;
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(303, 20);
            this.txtServerName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Server Name";
            // 
            // cmboSaveLocation
            // 
            this.cmboSaveLocation.FormattingEnabled = true;
            this.cmboSaveLocation.Items.AddRange(new object[] {
            "Desktop",
            "Documents",
            "Custom"});
            this.cmboSaveLocation.Location = new System.Drawing.Point(345, 59);
            this.cmboSaveLocation.Name = "cmboSaveLocation";
            this.cmboSaveLocation.Size = new System.Drawing.Size(303, 21);
            this.cmboSaveLocation.TabIndex = 2;
            this.cmboSaveLocation.SelectedIndexChanged += new System.EventHandler(this.cmboSaveLocation_SelectedIndexChanged);
            // 
            // lblSaveLocation
            // 
            this.lblSaveLocation.AutoSize = true;
            this.lblSaveLocation.Location = new System.Drawing.Point(256, 62);
            this.lblSaveLocation.Name = "lblSaveLocation";
            this.lblSaveLocation.Size = new System.Drawing.Size(76, 13);
            this.lblSaveLocation.TabIndex = 11;
            this.lblSaveLocation.Text = "Save Location";
            // 
            // txtActualSaveLocation
            // 
            this.txtActualSaveLocation.Location = new System.Drawing.Point(345, 86);
            this.txtActualSaveLocation.Name = "txtActualSaveLocation";
            this.txtActualSaveLocation.ReadOnly = true;
            this.txtActualSaveLocation.Size = new System.Drawing.Size(303, 20);
            this.txtActualSaveLocation.TabIndex = 12;
            this.txtActualSaveLocation.TabStop = false;
            // 
            // txtLogBox
            // 
            this.txtLogBox.Location = new System.Drawing.Point(256, 291);
            this.txtLogBox.Multiline = true;
            this.txtLogBox.Name = "txtLogBox";
            this.txtLogBox.ReadOnly = true;
            this.txtLogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogBox.Size = new System.Drawing.Size(677, 355);
            this.txtLogBox.TabIndex = 13;
            this.txtLogBox.TabStop = false;
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(256, 164);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(58, 13);
            this.lblIPAddress.TabIndex = 15;
            this.lblIPAddress.Text = "IP Address";
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(345, 161);
            this.txtIPAddress.MaxLength = 255;
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(303, 20);
            this.txtIPAddress.TabIndex = 4;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(256, 190);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 17;
            this.lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(345, 187);
            this.txtUsername.MaxLength = 255;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(303, 20);
            this.txtUsername.TabIndex = 5;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(256, 216);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 19;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(345, 213);
            this.txtPassword.MaxLength = 255;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(303, 20);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(797, 652);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(136, 51);
            this.btnClearLog.TabIndex = 21;
            this.btnClearLog.TabStop = false;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // cmboSavedSite
            // 
            this.cmboSavedSite.FormattingEnabled = true;
            this.cmboSavedSite.Items.AddRange(new object[] {
            "Desktop",
            "Documents",
            "Custom"});
            this.cmboSavedSite.Location = new System.Drawing.Point(345, 32);
            this.cmboSavedSite.Name = "cmboSavedSite";
            this.cmboSavedSite.Size = new System.Drawing.Size(303, 21);
            this.cmboSavedSite.TabIndex = 1;
            this.cmboSavedSite.SelectedIndexChanged += new System.EventHandler(this.cmboSavedSite_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Config";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(256, 242);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 25;
            this.lblPort.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(345, 239);
            this.txtPort.MaxLength = 8;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(303, 20);
            this.txtPort.TabIndex = 24;
            // 
            // usrcntrlRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboSavedSite);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.txtLogBox);
            this.Controls.Add(this.txtActualSaveLocation);
            this.Controls.Add(this.lblSaveLocation);
            this.Controls.Add(this.cmboSaveLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.lstboxSites);
            this.Controls.Add(this.btnBackitup);
            this.MaximumSize = new System.Drawing.Size(936, 706);
            this.Name = "usrcntrlRun";
            this.Size = new System.Drawing.Size(936, 706);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBackitup;
        private System.Windows.Forms.ListBox lstboxSites;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboSaveLocation;
        private System.Windows.Forms.Label lblSaveLocation;
        private System.Windows.Forms.TextBox txtActualSaveLocation;
        private System.Windows.Forms.TextBox txtLogBox;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.ComboBox cmboSavedSite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
    }
}
