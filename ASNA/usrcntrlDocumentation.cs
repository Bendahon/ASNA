using System;
using System.Windows.Forms;
using System.IO;

namespace ASNA
{
    public partial class usrcntrlDocumentation : UserControl
    {
        public usrcntrlDocumentation()
        {
            InitializeComponent();
            ReloadUserControl();
        }
        public void ReloadUserControl()
        {
            if(!File.Exists(Program.PIDdoc() + "Changelog.rtf"))
            {
                btnReadme.Enabled = false;
            }
            else
            {
                btnReadme.Enabled = true;
            }
            if (!File.Exists(Program.PIDdoc() + "UserGuide.rtf"))
            {
                btnUserGuide.Enabled = false;
            }
            else
            {
                btnUserGuide.Enabled = true;
            }
            if (!File.Exists(Program.PIDdoc() + "Credits.rtf"))
            {
                btnCredits.Enabled = false;
            }
            else
            {
                btnCredits.Enabled = true;
            }
            //rtxtDocumentationbox.Text = "";
        }

        private void btnReadme_Click(object sender, EventArgs e)
        {
            ReadACertainFile("Changelog.rtf");
        }

        private void btnUserGuide_Click(object sender, EventArgs e)
        {
            ReadACertainFile("UserGuide.rtf");
        }
        private void ReadACertainFile(string filename)
        {
            try
            {
                rtxtDocumentationbox.Text = "";
                StreamReader sr = new StreamReader(Program.PIDdoc() + filename);
                rtxtDocumentationbox.Rtf = @sr.ReadToEnd();
                sr.Close();
            }
            catch
            {
                ReloadUserControl();
                return;
            }
        }

        private void btnCredits_Click(object sender, EventArgs e)
        {
            ReadACertainFile("Credits.rtf");
        }
    }
}
