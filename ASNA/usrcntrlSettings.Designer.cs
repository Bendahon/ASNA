namespace ASNA
{
    partial class usrcntrlSettings
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
            this.lblDefaultUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtDefaultIP = new System.Windows.Forms.TextBox();
            this.lblDefaultIP = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPasswordField = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblNoteAboutPasswords = new System.Windows.Forms.Label();
            this.txtSiteName = new System.Windows.Forms.TextBox();
            this.lblSiteName = new System.Windows.Forms.Label();
            this.chckEnableICMP = new System.Windows.Forms.CheckBox();
            this.lblSkipICMP = new System.Windows.Forms.Label();
            this.cmboSiteName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddConfig = new System.Windows.Forms.Button();
            this.txtNewFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chckSkipStatus = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chckSkipConfig = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chckSkipSFTP = new System.Windows.Forms.CheckBox();
            this.lblSFTPWarning = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDefaultUsername
            // 
            this.lblDefaultUsername.AutoSize = true;
            this.lblDefaultUsername.Location = new System.Drawing.Point(22, 99);
            this.lblDefaultUsername.Name = "lblDefaultUsername";
            this.lblDefaultUsername.Size = new System.Drawing.Size(92, 13);
            this.lblDefaultUsername.TabIndex = 0;
            this.lblDefaultUsername.Text = "Default Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(122, 96);
            this.txtUsername.MaxLength = 64;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(246, 20);
            this.txtUsername.TabIndex = 3;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // txtDefaultIP
            // 
            this.txtDefaultIP.Location = new System.Drawing.Point(122, 70);
            this.txtDefaultIP.MaxLength = 255;
            this.txtDefaultIP.Name = "txtDefaultIP";
            this.txtDefaultIP.Size = new System.Drawing.Size(246, 20);
            this.txtDefaultIP.TabIndex = 2;
            this.txtDefaultIP.TextChanged += new System.EventHandler(this.txtDefaultIP_TextChanged);
            // 
            // lblDefaultIP
            // 
            this.lblDefaultIP.AutoSize = true;
            this.lblDefaultIP.Location = new System.Drawing.Point(22, 73);
            this.lblDefaultIP.Name = "lblDefaultIP";
            this.lblDefaultIP.Size = new System.Drawing.Size(54, 13);
            this.lblDefaultIP.TabIndex = 8;
            this.lblDefaultIP.Text = "Default IP";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(122, 122);
            this.txtPassword.MaxLength = 64;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(246, 20);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // lblPasswordField
            // 
            this.lblPasswordField.AutoSize = true;
            this.lblPasswordField.Location = new System.Drawing.Point(23, 125);
            this.lblPasswordField.Name = "lblPasswordField";
            this.lblPasswordField.Size = new System.Drawing.Size(90, 13);
            this.lblPasswordField.TabIndex = 10;
            this.lblPasswordField.Text = "Default Password";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(3, 652);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 51);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(145, 652);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 51);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblNoteAboutPasswords
            // 
            this.lblNoteAboutPasswords.AutoSize = true;
            this.lblNoteAboutPasswords.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteAboutPasswords.Location = new System.Drawing.Point(384, 125);
            this.lblNoteAboutPasswords.Name = "lblNoteAboutPasswords";
            this.lblNoteAboutPasswords.Size = new System.Drawing.Size(270, 18);
            this.lblNoteAboutPasswords.TabIndex = 14;
            this.lblNoteAboutPasswords.Text = "Note: Passwords are stored in plain text";
            // 
            // txtSiteName
            // 
            this.txtSiteName.Location = new System.Drawing.Point(122, 148);
            this.txtSiteName.MaxLength = 64;
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Size = new System.Drawing.Size(246, 20);
            this.txtSiteName.TabIndex = 5;
            this.txtSiteName.TextChanged += new System.EventHandler(this.txtSiteName_TextChanged);
            // 
            // lblSiteName
            // 
            this.lblSiteName.AutoSize = true;
            this.lblSiteName.Location = new System.Drawing.Point(22, 151);
            this.lblSiteName.Name = "lblSiteName";
            this.lblSiteName.Size = new System.Drawing.Size(56, 13);
            this.lblSiteName.TabIndex = 15;
            this.lblSiteName.Text = "Site Name";
            // 
            // chckEnableICMP
            // 
            this.chckEnableICMP.AutoSize = true;
            this.chckEnableICMP.Location = new System.Drawing.Point(122, 205);
            this.chckEnableICMP.Name = "chckEnableICMP";
            this.chckEnableICMP.Size = new System.Drawing.Size(15, 14);
            this.chckEnableICMP.TabIndex = 6;
            this.chckEnableICMP.UseVisualStyleBackColor = true;
            this.chckEnableICMP.CheckedChanged += new System.EventHandler(this.chckEnableICMP_CheckedChanged);
            // 
            // lblSkipICMP
            // 
            this.lblSkipICMP.AutoSize = true;
            this.lblSkipICMP.Location = new System.Drawing.Point(22, 205);
            this.lblSkipICMP.Name = "lblSkipICMP";
            this.lblSkipICMP.Size = new System.Drawing.Size(76, 13);
            this.lblSkipICMP.TabIndex = 18;
            this.lblSkipICMP.Text = "Skip ICMP first";
            // 
            // cmboSiteName
            // 
            this.cmboSiteName.FormattingEnabled = true;
            this.cmboSiteName.Location = new System.Drawing.Point(122, 43);
            this.cmboSiteName.Name = "cmboSiteName";
            this.cmboSiteName.Size = new System.Drawing.Size(246, 21);
            this.cmboSiteName.TabIndex = 1;
            this.cmboSiteName.SelectedIndexChanged += new System.EventHandler(this.cmboSiteName_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Modifying Config";
            // 
            // btnAddConfig
            // 
            this.btnAddConfig.Location = new System.Drawing.Point(287, 652);
            this.btnAddConfig.Name = "btnAddConfig";
            this.btnAddConfig.Size = new System.Drawing.Size(136, 51);
            this.btnAddConfig.TabIndex = 23;
            this.btnAddConfig.TabStop = false;
            this.btnAddConfig.Text = "Add Config";
            this.btnAddConfig.UseVisualStyleBackColor = true;
            this.btnAddConfig.Click += new System.EventHandler(this.btnAddConfig_Click);
            // 
            // txtNewFileName
            // 
            this.txtNewFileName.Location = new System.Drawing.Point(287, 626);
            this.txtNewFileName.Name = "txtNewFileName";
            this.txtNewFileName.Size = new System.Drawing.Size(278, 20);
            this.txtNewFileName.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(289, 610);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "New File Name";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(429, 652);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(136, 51);
            this.btnDelete.TabIndex = 27;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "Delete file";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(571, 625);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(52, 20);
            this.btnRename.TabIndex = 9;
            this.btnRename.TabStop = false;
            this.btnRename.Text = "OK";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click_1);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(122, 174);
            this.txtPort.MaxLength = 8;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(246, 20);
            this.txtPort.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Skip Status";
            // 
            // chckSkipStatus
            // 
            this.chckSkipStatus.AutoSize = true;
            this.chckSkipStatus.Location = new System.Drawing.Point(122, 227);
            this.chckSkipStatus.Name = "chckSkipStatus";
            this.chckSkipStatus.Size = new System.Drawing.Size(15, 14);
            this.chckSkipStatus.TabIndex = 30;
            this.chckSkipStatus.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 250);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Skip config";
            // 
            // chckSkipConfig
            // 
            this.chckSkipConfig.AutoSize = true;
            this.chckSkipConfig.Location = new System.Drawing.Point(123, 250);
            this.chckSkipConfig.Name = "chckSkipConfig";
            this.chckSkipConfig.Size = new System.Drawing.Size(15, 14);
            this.chckSkipConfig.TabIndex = 32;
            this.chckSkipConfig.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 312);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Skip SFTP";
            // 
            // chckSkipSFTP
            // 
            this.chckSkipSFTP.AutoSize = true;
            this.chckSkipSFTP.Location = new System.Drawing.Point(123, 312);
            this.chckSkipSFTP.Name = "chckSkipSFTP";
            this.chckSkipSFTP.Size = new System.Drawing.Size(15, 14);
            this.chckSkipSFTP.TabIndex = 34;
            this.chckSkipSFTP.UseVisualStyleBackColor = true;
            // 
            // lblSFTPWarning
            // 
            this.lblSFTPWarning.AutoSize = true;
            this.lblSFTPWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSFTPWarning.Location = new System.Drawing.Point(22, 273);
            this.lblSFTPWarning.Name = "lblSFTPWarning";
            this.lblSFTPWarning.Size = new System.Drawing.Size(641, 36);
            this.lblSFTPWarning.TabIndex = 36;
            this.lblSFTPWarning.Text = "WARNING: SFTP using WinSCP has shown to have increased RAM usage well after disco" +
    "nnect\r\nSee documentation for more info";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(23, 329);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(641, 36);
            this.label7.TabIndex = 37;
            this.label7.Text = "WARNING: SFTP using WinSCP has shown to have increased RAM usage well after disco" +
    "nnect\r\nSee documentation for more info";
            // 
            // usrcntrlSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblSFTPWarning);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chckSkipSFTP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chckSkipConfig);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chckSkipStatus);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtNewFileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddConfig);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboSiteName);
            this.Controls.Add(this.lblSkipICMP);
            this.Controls.Add(this.chckEnableICMP);
            this.Controls.Add(this.txtSiteName);
            this.Controls.Add(this.lblSiteName);
            this.Controls.Add(this.lblNoteAboutPasswords);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPasswordField);
            this.Controls.Add(this.txtDefaultIP);
            this.Controls.Add(this.lblDefaultIP);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblDefaultUsername);
            this.MaximumSize = new System.Drawing.Size(936, 706);
            this.Name = "usrcntrlSettings";
            this.Size = new System.Drawing.Size(936, 706);
            this.Load += new System.EventHandler(this.usrcntrlSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDefaultUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtDefaultIP;
        private System.Windows.Forms.Label lblDefaultIP;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPasswordField;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNoteAboutPasswords;
        private System.Windows.Forms.TextBox txtSiteName;
        private System.Windows.Forms.Label lblSiteName;
        private System.Windows.Forms.CheckBox chckEnableICMP;
        private System.Windows.Forms.Label lblSkipICMP;
        private System.Windows.Forms.ComboBox cmboSiteName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddConfig;
        private System.Windows.Forms.TextBox txtNewFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chckSkipStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chckSkipConfig;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chckSkipSFTP;
        private System.Windows.Forms.Label lblSFTPWarning;
        private System.Windows.Forms.Label label7;
    }
}
