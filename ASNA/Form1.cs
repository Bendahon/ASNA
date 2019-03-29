using System;
using System.Windows.Forms;

namespace ASNA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            usrcntrlConfiguration1.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = Program.PNameAndVersion();
            lblProgramName.Text = Program.PNameAndVersion();
            string RenciSSHdll = Program.GettingRunningDirectory() + @"\Renci.SshNet.dll";
            if (!System.IO.File.Exists(RenciSSHdll))
            {
                MessageBox.Show("Renci.SshNet.dll is missing from bin directory\nor program is being run wrong", Program.PNAme());
                btnRun.Enabled = false;
                return;
            }

        }

        private void btnASNAConfig_Click(object sender, EventArgs e)
        {
            CheckIfConfigNeedsToSaveChanges();
            CheckIfSettingsNeedsToSaveChanges();
            usrcntrlConfiguration1.BringToFront();
            usrcntrlConfiguration1.ReloadUserControl();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            CheckIfConfigNeedsToSaveChanges();
            CheckIfSettingsNeedsToSaveChanges();
            usrcntrlSettings1.BringToFront();
            usrcntrlSettings1.ReloadUserControl();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            CheckIfConfigNeedsToSaveChanges();
            CheckIfSettingsNeedsToSaveChanges();
            usrcntrlRun1.BringToFront();
            usrcntrlRun1.ReloadUserControl();
        }

        private void btnDocumentation_Click(object sender, EventArgs e)
        {
            CheckIfConfigNeedsToSaveChanges();
            CheckIfSettingsNeedsToSaveChanges();
            usrcntrlDocumentation1.BringToFront();
            usrcntrlDocumentation1.ReloadUserControl();
        }
        private void CheckIfConfigNeedsToSaveChanges()
        {
            usrcntrlConfiguration1.CheckIfNeedsToBeSaved();
        }

        private void CheckIfSettingsNeedsToSaveChanges()
        {
            usrcntrlSettings1.CheckIfNeedsToBeSaved();
        }

        #region Exiting Program
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogToExitProgram();
        }

        private static void DialogToExitProgram()
        {
            DialogResult dr = MessageBox.Show("Are you sure you wish to exit?", Program.PNAme(), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                Application.Exit();
            }
            else
            {
                return;
            }
        }
        #endregion
    }
}
