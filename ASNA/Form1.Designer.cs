namespace ASNA
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDocumentation = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnASNAConfig = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblProgramName = new System.Windows.Forms.Label();
            this.pnlDockME = new System.Windows.Forms.Panel();
            this.usrcntrlConfiguration1 = new ASNA.usrcntrlConfiguration();
            this.usrcntrlSettings1 = new ASNA.usrcntrlSettings();
            this.usrcntrlDocumentation1 = new ASNA.usrcntrlDocumentation();
            this.usrcntrlRun1 = new ASNA.usrcntrlRun();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlDockME.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDocumentation);
            this.panel2.Controls.Add(this.btnSettings);
            this.panel2.Controls.Add(this.btnRun);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnASNAConfig);
            this.panel2.Location = new System.Drawing.Point(3, 117);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(139, 589);
            this.panel2.TabIndex = 1;
            // 
            // btnDocumentation
            // 
            this.btnDocumentation.Location = new System.Drawing.Point(0, 478);
            this.btnDocumentation.Name = "btnDocumentation";
            this.btnDocumentation.Size = new System.Drawing.Size(136, 51);
            this.btnDocumentation.TabIndex = 3;
            this.btnDocumentation.TabStop = false;
            this.btnDocumentation.Text = "&Documentation";
            this.btnDocumentation.UseVisualStyleBackColor = true;
            this.btnDocumentation.Click += new System.EventHandler(this.btnDocumentation_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(3, 66);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(136, 51);
            this.btnSettings.TabIndex = 1;
            this.btnSettings.TabStop = false;
            this.btnSettings.Text = "&Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(3, 123);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(136, 51);
            this.btnRun.TabIndex = 2;
            this.btnRun.TabStop = false;
            this.btnRun.Text = "&Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(0, 535);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(136, 51);
            this.btnExit.TabIndex = 4;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnASNAConfig
            // 
            this.btnASNAConfig.Location = new System.Drawing.Point(3, 9);
            this.btnASNAConfig.Name = "btnASNAConfig";
            this.btnASNAConfig.Size = new System.Drawing.Size(136, 51);
            this.btnASNAConfig.TabIndex = 0;
            this.btnASNAConfig.TabStop = false;
            this.btnASNAConfig.Text = "&Configuration";
            this.btnASNAConfig.UseVisualStyleBackColor = true;
            this.btnASNAConfig.Click += new System.EventHandler(this.btnASNAConfig_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblProgramName);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(139, 117);
            this.panel3.TabIndex = 1;
            // 
            // lblProgramName
            // 
            this.lblProgramName.AutoSize = true;
            this.lblProgramName.Location = new System.Drawing.Point(25, 98);
            this.lblProgramName.Name = "lblProgramName";
            this.lblProgramName.Size = new System.Drawing.Size(77, 13);
            this.lblProgramName.TabIndex = 0;
            this.lblProgramName.Text = "Program Name";
            // 
            // pnlDockME
            // 
            this.pnlDockME.Controls.Add(this.usrcntrlConfiguration1);
            this.pnlDockME.Controls.Add(this.usrcntrlSettings1);
            this.pnlDockME.Controls.Add(this.usrcntrlDocumentation1);
            this.pnlDockME.Controls.Add(this.usrcntrlRun1);
            this.pnlDockME.Location = new System.Drawing.Point(148, 3);
            this.pnlDockME.Name = "pnlDockME";
            this.pnlDockME.Size = new System.Drawing.Size(934, 703);
            this.pnlDockME.TabIndex = 7;
            // 
            // usrcntrlConfiguration1
            // 
            this.usrcntrlConfiguration1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usrcntrlConfiguration1.Location = new System.Drawing.Point(0, 0);
            this.usrcntrlConfiguration1.MaximumSize = new System.Drawing.Size(936, 706);
            this.usrcntrlConfiguration1.Name = "usrcntrlConfiguration1";
            this.usrcntrlConfiguration1.Size = new System.Drawing.Size(934, 703);
            this.usrcntrlConfiguration1.TabIndex = 3;
            this.usrcntrlConfiguration1.TabStop = false;
            // 
            // usrcntrlSettings1
            // 
            this.usrcntrlSettings1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usrcntrlSettings1.Location = new System.Drawing.Point(0, 0);
            this.usrcntrlSettings1.MaximumSize = new System.Drawing.Size(936, 706);
            this.usrcntrlSettings1.Name = "usrcntrlSettings1";
            this.usrcntrlSettings1.Size = new System.Drawing.Size(934, 703);
            this.usrcntrlSettings1.TabIndex = 5;
            // 
            // usrcntrlDocumentation1
            // 
            this.usrcntrlDocumentation1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usrcntrlDocumentation1.Location = new System.Drawing.Point(0, 0);
            this.usrcntrlDocumentation1.MaximumSize = new System.Drawing.Size(936, 706);
            this.usrcntrlDocumentation1.Name = "usrcntrlDocumentation1";
            this.usrcntrlDocumentation1.Size = new System.Drawing.Size(934, 703);
            this.usrcntrlDocumentation1.TabIndex = 6;
            // 
            // usrcntrlRun1
            // 
            this.usrcntrlRun1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usrcntrlRun1.Location = new System.Drawing.Point(0, 0);
            this.usrcntrlRun1.MaximumSize = new System.Drawing.Size(936, 706);
            this.usrcntrlRun1.Name = "usrcntrlRun1";
            this.usrcntrlRun1.Size = new System.Drawing.Size(934, 703);
            this.usrcntrlRun1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1094, 716);
            this.Controls.Add(this.pnlDockME);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1110, 755);
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlDockME.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnASNAConfig;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDocumentation;
        private System.Windows.Forms.Label lblProgramName;
        private usrcntrlConfiguration usrcntrlConfiguration1;
        private usrcntrlRun usrcntrlRun1;
        private usrcntrlSettings usrcntrlSettings1;
        private usrcntrlDocumentation usrcntrlDocumentation1;
        private System.Windows.Forms.Panel pnlDockME;
    }
}

