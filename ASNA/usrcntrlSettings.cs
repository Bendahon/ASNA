using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

namespace ASNA
{
    public partial class usrcntrlSettings : UserControl
    {
        public usrcntrlSettings()
        {
            InitializeComponent();
            try
            {
                ReloadUserControl();
            }
            catch
            {

            }
        }
        bool CurrentSaveState = false;
        string SiteName = "";

        public void ReloadUserControl()
        {
            CurrentSaveState = false;
            cmboSiteName.Items.Clear();
            txtNewFileName.Text = "";
            foreach(string s in Directory.GetFiles(Program.PIDConfig()))
            {
                cmboSiteName.Items.Add(s.Replace(Program.PIDConfig(), ""));
            }
            if(cmboSiteName.Items.Count >= 1)
            {
                cmboSiteName.SelectedIndex = 0;
            }
            else
            {
                return;
            }
            SiteName = cmboSiteName.Text; 
        }

        private void usrcntrlSettings_Load(object sender, EventArgs e)
        {
            // think this is redundant
            //ReloadUserControl();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCurrentState();
        }

        public void SaveCurrentState(string ExistingSiteName = "")
        {
            string FileName;
            if(cmboSiteName.Text == "")
            {
                string DateTimeFileName = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
                FileName = $@"{Program.PIDConfig()}{DateTimeFileName}";
                MessageBox.Show($"Creating new file: {FileName}");
            }
            else if (!string.IsNullOrWhiteSpace(ExistingSiteName))
            {
                FileName = $@"{Program.PIDConfig()}{ExistingSiteName}";
            }
            else
            {
                FileName = $@"{Program.PIDConfig()}{cmboSiteName.Text}";
            }
            Settings se = new Settings();
            PasswordManager pm = new PasswordManager();
            se.isPWEncrypted = true;
            se.HostIP = txtDefaultIP.Text;
            se.Username = txtUsername.Text;
            se.Password = pm.EncodeString(txtPassword.Text);
            se.GenericName = txtSiteName.Text;
            if (string.IsNullOrWhiteSpace(txtPort.Text))
            {
                se.Port = "22";
            }
            else
            {
                se.Port = txtPort.Text;
            }
            se.SkipICMPScan = chckEnableICMP.Checked;
            se.SkipStatus = chckSkipStatus.Checked;
            se.SkipConfig = chckSkipConfig.Checked;
            se.SkipSFTP = chckSkipSFTP.Checked;
            Settings.SaveData(se, FileName);
            CurrentSaveState = false;
            MessageBox.Show("Saved!", Program.PNAme());
            ReloadUserControl();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReloadUserControl();
        }

        private void btnAddConfig_Click(object sender, EventArgs e)
        {
            AddNewConfigFile();
        }

        private void AddNewConfigFile()
        {
            string DateTimeFileName = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
            string FileName = $@"{Program.PIDConfig()}{DateTimeFileName}";
            Settings se = new Settings();
            se.SkipICMPScan = false;
            se.GenericName = "";
            se.HostIP = "";
            se.Password = "";
            se.Username = "";
            se.Port = "22";
            se.SkipSFTP = true;
            se.isPWEncrypted = true;
            Settings.SaveData(se, FileName);
            ReloadUserControl();
        }

        private void cmboSiteName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GetCurrentSaveState())
            {
                DialogResult dr = MessageBox.Show("Save File?", Program.PNAme(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dr == DialogResult.Yes)
                {
                    SaveCurrentState(SiteName);
                }
            }
            txtNewFileName.Text = cmboSiteName.Text;
            try
            {
                SiteName = cmboSiteName.Text;
                TurnOffDatSaveState();
                string FileName = $@"{Program.PIDConfig()}{cmboSiteName.Text}";
                XmlSerializer xs = new XmlSerializer(typeof(Settings));
                FileStream read = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                Settings se = (Settings)xs.Deserialize(read);
                if(se.isPWEncrypted == false)
                {
                    txtPassword.Text = se.Password;
                }
                else
                {
                    PasswordManager pm = new PasswordManager();
                    txtPassword.Text = pm.DecodeString(se.Password);
                }
                read.Dispose();
                txtDefaultIP.Text = se.HostIP;
                txtUsername.Text = se.Username;
                txtSiteName.Text = se.GenericName;
                txtPort.Text = se.Port;
                chckSkipStatus.Checked = se.SkipStatus;
                chckSkipConfig.Checked = se.SkipConfig;
                chckSkipSFTP.Checked = se.SkipSFTP;
                chckEnableICMP.Checked = se.SkipICMPScan;
                TurnOffDatSaveState();
            }
            catch
            {
                //MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if(txtNewFileName.Text == "" || txtNewFileName.Text == null)
            {
                return;
            }
            string OriginalFileName = $@"{Program.PIDConfig()}{cmboSiteName.Text}";
            string NewFileName = $@"{Program.PIDConfig()}{txtNewFileName.Text}";
            if (File.Exists(NewFileName))
            {
                MessageBox.Show("File already exists", Program.PNAme());
                return;
            }
            File.Move(OriginalFileName, NewFileName);
            ReloadUserControl();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string FileToDelete = $@"{Program.PIDConfig()}{cmboSiteName.Text}";
            DialogResult dr = MessageBox.Show("Are you sure?", Program.PNAme(), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(dr == DialogResult.OK)
            {
                File.Delete(FileToDelete);
                ReloadUserControl();
            }
            else
            {
                return;
            }
        }
        #region stupid textchanged
        private void txtDefaultIP_TextChanged(object sender, EventArgs e)
        {
            CurrentSaveState = true;
        }
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            CurrentSaveState = true;
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            CurrentSaveState = true;
        }
        private void txtSiteName_TextChanged(object sender, EventArgs e)
        {
            CurrentSaveState = true;
        }
        private void chckEnableICMP_CheckedChanged(object sender, EventArgs e)
        {
            CurrentSaveState = true;
        }
        #endregion

        public bool GetCurrentSaveState()
        {
            return CurrentSaveState;
        }

        public void TurnOffDatSaveState()
        {
            CurrentSaveState = false;
        }

        private void btnRename_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewFileName.Text))
            {
                MessageBox.Show("String is empty");
                return;
            }
            string OriginalFileName = Program.PIDConfig() + cmboSiteName.Text;
            if (!File.Exists(OriginalFileName))
            {
                MessageBox.Show("File doesn't exist");
                return;
            }

            if (!IsValidFileName(cmboSiteName.Text, true))
            {
                MessageBox.Show("Invalid file name");
                return;
            }

            string NewFileName = Program.PIDConfig() + txtNewFileName.Text;

            if (File.Exists(NewFileName))
            {
                MessageBox.Show("File already exists, try again", Program.PNAme());
                txtNewFileName.Text = "";
                return;
            }
            File.Move(OriginalFileName, NewFileName);
            ReloadUserControl();
        }

        public static bool IsValidFileName(string expression, bool platformIndependent)
        {
            string sPattern = @"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\"";|/]+$";
            if (platformIndependent)
            {
                sPattern = @"^(([a-zA-Z]:|\\)\\)?(((\.)|(\.\.)|([^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?))\\)*[^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?$";
            }
            return (Regex.IsMatch(expression, sPattern, RegexOptions.CultureInvariant));
        }
    }
}
