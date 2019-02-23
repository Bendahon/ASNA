namespace ASNA
{
    partial class usrcntrlDocumentation
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
            this.btnReadme = new System.Windows.Forms.Button();
            this.btnUserGuide = new System.Windows.Forms.Button();
            this.btnCredits = new System.Windows.Forms.Button();
            this.rtxtDocumentationbox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnReadme
            // 
            this.btnReadme.Location = new System.Drawing.Point(3, 47);
            this.btnReadme.Name = "btnReadme";
            this.btnReadme.Size = new System.Drawing.Size(136, 51);
            this.btnReadme.TabIndex = 1;
            this.btnReadme.TabStop = false;
            this.btnReadme.Text = "README";
            this.btnReadme.UseVisualStyleBackColor = true;
            this.btnReadme.Click += new System.EventHandler(this.btnReadme_Click);
            // 
            // btnUserGuide
            // 
            this.btnUserGuide.Location = new System.Drawing.Point(145, 47);
            this.btnUserGuide.Name = "btnUserGuide";
            this.btnUserGuide.Size = new System.Drawing.Size(136, 51);
            this.btnUserGuide.TabIndex = 2;
            this.btnUserGuide.TabStop = false;
            this.btnUserGuide.Text = "User Guide";
            this.btnUserGuide.UseVisualStyleBackColor = true;
            this.btnUserGuide.Click += new System.EventHandler(this.btnUserGuide_Click);
            // 
            // btnCredits
            // 
            this.btnCredits.Location = new System.Drawing.Point(287, 47);
            this.btnCredits.Name = "btnCredits";
            this.btnCredits.Size = new System.Drawing.Size(136, 51);
            this.btnCredits.TabIndex = 3;
            this.btnCredits.TabStop = false;
            this.btnCredits.Text = "Credits";
            this.btnCredits.UseVisualStyleBackColor = true;
            this.btnCredits.Click += new System.EventHandler(this.btnCredits_Click);
            // 
            // rtxtDocumentationbox
            // 
            this.rtxtDocumentationbox.Location = new System.Drawing.Point(0, 120);
            this.rtxtDocumentationbox.Name = "rtxtDocumentationbox";
            this.rtxtDocumentationbox.ReadOnly = true;
            this.rtxtDocumentationbox.Size = new System.Drawing.Size(936, 583);
            this.rtxtDocumentationbox.TabIndex = 4;
            this.rtxtDocumentationbox.TabStop = false;
            this.rtxtDocumentationbox.Text = "";
            // 
            // usrcntrlDocumentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtxtDocumentationbox);
            this.Controls.Add(this.btnCredits);
            this.Controls.Add(this.btnUserGuide);
            this.Controls.Add(this.btnReadme);
            this.MaximumSize = new System.Drawing.Size(936, 706);
            this.Name = "usrcntrlDocumentation";
            this.Size = new System.Drawing.Size(936, 706);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReadme;
        private System.Windows.Forms.Button btnUserGuide;
        private System.Windows.Forms.Button btnCredits;
        private System.Windows.Forms.RichTextBox rtxtDocumentationbox;
    }
}
