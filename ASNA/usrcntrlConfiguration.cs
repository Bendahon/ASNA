using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ASNA
{
    public partial class usrcntrlConfiguration : UserControl
    {
        public usrcntrlConfiguration()
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
        bool CurrentActiveSite = false;
        bool ChangesToMake = false;
        public void ReloadUserControl()
        {
            lstboxSites.Items.Clear();
            string[] filePaths = Directory.GetFiles(Program.PIDsites());
            foreach (string file in filePaths)
            {
                string opfile = file.Replace(Program.PIDsites(), "");

                if (opfile.StartsWith("."))
                {

                }
                else
                {
                    lstboxSites.Items.Add(opfile);
                }

            }
            dgridSystem.DataSource = null;
            dgridSFTP.DataSource = null;
            txtRenameBox.Text = "";
            txtStatusBox.Text = "";
            ChangesToMake = false;
            CurrentActiveSite = false;

            btnSave.Enabled = false;
            btnAddCommand.Enabled = false;
            btnAddNewline.Enabled = false;
            btnRenameOK.Enabled = false;
            btnRemove.Enabled = false;
            txtRenameBox.Enabled = false;
            txtStatusBox.Enabled = false;
        }

        private void lstboxSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(lstboxSites.Text))
            {
                return;
            }
            CurrentActiveSite = true;
            string SelectedItem = Program.PIDsites() + lstboxSites.Text;
            string StatusItem = Program.PIDsites() + "." + lstboxSites.Text;

            if (GetCurrentSaveState())
            {
                DialogResult dr = MessageBox.Show("Save changes?", Program.PNAme(), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    SaveActiveInformation();
                }
                else
                {
                    ChangeSaveToANegative();
                }
            }

            if (File.Exists(SelectedItem))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(SelectedItem);
                dgridSystem.DataSource = ds;
                dgridSFTP.DataSource = ds;
                dgridSystem.DataMember = "ConfigCmd";
                dgridSFTP.DataMember = "SFTPCommand";
            }
            else
            {
                ReloadUserControl();
            }
            if (File.Exists(StatusItem))
            {
                StreamReader sr = new StreamReader(StatusItem);
                txtStatusBox.Text = sr.ReadToEnd();
                sr.Close();
            }
            else
            {
                txtStatusBox.Text = "";
            }
            ResetColour();
            ColourMeTimbers();
            ChangesToMake = false;

            btnSave.Enabled = true;
            btnAddCommand.Enabled = true;
            btnAddNewline.Enabled = true;
            btnRenameOK.Enabled = true;
            btnRemove.Enabled = true;
            txtRenameBox.Enabled = true;
            txtStatusBox.Enabled = true;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CurrentActiveSite)
            {
                SaveActiveInformation();
            }
            else
            {
                MessageBox.Show("No site selected", Program.PNAme());
                return;
            }
        }
        public void SaveActiveInformation()
        {
            if (!ParseTheSystemDataGrids())
            {
                return;
            }
            if (!ParseTheSFTPDataGrids())
            {
                return;
            }

            string filePath;
            string StatusFilePath;
            if (lstboxSites.Text == "" || lstboxSites.Text == null)
            {
                filePath = "";
                StatusFilePath = "";
                MessageBox.Show("You need to add a new site first!");
            }
            else
            {
                filePath = Program.PIDsites() + lstboxSites.Text;
                StatusFilePath = Program.PIDsites() + "." + lstboxSites.Text; ;
            }
            DataSet dsSystem = (DataSet)dgridSystem.DataSource;
            DataSet dsSFTP = (DataSet)dgridSFTP.DataSource;
            dsSystem.WriteXml(filePath);
            dsSFTP.WriteXml(filePath);
            if (string.IsNullOrWhiteSpace(txtStatusBox.Text))
            {

            }
            else
            {
                StreamWriter sw = new StreamWriter(StatusFilePath);
                sw.Write(txtStatusBox.Text);
                sw.Close();
                sw.Dispose();
            }
            MessageBox.Show("Saved!", Program.PNAme());
            ReloadUserControl();
        }
        private bool ParseTheSystemDataGrids()
        {
            double count = 1.0;
            foreach (DataGridViewRow row in dgridSystem.Rows)
            {
                //MessageBox.Show(row.ToString());
                foreach (DataGridViewCell cell in row.Cells)
                {
                    try
                    {
                        if (cell.Value.ToString().Trim() == "")
                        {
                            int counter = Convert.ToInt32(Math.Floor(count));
                            MessageBox.Show($"Row {counter} in system contains blank data, can't continue", Program.PNAme());
                            return false;
                        }

                    }
                    catch
                    {

                    }
                    count += 0.5;
                }
            }
            return true;
        }
        private bool ParseTheSFTPDataGrids()
        {
            double count = 1.0;
            foreach (DataGridViewRow row in dgridSFTP.Rows)
            {
                //MessageBox.Show(row.ToString());
                foreach (DataGridViewCell cell in row.Cells)
                {
                    try
                    {
                        if (cell.Value.ToString().Trim() == "")
                        {
                            int counter = Convert.ToInt32(Math.Floor(count));
                            MessageBox.Show($"Row {counter} in SFTP contains blank data, can't continue", Program.PNAme());
                            return false;
                        }

                    }
                    catch
                    {

                    }
                    count += 0.5;
                }
            }
            return true;
        }
        private void btnAddSite_Click(object sender, EventArgs e)
        {
            if (GetCurrentSaveState())
            {
                DialogResult dr = MessageBox.Show("Save changes?", Program.PNAme(), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    SaveActiveInformation();
                }
                else
                {
                    ChangeSaveToANegative();
                }
            }
            string TemplateName = Program.PIDdoc() + "DefaultSite";
            //string DateTimeFileName = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            string DateTimeFileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string DirectoryToCopyTo = Program.PIDsites() + DateTimeFileName;
            if (File.Exists(DirectoryToCopyTo))
            {
                return;
            }
            File.Copy(TemplateName, DirectoryToCopyTo);
            ReloadUserControl();
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            string SelectedItem = Program.PIDsites() + lstboxSites.Text;
            string HiddenSelectedItem = Program.PIDsites() + "." + lstboxSites.Text;
            if (!File.Exists(SelectedItem))
            {
                return;
            }
            DialogResult dr = MessageBox.Show("Are you sure?", Program.PNAme(), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(dr == DialogResult.OK)
            {
                try
                {
                    File.Delete(SelectedItem);
                    File.Delete(HiddenSelectedItem);
                }
                catch
                {

                }
                ReloadUserControl();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReloadUserControl();
        }
        private void btnRenameOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lstboxSites.Text))
            {
                return;
            }
            string OriginalFileName = Program.PIDsites() + lstboxSites.Text;
            string OriginalHiddenFileName = Program.PIDsites() + "." + lstboxSites.Text;
            if (!File.Exists(OriginalFileName))
            {
                return;
            }

            if (!IsValidFileName(txtRenameBox.Text, true))
            {
                MessageBox.Show("Invalid file name");
                return;
            }

            string NewFileName = Program.PIDsites() + txtRenameBox.Text;
            string HiddenFileName = Program.PIDsites() + "." + txtRenameBox.Text;

            if (File.Exists(NewFileName))
            {
                MessageBox.Show("File already exists, try again", Program.PNAme());
                txtRenameBox.Text = "";
                return;
            }
            if (File.Exists(HiddenFileName))
            {
                MessageBox.Show("Status file already exists, manual clean me please", Program.PNAme());
                txtRenameBox.Text = "";
                return;
            }
            try
            {
                File.Move(OriginalFileName, NewFileName);
                File.Move(OriginalHiddenFileName, HiddenFileName);
            }
            catch
            {

            }
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

        private void btnAddNewline_Click(object sender, EventArgs e)
        {
            txtStatusBox.Text = txtStatusBox.Text.Insert(txtStatusBox.SelectionStart, "ASNAnewline \n");
            ResetColour();
            ColourMeTimbers();
        }
        private void btnAddCommand_Click(object sender, EventArgs e)
        {
            txtStatusBox.Text = txtStatusBox.Text.Insert(txtStatusBox.SelectionStart, "ASNAcommand ");
            ResetColour();
            ColourMeTimbers();
        }
        private void dgridSystem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ChangesToMake = true;
        }
        private void dgridSFTP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ChangesToMake = true;
        }
        public bool GetCurrentSaveState()
        {
            return ChangesToMake;
        }
        public void ChangeSaveToANegative()
        {
            ChangesToMake = false;
        }
        private void txtStatusBox_TextChanged_1(object sender, EventArgs e)
        {
            //ResetColour();
            //ColourMeTimbers();
            ChangesToMake = true;
        }
        #region Colouring Books
        private void ResetColour()
        {
            txtStatusBox.SelectAll();
            txtStatusBox.SelectionBackColor = System.Drawing.Color.White;
            txtStatusBox.DeselectAll();
        }
        private void ColourMeTimbers()
        {
            List<string> WordList = new List<string>();
            WordList.Add("ASNAnewline");
            WordList.Add("ASNAcommand");

            foreach (string word in WordList)
            {
                Color col = GetColour(word);

                int StartIndex = 0;

                while (StartIndex < txtStatusBox.TextLength)
                {
                    int WordStartIndex = txtStatusBox.Find(word, StartIndex, RichTextBoxFinds.None);

                    if (WordStartIndex != -1)
                    {
                        txtStatusBox.SelectionStart = WordStartIndex;
                        txtStatusBox.SelectionLength = word.Length;
                        txtStatusBox.SelectionBackColor = col;
                    }
                    else
                    {
                        break;
                    }
                    StartIndex = WordStartIndex + word.Length;
                }
            }
        }
        private static Color GetColour(string word)
        {
            switch (word)
            {
                case "ASNAnewline":
                    return Color.LightGreen;
                case "ASNAcommand":
                    return Color.LightBlue;
                default:
                    return Color.Blue;
            }
        }
        #endregion

    }
}
